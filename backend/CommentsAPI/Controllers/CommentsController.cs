using CommentsAPI.Interfaces;
using CommentsAPI.Models.DTO;
using CommentsAPI.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.WebSockets;
using System.Text;

namespace CommentsAPI.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private static readonly List<WebSocket> _clients = new();
        private readonly ICommentRepository _commentRepository;
        private readonly IFileService _fileService;
        private readonly IWebSocketService _webSocketService;
        private readonly IValidationService _validationService;
        private const int COMMENTS_PER_PAGE = 25;
        public CommentsController(ICommentRepository commentRepository, IFileService fileService, IWebSocketService webSocketService, IValidationService validationService)
        {
            _commentRepository = commentRepository;
            _fileService = fileService;
            _webSocketService = webSocketService;
            _validationService = validationService;
        }

        [HttpGet("ws")]
        public async Task WebSocketHandler()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                var socket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                _webSocketService.AddClient(socket);

                try
                {
                    var buffer = new byte[1024 * 4];
                    while (socket.State == WebSocketState.Open)
                    {
                        var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                        if (result.MessageType == WebSocketMessageType.Close)
                        {
                            await _webSocketService.RemoveClientAsync(socket);
                        }
                    }
                }
                catch (WebSocketException ex)
                {
                    Console.WriteLine($"WebSocket error: {ex.Message}");
                }
            }
            else
            {
                HttpContext.Response.StatusCode = 400;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetTopLayerComments(int page, string? sort)
        {
            var response = await _commentRepository.GetTopLayerComments(COMMENTS_PER_PAGE, page, sort);

            return Ok(new { Comments = response.Comments.Select(el => new CommentResponse(el)), TotalPages = response.TotalPages, CommentsPerPage = COMMENTS_PER_PAGE });
        }

        [HttpGet("children/{id}")]
        public async Task<IActionResult> GetChildren(int id)
        {
            var comments = await _commentRepository.GetChildrenAsync(id);
            return Ok(new { Comments = comments.Select(el => new CommentResponse(el)) });
        }

        [HttpPost]
        public async Task<IActionResult> PostComment([FromForm] CommentRequest request)
        {
            var captchaText = HttpContext.Session.GetString("captchaText");
            var errors = new Dictionary<string, string>();

            if (captchaText == null || !captchaText.Equals(request.CaptchaText, StringComparison.OrdinalIgnoreCase))
                errors["Captcha"] = "Invalid captcha";

            if (!_validationService.IsValidEmail(request.Email))
                errors["Email"] = "Invalid email";

            if (request.Homepage != null && !_validationService.IsValidURL(request.Homepage))
                errors["Homepage"] = "Invalid homepage URL";

            if (!_validationService.IsValidHtml(request.Content))
                errors["Content"] = "The line contains errors or invalid tags (only <a><strong><i><code> are allowed with no optional attributes)";

            if (request.File != null && !_validationService.IsValidFileExtension(request.File))
                errors["FileExtension"] = $"Unsupported file extension (only .png, .jpg, .gif & .txt are supported)";

            if (request.File != null && request.File.FileName.EndsWith(".txt") && !_validationService.IsValidTxtFile(request.File))
                errors["File"] = $"The file {request.File.FileName} is too big (100KB max)";


            if (errors.Any())
                return BadRequest(new { Errors = errors });

            string? fileName = null;
            if (request.File != null)
                fileName = await _fileService.SaveFileAsync(request.File);

            var comment = new CommentEntity(request, fileName);

            await _commentRepository.AddAsync(comment);

            var response = new CommentResponse(comment);

            var responseJson = JsonConvert.SerializeObject(response);
            _webSocketService.QueueMessage(responseJson);

            return Ok(response);
        }
    }
}
