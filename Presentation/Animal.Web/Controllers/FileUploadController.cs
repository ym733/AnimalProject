using Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Animal.Web.Controllers
{
	public class FileUploadController : Base.AuthorizationController
	{
		public static IWebHostEnvironment _webHostEnvironment;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public FileUploadController(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
		{
			_webHostEnvironment = webHostEnvironment;
			_httpContextAccessor = httpContextAccessor;
		}
		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public IActionResult UploadFile()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult UploadFile(FileUpload fileUpload)
		{
			try
			{
				if (fileUpload.files.Length > 0) 
				{
					string path = _webHostEnvironment.ContentRootPath + "\\FileUploads\\"; 
					if (!Directory.Exists(path))
					{
						Directory.CreateDirectory(path); 
					}
					using (FileStream fileStream = System.IO.File.Create(path + fileUpload.files.FileName))
					{
						fileUpload.files.CopyTo(fileStream);
						fileStream.Flush();
						fileStream.Close();
					}
					//Successful
					return View();
				}
				else
				{
					ModelState.AddModelError("FormValidation", "empty or no file sent");
					return View();
				}
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("FormValidation", "an error has occured");
				return View();
			}
		}

		[HttpGet]
		public IActionResult GetFile()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult GetFile(string fileName)
		{
			string path = _webHostEnvironment.ContentRootPath + "\\FileUploads\\"; 
			string filePath = path + fileName; 

			if (System.IO.File.Exists(filePath)) 
			{
				//Successful
				var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read); 
				return new FileStreamResult(fileStream, "image/png"); 
			}
			else
			{
				ModelState.AddModelError("FormValidation", "file does not exist");
				return View();
			}
		}
	}
}
