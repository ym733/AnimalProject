using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Animal.Web.Controllers
{
	public class UserController : Base.AuthorizationController
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		public UserController(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult GetAllUsers()
		{
			var obj = new AnimalProvider.Users();
			var objList = obj.getAllUsers();
			return View(objList);
		}

		public IActionResult GetUser(int id)
		{
			if (id == 0)
			{
				return View(new Entities.User());
			}
			var obj = new AnimalProvider.Users();
			var model = obj.getUser(id);
			return View(model);
		}

		[HttpGet]
		public IActionResult AddUser()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult AddUser(viewModel.User user)
		{
			using var obj = new AnimalProvider.Users();
			if (obj.addUser(user))
			{
				return View();
			}
			else
			{
				ModelState.AddModelError("FormValidation", "an error has occured");
				return View();
			}
		}

		[HttpGet]
		public IActionResult UpdateUser() 
		{ 
			return View(); 
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult UpdateUser(viewModel.User user) 
		{
			using var obj = new AnimalProvider.Users();
			if (obj.updateUser(user))
			{
				return View();
			}
			else
			{
				ModelState.AddModelError("FormValidation", "an error has occured");
				return View();
			}
		}

		[HttpGet]
		public IActionResult DeleteUser()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteUser(int id) 
		{
			using var obj = new AnimalProvider.Users();
			if (obj.deleteUser(id))
			{
				return View();
			}
			else
			{
				ModelState.AddModelError("FormValidation", "an error has occured");
				return View();
			}
		}
	}
}
