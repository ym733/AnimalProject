using Animal.Web.MediaComponents;
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
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Index(ViewModel.Login model)
		{
			if (ModelState.IsValid)
			{
				var data = new AnimalProvider.Users().getUserByInfo(model.Name);

				if (data == null)
				{
					ModelState.AddModelError("FormValidation", "Wrong Username");
					return View(model);
				}

				if (!(new PasswordHasher().verify(data.password, model.Password)))
				{
					ModelState.AddModelError("FormValidation", "Wrong Password");
					return View(model);
				}

				var varClaims = new ClaimsPrincipal(new ClaimsIdentity(new[]
				{
					new Claim("Id", data.Id.ToString()),
					new Claim(ClaimTypes.Name, data.Name),
					new Claim(ClaimTypes.Email, data.Email),
					new Claim(ClaimTypes.DateOfBirth, data.DateOfBirth),
					new Claim(ClaimTypes.Role, data.role)
				}, "Custom"));

				var authProperties = new AuthenticationProperties
				{
					IsPersistent = true,
					ExpiresUtc = DateTime.UtcNow.AddMinutes(60),
					AllowRefresh = true,
				};

				await HttpContext.SignInAsync(varClaims, authProperties);
			}
			else
			{
				return View(model);
			}

			//Success
			return RedirectToAction("Index", "Home");
		}

		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}


		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(ViewModel.Register model)
		{
			if (ModelState.IsValid)
			{
				using var obj = new AnimalProvider.Users();
				model.Password = new PasswordHasher().hash(model.Password);

				if (obj.registerUser(model))
				{
					var data = new AnimalProvider.Users().getUserByInfo(model.Name);

					var varClaims = new ClaimsPrincipal(new ClaimsIdentity(new[]
					{
					new Claim("Id", data.Id.ToString()),
					new Claim(ClaimTypes.Name, data.Name),
					new Claim(ClaimTypes.Email, data.Email),
					new Claim(ClaimTypes.DateOfBirth, data.DateOfBirth),
					new Claim(ClaimTypes.Role, data.role)
						}, "Custom"));

					var authProperties = new AuthenticationProperties
					{
						IsPersistent = true,
						ExpiresUtc = DateTime.UtcNow.AddMinutes(60),
						AllowRefresh = true,
					};

					await HttpContext.SignInAsync(varClaims, authProperties);

					return RedirectToAction("Index", "Home");
				}
				else
				{
					ModelState.AddModelError("FormValidation", "an error has occured");
					return View(model);
				}
			}
			else
			{
				return View(model);
			}
		}
	}
}
