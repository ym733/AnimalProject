using Animal.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Animal.Web.Controllers
{
    public class HomeController : Base.AuthorizationController
	{
        private readonly ILogger<HomeController> _logger;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
		{
            _logger = logger;
			_httpContextAccessor = httpContextAccessor;
		}

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
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