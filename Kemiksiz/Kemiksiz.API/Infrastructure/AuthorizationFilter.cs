using Kemiksiz.Service.Jwt;
using Kemiksiz.Service.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;

namespace Kemiksiz.API.Infrastructure
{
    public class AuthorizationFilter : Attribute, IActionFilter
    {
        private readonly IUserService userService;
        private readonly IJwtService jwtService;
        public AuthorizationFilter(IUserService _userService, IJwtService _jwtService)
        {
            userService = _userService;
            jwtService = _jwtService;
        }
        
        public void OnActionExecuted(ActionExecutedContext context)
        {
            return;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}
