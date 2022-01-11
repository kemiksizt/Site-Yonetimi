using Kemiksiz.Model.User;
using Kemiksiz.Service.Jwt;
using Kemiksiz.Service.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kemiksiz.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IJwtService jwtService;

        public AuthController(IUserService _userService, IJwtService _jwtService)
        {
            userService = _userService;
            jwtService = _jwtService;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginViewModel loginUser)
        {
            var user = userService.Login(loginUser);

            Response.Cookies.Append("jwt", user.Token, new CookieOptions
            {
                HttpOnly = true
            });

            return Ok(new
            {
                message = "success"
            });
        }

    }
}
