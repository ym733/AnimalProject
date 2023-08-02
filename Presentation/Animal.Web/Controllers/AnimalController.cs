using Microsoft.AspNetCore.Mvc;

namespace Animal.Web.Controllers
{
    public class AnimalController : Controller
    {
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

        public IActionResult AddAnimal()
        {
            return View();
        }

        public IActionResult FormAddAnimal(Entities.Animal animal)
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

        public IActionResult UpdateAnimal()
        {
            return View();
        }

        public IActionResult FormUpdateAnimal(Entities.Animal animal)
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

        public IActionResult DeleteAnimal()
        {
            return View();
        }

        public IActionResult FormDeleteAnimal(int id)
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
