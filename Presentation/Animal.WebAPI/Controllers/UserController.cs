using Animal.WebAPI.Base;
using viewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Animal.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles = "admin,superAdmin")]
    public class UserController : BaseController
    {
        public UserController(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            // Additional child controller initialization code, if needed
        }


        [HttpGet("GetCurrentUser", Name = "GetCurrentUser")]
        public IActionResult GetCurrentUser()
        {
            return Ok(CurrentUser);
        }


        [HttpGet("GetAllUsers", Name = "GetAllUsers")]
        public IActionResult GetAllUsers() 
        {
            using var obj = new AnimalProvider.Users();
            return Ok(obj.getAllUsers());

        }


        [HttpGet("GetUser", Name = "GetUser")]
        public IActionResult GetUser(int id) 
        {
            using var obj = new AnimalProvider.Users();
            return Ok(obj.getUser(id));
        }


        [HttpPost("AddUser", Name = "AddUser")]
        public IActionResult AddUser(ViewModel.User user)
        {
            using var obj = new AnimalProvider.Users();
            if (obj.addUser(user))
            {
                return Ok("record added succesfully");
            }
            else
            {
                return BadRequest("an error has occured");
            }
        }


        [HttpPost("DeleteUser", Name ="DeleteUser")]
        public IActionResult DeleteUser(int id)
        {
            using var obj = new AnimalProvider.Users();
            if (obj.deleteUser(id))
            {
                return Ok("record deleted succesfully");
            }
            else
            {
                return BadRequest("an error has occured");
            }
        }


        [HttpPost("UpdateUser", Name="UpdateUser")]
        public IActionResult UpdateUser(ViewModel.User user)
        {
            using var obj = new AnimalProvider.Users();
            if (obj.updateUser(user))
            {
                return Ok("record updated successfully");
            }
            else
            {
                return BadRequest("an error has occured");
            }
        }
    }
}
