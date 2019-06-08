using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AMS.frontend.web.Areas.Administration.Models;
using AMS.frontend.web.Areas.Operations.Models;
using AMS.frontend.web.Extensions;
using AMS.frontend.web.Helpers.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AMS.frontend.web.Areas.Administration.Controllers
{
    [Area(AreaNames.Administration)]
    public class LookupCrudController : BaseController
    {

        private readonly ILogger<LookupCrudController> _logger;

        public LookupCrudController(ILogger<LookupCrudController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(CrudModel model)
        {
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveData(CrudModel model)
        {
            if (ModelState.IsValid)
            {
                var success = await new RestfulClient(_logger, HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).AddNewData(model);
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

            return RedirectToAction(model.ActionName);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateData(CrudModel model)
        {
            if (ModelState.IsValid)
            {
                var success = await new RestfulClient(_logger, HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).UpdateLookUpData(model);
                if (success)
                {
                    TempData["MessageType"] = MessageTypes.Success;
                    TempData["Message"] = Messages.successfullyUpdated;

                    ViewBag.MessageType = MessageTypes.Success;
                    ViewBag.Message = Messages.successfullyUpdated;
                }
                else
                {
                    ViewBag.MessageType = MessageTypes.Error;
                    ViewBag.Message = Messages.GeneralError;
                }
            }

            return RedirectToAction(model.ActionName);
        }

        public async Task<IActionResult> AkdnTraining()
        {
            ViewBag.MessageType = TempData["MessageType"];
            ViewBag.Message = TempData["Message"];

            var akdnTraining = await new RestfulClient(_logger,
                    HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetAkdnTraining(true);
            return View("Index", new CrudModel { Url = "constants/akdn-training", Title = "Akdn Training", lookUpList = akdnTraining, ActionName = "AkdnTraining" });
        }

        public async Task<IActionResult> AreaOfStudy()
        {
            ViewBag.MessageType = TempData["MessageType"];
            ViewBag.Message = TempData["Message"];

            var areaOfStudy = await new RestfulClient(_logger,
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetMajorAreaOfStudy(true);
            return View("Index", new CrudModel { Url = "constants/area-of-study", Title = "Area Of Study", lookUpList = areaOfStudy, ActionName = "AreaOfStudy" });
        }

        public async Task<IActionResult> EducationalInstitution()
        {
            ViewBag.MessageType = TempData["MessageType"];
            ViewBag.Message = TempData["Message"];

            var educationalInstitution = await new RestfulClient(_logger,
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetAllInstitutions(true);
            return View("Index", new CrudModel { Url = "constants/educational-publicserviceinstitution", Title = "Educational Institution", lookUpList = educationalInstitution, ActionName = "EducationalInstitution" });
        }

        public async Task<IActionResult> AreaOfOrigin()
        {
            ViewBag.MessageType = TempData["MessageType"];
            ViewBag.Message = TempData["Message"];

            var areaOfOrigin = await new RestfulClient(_logger,
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetAreaOfOrigin(true);
            return View("Index", new CrudModel { Url = "constants/area-of-origin", Title = "Area of Origin", lookUpList = areaOfOrigin, ActionName = "AreaOfOrigin" });
        }

        public async Task<IActionResult> BusinessNature()
        {
            ViewBag.MessageType = TempData["MessageType"];
            ViewBag.Message = TempData["Message"];

            var buisnessNature = await new RestfulClient(_logger,
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetBussinessNature(true);
            return View("Index", new CrudModel { Url = "constants/business-nature", Title = "Business Nature", lookUpList = buisnessNature, ActionName = "BusinessNature" });
        }

        public async Task<IActionResult> BusinessType()
        {
            ViewBag.MessageType = TempData["MessageType"];
            ViewBag.Message = TempData["Message"];

            var buisnessNature = await new RestfulClient(_logger,
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetBussinessType(true);
            return View("Index", new CrudModel { Url = "constants/business-type", Title = "Business Type", lookUpList = buisnessNature, ActionName = "BusinessType" });
        }

        public async Task<IActionResult> EducationalDegree()
        {
            ViewBag.MessageType = TempData["MessageType"];
            ViewBag.Message = TempData["Message"];

            var educationalDegree = await new RestfulClient(_logger,
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetEducationalDegree(true);
            return View("Index", new CrudModel { Url = "constants/educational-degree", Title = "Educational Degree", lookUpList = educationalDegree, ActionName = "EducationalDegree" });
        }

        public async Task<IActionResult> FieldOfExpertise()
        {
            ViewBag.MessageType = TempData["MessageType"];
            ViewBag.Message = TempData["Message"];

            var fieldOfExpertise = await new RestfulClient(_logger,
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetFieldOfExpertise(true);
            return View("Index", new CrudModel { Url = "constants/field-of-expertise", Title = "Field of Expertise", lookUpList = fieldOfExpertise, ActionName = "FieldOfExpertise" });
        }

        public async Task<IActionResult> FieldOfInterest()
        {
            ViewBag.MessageType = TempData["MessageType"];
            ViewBag.Message = TempData["Message"];

            var fieldOfInterest = await new RestfulClient(_logger,
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetFieldOfInterests(true);
            return View("Index", new CrudModel { Url = "constants/field-of-interest", Title = "Field of Interest", lookUpList = fieldOfInterest, ActionName = "FieldOfInterest" });
        }

        public async Task<IActionResult> Language()
        {
            ViewBag.MessageType = TempData["MessageType"];
            ViewBag.Message = TempData["Message"];

            var language = await new RestfulClient(_logger,
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetLanguages(true);
            return View("Index", new CrudModel { Url = "constants/language", Title = "Language", lookUpList = language, ActionName = "Language" });
        }

        public async Task<IActionResult> MaritalStatus()
        {
            ViewBag.MessageType = TempData["MessageType"];
            ViewBag.Message = TempData["Message"];

            var maritalStatus = await new RestfulClient(_logger,
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetMartialStatuses(true);
            return View("Index", new CrudModel { Url = "constants/marital-status", Title = "Marital Status", lookUpList = maritalStatus, ActionName = "MaritalStatus" });
        }

        public async Task<IActionResult> Occupation()
        {
            ViewBag.MessageType = TempData["MessageType"];
            ViewBag.Message = TempData["Message"];

            var occupation = await new RestfulClient(_logger,
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetOcupations(true);
            return View("Index", new CrudModel { Url = "constants/occupation", Title = "Occupation", lookUpList = occupation, ActionName = "Occupation" });
        }

        public async Task<IActionResult> ProfessionalMembership()
        {
            ViewBag.MessageType = TempData["MessageType"];
            ViewBag.Message = TempData["Message"];

            var professionalMembership = await new RestfulClient(_logger,
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetProfessionalMemeberShipDetails(true);
            return View("Index", new CrudModel { Url = "constants/professional-membership", Title = "Professional Membership", lookUpList = professionalMembership, ActionName = "ProfessionalMembership" });
        }

        public async Task<IActionResult> PublicServiceInstitution()
        {
            ViewBag.MessageType = TempData["MessageType"];
            ViewBag.Message = TempData["Message"];

            var publicServiceInstitution = await new RestfulClient(_logger,
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetAllInstitutions(true);
            return View("Index", new CrudModel { Url = "constants/public-service-institution", Title = "Public Service Institution", lookUpList = publicServiceInstitution, ActionName = "PublicServiceInstitution" });
        }

        public async Task<IActionResult> ReligiousQualification()
        {
            ViewBag.MessageType = TempData["MessageType"];
            ViewBag.Message = TempData["Message"];

            var religiousQualification = await new RestfulClient(_logger,
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetReligiousEducation(true);
            return View("Index", new CrudModel { Url = "constants/religious-qualification", Title = "Religious Qualification", lookUpList = religiousQualification, ActionName = "ReligiousQualification" });
        }

        public async Task<IActionResult> Salutation()
        {
            ViewBag.MessageType = TempData["MessageType"];
            ViewBag.Message = TempData["Message"];

            var salutation = await new RestfulClient(_logger,
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetSalutation(true);
            return View("Index", new CrudModel { Url = "constants/salutation", Title = "Salutation", lookUpList = salutation, ActionName = "Salutation" });
        }

        public async Task<IActionResult> SecularStudyLevel()
        {
            ViewBag.MessageType = TempData["MessageType"];
            ViewBag.Message = TempData["Message"];

            var secularStudyLevel = await new RestfulClient(_logger,
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetHighestLevelOfStudy(true);
            return View("Index", new CrudModel { Url = "constants/secular-study-level", Title = "Secular Study Level", lookUpList = secularStudyLevel, ActionName = "SecularStudyLevel" });
        }

        public async Task<IActionResult> Skills()
        {
            ViewBag.MessageType = TempData["MessageType"];
            ViewBag.Message = TempData["Message"];

            var skills = await new RestfulClient(_logger,
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetSkills(true);
            return View("Index", new CrudModel { Url = "constants/skill", Title = "Skills", lookUpList = skills, ActionName = "Skills" });
        }

        public async Task<IActionResult> Position()
        {
            ViewBag.MessageType = TempData["MessageType"];
            ViewBag.Message = TempData["Message"];

            var positions = await new RestfulClient(_logger,
            HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetPositions(true);
            return View("Index", new CrudModel { Url = "position", Title = "Position", lookUpList = positions, ActionName = "Position" });
        }

        public IActionResult Country()
        {
            return View(new CountryModel { Url = "constants/country", Title = "Country" });
        }

        [HttpPost]
        public async Task<IActionResult> AddCountry(CountryModel model)
        {
            if (ModelState.IsValid)
            {
                var success = await new RestfulClient(_logger, HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).AddNewCountry(model);
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

            return View("Country", model);
        }

        public async Task<IActionResult> City()
        {
            ViewBag.CountryList = await new RestfulClient(_logger, HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetAllCountries();
            return View(new CityModel { Url = "constants/city", Title = "City" });
        }

        [HttpPost]
        public async Task<IActionResult> AddCity(CityModel model)
        {
            if (ModelState.IsValid)
            {
                var success = await new RestfulClient(_logger, HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).AddNewCity(model);
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

            ViewBag.CountryList = await new RestfulClient(_logger, HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetAllCountries();
            return View("City", model);
        }

        public async Task<IActionResult> JamatiTitle()
        {
            return View(new JamatiTitle { Url = "constants/jamati-title", Title = "JamatiTitle" });
        }

        public async Task<IActionResult> AddJamatiTitle(JamatiTitle model)
        {
            if (ModelState.IsValid)
            {
                var success = await new RestfulClient(_logger, HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).AddNewJamatiTitle(model);
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

            return View("JamatiTitle", model);
        }

        public IActionResult VoluntaryInstitution()
        {
            return View(new VoluntaryInstitutionModel { Url = "institution", Title = "Voluntary Institution" });
        }

        [HttpPost]
        public async Task<IActionResult> AddVoluntaryInstitution(VoluntaryInstitutionModel model)
        {
            if (ModelState.IsValid)
            {
                var success = await new RestfulClient(_logger, HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).AddNewVoluntaryInstitution(model);
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

            return View("VoluntaryInstitution", model);
        }


        public async Task<IActionResult> AddNewVoluntaryInstitution(NewVoluntaryInstitutionModel model)
        {
            ViewBag.MessageType = TempData["MessageType"];
            ViewBag.Message = TempData["Message"];

            var voluntaryInstitution = await new RestfulClient(_logger,
            HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetPositionInstitution();

            return View(new NewVoluntaryInstitutionModel {Title = "Voluntary Institution", lookUpList = voluntaryInstitution});
        }

        [HttpPost]
        public async Task<IActionResult> AddNewInstitution(NewVoluntaryInstitutionModel model)
        {
            return RedirectToAction("AddNewVoluntaryInstitution");
        }
    }
}