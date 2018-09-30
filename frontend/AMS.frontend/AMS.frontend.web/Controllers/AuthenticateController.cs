using System;
using System.Threading.Tasks;
using AMS.frontend.web.Extensions;
using AMS.frontend.web.Helpers.Constants;
using AMS.frontend.web.Models.Authenticate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AMS.frontend.web.Controllers
{
    public class AuthenticateController : Controller
    {
        #region Public Methods

        public IActionResult Index()
        {
            var model = new LoginModel();
            if (Request.Cookies.TryGetValue(CookieNames.RememberMe, out var rememberMe))
            {
                model.RememberMe = true;
                if (Request.Cookies.TryGetValue(CookieNames.Company, out var company)) model.Company = company;
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Index(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.RememberMe)
                {
                    var cookieOptions = new CookieOptions { HttpOnly = false };
                    Response.Cookies.Append(CookieNames.RememberMe, Convert.ToString(model.RememberMe), cookieOptions);
                    Response.Cookies.Append(CookieNames.Company, model.Company, cookieOptions);
                }
                else
                {
                    Response.Cookies.Delete(CookieNames.RememberMe);
                    Response.Cookies.Delete(CookieNames.Company);
                }

                //todo by aa - call login api
                HttpContext.Session.Set("AuthenticationToken", "asdghjaskdhjaskd");

                return RedirectToAction(ActionNames.Index, ControllerNames.Home);
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Reset(LoginModel model)
        {
            //todo by aa - call forget password api

            return Json("Success");
        }

        #endregion Public Methods
    }
}