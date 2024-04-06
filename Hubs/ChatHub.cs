using Fiorello.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace Fiorello.Hubs
{
    public class ChatHub:Hub
    {
        private readonly UserManager<AppUser> _userManager;
        public ChatHub(UserManager<AppUser> userManager)
        {
            _userManager = userManager;

        }
        public async Task SendMessage(string[] users, string message)
        {
            // await Clients.All.SendAsync("ReceiveMessage", user, message,DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
            //var messageUser = await _userManager.FindByNameAsync(user);
            List<string> connectionIds = new();
            foreach (string user in users) 
            {
                var messageUser=await _userManager.FindByNameAsync(user);
                connectionIds.Add(messageUser.ConnectionId);
            }
           // await Clients.Client("messageUser.ConnectionId").SendAsync("PrivateMessage", users, message, DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
            await Clients.Clients(connectionIds).SendAsync("PrivateMessage",  message);
        }
        public override Task OnConnectedAsync()
        {
            if(Context.User.Identity.IsAuthenticated)
            {
                var user=_userManager.FindByNameAsync(Context.User.Identity.Name).Result;
                user.ConnectionId = Context.ConnectionId;
                var result=_userManager.UpdateAsync(user).Result;
                Clients.All.SendAsync("userconnect", user.Id);
            }
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                var user = _userManager.FindByNameAsync(Context.User.Identity.Name).Result;
                user.ConnectionId = null;
                var result = _userManager.UpdateAsync(user).Result;
                Clients.All.SendAsync("userdisconnect", user.Id);
            }
            return base.OnDisconnectedAsync(exception);
            //signalr continue
        }

    }
}
