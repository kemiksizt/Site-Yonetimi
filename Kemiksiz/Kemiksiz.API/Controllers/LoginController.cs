using Kemiksiz.Model;
using Kemiksiz.Model.User;
using Kemiksiz.Service.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kemiksiz.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserService userService;

        public LoginController(IUserService _userService)
        {
            userService = _userService;
        }

        [HttpPost]
        public General<UserViewModel> Insert(LoginViewModel loginUser)
        {
            return userService.Login(loginUser);
        }


    }
}
