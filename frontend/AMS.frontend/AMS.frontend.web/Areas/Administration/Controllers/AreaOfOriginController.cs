using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AMS.frontend.web.Areas.Administration.Models.AreaOfOrigin;
using AMS.frontend.web.Areas.Operations.Models;
using AMS.frontend.web.Extensions;
using AMS.frontend.web.Helpers.Constants;
using Microsoft.AspNetCore.Mvc;

namespace AMS.frontend.web.Areas.Administration.Controllers
{
    [Area(AreaNames.Administration)]
    public class AreaOfOriginController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(AreaOfOriginModel model)
        {
            if (ModelState.IsValid)
            {
                var success = await new RestfulClient(HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).AddNewAreaOfOrign(model);
                if (success)
                {
                    TempData["MessageType"] = MessageTypes.Success;
                    TempData["Message"] = Messages.successfullyAdded;

                    ViewBag.MessageType = MessageTypes.Success;
                    ViewBag.Message = Messages.successfullyAdded;
                }
                else
                {
                    ViewBag.MessageType = MessageTypes.Error;
                    ViewBag.Message = Messages.GeneralError;
                }
            }
            return View();
        }
    }
}