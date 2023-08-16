using Microsoft.AspNetCore.Mvc;

namespace Animal.Web.Controllers
{
	public class AnimalController : Base.AuthorizationController
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		public AnimalController(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult GetAnimal(int id)
		{
			if (id == 0)
			{
				return View(new Entities.Animal());
			}

			var obj = new AnimalProvider.Animal();
			Entities.Animal entity = obj.getAnimal(id);

			return View(entity);
		}

		public IActionResult GetAllAnimals()
		{
			var obj = new AnimalProvider.Animal();
			List<Entities.Animal> entityList = obj.getAllAnimals();

			return View(entityList);
		}

		[HttpGet]
		public IActionResult AddAnimal()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult AddAnimal(Entities.Animal animal)
		{
			if (ModelState.IsValid)
			{
				var obj = new AnimalProvider.Animal();
				if (obj.addAnimal(animal))
				{
					//Success
					ModelState.AddModelError("FormValidation", "Success");
					return View();
				}
				else
				{
					ModelState.AddModelError("FormValidation", "an error has occured");
					return View(animal);
				}
			}
			else
			{
				return View(animal);
			}
		}

		[HttpGet]
		public IActionResult UpdateAnimal()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult UpdateAnimal(Entities.Animal animal)
		{
			if (ModelState.IsValid)
			{
				using var Obj = new AnimalProvider.Animal();

				if (Obj.updateAnimal(animal))
				{
					//Success
					ModelState.AddModelError("FormValidation", "Success");
					return View();
				}
				else
				{
					ModelState.AddModelError("FormValidation", "an error has occured");
					return View(animal);
				}
			}
			else
			{
				return View(animal);
			}
		}

		[HttpGet]
		public IActionResult DeleteAnimal()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteAnimal(int id)
		{
			if (ModelState.IsValid)
			{
				using var obj = new AnimalProvider.Animal();
				if (obj.deleteAnimal(id))
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
