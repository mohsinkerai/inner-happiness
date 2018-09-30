using AMS.frontend.web.Helpers.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AMS.frontend.web.Helpers.CustomAttributes
{
    public class AuthenticateAttribute : TypeFilterAttribute
    {
        public AuthenticateAttribute() : base(typeof(AuthenticationActionFilter))
        {
        }
    }
}
