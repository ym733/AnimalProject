using Animal.Web.Models;
using Core;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Animal.Web.Controllers
{
    public class HomeController : Base.BaseController
    {
		public IActionResult Index()
        {
            return View();
        }

		public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Unauthorized()
        {
            return View();
        }

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Test()
        {
			var obj = new AnimalProvider.Users();
			var model = "this is a normal string";

            var encrypted = model.Encrypt<string>();

			ModelState.AddModelError("encrypted", encrypted);

            var decrypted = encrypted.Decrypt<string>(EncryptionServices.genKey, EncryptionServices.genIV);

			ModelState.AddModelError("decrypted", decrypted);

			return View();
        }
    }
}