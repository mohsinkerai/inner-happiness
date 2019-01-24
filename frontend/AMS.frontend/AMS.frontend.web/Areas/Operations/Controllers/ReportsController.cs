using AMS.frontend.web.Areas.Operations.Models;
using AMS.frontend.web.Areas.Operations.Models.Persons;
using AMS.frontend.web.Helpers.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading.Tasks;
using AMS.frontend.web.Extensions;

namespace AMS.frontend.web.Areas.Operations.Controllers
{
    [Area(AreaNames.Operations)]
    public class ReportsController : BaseController
    {
        public async Task<IActionResult> Index()
        {
            ViewBag.MessageType = TempData["MessageType"];
            ViewBag.Message = TempData["Message"];

            ViewBag.Cycle =
                await new RestfulClient(
                    HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetCycles();

            return View();
        }
        
        public IActionResult ThreePlusOne(int insitutionId, int cycleId)
        {
            using(var client = new WebClient()) {
                client.Credentials = new NetworkCredential("japseradmin", "japseradmin");
                using (var stream = new MemoryStream(client.DownloadData(
                    $"http://localhost:8081/jasperserver/rest_v2/reports/reports/Appointment/Three_Plus_One.pdf?institutionid={insitutionId}&cycleid={cycleId}"))
                )
                {
                    return File(stream, "application/pdf", $"Three-plus-one[{DateTime.Now.ToString()}]");
                }
            }
        }
    }
}