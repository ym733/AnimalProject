using Microsoft.AspNetCore.SignalR;
using System.Collections;

namespace Animal.Web.Hubs
{
    public sealed class ChatHub : Hub
    {
        public static Hashtable users = new Hashtable();

        public async Task onConnect(string user)
        {
			await Clients.All.SendAsync("ReceiveMessage", $"{user} has joined, id: {Context.ConnectionId}.");
            users.Add(user, Context.ConnectionId);
		}
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", $"{user}: {message}");
        }
        public async Task SendToMessage(string toUser, string user, string message)
        {
			await Clients.Client((string)users[toUser]).SendAsync("ReceiveMessage", $"whisper from {user}: {message}");
		}
    }
}
