using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Animal.Web.Controllers
{
	public class LoginController : Base.BaseController
	{
		[HttpGet]
		public IActionResult Index()
		{
			return View(new ViewModel.Login());
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Index(ViewModel.Login model)
		{
			if (ModelState.IsValid)
			{
				var data = new AnimalProvider.Users().getUserByInfo(model.Name, model.Password);

				if (data == null)
				{
					ModelState.AddModelError("FormValidation", "Wrong Username or Password");
					return View();
				}

				var varClaims = new ClaimsPrincipal(new ClaimsIdentity(new[]
				{
					new Claim(ClaimTypes.Name, data.Name),
					new Claim(ClaimTypes.Email, data.Email),
					new Claim(ClaimTypes.DateOfBirth, data.DateOfBirth),
					new Claim(ClaimTypes.Role, data.role)
				}, "Custom"));

				var authProperties = new AuthenticationProperties
				{
					IsPersistent = true,
				};

				await HttpContext.SignInAsync(varClaims, authProperties);
			}
			else
			{
				return View(model);
			}
			return RedirectToAction("Index", "Home");
		}
	}
}
