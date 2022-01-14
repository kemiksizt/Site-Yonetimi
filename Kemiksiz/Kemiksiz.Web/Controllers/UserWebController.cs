using Kemiksiz.Model.User;
using Kemiksiz.Service.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kemiksiz.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserWebController : ControllerBase
    {
        private readonly IUserService userService;
        public UserWebController(IUserService _userService)
        {
            userService = _userService;
        }

        [HttpGet("Users")]
        public IActionResult GetUsers()
        {
            var userList = userService.GetUsers();

            return Ok(userList);
        }


        [HttpDelete("DeleteUser")]
        public IActionResult DeleteUser(int id)
        {
            userService.Delete(id);

            return Ok();
        }


        [HttpPut("UpdateUser")]
        public IActionResult UpdateUser(UserViewModel updatedUser)
        {
            var user = userService.Update(updatedUser);

            return Ok(user);
        }

        [HttpPost("InsertUser")]
        public IActionResult InsertUser(InsertUserViewModel newUser)
        {
            var user = userService.Insert(newUser);

            return Ok(user);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = userService.GetById(id);

            return Ok(user);
        }
    }
}
