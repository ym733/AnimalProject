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

			var tuple = new Tuple<Entities.User, List<Entities.Message>, List<HubCallerContext>>(CurrentUser, messages, ChatHub.users);

            return View(tuple);
		}

		public IActionResult PrivateChat(HubCallerContext context)
		{
			//list of private messages
			//current user??
			//other user context
			return View();
		}
	}
}
