using CommentsAPI.Interfaces;
using CommentsAPI.Models.DTO;
using CommentsAPI.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

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

        [HttpPost]
        public async Task<IActionResult> PostComment([FromForm] CommentRequest request)
        {
            // Перевірка капчі
            var captchaText = HttpContext.Session.GetString("captchaText");
            if (captchaText == null || !captchaText.Equals(request.CaptchaText, StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("Invalid captcha");
            }

            string? fileName = null;
            if (request.File != null)
            {
                fileName = await _fileService.SaveFileAsync(request.File);
            }
            // Створення нового коментаря
            var comment = new CommentEntity(request, fileName);

            // Збереження в базу даних
            await _commentRepository.AddAsync(comment);

            // Формуємо відповідь
            var response = new CommentResponse
            {
                Id = comment.Id,
                Content = comment.Content,
                Username = comment.Username,
                Email = comment.Email,
                Homepage = comment.Homepage,
                FileURL = comment.FileURL,
                ParentId = comment.ParentId,
                CreatedAt = comment.CreatedAt,
                ChildrenCount = 0
            };

            await BroadcastMessage(response);

            return Ok(response);
        }

        private async Task ReceiveMessage(WebSocket socket)
        {
            var buffer = new byte[1024 * 4];
            WebSocketReceiveResult result;
            do
            {
                result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            } while (!result.CloseStatus.HasValue);

            _clients.Remove(socket);
            await socket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }

        private async Task BroadcastMessage(CommentResponse message)
        {
            var messageJson = JsonSerializer.Serialize(message);
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
