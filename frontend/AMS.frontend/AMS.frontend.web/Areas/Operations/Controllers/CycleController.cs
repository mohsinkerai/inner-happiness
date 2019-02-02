using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AMS.frontend.web.Areas.Operations.Models;
using AMS.frontend.web.Areas.Operations.Models.Cycle;
using AMS.frontend.web.Extensions;
using AMS.frontend.web.Helpers.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AMS.frontend.web.Areas.Operations.Controllers
{
    [Area(AreaNames.Operations)]
    public class CycleController : BaseController
    {

        private readonly ILogger<CycleController> _logger;

        public CycleController(ILogger<CycleController> logger)
        {
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            var allCycles = await new RestfulClient(_logger,HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetAllCycles();
            return View(allCycles);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.PreviousCycle = await new RestfulClient(_logger,HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetCycles();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CycleModel model)
        {
            bool success = await new RestfulClient(_logger,HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).CreateNewCycle(model);

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
                ViewBag.PreviousCycle = await new RestfulClient(_logger,HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetCycles();

                ViewBag.MessageType = MessageTypes.Error;
                ViewBag.Message = Messages.GeneralError;
            }

            return View();
        }

        public async Task<IActionResult> Appoint(MidTermCycle model)
        {
            var success = await new RestfulClient(_logger,HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).appoint(model.CycleId);

            if (success)
            {
                TempData["MessageType"] = MessageTypes.Success;
                TempData["Message"] = Messages.CycleAppoint;

                ViewBag.MessageType = MessageTypes.Success;
                ViewBag.Message = Messages.CycleAppoint;
            }
            else
            {
                ViewBag.MessageType = MessageTypes.Error;
                ViewBag.Message = Messages.GeneralError;
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Close(MidTermCycle model)
        {
            var success = await new RestfulClient(_logger,HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).close(model);

            if (success)
            {
                TempData["MessageType"] = MessageTypes.Success;
                TempData["Message"] = Messages.CycleClosed;

                ViewBag.MessageType = MessageTypes.Success;
                ViewBag.Message = Messages.CycleClosed;
            }
            else
            {
                ViewBag.MessageType = MessageTypes.Error;
                ViewBag.Message = Messages.GeneralError;
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Detail(string id)
        {
            ViewBag.Institution = await new RestfulClient(_logger,HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetPositionInstitution();

            return View(new MidTermCycle {CycleId = id });
        }

        public async Task<JsonResult> GetAllPositionOfInstitution(string uid)
        {
            var list = await new RestfulClient(_logger,HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).findPositionByInstituionId(uid);

            return new JsonResult(list);
        }
    }
}