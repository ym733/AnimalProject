using Animal.Web.Base;
using Animal.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    }
}