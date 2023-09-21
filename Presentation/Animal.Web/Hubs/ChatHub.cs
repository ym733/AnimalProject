using Microsoft.AspNetCore.SignalR;
using System.Collections;
using System.Diagnostics;
using System.Security.Claims;

namespace Animal.Web.Hubs
{
    public  class ChatHub : Hub
    {
        public static List<HubCallerContext> users = new List<HubCallerContext>();
        
        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("ReceiveMessage", $"{Context.User.Identity.Name} has joined.");
            await Clients.All.SendAsync("UserJoins", Context.User.FindFirstValue("Id"), Context.User.Identity.Name, Context.ConnectionId);
            
            Context.Items.Add("id", Context.User.FindFirstValue("Id"));
            Context.Items.Add("username", Context.User.Identity.Name);
            users.Add(Context);
		}
        public async Task SendGlobalMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", $"{Context.Items["username"]}: {message}");

            //obj.sendGlobalMessage((int)Context.Items["id"], message);                 
		}
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await Clients.All.SendAsync("ReceiveMessage", $"{Context.Items["username"]} has disconnected.");
            await Clients.All.SendAsync("UserLeaves", Context.User.Identity.Name);

            users.Remove(Context);
        }
 
        public async Task SendPrivateMessage(int toUserID, string toUserConnectionID, string message)
        {
            await Clients.Caller.SendAsync("ReceivePrivateMessage", $"{Context.Items["username"]}: {message}");
            await Clients.Client(toUserConnectionID).SendAsync("ReceivePrivateMessage", $"{Context.Items["username"]}: {message}");

            //obj.sendPrivateMessage((int)Context.Items["id"], toUserID, message);
        }

    }
}
