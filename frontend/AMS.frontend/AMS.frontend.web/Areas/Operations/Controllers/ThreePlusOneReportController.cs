using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AMS.frontend.web.Areas.Operations.Models;
using AMS.frontend.web.Areas.Operations.Models.ThreePlusOneReport;
using AMS.frontend.web.Extensions;
using AMS.frontend.web.Helpers.Constants;
using Microsoft.AspNetCore.Mvc;

namespace AMS.frontend.web.Areas.Operations.Controllers
{
    [Area(AreaNames.Operations)]
    public class ThreePlusOneReportController : BaseController
    {
        public IActionResult Index()
        {
            var sessionInstituionList = new List<ThreePlusOneReportModel>();
            HttpContext.Session.Set("InstitutionList", sessionInstituionList);
            return View();
        }

        [HttpPost]
        public IActionResult InstitutionListAdd(string id, string institution)
        {
            var sessionEducationList = AddInstitutionToSession(id, institution);

            return PartialView("_InstitutionTablePartial", sessionEducationList);
        }

        private List<ThreePlusOneReportModel> AddInstitutionToSession(string id, string institution)
        {
            var sessionInstituionList = HttpContext.Session.Get<List<ThreePlusOneReportModel>>("InstitutionList") ??
                                       new List<ThreePlusOneReportModel>();

            if (string.IsNullOrWhiteSpace(id))
                id = Guid.NewGuid().ToString();
            else
                sessionInstituionList.Remove(sessionInstituionList.Find(e => e.SessionId == id));

            sessionInstituionList.Add(new ThreePlusOneReportModel
            {
                InstitutionName = institution,
                SessionId = id
            });
            
            HttpContext.Session.Set("InstitutionList", sessionInstituionList);
            return sessionInstituionList;
        }

        public async Task<JsonResult> GetNationalInstitutions()
        {
            var list = await new RestfulClient(
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetNationalInstitutions();

            return new JsonResult(list);
        }

        public async Task<JsonResult> GetRegionalInstitutions()
        {
            var list = await new RestfulClient(
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetRegionalInstitutions();

            return new JsonResult(list);
        }

        public async Task<JsonResult> GetLocalInstitutions()
        {
            var list = await new RestfulClient(
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetLocalInstitutions();

            return new JsonResult(list);
        }

    }
}