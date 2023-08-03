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

		public IActionResult AddUser()
		{
			return View();
		}

		public IActionResult FormAddUser(viewModel.User user)
		{
			using var obj = new AnimalProvider.Users();
			if (obj.addUser(user))
			{
				return View("AddUser");
			}
			else
			{
				return BadRequest("an error has occured");
			}
		}

		public IActionResult UpdateUser() 
		{ 
			return View(); 
		}

		public IActionResult FormUpdateUser(viewModel.User user) 
		{
			using var obj = new AnimalProvider.Users();
			if (obj.updateUser(user))
			{
				return View("UpdateUser");
			}
			else
			{
				return BadRequest("an error has occured");
			}
		}

		public IActionResult DeleteUser()
		{
			return View();
		}

		public IActionResult FormDeleteUser(int id) 
		{
			using var obj = new AnimalProvider.Users();
			if (obj.deleteUser(id))
			{
				return View("DeleteUser");
			}
			else
			{
				return BadRequest("an error has occured");
			}
		}
	}
}
