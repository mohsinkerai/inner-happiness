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
using System.Runtime.Serialization;
using System.Threading.Tasks;
using AMS.frontend.web.Extensions;
using Microsoft.Extensions.Logging;
using AMS.frontend.web.Areas.Operations.Models.Nominations;
using Microsoft.AspNetCore.Mvc.Rendering;
using AMS.frontend.web.Areas.Operations.Models.Reports;

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

            var sessionInstituionList = new List<InstitutionModel>();
            HttpContext.Session.Set("InstitutionList", sessionInstituionList);
            return View();
        }

        public IActionResult ThreePlusOne(string insitutionId, int cycleId)
        {
            using (var client = new WebClient())
            {
                client.Credentials = new NetworkCredential("jasperadmin", "jasperadmin");

                var stream = new MemoryStream(client.DownloadData(
                    $"http://localhost:8081/jasperserver/rest_v2/reports/reports/Appointment/Three_Plus_One.pdf?institutionid={insitutionId}&cycleid={cycleId}&showremarks=false&pagenumber=1&membernominations=1"));

                return File(stream, "application/pdf", $"Three-plus-one[{DateTime.Now.ToString()}].pdf");
            }
        }

        public async Task<JsonResult> GetNationalInstitutions(string level)
        {
            //level would serve as category
            var list = await new RestfulClient(_logger,
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetNationalInstitutions();
            
            return new JsonResult(list);
        }


        public async Task<List<SelectListItem>> GetRegionalInstitutions()
        {
            var list = await new RestfulClient(_logger,
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetRegionalInstitutions();
            
            return list;
        }

        public async Task<List<SelectListItem>> GetLocalInstitutions(string regionId)
        {
            var list = await new RestfulClient(_logger,
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetAllLocal(regionId);
            
            return list;
        }

        [HttpPost]
        public async Task<IActionResult> Index(ReportModel model)
        {
            if (ModelState.IsValid)
            {
                var institutions = string.Empty;
                var includeMemberNominations = 0;
                var pageNumber = model.PageNumber == null ? 0 : model.PageNumber.Value;

                switch (model.Layout)
                {
                    case "ThreePlusOne":
                        {
                            institutions = GetInstitutions(institutions);
                            includeMemberNominations = 1;
                            break;
                        }
                    case "Running":
                        {
                            institutions = GetInstitutions(institutions);
                            includeMemberNominations = 0;
                            break;
                        }
                    default:
                        {
                            TempData["MessageType"] = MessageTypes.Warn;
                            TempData["Message"] = "Report is currently under development.";

                            ViewBag.MessageType = MessageTypes.Warn;
                            ViewBag.Message = "Report is currently under development.";

                            return RedirectToAction(ActionNames.Index);
                        }
                }

                //var list = model.Level == "National" ? await GetNationalInstitutions() :
                //    model.Level == "Regional" ? await GetRegionalInstitutions() :
                //    model.Level == "Local" ? await GetLocalInstitutions(model.Institution.Split("-")[0]) : null;

                ////var sessionInstituionList = HttpContext.Session.Get<List<InstitutionModel>>("InstitutionList") ?? new List<InstitutionModel>();
                //var institutions = (model.IncludeParent && model.Level == "Local") ? $"{model.Institution.Split("-")[0]}," : string.Empty;

                //foreach (var item in list)
                //{
                //    //item.Id => this is sessionId.
                //    //item.Name => this is intitution name with institution id like (2-AKEPB) so we have to split value.
                //    //var institution = item.Name.Split("-")[0];
                //    var institution = item.Value.Split("-")[0];
                //    institutions += $"{institution},";
                //}

                if (!string.IsNullOrWhiteSpace(institutions))
                {
                    institutions = institutions.Substring(0, institutions.Length - 1);
                }

                using (var client = new CustomWebClient())
                {
                    client.Credentials = new NetworkCredential("jasperadmin", "jasperadmin");
                    client.Timeout = 600 * 60 * 1000;

                    var url =
                        $"http://localhost:8081/jasperserver/rest_v2/reports/reports/Appointment/Three_Plus_One.pdf?institutionid={institutions}&cycleid=19&showremarks={model.Remarks}&pagenumber={pageNumber}&membernominations={includeMemberNominations}";
                    var stream = new MemoryStream(client.DownloadData(url));

                    return File(stream, "application/pdf", $"{model.Layout}[{DateTime.Now.ToString()}].pdf");
                }
            }

            return RedirectToAction(ActionNames.Index);

            string GetInstitutions(string institutions)
            {
                if (model.Level == "National")
                {
                    if (model.Category == "CAB")
                    {
                        institutions = "1,";
                    }
                    else if (model.Category == "GRB")
                    {
                        institutions = "11,";
                    }
                    else if (model.Category == "ITREB")
                    {
                        institutions = "3,";
                    }
                    else if (model.Category == "Council")
                    {
                        institutions = $"{model.Institution},";
                    }
                }

                return institutions;
            }
        }
    }

    class CustomWebClient : WebClient
    {
        public int Timeout { get; set; }

        protected override WebRequest GetWebRequest(Uri uri)
        {
            WebRequest lWebRequest = base.GetWebRequest(uri);
            lWebRequest.Timeout = Timeout;
            ((HttpWebRequest)lWebRequest).ReadWriteTimeout = Timeout;
            return lWebRequest;
        }
    }
}