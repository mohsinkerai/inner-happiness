using AMS.frontend.web.Areas.Operations.Models;
using AMS.frontend.web.Areas.Operations.Models.Nominations;
using AMS.frontend.web.Areas.Operations.Models.Reports;
using AMS.frontend.web.Extensions;
using AMS.frontend.web.Helpers.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace AMS.frontend.web.Areas.Operations.Controllers
{
    [Area(AreaNames.Operations)]
    public class ReportsController : BaseController
    {
        #region Private Fields

        private readonly ILogger<ReportsController> _logger;

        #endregion Private Fields

        #region Public Constructors

        public ReportsController(ILogger<ReportsController> logger)
        {
            _logger = logger;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<List<SelectListItem>> GetChildInstitutions(string regionId, bool sortByCodeNc = false)
        {
            var list = await new RestfulClient(_logger,
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetAllLocal(regionId, sortByCodeNc);

            return list;
        }

        public async Task<List<SelectListItem>> GetLocalInstitutions(string level)
        {
            //level would serve as category
            var list = await new RestfulClient(_logger,
                HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetLocalInstitutions();

            return list;
        }

        public async Task<List<SelectListItem>> GetNationalInstitutions(string level)
        {
            //level would serve as category
            var list = await new RestfulClient(_logger,
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetNationalInstitutions();

            return list;
        }

        public async Task<List<SelectListItem>> GetRegionalInstitutions(string level)
        {
            //level would serve as category
            var list = await new RestfulClient(_logger,
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetRegionalInstitutions();

            return list;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.MessageType = TempData["MessageType"];
            ViewBag.Message = TempData["Message"];

            var sessionInstituionList = new List<InstitutionModel>();
            HttpContext.Session.Set("InstitutionList", sessionInstituionList);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(ReportModel model)
        {
            if (ModelState.IsValid)
            {
                var institutions = string.Empty;
                var pageNumber = model.PageNumber == null ? 0 : model.PageNumber.Value;

                int includeMemberNominations;
                switch (model.Layout)
                {
                    case "ThreePlusOne":
                        {
                            institutions = await GetInstitutions(institutions);
                            includeMemberNominations = 1;
                            break;
                        }
                    case "Running":
                        {
                            institutions = await GetInstitutions(institutions);
                            includeMemberNominations = 0;
                            break;
                        }
                    case "Shortlist":
                        {
                            institutions = await GetInstitutions(institutions);

                            if (model.Level == "National")
                            {
                                using (var client = new CustomWebClient())
                                {
                                    client.Credentials = new NetworkCredential("jasperadmin", "jasperadmin");
                                    client.Timeout = 600 * 60 * 1000;

                                    var url =
                                        $"http://localhost:8081/jasperserver/rest_v2/reports/reports/Appointment/National_Shortlist.{GetFileExtension(model.FileType)}?institutionid={institutions}&cycleid=19&pagenumber={pageNumber}";
                                    _logger.LogInformation($"Generated URL for Report is : {url}");
                                    var stream = new MemoryStream(client.DownloadData(url));

                                    return File(stream, GetContentType(model.FileType), $"{model.Layout}[{DateTime.Now.ToString()}].{GetFileExtension(model.FileType)}");
                                }
                            }

                            return NotSupportedReport();
                        }
                    case "Summary":
                        {
                            using (var client = new CustomWebClient())
                            {
                                client.Credentials = new NetworkCredential("jasperadmin", "jasperadmin");
                                client.Timeout = 600 * 60 * 1000;

                                var url =
                                    $"http://localhost:8081/jasperserver/rest_v2/reports/reports/Appointment/National_Council_Summary.{GetFileExtension(model.FileType)}";
                                _logger.LogInformation($"Generated URL for Report is : {url}");
                                var stream = new MemoryStream(client.DownloadData(url));

                                return File(stream, GetContentType(model.FileType), $"{model.Layout}[{DateTime.Now.ToString()}].{GetFileExtension(model.FileType)}");
                            }
                        }
                    default:
                        {
                            return NotSupportedReport();
                        }
                }

                using (var client = new CustomWebClient())
                {
                    client.Credentials = new NetworkCredential("jasperadmin", "jasperadmin");
                    client.Timeout = 600 * 60 * 1000;

                    var url =
                        $"http://localhost:8081/jasperserver/rest_v2/reports/reports/Appointment/Three_Plus_One.{GetFileExtension(model.FileType)}?institutionid={institutions}&cycleid=19&showremarks={model.Remarks}&pagenumber={pageNumber}&membernominations={includeMemberNominations}&showrecommendation={model.Recommendation}&officebearersonly={model.OfficeBearersOnly}";
                    _logger.LogInformation($"Generated URL for Report is : {url}");
                    var stream = new MemoryStream(client.DownloadData(url));

                    return File(stream, GetContentType(model.FileType), $"{model.Layout}[{DateTime.Now.ToString()}].{GetFileExtension(model.FileType)}");
                }
            }

            return RedirectToAction(ActionNames.Index);

            async Task<string> GetInstitutions(string institutions)
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
                        if (string.IsNullOrWhiteSpace(model.Institution))
                        {
                            institutions = "8,16,15,2,6,7,";
                        }
                        else
                        {
                            institutions = $"{model.Institution},";
                        }
                    }
                }
                else if (model.Level == "Regional")
                {
                    if (!string.IsNullOrWhiteSpace(model.Institution))
                    {
                        if (!model.LocalsOnly)
                        {
                            institutions = $"{model.Institution},";
                        }

                        if (model.IncludeLocals)
                        {
                            var locals = await GetChildInstitutions(model.Institution);
                            foreach (var local in locals)
                            {
                                institutions += $"{local.Value},";
                            }
                        }
                    }
                    else
                    {
                        if (model.Category == "Council")
                        {
                            var regions = await GetChildInstitutions("8", true);
                            foreach (var region in regions)
                            {
                                institutions += $"{region.Value},";
                            }
                        }
                        else if (model.Category == "ITREB")
                        {
                            var regions = await GetChildInstitutions("3", true);
                            foreach (var region in regions)
                            {
                                institutions += $"{region.Value},";
                            }
                        }
                        else if (model.Category == "CAB")
                        {
                            var regions = await GetChildInstitutions("1", true);
                            foreach (var region in regions)
                            {
                                institutions += $"{region.Value},";
                            }
                        }
                    }
                }
                else if (model.Level == "Local")
                {
                    if (!string.IsNullOrWhiteSpace(model.Institution))
                    {
                        institutions = $"{model.Institution},";
                    }
                    else
                    {
                        if (model.Category == "Council")
                        {
                            var regions = await GetChildInstitutions("8", true);
                            foreach (var region in regions)
                            {
                                var locals = await GetChildInstitutions(region.Value, true);
                                foreach (var local in locals)
                                {
                                    institutions += $"{local.Value},";
                                }
                            }
                        }
                        else if (model.Category == "ITREB")
                        {
                            var regions = await GetChildInstitutions("3", true);
                            foreach (var region in regions)
                            {
                                var locals = await GetChildInstitutions(region.Value, true);
                                foreach (var local in locals)
                                {
                                    institutions += $"{local.Value},";
                                }
                            }
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(institutions))
                {
                    institutions = institutions.Substring(0, institutions.Length - 1);
                }

                return institutions;
            }

            IActionResult NotSupportedReport()
            {
                TempData["MessageType"] = MessageTypes.Warn;
                TempData["Message"] = "Report is currently under development.";

                ViewBag.MessageType = MessageTypes.Warn;
                ViewBag.Message = "Report is currently under development.";

                return RedirectToAction(ActionNames.Index);
            }

            string GetFileExtension(string fileType)
            {
                switch (fileType)
                {
                    case "PDF":
                        return "pdf";

                    case "Excel":
                        return "xlsx";

                    case "Word":
                        return "docx";

                    default:
                        return "pdf";
                }
            }

            string GetContentType(string fileType)
            {
                switch (fileType)
                {
                    case "PDF":
                        return "application/pdf";

                    case "Excel":
                        return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                    case "Word":
                        return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";

                    default:
                        return "application/pdf";
                }
            }
        }

        public IActionResult ThreePlusOne(string insitutionId, int cycleId)
        {
            using (var client = new WebClient())
            {
                client.Credentials = new NetworkCredential("jasperadmin", "jasperadmin");

                var stream = new MemoryStream(client.DownloadData(
                    $"http://localhost:8081/jasperserver/rest_v2/reports/reports/Appointment/Three_Plus_One.pdf?institutionid={insitutionId}&cycleid={cycleId}&showremarks=false&pagenumber=1&membernominations=1&showrecommendation=true&officebearersonly=false"));

                return File(stream, "application/pdf", $"Three-plus-one[{DateTime.Now.ToString()}].pdf");
            }
        }

        #endregion Public Methods
    }

    internal class CustomWebClient : WebClient
    {
        #region Public Properties

        public int Timeout { get; set; }

        #endregion Public Properties

        #region Protected Methods

        protected override WebRequest GetWebRequest(Uri uri)
        {
            WebRequest lWebRequest = base.GetWebRequest(uri);
            lWebRequest.Timeout = Timeout;
            ((HttpWebRequest)lWebRequest).ReadWriteTimeout = Timeout;
            return lWebRequest;
        }

        #endregion Protected Methods
    }
}