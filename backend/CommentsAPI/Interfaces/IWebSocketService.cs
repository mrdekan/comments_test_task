using System.Net.WebSockets;

namespace CommentsAPI.Interfaces
{
    public interface IWebSocketService
    {
        void AddClient(WebSocket socket);
        void QueueMessage(string message);
        Task RemoveClientAsync(WebSocket socket);
        void StopService();
    }
}