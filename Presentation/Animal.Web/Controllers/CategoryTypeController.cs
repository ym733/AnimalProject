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

		[HttpGet]
		public IActionResult addCategoryType()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult AddCategoryType(ViewModel.CategoryType CategoryType)
		{
			if (ModelState.IsValid)
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
						ModelState.AddModelError("FormValidation", "empty or no file sent");
						return View(CategoryType);
					}
				}
				catch (Exception ex)
				{
					ModelState.AddModelError("FormValidation", "an error has occured\n" + ex.Message);
					return View(CategoryType);
				}

				Entities.CategoryType modelSent = new Entities.CategoryType();
				modelSent.Id = CategoryType.Id;
				modelSent.CategoryName = CategoryType.CategoryName;
				modelSent.ImagePath = pathToFile;

				if (obj.addCategoryType(modelSent))
				{
					//Success
					ModelState.AddModelError("FormValidation", "Success");
					return View();
				}
				else
				{
					ModelState.AddModelError("FormValidation", "an error has occured");
					return View(CategoryType);
				}
			}
			else
			{
				return View(CategoryType);
			}
			
		}

		[HttpGet]
		public IActionResult UpdateCategoryType()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult UpdateCategoryType(ViewModel.CategoryType CategoryType)
		{
			if (ModelState.IsValid)
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
						ModelState.AddModelError("FormValidation", "empty or no file sent");
						return View(CategoryType);
					}
				}
				catch (Exception ex)
				{
					ModelState.AddModelError("FormValidation", "an error has occured\n" + ex.Message);
					return View(CategoryType);
				}

				Entities.CategoryType modelSent = new Entities.CategoryType();
				modelSent.Id = CategoryType.Id;
				modelSent.CategoryName = CategoryType.CategoryName;
				modelSent.ImagePath = pathToFile;

				if (obj.updateCategoryType(modelSent))
				{
					//Success
					ModelState.AddModelError("FormValidation", "Success");
					return View();
				}
				else
				{
					ModelState.AddModelError("FormValidation", "an error has occured");
					return View(CategoryType);
				}
			}
			else
			{
				return View(CategoryType);
			}
		}

		[HttpGet]
		public IActionResult DeleteCategoryType()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteCategoryType(int id)
		{
			if (ModelState.IsValid)
			{
				using var obj = new AnimalProvider.CategoryType();

				if (obj.deleteCategoryType(id))
				{
					//Success
					ModelState.AddModelError("FormValidation", "Success");
					return View();
				}
				else
				{
					ModelState.AddModelError("FormValidation", "an error has occured");
					return View(id);
				}
			}
			else
			{
				return View(id);
			}
		}
	}
}
