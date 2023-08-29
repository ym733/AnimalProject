using Animal.Web.Base;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Animal.Web.Controllers
{
	public class UserController : Base.UserController
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		public UserController(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}
		[CustomAuthorize("admin", "superAdmin")]
		public IActionResult Index()
		{
			return View();
		}

		[CustomAuthorize("admin", "superAdmin")]
		public IActionResult GetAllUsers()
		{
			var obj = new AnimalProvider.Users();
			var objList = obj.getAllUsers();
			return View(objList);
		}

		[CustomAuthorize("admin", "superAdmin")]
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

		public IActionResult GetCurrentUser()
		{
			var obj = new AnimalProvider.Users();
            var model = obj.getUser(CurrentUser.Id);
            return View("GetUser", model);
        }

		[HttpGet]
		[CustomAuthorize("admin", "superAdmin")]
		public IActionResult AddUser()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[CustomAuthorize("admin", "superAdmin")]
		public IActionResult AddUser(ViewModel.User user)
		{
			if (ModelState.IsValid)
			{
				using var obj = new AnimalProvider.Users();
				if (obj.addUser(user))
				{
					//Success
					ModelState.AddModelError("FormValidation", "Success");
					return View();
				}
				else
				{
					ModelState.AddModelError("FormValidation", "an error has occured");
					return View(user);
				}
			}
			else
			{
				return View(user);
			}
		}

		[HttpGet]
		[CustomAuthorize("admin", "superAdmin")]
		public IActionResult UpdateUser() 
		{ 
			return View(); 
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[CustomAuthorize("admin", "superAdmin")]
		public IActionResult UpdateUser(ViewModel.User user) 
		{
			if (ModelState.IsValid)
			{
				using var obj = new AnimalProvider.Users();
				if (obj.updateUser(user))
				{
					//Success
					ModelState.AddModelError("FormValidation", "Success");
					return View();
				}
				else
				{
					ModelState.AddModelError("FormValidation", "an error has occured");
					return View(user);
				}
			}
			else
			{
				return View(user);
			}
		}

		[HttpGet]
		[CustomAuthorize("admin", "superAdmin")]
		public IActionResult DeleteUser()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[CustomAuthorize("admin", "superAdmin")]
		public IActionResult DeleteUser(int id) 
		{
			if (ModelState.IsValid)
			{
				using var obj = new AnimalProvider.Users();
				if (obj.deleteUser(id))
				{
					//Success
					ModelState.AddModelError("FormValidation", "Success");
					return View();
				}
				else
				{
					ModelState.AddModelError("FormValidation", "an error has occured");
					return View(id);
				}
			}
			else
			{
				return View(id);
			}
		}
	}
}
