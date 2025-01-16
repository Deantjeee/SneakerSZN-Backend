using Microsoft.AspNetCore.SignalR;

namespace SneakerSZN.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessageToAll(string message)
        {
            Console.WriteLine($"Message sent: {message}");

            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}