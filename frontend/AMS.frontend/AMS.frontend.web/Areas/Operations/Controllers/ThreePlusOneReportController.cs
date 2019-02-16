using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AMS.frontend.web.Areas.Operations.Models;
using AMS.frontend.web.Areas.Operations.Models.Nominations;
using AMS.frontend.web.Areas.Operations.Models.ThreePlusOneReport;
using AMS.frontend.web.Extensions;
using AMS.frontend.web.Helpers.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace AMS.frontend.web.Areas.Operations.Controllers
{
    [Area(AreaNames.Operations)]
    public class ThreePlusOneReportController : BaseController
    {

        private readonly ILogger<ThreePlusOneReportController> _logger;

        public ThreePlusOneReportController(ILogger<ThreePlusOneReportController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            var sessionInstituionList = new List<InstitutionModel>();
            HttpContext.Session.Set("InstitutionList", sessionInstituionList);
            return View();
        }

        /*[HttpPost]
        public IActionResult InstitutionListAdd(string id, string institution)
        {
            var sessionEducationList = AddInstitutionToSession(id, institution);

            return PartialView("_InstitutionTablePartial", sessionEducationList);
        }*/

        /*private List<InstitutionModel> AddInstitutionToSession(string id, string institution)
        {
            var sessionInstituionList = HttpContext.Session.Get<List<InstitutionModel>>("InstitutionList") ??
                                       new List<InstitutionModel>();

            if (string.IsNullOrWhiteSpace(id))
                id = Guid.NewGuid().ToString();
            else
                sessionInstituionList.Remove(sessionInstituionList.Find(e => e.Id == id));
            
            sessionInstituionList.Add(new InstitutionModel
            {
                Id = id,
                Name = institution
            });
            
            HttpContext.Session.Set("InstitutionList", sessionInstituionList);
            return sessionInstituionList;
        }*/

        /*[HttpPost]
        public IActionResult InstitutionListDelete(string id)
        {
            var sessionInstituionList = HttpContext.Session.Get<List<InstitutionModel>>("InstitutionList") ??
                                       new List<InstitutionModel>();
            sessionInstituionList.Remove(sessionInstituionList.Find(e => e.Id == id));
            HttpContext.Session.Set("InstitutionList", sessionInstituionList);

            return PartialView("_InstitutionTablePartial", sessionInstituionList);
        }*/

        public async Task<List<SelectListItem>> GetNationalInstitutions()
        {
            var list = await new RestfulClient(_logger,
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetNationalInstitutions();

            foreach (var item in list)
            {
                item.Value = item.Value+"-"+item.Text;
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

        public async Task<List<SelectListItem>> GetLocalInstitutions(string id)
        {
            var list = await new RestfulClient(_logger,
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetAllLocal(id);

            foreach (var item in list)
            {
                item.Value = item.Value + "-" + item.Text;
            }

            return list;
        }
        
        [HttpPost]
        public async Task<IActionResult> GenerateReport(ThreePlusOneReportModel model)
        {

            //------------------------------------------------------------------------------------------------------
            
            if (ModelState.IsValid)
            {
                if (model.Level == "National")
                {
                    var list = await GetNationalInstitutions();
                }
                else if (model.Level == "Regional")
                {
                    var list = await GetRegionalInstitutions();
                }
                else if (model.Level == "Local")
                {
                    var list = await GetLocalInstitutions(model.InstitutionName.Split("-")[0]);
                }
            }
            //------------------------------------------------------------------------------------------------------


            var sessionInstituionList = HttpContext.Session.Get<List<InstitutionModel>>("InstitutionList") ??
                           new List<InstitutionModel>();
            var institutions = string.Empty;

            foreach (var item in sessionInstituionList)
            {
                //item.Id => this is sessionId.
                //item.Name => this is intitution name with institution id like (2-AKEPB) so we have to split value.
                var institution = item.Name.Split("-")[0];
                institutions += $"{institution},";
            }

            institutions = institutions.Substring(0, institutions.Length - 1);

            using (var client = new WebClient())
            {
                client.Credentials = new NetworkCredential("jasperadmin", "jasperadmin");

                var stream = new MemoryStream(client.DownloadData(
                    $"http://localhost:8081/jasperserver/rest_v2/reports/reports/Appointment/Three_Plus_One.pdf?institutionid={institutions}&cycleid=19"));

                return File(stream, "application/pdf", $"Three-plus-one[{DateTime.Now.ToString()}].pdf");
            }
        }

    }
}