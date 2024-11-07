using CommentsAPI.Interfaces;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
namespace CommentsAPI.Services
{

    public class WebSocketService : IWebSocketService
    {
        private readonly ConcurrentBag<WebSocket> _clients = new();
        private readonly ConcurrentQueue<string> _messageQueue = new();
        private readonly CancellationTokenSource _cancellationTokenSource = new();

        public WebSocketService()
        {
            Task.Run(() => ProcessMessages(_cancellationTokenSource.Token));
        }

        public void AddClient(WebSocket socket)
        {
            _clients.Add(socket);
        }

        public void QueueMessage(string message)
        {
            _messageQueue.Enqueue(message);
        }

        private async Task ProcessMessages(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                while (_messageQueue.TryDequeue(out var message))
                {
                    var buffer = Encoding.UTF8.GetBytes(message);
                    for (int i = 0; i < _clients.Count; i++)
                    {
                        var client = _clients.ElementAt(i);
                        if (client.State == WebSocketState.Open)
                        {
                            try
                            {
                                await client.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
                            }
                            catch (WebSocketException)
                            {
                                _clients.TryTake(out client);
                            }
                        }
                    }
                }
                await Task.Delay(50, cancellationToken);
            }
        }

        public async Task RemoveClientAsync(WebSocket socket)
        {
            if (_clients.Contains(socket))
            {
                _clients.TryTake(out _);
                if (socket.State != WebSocketState.Closed)
                {
                    await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Connection closed", CancellationToken.None);
                }
                socket.Dispose();
            }
        }

        public void StopService()
        {
            _cancellationTokenSource.Cancel();
        }
    }

}
