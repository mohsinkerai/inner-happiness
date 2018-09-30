using AMS.frontend.web.Areas.Operations.Models;
using AMS.frontend.web.Extensions;
using AMS.frontend.web.Helpers.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace AMS.frontend.web.Helpers.Filters
{
    public class AuthenticationActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            AuthenticationResponse authReponse = context.HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse");

            if (authReponse != null && !string.IsNullOrWhiteSpace(authReponse.Token) && authReponse.Expiry > DateTime.Now)
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
