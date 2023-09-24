using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Animal.Web.Hubs
{
	public class ChatHub : Hub
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

		public override async Task OnDisconnectedAsync(Exception? exception)
		{
			await Clients.All.SendAsync("ReceiveMessage", $"{Context.Items["username"]} has disconnected.");
			await Clients.All.SendAsync("UserLeaves", Context.User.Identity.Name);

			users.Remove(Context);
		}

		public async Task SendGlobalMessage(string message)
		{
			await Clients.All.SendAsync("ReceiveMessage", $"{Context.Items["username"]}: {message}");
		}

		public async Task SendPrivateMessage(int toUserID, string toUserConnectionID, string message)
		{
			var temp = toUserID != Int32.Parse(Context.Items["id"].ToString()) && toUserConnectionID != null;
			if (temp)
			{
                await Clients.Client(toUserConnectionID).SendAsync("ReceivePrivateMessage", $"{Context.Items["username"]}: {message}");
            }
            await Clients.Caller.SendAsync("ReceivePrivateMessage", Int32.Parse(Context.Items["id"].ToString()), $"{Context.Items["username"]}: {message}");
        }
	}
}
