using System;
using System.Threading.Tasks;
using AMS.frontend.web.Areas.Operations.Models;
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
            ViewBag.MessageType = TempData["MessageType"];
            ViewBag.Message = TempData["Message"];

            var model = new LoginModel();
            if (Request.Cookies.TryGetValue(CookieNames.RememberMe, out var rememberMe))
            {
                model.RememberMe = true;
                if (Request.Cookies.TryGetValue(CookieNames.Company, out var company)) model.Company = company;

                if (Request.Cookies.TryGetValue(CookieNames.CustomerName, out var customerName))
                    model.Username = customerName;
            }

            HttpContext.Session.Set("AuthenticationResponse", new AuthenticationResponse());

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await new RestfulClient(string.Empty).GetToken(model);

                if (response != null)
                {
                    HttpContext.Session.Set("AuthenticationResponse", response);

                    if (model.RememberMe)
                    {
                        var cookieOptions = new CookieOptions {HttpOnly = false};
                        Response.Cookies.Append(CookieNames.RememberMe, Convert.ToString(model.RememberMe),
                            cookieOptions);
                        Response.Cookies.Append(CookieNames.Company, model.Company, cookieOptions);
                        Response.Cookies.Append(CookieNames.CustomerName, model.Username, cookieOptions);
                    }
                    else
                    {
                        Response.Cookies.Delete(CookieNames.RememberMe);
                        Response.Cookies.Delete(CookieNames.Company);
                        Response.Cookies.Delete(CookieNames.CustomerName);
                    }

                    return RedirectToAction(ActionNames.Index, ControllerNames.Persons, new { area = AreaNames.Operations});
                }

                ViewBag.MessageType = MessageTypes.Error;
                ViewBag.Message = Messages.GeneralError;
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