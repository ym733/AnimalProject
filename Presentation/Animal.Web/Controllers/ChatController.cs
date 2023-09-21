using Animal.Web.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections;
using System.Collections.Generic;

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
			var obj = new AnimalProvider.Message();
			List<Entities.Message> messages = obj.getGlobalMessages();

			var tuple = new Tuple<List<Entities.Message>, List<HubCallerContext>, Entities.User>(messages, ChatHub.users, CurrentUser);

            return View(tuple);
		}

		public IActionResult PrivateChat(int id, string username, string connectionID)
		{
			var obj = new AnimalProvider.Message();
			List<Entities.PrivateMessage> messages = obj.getPrivateMessages(CurrentUser.Id, id);

			var tuple = new Tuple<List<Entities.PrivateMessage>, int, string, string>(messages, id, username, connectionID);

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
	}
}
