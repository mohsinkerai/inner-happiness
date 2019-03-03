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
using Microsoft.Extensions.Logging;
using AMS.frontend.web.Areas.Operations.Models.Nominations;
using AMS.frontend.web.Areas.Operations.Models.ThreePlusOneReport;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AMS.frontend.web.Areas.Operations.Controllers
{
    [Area(AreaNames.Operations)]
    public class ReportsController : BaseController
    {
        private readonly ILogger<ReportsController> _logger;

        public ReportsController(ILogger<ReportsController> logger)
        {
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.MessageType = TempData["MessageType"];
            ViewBag.Message = TempData["Message"];

            ViewBag.Cycle =
                await new RestfulClient(_logger,
                    HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetCycles();

            return View();
        }

        public IActionResult ThreePlusOne(string insitutionId, int cycleId)
        {
            using (var client = new WebClient())
            {
                client.Credentials = new NetworkCredential("jasperadmin", "jasperadmin");

                var stream = new MemoryStream(client.DownloadData(
                    $"http://localhost:8081/jasperserver/rest_v2/reports/reports/Appointment/Three_Plus_One.pdf?institutionid={insitutionId}&cycleid={cycleId}&showremarks=false"));

                return File(stream, "application/pdf", $"Three-plus-one[{DateTime.Now.ToString()}].pdf");
            }
        }

        public IActionResult Nominations()
        {
            var sessionInstituionList = new List<InstitutionModel>();
            HttpContext.Session.Set("InstitutionList", sessionInstituionList);
            return View();
        }

        public async Task<List<SelectListItem>> GetNationalInstitutions()
        {
            var list = await new RestfulClient(_logger,
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetNationalInstitutions();

            foreach (var item in list)
            {
                item.Value = item.Value + "-" + item.Text;
            }

            return list;
        }

        public async Task<List<SelectListItem>> GetRegionalInstitutions()
        {
            var list = await new RestfulClient(_logger,
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetRegionalInstitutions();

            foreach (var item in list)
            {
                item.Value = item.Value + "-" + item.Text;
            }

            return list;
        }

        public async Task<List<SelectListItem>> GetLocalInstitutions(string regionId)
        {
            var list = await new RestfulClient(_logger,
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetAllLocal(regionId);

            foreach (var item in list)
            {
                item.Value = item.Value + "-" + item.Text;
            }

            return list;
        }

        [HttpPost]
        public async Task<IActionResult> GenerateReport(ThreePlusOneReportModel model)
        {
            if (ModelState.IsValid)
            {
                var list = model.Level == "National" ? await GetNationalInstitutions() :
                    model.Level == "Regional" ? await GetRegionalInstitutions() :
                    model.Level == "Local" ? await GetLocalInstitutions(model.RegionalInstitution.Split("-")[0]) : null;

                //var sessionInstituionList = HttpContext.Session.Get<List<InstitutionModel>>("InstitutionList") ?? new List<InstitutionModel>();
                var institutions = model.IncludeParent ? $"{model.RegionalInstitution.Split("-")[0]}," : string.Empty;

                foreach (var item in list)
                {
                    //item.Id => this is sessionId.
                    //item.Name => this is intitution name with institution id like (2-AKEPB) so we have to split value.
                    //var institution = item.Name.Split("-")[0];
                    var institution = item.Value.Split("-")[0];
                    institutions += $"{institution},";
                }

                institutions = institutions.Substring(0, institutions.Length - 1);

                using (var client = new WebClient())
                {
                    client.Credentials = new NetworkCredential("jasperadmin", "jasperadmin");

                    var stream = new MemoryStream(client.DownloadData(
                        $"http://localhost:8081/jasperserver/rest_v2/reports/reports/Appointment/Three_Plus_One.pdf?institutionid={institutions}&cycleid=19&showremarks={model.Remarks}"));

                    return File(stream, "application/pdf", $"Three-plus-one[{DateTime.Now.ToString()}].pdf");
                }
            }

            return null;
        }
    }
}