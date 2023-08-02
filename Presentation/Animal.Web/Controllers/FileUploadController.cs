using Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Animal.Web.Controllers
{
	public class FileUploadController : Controller
	{
		public static IWebHostEnvironment _webHostEnvironment;

		public FileUploadController(IWebHostEnvironment webHostEnvironment)
		{
			_webHostEnvironment = webHostEnvironment;
		}
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult UploadFile()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult FormUploadFile(FileUpload fileUpload)
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
					return View("UploadFile");
				}
				else
				{
					return BadRequest("File does not exist");
				}
			}
			catch (Exception ex)
			{
				return BadRequest("Error has occured");
			}
		}

		public IActionResult GetFile()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult FormGetFile(string fileName)
		{
			string path = _webHostEnvironment.ContentRootPath + "\\FileUploads\\"; 
			string filePath = path + fileName; 

			if (System.IO.File.Exists(filePath)) 
			{
				var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read); 
				return new FileStreamResult(fileStream, "image/png"); 
			}
			else
			{
				return BadRequest("file does not exist");
			}
		}
	}
}
