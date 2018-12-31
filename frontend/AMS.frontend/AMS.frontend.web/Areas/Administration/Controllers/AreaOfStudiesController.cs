﻿using AMS.frontend.web.Areas.Administration.Models.AreaOfStudies;
using AMS.frontend.web.Areas.Operations.Models;
using AMS.frontend.web.Extensions;
using AMS.frontend.web.Helpers.Constants;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AMS.frontend.web.Areas.Administration.Controllers
{
    [Area(AreaNames.Administration)]
    public class AreaOfStudiesController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(AreaOfStudyModel model)
        {
            if (ModelState.IsValid)
            {
                var success = await new RestfulClient(HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).AddNewAreaOfStudies(model);
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