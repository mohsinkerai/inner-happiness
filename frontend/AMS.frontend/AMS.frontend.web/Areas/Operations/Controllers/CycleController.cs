using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AMS.frontend.web.Areas.Operations.Models;
using AMS.frontend.web.Areas.Operations.Models.Cycle;
using AMS.frontend.web.Extensions;
using AMS.frontend.web.Helpers.Constants;
using Microsoft.AspNetCore.Mvc;

namespace AMS.frontend.web.Areas.Operations.Controllers
{
    [Area(AreaNames.Operations)]
    public class CycleController : BaseController
    {
        public async Task<IActionResult> Index()
        {
            var allCycles = await new RestfulClient(HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetAllCycles();
            return View(allCycles);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.PreviousCycle = await new RestfulClient(HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetCycles();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CycleModel model)
        {
            bool success = await new RestfulClient(HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).CreateNewCycle(model);

            if (success)
            {
                TempData["MessageType"] = MessageTypes.Success;
                TempData["Message"] = Messages.CycleCreated;

                ViewBag.MessageType = MessageTypes.Success;
                ViewBag.Message = Messages.CycleCreated;

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.PreviousCycle = await new RestfulClient(HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetCycles();

                ViewBag.MessageType = MessageTypes.Error;
                ViewBag.Message = Messages.GeneralError;
            }

            return View();
        }

        public async Task<IActionResult> Detail()
        {
            return View();
        }
    }
}