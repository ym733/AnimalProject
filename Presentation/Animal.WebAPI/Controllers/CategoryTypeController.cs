using Animal.WebAPI.Base;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace Animal.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class CategoryTypeController : BaseController
    {
        public static IWebHostEnvironment _webHostEnvironment;
        public CategoryTypeController(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor): base(httpContextAccessor)
        {
            _webHostEnvironment = webHostEnvironment;
        }


        [HttpGet("GetAllCategories", Name = "GetAllCategories")]
        public IActionResult getAllCategories()
        {
            using var obj = new AnimalProvider.CategoryType();
            var data = obj.getAllCategories();
            return Ok(data);
        }


        [HttpGet("GetCategoryType", Name = "GetCategoryType")]
        public IActionResult getCategoryType(int id)
        {
            using var obj = new AnimalProvider.CategoryType();
            return Ok(obj.getCategoryType(id));
        }


        [HttpPost("AddCategoryType", Name = "AddCategoryType")]
        public IActionResult addCategoryType([FromForm]viewModel.CategoryType CategoryType)
        {
            using var obj = new AnimalProvider.CategoryType();

            string pathToDirectory = _webHostEnvironment.ContentRootPath + "\\uploads\\";
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
                return Ok("record added succesfully");
            }
            else
            {
                return BadRequest("an error has occured");
            }
        }


        [HttpPost("UpdateCategoryType", Name = "UpdateCategoryType")]
        public IActionResult updateCategoryType([FromForm]viewModel.CategoryType CategoryType)
        {
            using var obj = new AnimalProvider.CategoryType();

            //to delete the current categorytype image currently stored
            Entities.CategoryType prevInstance = obj.getCategoryType(CategoryType.Id);
            System.IO.File.Delete(prevInstance.ImagePath);

            string pathToDirectory = _webHostEnvironment.ContentRootPath + "\\uploads\\";
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
                return Ok("record updated successfully");
            }
            else
            {
                return BadRequest("an error ahs occured");
            }
        }


        [HttpPost("DeleteCategoryType", Name = "DeleteCategoryType")]
        public IActionResult deleteCategoryType(int id)
        {
            using var obj = new AnimalProvider.CategoryType();

            if (obj.deleteCategoryType(id))
            {
                return Ok("record deleted successfully");
            }
            else
            {
                return BadRequest("an error has occured");
            }
        }

        [HttpGet("GetCategoryTypeImage", Name = "GetCategoryTypeImage")]
        public IActionResult getCategoryTypeImage(int id)
        {
            using var obj = new AnimalProvider.CategoryType();
            Entities.CategoryType categoryType =  obj.getCategoryType(id);

            if( categoryType.ImagePath == null)
            {
                return BadRequest("image path is null");
            }

            if (System.IO.File.Exists(categoryType.ImagePath))
            {
                var fileStream = new FileStream(categoryType.ImagePath, FileMode.Open, FileAccess.Read);
                return new FileStreamResult(fileStream, "image/png");
            }
            else
            { 
                return BadRequest("file does not exist"); 
            }
        }
    }
}
