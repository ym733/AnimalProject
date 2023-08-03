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
            if(id == 0) 
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
            var obj = new AnimalProvider.Animal();
			if (obj.addAnimal(animal))
			{
				return View("AddAnimal");
			}
			else
			{
				return BadRequest("an error has occured");
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
			using var Obj = new AnimalProvider.Animal();

			if (Obj.updateAnimal(animal))
			{
				return View("UpdateAnimal");
			}
			else
			{
				return BadRequest("an error ahs occured");
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
			using var obj = new AnimalProvider.Animal();

			if (obj.deleteAnimal(id))
			{
				return View("DeleteAnimal");
			}
			else
			{
				return BadRequest("an error has occured");
			}
		}
    }
}
