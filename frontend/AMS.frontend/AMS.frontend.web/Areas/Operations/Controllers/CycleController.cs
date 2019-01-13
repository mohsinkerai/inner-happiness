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
            return View();
        }
    }
}