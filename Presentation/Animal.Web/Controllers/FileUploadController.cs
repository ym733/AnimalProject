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
			if (ModelState.IsValid)
			{
				try
				{
					if (fileUpload.files.Length > 0)
					{
						string path = _webHostEnvironment.WebRootPath + "\\FileUploads\\";
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
						//Success
						ModelState.AddModelError("FormValidation", "Success");
						return View();
					}
					else
					{
						ModelState.AddModelError("FormValidation", "empty or no file sent");
						return View(fileUpload);
					}
				}
				catch
				{
					ModelState.AddModelError("FormValidation", "an error has occured");
					return View(fileUpload);
				}
			}
			else
			{
				return View(fileUpload);
			}
		}

		[HttpGet]
		public IActionResult GetFile()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult GetFile(string FileName)
		{
			if(ModelState.IsValid)
			{
				string path = _webHostEnvironment.WebRootPath + "\\FileUploads\\";
				string filePath = path + FileName;

				if (System.IO.File.Exists(filePath))
				{
					//Success
					var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
					return new FileStreamResult(fileStream, "image/png");
				}
				else
				{
					ModelState.AddModelError("FormValidation", "file does not exist");
					return View(FileName);
				}
			}
			else
			{
				return View(FileName);
			}
		}
	}
}
