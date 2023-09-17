using Microsoft.AspNetCore.SignalR;
using System.Collections;
using System.Diagnostics;

namespace Animal.Web.Hubs
{
    public sealed class ChatHub : Hub
    {
        public static List<HubCallerContext> users = new List<HubCallerContext>();
        
        public async Task onConnect(int id, string username)
        {
			await Clients.All.SendAsync("ReceiveMessage", $"{username} has joined.");
            Context.Items.Add("id", id);
            Context.Items.Add("username", username);
            users.Add(Context);
		}
        public async Task SendGlobalMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", $"{Context.Items["username"]}: {message}");
			using var obj = new AnimalProvider.Message();
            obj.sendGlobalMessage((int)Context.Items["id"], message);
		}
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await Clients.All.SendAsync("ReceiveMessage", $"{Context.Items["username"]} has disconnected.");
            users.Remove(Context);
        }
        /*
        public async Task SendPrivateMessage(string toUser, string user, string message)
        {
            ViewModel.ConnectedUser? connectedUser = (ViewModel.ConnectedUser)users[toUser];
            if (connectedUser == null )
            {
                await Clients.Caller.SendAsync("ReceiveMessage", $"{toUser} either doesn't exist or is not online right now");
            }
			await Clients.Client(connectedUser.connectionID).SendAsync("ReceiveMessage", $"whisper from {user}: {message}");
		}
        */
    }
}
