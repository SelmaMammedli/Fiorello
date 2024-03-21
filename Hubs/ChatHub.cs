using Microsoft.AspNetCore.SignalR;

namespace Fiorello.Hubs
{
    public class ChatHub:Hub
    {
        public async Task SendMessage(string user, string message,DateTime time)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message,time.ToString("MM/dd/yyyy"));
        }
    }
}
