using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Animal.Web.Controllers
{
	public class CategoryTypeController : Base.AuthorizationController
	{
		public static IWebHostEnvironment _webHostEnvironment;
		private readonly IHttpContextAccessor _httpContextAccessor;


		public CategoryTypeController(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
		{
			_webHostEnvironment = webHostEnvironment;
			_httpContextAccessor = httpContextAccessor;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult GetAllCategories()
		{
			var obj = new AnimalProvider.CategoryType();
			var objList = obj.getAllCategories();

			return View(objList);
		}

		public IActionResult GetCategoryType(int id)
		{
			if (id == 0)
			{
				return View(new Entities.CategoryType());
			}

			var obj = new AnimalProvider.CategoryType();
			var model = obj.getCategoryType(id);

			return View(model);
		}

		public IActionResult addCategoryType()
		{
			return View();
		}

		public IActionResult FormAddCategoryType(viewModel.CategoryType CategoryType)
		{
			using var obj = new AnimalProvider.CategoryType();

			string pathToDirectory = _webHostEnvironment.ContentRootPath + "\\FileUploads\\";
			string pathToFile = pathToDirectory + String.Format("{0}.{1}", DateTimeOffset.Now.ToUnixTimeMilliseconds(), CategoryType.files.ContentType.Split("/")[1]);
			//(DateTimeOffset.Now.ToUnixTimeSeconds()) should return the current unix time in milliseconds
			//(CategoryType.files.ContentType.Split("/")[1]) should return "png" as in the extension of the image

			try
			{
				if (CategoryType.files.Length > 0)
				{
					if (!Directory.Exists(pathToDirectory))
					{
						Directory.CreateDirectory(pathToDirectory);
					}
					using (FileStream fileStream = System.IO.File.Create(pathToFile))
					{
						CategoryType.files.CopyTo(fileStream);
						fileStream.Flush();
						fileStream.Close();
					}
				}
				else
				{
					return BadRequest("empty or no file sent");
				}
			}
			catch (Exception ex)
			{
				return BadRequest("an error has occured\n" + ex.Message);
			}

			Entities.CategoryType modelSent = new Entities.CategoryType();
			modelSent.Id = CategoryType.Id;
			modelSent.CategoryName = CategoryType.CategoryName;
			modelSent.ImagePath = pathToFile;

			if (obj.addCategoryType(modelSent))
			{
				return View("AddCategoryType");
			}
			else
			{
				return BadRequest("an error has occured");
			}
		}

		public IActionResult UpdateCategoryType()
		{
			return View();
		}

		public IActionResult FormUpdateCategoryType(viewModel.CategoryType CategoryType)
		{
			using var obj = new AnimalProvider.CategoryType();

			//to delete the current categorytype image currently stored
			Entities.CategoryType prevInstance = obj.getCategoryType(CategoryType.Id);
			System.IO.File.Delete(prevInstance.ImagePath);

			string pathToDirectory = _webHostEnvironment.ContentRootPath + "\\FileUploads\\";
			string pathToFile = pathToDirectory + String.Format("{0}.{1}", DateTimeOffset.Now.ToUnixTimeMilliseconds(), CategoryType.files.ContentType.Split("/")[1]);
			//(DateTimeOffset.Now.ToUnixTimeSeconds()) should return the current unix time in milliseconds
			//(CategoryType.files.ContentType.Split("/")[1]) should return "png" as in the extension of the image

			try
			{
				if (CategoryType.files.Length > 0)
				{
					if (!Directory.Exists(pathToDirectory))
					{
						Directory.CreateDirectory(pathToDirectory);
					}
					using (FileStream fileStream = System.IO.File.Create(pathToFile))
					{
						CategoryType.files.CopyTo(fileStream);
						fileStream.Flush();
						fileStream.Close();
					}
				}
				else
				{
					return BadRequest("empty or no file sent");
				}
			}
			catch (Exception ex)
			{
				return BadRequest("an error has occured\n" + ex.Message);
			}

			Entities.CategoryType modelSent = new Entities.CategoryType();
			modelSent.Id = CategoryType.Id;
			modelSent.CategoryName = CategoryType.CategoryName;
			modelSent.ImagePath = pathToFile;

			if (obj.updateCategoryType(modelSent))
			{
				return View("UpdateCategoryType");
			}
			else
			{
				return BadRequest("an error ahs occured");
			}
		}

		public IActionResult DeleteCategoryType()
		{
			return View();
		}

		public IActionResult FormDeleteCategoryType(int id)
		{
			using var obj = new AnimalProvider.CategoryType();

			if (obj.deleteCategoryType(id))
			{
				return View("DeleteCategoryType");
			}
			else
			{
				return BadRequest("an error has occured");
			}
		}
	}
}
