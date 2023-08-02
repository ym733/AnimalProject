using Animal.WebAPI.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Animal.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class AnimalController : BaseController
    {
        public AnimalController(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            // Additional child controller initialization code, if needed
        }


        [HttpGet("GetAllAnimals", Name = "GetAllAnimals")]
        public IActionResult getAllAnimals()
        {
            using var obj = new AnimalProvider.Animal();
            return Ok(obj.getAllAnimals());
        }


        [HttpGet("GetAnimal", Name = "GetAnimal")]
        public IActionResult GetAnimal(int id)
        {
            using var obj = new AnimalProvider.Animal();
            return Ok(obj.getAnimal(id));
        }


        [HttpPost("AddAnimal", Name = "AddAnimal")]
        public IActionResult addAnimal(Entities.Animal animal)
        {
            using var obj = new AnimalProvider.Animal();

            if (obj.addAnimal(animal))
            {
                return Ok("record added succesfully");
            }
            else
            {
                return BadRequest("an error has occured");
            }
        }


        [HttpPost("UpdateAnimal", Name = "UpdateAnimal")]
        public IActionResult updateAnimal(Entities.Animal animal)
        {
            using var Obj = new AnimalProvider.Animal();

            if (Obj.updateAnimal(animal))
            {
                return Ok("record updated successfully");
            }
            else
            {
                return BadRequest("an error has occured");
            }
        }


        [HttpPost("DeleteAnimal", Name = "DeleteAnimal")]
        public IActionResult deleteAnimal(int id)
        {
            using var obj = new AnimalProvider.Animal();

            if (obj.deleteAnimal(id))
            {
                return Ok("record deleted successfully");
            }
            else
            {
                return BadRequest("an error has occured");
            }
        }
    }
}
