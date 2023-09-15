using Microsoft.AspNetCore.SignalR;
using System.Collections;

namespace Animal.Web.Hubs
{
    public sealed class ChatHub : Hub
    {
        public static Hashtable users = new Hashtable();

        public async Task onConnect(int id, string user)
        {
			await Clients.All.SendAsync("ReceiveMessage", $"{user} has joined.");
            users.Add(user, new ViewModel.ConnectedUser(id, user, Context.ConnectionId));
		}
        public async Task SendGlobalMessage(int id, string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", $"{user}: {message}");
			using var obj = new AnimalProvider.Message();
            obj.sendGlobalMessage(id, message);
		}
        public async Task SendPrivateMessage(string toUser, string user, string message)
        {
            ViewModel.ConnectedUser? connectedUser = (ViewModel.ConnectedUser)users[toUser];
            if (connectedUser == null )
            {
                await Clients.Caller.SendAsync("ReceiveMessage", $"{toUser} either doesn't exist or is not online right now");
            }
			await Clients.Client(connectedUser.connectionID).SendAsync("ReceiveMessage", $"whisper from {user}: {message}");
		}
    }
}
