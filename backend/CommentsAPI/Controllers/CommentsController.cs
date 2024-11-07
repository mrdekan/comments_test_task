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
        public CommentsController(ICommentRepository commentRepository, IFileService fileService)
        {
            _commentRepository = commentRepository;
            _fileService = fileService;
        }

        [HttpGet("ws")]
        public async Task WebSocketHandler()
        {
            Console.WriteLine("WebSocket request received");
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using (var socket = await HttpContext.WebSockets.AcceptWebSocketAsync())
                {
                    _clients.Add(socket);
                    await ReceiveMessage(socket);
                }
            }
            else
            {
                HttpContext.Response.StatusCode = 400;
            }
        }

        private async Task ReceiveMessage(WebSocket socket)
        {
            var buffer = new byte[1024 * 4];
            WebSocketReceiveResult result;

            try
            {
                do
                {
                    result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                    if (result.MessageType == WebSocketMessageType.Text)
                    {
                        var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                        await BroadcastMessage(message);
                    }
                } while (!result.CloseStatus.HasValue);

                _clients.Remove(socket);
                await socket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error during WebSocket communication: {ex.Message}");
            }
            finally
            {
                if (socket.State != WebSocketState.Closed)
                {
                    await socket.CloseAsync(WebSocketCloseStatus.InternalServerError, "Internal error", CancellationToken.None);
                }
            }
        }


        private async Task BroadcastMessage(string message)
        {
            var buffer = Encoding.UTF8.GetBytes(message);
            foreach (var client in _clients)
            {
                if (client.State == WebSocketState.Open)
                {
                    await client.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetTopLayerComments()
        {
            var comments = await _commentRepository.GetTopLayerComments(25, 0);
            return Ok(new { Comments = comments.Select(el => new CommentResponse(el)) });
        }

        [HttpPost]
        public async Task<IActionResult> PostComment([FromForm] CommentRequest request)
        {
            var captchaText = HttpContext.Session.GetString("captchaText");
            if (captchaText == null || !captchaText.Equals(request.CaptchaText, StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("Invalid captcha");
            }

            string? fileName = null;
            if (request.File != null)
                fileName = await _fileService.SaveFileAsync(request.File);

            var comment = new CommentEntity(request, fileName);

            await _commentRepository.AddAsync(comment);

            var response = new CommentResponse(comment);

            await BroadcastMessage(response);

            return Ok(response);
        }
        private async Task BroadcastMessage(CommentResponse message)
        {
            var settings = new JsonSerializerSettings
            {
                DateFormatString = "yyyy-MM-ddTHH:mm:ss.fffK"
            };
            var messageJson = JsonConvert.SerializeObject(message, settings);
            var buffer = Encoding.UTF8.GetBytes(messageJson);

            foreach (var client in _clients)
            {
                if (client.State == WebSocketState.Open)
                {
                    await client.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }
        }
    }
}
