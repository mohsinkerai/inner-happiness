using AMS.frontend.web.Helpers.Filters;
using Microsoft.AspNetCore.Mvc;

namespace AMS.frontend.web.Helpers.CustomAttributes
{
    public class AuthenticateAttribute : TypeFilterAttribute
    {
        #region Public Constructors

        public AuthenticateAttribute() : base(typeof(AuthenticationActionFilter))
        {
        }

        #endregion Public Constructors
    }
}