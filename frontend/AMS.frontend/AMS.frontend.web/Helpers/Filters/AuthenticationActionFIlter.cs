using AMS.frontend.web.Extensions;
using AMS.frontend.web.Helpers.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AMS.frontend.web.Helpers.Filters
{
    public class AuthenticationActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var token = context.HttpContext.Session.Get<string>("AuthenticationToken");

            if (!string.IsNullOrWhiteSpace(token))
            {
                ActionExecutedContext resultContext = await next();
            }
            else
            {
                context.Result = new RedirectToActionResult(ActionNames.Index, ControllerNames.Authenticate, new { area = AreaNames.Blank });
            }
        }
    }
}
