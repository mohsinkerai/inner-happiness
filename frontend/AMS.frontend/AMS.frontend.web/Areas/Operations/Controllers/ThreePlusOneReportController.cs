using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AMS.frontend.web.Areas.Operations.Models;
using AMS.frontend.web.Areas.Operations.Models.Nominations;
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
            var sessionInstituionList = new List<InstitutionModel>();
            HttpContext.Session.Set("InstitutionList", sessionInstituionList);
            return View();
        }

        [HttpPost]
        public IActionResult InstitutionListAdd(string id, string institution)
        {
            var sessionEducationList = AddInstitutionToSession(id, institution);

            return PartialView("_InstitutionTablePartial", sessionEducationList);
        }

        private List<InstitutionModel> AddInstitutionToSession(string id, string institution)
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
        }

        [HttpPost]
        public IActionResult InstitutionListDelete(string id)
        {
            var sessionInstituionList = HttpContext.Session.Get<List<InstitutionModel>>("InstitutionList") ??
                                       new List<InstitutionModel>();
            sessionInstituionList.Remove(sessionInstituionList.Find(e => e.Id == id));
            HttpContext.Session.Set("InstitutionList", sessionInstituionList);

            return PartialView("_InstitutionTablePartial", sessionInstituionList);
        }

        public async Task<JsonResult> GetNationalInstitutions()
        {
            var list = await new RestfulClient(
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetNationalInstitutions();

            foreach (var item in list)
            {
                item.Value = item.Value+"-"+item.Text;
            }

            return new JsonResult(list);
        }

        public async Task<JsonResult> GetRegionalInstitutions()
        {
            var list = await new RestfulClient(
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetRegionalInstitutions();

            foreach (var item in list)
            {
                item.Value = item.Value + "-" + item.Text;
            }

            return new JsonResult(list);
        }

        public async Task<JsonResult> GetLocalInstitutions()
        {
            var list = await new RestfulClient(
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetLocalInstitutions();

            foreach (var item in list)
            {
                item.Value = item.Value + "-" + item.Text;
            }

            return new JsonResult(list);
        }

        public async Task<IActionResult> GenerateReport()
        {
            var sessionInstituionList = HttpContext.Session.Get<List<InstitutionModel>>("InstitutionList") ??
                           new List<InstitutionModel>();

            foreach (var item in sessionInstituionList)
            {
                //item.Id => this is sessionId.
                //item.Name => this is intitution name with institution id like (2-AKEPB) so we have to split value.
            }

            return null;
        }

    }
}