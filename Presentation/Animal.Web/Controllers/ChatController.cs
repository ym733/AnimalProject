using Animal.Web.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Animal.Web.Controllers
{
    public class ChatController : Base.UserController
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		public ChatController(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}
		public IActionResult Index()
		{
			using var obj = new AnimalProvider.Message();
            List<Entities.Message> messages = obj.getGlobalMessages();

			var tuple = new Tuple<List<Entities.Message>, Entities.User>(messages, CurrentUser);

            return View(tuple);
		}

		public IActionResult PrivateChat(int id, string username, string connectionID)
		{
			var obj = new AnimalProvider.Message();
			List<Entities.PrivateMessage> messages = obj.getPrivateMessages(CurrentUser.Id, id);

			var tuple = new Tuple<List<Entities.PrivateMessage>, int, string, string?, int>(messages, id, username, connectionID, CurrentUser.Id);

			return View(tuple);
		}

		public IActionResult sendGlobalMessage(int senderID, string text)
		{
            using var obj = new AnimalProvider.Message();
            if (obj.sendGlobalMessage(senderID, text))
            {
                return Ok("sent");
            }
            else
            {
                return BadRequest("an error has occured");
            }
        }

        public IActionResult sendPrivateMessage(int senderID, int receiverID, string text)
		{
            using var obj = new AnimalProvider.Message();
            if (obj.sendPrivateMessage(senderID, receiverID, text))
            {
                return Ok("Message sent");
            }
            else
            {
                return BadRequest("an error has occured");
            }
        }

        public IActionResult sideBar()
        {
            List<string> onlineUsernames = new List<string>();
            onlineUsernames.Add(CurrentUser.Name);

            foreach (var user in ChatHub.users)
            {
                onlineUsernames.Add((string)user.Items["username"]);
            }

            using var obj = new AnimalProvider.Users();
            List<Entities.User> users = obj.getAllUsers();

            List<Entities.User> offlineUsers = new List<Entities.User>();
            foreach (var item in users)
            {
                if (!onlineUsernames.Contains(item.Name))
                {
                    offlineUsers.Add(item);
                }
            }

            var tuple = new Tuple<List<HubCallerContext>, List<Entities.User>>(ChatHub.users, offlineUsers);

            return PartialView(tuple);
        }
    }
}
