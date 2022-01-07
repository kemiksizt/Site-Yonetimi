using Kemiksiz.Model;
using Kemiksiz.Model.User;
using Kemiksiz.Service.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kemiksiz.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        public UserController(IUserService _userService)
        {
            userService = _userService;
        }

        [HttpGet]
        public General<UserViewModel> GetUsers()
        {
            return userService.GetUsers();
        }

        [HttpDelete("{id}")]
        public General<UserViewModel> Delete(int id)
        {
            return userService.Delete(id);
        }

        [HttpPut]
        public General<UserViewModel> Update(UserViewModel updatedUser)
        {
            return userService.Update(updatedUser);
        }

        [HttpPost]
        public General<InsertUserViewModel> Insert(InsertUserViewModel newUser)
        {
            return userService.Insert(newUser);
        }
    }
}
