using Microsoft.AspNetCore.Mvc;

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
			return View(CurrentUser);
		}
	}
}
