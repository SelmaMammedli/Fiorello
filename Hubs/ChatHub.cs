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
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message,DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
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
