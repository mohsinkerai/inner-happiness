using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AMS.frontend.web.Areas.Administration.Models;
using AMS.frontend.web.Areas.Operations.Models;
using AMS.frontend.web.Extensions;
using AMS.frontend.web.Helpers.Constants;
using Microsoft.AspNetCore.Mvc;

namespace AMS.frontend.web.Areas.Administration.Controllers
{
    [Area(AreaNames.Administration)]
    public class LookupCrudController : BaseController
    {
        public IActionResult Index(CrudModel model)
        {
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveData(CrudModel model)
        {
            if (ModelState.IsValid)
            {
                var success = await new RestfulClient(HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).AddNewData(model);
                if (success)
                {
                    TempData["MessageType"] = MessageTypes.Success;
                    TempData["Message"] = Messages.successfullyAdded;

                    ViewBag.MessageType = MessageTypes.Success;
                    ViewBag.Message = Messages.successfullyAdded;
                }
                else
                {
                    ViewBag.MessageType = MessageTypes.Error;
                    ViewBag.Message = Messages.GeneralError;
                }
            }

            return View("Index", model);
        }

        public IActionResult AkdnTraining()
        {         
            return View("Index", new CrudModel {Url= "constants/akdn-training", Title = "Akdn Training" });
        }

        public IActionResult AreaOfStudy()
        {
            return View("Index", new CrudModel { Url = "constants/area-of-study", Title = "Area Of Study" });
        }

        public IActionResult EducationalInstitution()
        {
            return View("Index", new CrudModel { Url = "constants/educational-publicserviceinstitution", Title = "Educational Institution" });
        }

        public IActionResult AreaOfOrigin()
        {
            return View("Index", new CrudModel { Url = "constants/area-of-origin", Title = "Area of Origin" });
        }

        public IActionResult BusinessNature()
        {
            return View("Index", new CrudModel { Url = "constants/business-nature", Title = "Business Nature" });
        }

        public IActionResult BusinessType()
        {
            return View("Index", new CrudModel { Url = "constants/business-type", Title = "Business Type" });
        }

        public IActionResult EducationalDegree()
        {
            return View("Index", new CrudModel { Url = "constants/educational-degree", Title = "Educational Degree" });
        }

        public IActionResult FieldOfExpertise()
        {
            return View("Index", new CrudModel { Url = "constants/field-of-expertise", Title = "Field of Expertise" });
        }

        public IActionResult FieldOfInterest()
        {
            return View("Index", new CrudModel { Url = "constants/field-of-interest", Title = "Field of Interest" });
        }

        public IActionResult Language()
        {
            return View("Index", new CrudModel { Url = "constants/language", Title = "Language" });
        }

        public IActionResult MaritalStatus()
        {
            return View("Index", new CrudModel { Url = "constants/marital-status", Title = "Marital Status" });
        }

        public IActionResult Occupation()
        {
            return View("Index", new CrudModel { Url = "constants/occupation", Title = "Occupation" });
        }

        public IActionResult ProfessionalMembership()
        {
            return View("Index", new CrudModel { Url = "constants/professional-membership", Title = "Professional Membership" });
        }

        public IActionResult PublicServiceInstitution()
        {
            return View("Index", new CrudModel { Url = "constants/public-service-institution", Title = "Public Service Institution" });
        }

        public IActionResult ReligiousQualification()
        {
            return View("Index", new CrudModel { Url = "constants/religious-qualification", Title = "Religious Qualification" });
        }

        public IActionResult Salutation()
        {
            return View("Index", new CrudModel { Url = "constants/salutation", Title = "Salutation" });
        }

        public IActionResult SecularStudyLevel()
        {
            return View("Index", new CrudModel { Url = "constants/secular-study-level", Title = "Secular Study Level" });
        }

        public IActionResult Skills()
        {
            return View("Index", new CrudModel { Url = "constants/skill", Title = "Skills" });
        }

        public IActionResult Country()
        {
            return View("Index", new CrudModel { Url = "constants/country", Title = "Country" });
        }

    }
}