using Animal.WebAPI.Base;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Animal.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [AllowAnonymous] //1
    public class FileUploadController : BaseController
    {
        public static IWebHostEnvironment _webHostEnvironment;
        public FileUploadController(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _webHostEnvironment = webHostEnvironment; //2
        }

        [HttpPost("AddImage", Name = "AddImage")]
        public IActionResult addImage([FromForm]FileUpload fileUpload) //3 addImage,4 fromForm,5 fileUpload
        {
            try
            {
                if (fileUpload.files.Length > 0) //6 fileUpload.files.Length,7 larger than zero
                {
                    string path = _webHostEnvironment.ContentRootPath + "\\uploads\\"; //8 path,9 uploads
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path); //10
                    }
                    using (FileStream fileStream = System.IO.File.Create(path + fileUpload.files.FileName))
                    {
                        fileUpload.files.CopyTo(fileStream); //21
                        fileStream.Flush();
                        fileStream.Close();
                    }
                    return Ok("file uploaded successfully"); //11
                }
                else
                {
                    return BadRequest("empty or no file sent"); //12
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); //13
            }
        }

        [HttpGet("GetImage", Name = "GetImage")]
        public IActionResult getImage(string fileName) //14
        {
            string path = _webHostEnvironment.ContentRootPath + "\\uploads\\"; //15
            string filePath = path + fileName; //16 

            if (System.IO.File.Exists(filePath)) //17
            {
                var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read); //18
                return new FileStreamResult(fileStream, "image/png"); //19
            }
            else
            {
                return BadRequest("file does not exist"); //20
            }
        }
    }
}
