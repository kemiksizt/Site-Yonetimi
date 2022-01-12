using Kemiksiz.Model;
using Kemiksiz.Model.User;
using Kemiksiz.Service.Jwt;
using Kemiksiz.Service.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Kemiksiz.Web.Controllers
{
    [Route("")]
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

        [HttpPost("register")]
        public IActionResult Register(LoginViewModel registerUser)
        {
            var user = userService.GetPassword(registerUser);

            if (user.IsSuccess)
            {
                return Ok("success");
            }

            return Unauthorized();


            
        }

        [HttpPost("login")]
        public IActionResult Login(LoginViewModel loginUser)
        {
             var user = userService.GetByName(loginUser);
            if(user.IsSuccess)
            { 
                user.Entity.Password = BCrypt.Net.BCrypt.HashPassword(loginUser.Password);

                if (user == null) return BadRequest(new { message = "Invalid Credentials" });

                if (!BCrypt.Net.BCrypt.Verify(loginUser.Password, user.Entity.Password))
                {
                    return BadRequest(new { message = "Invalid Credentials" });
                }

                var jwt = jwtService.Generate(user.Entity.Id);

                Response.Cookies.Append("jwt", jwt, new CookieOptions
                {
                    HttpOnly = true
                });

                return Ok(new
                {
                    message = "success"
                });
            }

            else
            {
                return BadRequest(new { message = "Invalid Credentials" });
            }

        }

        [HttpGet("user")]
        public IActionResult User()
        {

            try
            {
                var jwt = Request.Cookies["jwt"];

                var token = jwtService.Verify(jwt);

                int userId = int.Parse(token.Issuer);

                var user = userService.GetById(userId);

                user.Entity.Password = BCrypt.Net.BCrypt.HashPassword(user.Entity.Password);

                return Ok(user);
            }
            catch (Exception e)
            {

                return Unauthorized();
            }


        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");

            return Ok(new
            {
                message = "success"
            });
        }

    }
}
