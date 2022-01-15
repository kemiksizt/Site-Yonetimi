using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;
using System;

namespace Kemiksiz.API.Infrastructure
{
    public class AuthorizationFilter : Attribute, IActionFilter
    {
        private readonly IDistributedCache distributedCache;
        public AuthorizationFilter(IDistributedCache _distributedCache)
        {
            distributedCache = _distributedCache;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            throw new NotImplementedException();
        }
    }
}
