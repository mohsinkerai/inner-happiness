using AMS.frontend.web.Areas.Operations.Models;
using AMS.frontend.web.Areas.Operations.Models.Persons;
using AMS.frontend.web.Extensions;
using AMS.frontend.web.Helpers.Constants;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AMS.frontend.web.Areas.Operations.Controllers
{
    [Area(AreaNames.Operations)]
    public class PersonsController : Controller
    {
        private readonly Configuration _configuration;
        private readonly IMapper _mapper;

        public PersonsController(IMapper mapper, IOptions<Configuration> configuration)
        {
            _mapper = mapper;
            _configuration = configuration.Value;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.MessageType = TempData["MessageType"];
            ViewBag.Message = TempData["Message"];
            //return View(new List<PersonModel>());
            return View(new IndexPersonModel { Persons = await RestfulClient.getPersonDetails() });
        }

        [HttpPost]
        public async Task<IActionResult> Index(string cnic, string firstName, string lastName)
        {
            //return View(new List<PersonModel>());
            return View(new IndexPersonModel { Persons = await RestfulClient.searchPerson(cnic, firstName, lastName), Cnic = cnic, FirstName = firstName, LastName = lastName });
        }

        public async Task<IActionResult> Add()
        {
            try
            {
                ViewBag.SalutationList = await RestfulClient.getSalutation();
                ViewBag.JamatiTitleList = await RestfulClient.getJamatiTitles();
                ViewBag.MaritalStatusList = await RestfulClient.getMartialStatuses();
                ViewBag.CityList = await RestfulClient.getCities();
                ViewBag.AreaOfOriginList = await RestfulClient.getAreaOfOrigin();
                ViewBag.InstitutionList = await RestfulClient.getAllInstitutions();
                ViewBag.NameOfDegreeList = await RestfulClient.getEducationalDegree();
                ViewBag.ReligiousEducationList = await RestfulClient.getReligiousEducation();
                ViewBag.RegionalCouncilList = await RestfulClient.getRegionalCouncil();

                List<SelectListItem> ListOfCountries = await RestfulClient.getAllCountries();
                ViewBag.CountryOfStudyList = ListOfCountries;
                ViewBag.AkdnTrainingCountryList = ListOfCountries;
                ViewBag.ProfessionalTrainingCountryList = ListOfCountries;

                List<SelectListItem> ListOfLanguageProficiency = await RestfulClient.getLanguageProficiency();
                ViewBag.Proficiency = ListOfLanguageProficiency;

                ViewBag.VoluntaryCommunityPositionList = await RestfulClient.getPositions();
                ViewBag.HighestLevelOfStudyList = await RestfulClient.getHighestLevelOfStudy();
                ViewBag.AkdnTrainingList = await RestfulClient.getAkdnTraining();
                ViewBag.VoluntaryCommunityInstitutionList = await RestfulClient.getVoluntaryInstitution();
                ViewBag.FieldOfInterestsList = await RestfulClient.getFieldOfInterests();
                ViewBag.OccupationTypeList = await RestfulClient.getOcupations();
                ViewBag.TypeOfBusinessList = await RestfulClient.getBussinessType();
                ViewBag.NatureOfBusinessList = await RestfulClient.getBussinessNature();
                ViewBag.ProfessionalMembershipsList = await RestfulClient.getProfessionalMemeberShipDetails();
                ViewBag.LanguageList = await RestfulClient.getLanguages();
                ViewBag.SkillsList = await RestfulClient.getSkills();
                ViewBag.RelationList = await RestfulClient.getAllRelatives();

                HttpContext.Session.Set("EducationList", new List<EducationModel>());
                HttpContext.Session.Set("AkdnTrainingList", new List<AkdnTrainingModel>());
                HttpContext.Session.Set("ProfessionalTrainingList", new List<ProfessionalTrainingModel>());
                HttpContext.Session.Set("LanguageList", new List<LanguageProficiencyModel>());
                HttpContext.Session.Set("VoluntaryCommunityList", new List<VoluntaryCommunityModel>());
                HttpContext.Session.Set("VoluntaryPublicList", new List<VoluntaryPublicModel>());
                HttpContext.Session.Set("EmploymentList", new List<EmploymentModel>());
                HttpContext.Session.Set("FamilyRelationList", new List<FamilyRelationModel>());
            }
            catch { }

            return View();
        }

        public async Task<IActionResult> Detail(string id)
        {
            try
            {
                ViewBag.SalutationList = await RestfulClient.getSalutation();
                ViewBag.JamatiTitleList = await RestfulClient.getJamatiTitles();
                ViewBag.MaritalStatusList = await RestfulClient.getMartialStatuses();
                ViewBag.CityList = await RestfulClient.getCities();
                ViewBag.AreaOfOriginList = await RestfulClient.getAreaOfOrigin();
                ViewBag.InstitutionList = await RestfulClient.getAllInstitutions();
                ViewBag.NameOfDegreeList = await RestfulClient.getEducationalDegree();
                ViewBag.ReligiousEducationList = await RestfulClient.getReligiousEducation();
                ViewBag.RegionalCouncilList = await RestfulClient.getRegionalCouncil();

                List<SelectListItem> ListOfCountries = await RestfulClient.getAllCountries();
                ViewBag.CountryOfStudyList = ListOfCountries;
                ViewBag.AkdnTrainingCountryList = ListOfCountries;
                ViewBag.ProfessionalTrainingCountryList = ListOfCountries;

                ViewBag.VoluntaryCommunityPositionList = await RestfulClient.getPositions();
                ViewBag.HighestLevelOfStudyList = await RestfulClient.getHighestLevelOfStudy();
                ViewBag.AkdnTrainingList = await RestfulClient.getAkdnTraining();
                ViewBag.VoluntaryCommunityInstitutionList = await RestfulClient.getVoluntaryInstitution();
                ViewBag.FieldOfInterestsList = await RestfulClient.getFieldOfInterests();
                ViewBag.OccupationTypeList = await RestfulClient.getOcupations();
                ViewBag.TypeOfBusinessList = await RestfulClient.getBussinessType();
                ViewBag.NatureOfBusinessList = await RestfulClient.getBussinessNature();
            }
            catch { }

            return View(await RestfulClient.getPersonDetailsById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add(PersonModel model)
        {

            var sessionAkdnTrainingList = HttpContext.Session.Get<List<AkdnTrainingModel>>("AkdnTrainingList") ?? new List<AkdnTrainingModel>();
            var sessionEducationList = HttpContext.Session.Get<List<EducationModel>>("EducationList") ?? new List<EducationModel>();
            var sessionProfessionalTrainingList = HttpContext.Session.Get<List<ProfessionalTrainingModel>>("ProfessionalTrainingList") ?? new List<ProfessionalTrainingModel>();
            var sessionLanguageList = HttpContext.Session.Get<List<LanguageProficiencyModel>>("LanguageList") ?? new List<LanguageProficiencyModel>();
            var sessionVoluntaryCommunityList = HttpContext.Session.Get<List<VoluntaryCommunityModel>>("VoluntaryCommunityList") ?? new List<VoluntaryCommunityModel>();
            var sessionVoluntaryPublicList = HttpContext.Session.Get<List<VoluntaryPublicModel>>("VoluntaryPublicList") ?? new List<VoluntaryPublicModel>();
            var sessionEmploymentList = HttpContext.Session.Get<List<EmploymentModel>>("EmploymentList") ?? new List<EmploymentModel>();

            model.AkdnTrainings = sessionAkdnTrainingList;
            model.Educations = sessionEducationList;
            model.ProfessionalTrainings = sessionProfessionalTrainingList;
            model.LanguageProficiencies = sessionLanguageList;
            model.VoluntaryCommunityServices = sessionVoluntaryCommunityList;
            model.VoluntaryPublicServices = sessionVoluntaryPublicList;
            model.Employments = sessionEmploymentList;
            
            bool success = await RestfulClient.savePersonData(model);
            if (success)
            {
                TempData["MessageType"] = MessageTypes.Success;
                TempData["Message"] = Messages.SuccessfulUserAdd;
            }
            else
            {
                ViewBag.MessageType = MessageTypes.Error;
                ViewBag.Message = Messages.GeneralError;
            }

            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public async Task<IActionResult> Verify(string cnic)
        {
            return Json(true);
        }

        public async Task<JsonResult> GetLocalCouncil(string uid)
        {
            //var list = new List<SelectListItem> {new SelectListItem {Text = "Karimabad", Value = "Karimabad"}};
            var list = await RestfulClient.getLocalCouncil(uid);

            return new JsonResult(list);
        }

        public async Task<JsonResult> GetJamatkhana(string uid)
        {
            //var list = new List<SelectListItem> {new SelectListItem {Text = "Karimabad", Value = "Karimabad"}};
            var list = await RestfulClient.getJamatkhana(uid);

            return new JsonResult(list);
        }

        [HttpPost]
        public async Task<IActionResult> EducationListAdd(string id, string institution, string countryOfStudy,
            string fromYear,
            string toYear, string nameOfDegree, string majorAreaOfStudy)
        {
            var sessionEducationList = HttpContext.Session.Get<List<EducationModel>>("EducationList") ?? new List<EducationModel>();

            if (string.IsNullOrWhiteSpace(id))
            {
                id = Guid.NewGuid().ToString();
            }
            else
            {
                sessionEducationList.Remove(sessionEducationList.Find(e => e.EducationId == id));
            }

            sessionEducationList.Add(new EducationModel
            {
                EducationId = id,
                CountryOfStudy = string.IsNullOrWhiteSpace(countryOfStudy) ? string.Empty : countryOfStudy.Split('-')[0],
                CountryOfStudyName = string.IsNullOrWhiteSpace(countryOfStudy) ? string.Empty : countryOfStudy.Split('-')[1],
                FromYear = string.IsNullOrWhiteSpace(fromYear) ? (int?)null : Convert.ToInt32(fromYear),
                Institution = string.IsNullOrWhiteSpace(institution) ? string.Empty : institution.Split('-')[0],
                InstitutionName = string.IsNullOrWhiteSpace(institution) ? string.Empty : institution.Split('-')[1],
                MajorAreaOfStudy = majorAreaOfStudy,
                NameOfDegree = string.IsNullOrWhiteSpace(nameOfDegree) ? string.Empty : nameOfDegree.Split('-')[0],
                NameOfDegreeName = string.IsNullOrWhiteSpace(nameOfDegree) ? string.Empty : nameOfDegree.Split('-')[1],
                ToYear = string.IsNullOrWhiteSpace(toYear) ? (int?)null : Convert.ToInt32(toYear)
            });
            HttpContext.Session.Set("EducationList", sessionEducationList);

            return PartialView("_EducationTablePartial", sessionEducationList);
        }

        [HttpPost]
        public async Task<IActionResult> EducationListDelete(string id)
        {
            var sessionEducationList = HttpContext.Session.Get<List<EducationModel>>("EducationList") ?? new List<EducationModel>();
            sessionEducationList.Remove(sessionEducationList.Find(e => e.EducationId == id));
            HttpContext.Session.Set("EducationList", sessionEducationList);

            return PartialView("_EducationTablePartial", sessionEducationList);
        }

        [HttpPost]
        public async Task<IActionResult> AkdnTrainingListAdd(string id, string training, string countryOfTarining,
            string month, string year)
        {
            var sessionAkdnTrainingList = HttpContext.Session.Get<List<AkdnTrainingModel>>("AkdnTrainingList") ?? new List<AkdnTrainingModel>();

            if (string.IsNullOrWhiteSpace(id))
            {
                id = Guid.NewGuid().ToString();
            }
            else
            {
                sessionAkdnTrainingList.Remove(sessionAkdnTrainingList.Find(e => e.TrainingId == id));
            }

            sessionAkdnTrainingList.Add(new AkdnTrainingModel
            {
                TrainingId = id,
                CountryOfTraining = string.IsNullOrWhiteSpace(countryOfTarining) ? string.Empty : countryOfTarining.Split('-')[0],
                CountryOfTrainingName = string.IsNullOrWhiteSpace(countryOfTarining) ? string.Empty : countryOfTarining.Split('-')[1],
                Month = month,
                Training = string.IsNullOrWhiteSpace(training) ? string.Empty : training.Split('-')[0],
                TrainingName = string.IsNullOrWhiteSpace(training) ? string.Empty : training.Split('-')[1],
                Year = string.IsNullOrWhiteSpace(year) ? (int?)null : Convert.ToInt32(year)
            });
            HttpContext.Session.Set("AkdnTrainingList", sessionAkdnTrainingList);

            return PartialView("_AkdnTrainingTablePartial", sessionAkdnTrainingList);
        }

        [HttpPost]
        public async Task<IActionResult> AkdnTrainingListDelete(string id)
        {
            var sessionAkdnTrainingList = HttpContext.Session.Get<List<AkdnTrainingModel>>("AkdnTrainingList") ?? new List<AkdnTrainingModel>();
            sessionAkdnTrainingList.Remove(sessionAkdnTrainingList.Find(e => e.TrainingId == id));
            HttpContext.Session.Set("AkdnTrainingList", sessionAkdnTrainingList);

            return PartialView("_AkdnTrainingTablePartial", sessionAkdnTrainingList);
        }

        [HttpPost]
        public async Task<IActionResult> ProfessionalTrainingListAdd(string id, string training, string institution,
            string countryOfTarining, string month, string year)
        {
            var sessionProfessionalTrainingList = HttpContext.Session.Get<List<ProfessionalTrainingModel>>("ProfessionalTrainingList") ?? new List<ProfessionalTrainingModel>();

            if (string.IsNullOrWhiteSpace(id))
            {
                id = Guid.NewGuid().ToString();
            }
            else
            {
                sessionProfessionalTrainingList.Remove(sessionProfessionalTrainingList.Find(e => e.TrainingId == id));
            }

            sessionProfessionalTrainingList.Add(new ProfessionalTrainingModel
            {
                TrainingId = id,
                CountryOfTraining = string.IsNullOrWhiteSpace(countryOfTarining) ? string.Empty : countryOfTarining.Split('-')[0],
                CountryOfTrainingName = string.IsNullOrWhiteSpace(countryOfTarining) ? string.Empty : countryOfTarining.Split('-')[1],
                Institution = institution,
                Month = month,
                Training = training,
                Year = string.IsNullOrWhiteSpace(year) ? (int?)null : Convert.ToInt32(year)
            });
            HttpContext.Session.Set("ProfessionalTrainingList", sessionProfessionalTrainingList);

            return PartialView("_ProfessionalTrainingTablePartial", sessionProfessionalTrainingList);
        }

        [HttpPost]
        public async Task<IActionResult> ProfessionalTrainingListDelete(string id)
        {
            var sessionProfessionalTrainingList = HttpContext.Session.Get<List<ProfessionalTrainingModel>>("ProfessionalTrainingList") ?? new List<ProfessionalTrainingModel>();
            sessionProfessionalTrainingList.Remove(sessionProfessionalTrainingList.Find(e => e.TrainingId == id));
            HttpContext.Session.Set("ProfessionalTrainingList", sessionProfessionalTrainingList);

            return PartialView("_ProfessionalTrainingTablePartial", sessionProfessionalTrainingList);
        }

        [HttpPost]
        public async Task<IActionResult> LanguageListAdd(string id, string language, string read,
            string write, string speak)
        {
            var sessionLanguageList = HttpContext.Session.Get<List<LanguageProficiencyModel>>("LanguageList") ?? new List<LanguageProficiencyModel>();

            if (string.IsNullOrWhiteSpace(id))
            {
                id = Guid.NewGuid().ToString();
            }
            else
            {
                sessionLanguageList.Remove(sessionLanguageList.Find(e => e.LanguageProficiencyId == id));
            }

            sessionLanguageList.Add(new LanguageProficiencyModel
            {
                LanguageProficiencyId = id,
                Language = string.IsNullOrWhiteSpace(language) ? string.Empty : language.Split('-')[0],
                LanguageName = string.IsNullOrWhiteSpace(language) ? string.Empty : language.Split('-')[1],
                Read = string.IsNullOrWhiteSpace(read) ? string.Empty : read.Split('-')[1],
                Speak = string.IsNullOrWhiteSpace(speak) ? string.Empty : speak.Split('-')[1],
                Write = string.IsNullOrWhiteSpace(write) ? string.Empty : write.Split('-')[1]
            });
            HttpContext.Session.Set("LanguageList", sessionLanguageList);

            return PartialView("_LanguageTablePartial", sessionLanguageList);
        }

        [HttpPost]
        public async Task<IActionResult> LanguageListDelete(string id)
        {
            var sessionLanguageList = HttpContext.Session.Get<List<LanguageProficiencyModel>>("LanguageList") ?? new List<LanguageProficiencyModel>();
            sessionLanguageList.Remove(sessionLanguageList.Find(e => e.LanguageProficiencyId == id));
            HttpContext.Session.Set("LanguageList", sessionLanguageList);

            return PartialView("_LanguageTablePartial", sessionLanguageList);
        }

        [HttpPost]
        public async Task<IActionResult> VoluntaryCommunityListAdd(string id, string institution, string fromYear,
            string toYear, string position)
        {
            var sessionVoluntaryCommunityList = HttpContext.Session.Get<List<VoluntaryCommunityModel>>("VoluntaryCommunityList") ?? new List<VoluntaryCommunityModel>();

            if (string.IsNullOrWhiteSpace(id))
            {
                id = Guid.NewGuid().ToString();
            }
            else
            {
                sessionVoluntaryCommunityList.Remove(sessionVoluntaryCommunityList.Find(e => e.VoluntaryCommunityId == id));
            }

            sessionVoluntaryCommunityList.Add(new VoluntaryCommunityModel
            {
                VoluntaryCommunityId = id,
                FromYear = string.IsNullOrWhiteSpace(fromYear) ? (int?) null : Convert.ToInt32(fromYear),
                Institution = string.IsNullOrWhiteSpace(institution) ? string.Empty : institution.Split('-')[0],
                InstitutionName = string.IsNullOrWhiteSpace(institution) ? string.Empty : institution.Split('-')[1],
                ToYear = string.IsNullOrWhiteSpace(toYear) ? (int?) null : Convert.ToInt32(toYear),
                Position = string.IsNullOrWhiteSpace(position) ? string.Empty : position.Split('-')[0],
                PositionName = string.IsNullOrWhiteSpace(position) ? string.Empty : position.Split('-')[1]
            });
            HttpContext.Session.Set("VoluntaryCommunityList", sessionVoluntaryCommunityList);

            return PartialView("_VoluntaryCommunityTablePartial", sessionVoluntaryCommunityList);
        }

        [HttpPost]
        public async Task<IActionResult> VoluntaryCommunityListDelete(string id)
        {
            var sessionVoluntaryCommunityList = HttpContext.Session.Get<List<VoluntaryCommunityModel>>("VoluntaryCommunityList") ?? new List<VoluntaryCommunityModel>();
            sessionVoluntaryCommunityList.Remove(sessionVoluntaryCommunityList.Find(e => e.VoluntaryCommunityId == id));
            HttpContext.Session.Set("VoluntaryCommunityList", sessionVoluntaryCommunityList);

            return PartialView("_VoluntaryCommunityTablePartial", sessionVoluntaryCommunityList);
        }

        [HttpPost]
        public async Task<IActionResult> VoluntaryPublicListAdd(string id, string institution, string fromYear,
            string toYear, string position)
        {
            var sessionVoluntaryPublicList = HttpContext.Session.Get<List<VoluntaryPublicModel>>("VoluntaryPublicList") ?? new List<VoluntaryPublicModel>();

            if (string.IsNullOrWhiteSpace(id))
            {
                id = Guid.NewGuid().ToString();
            }
            else
            {
                sessionVoluntaryPublicList.Remove(sessionVoluntaryPublicList.Find(e => e.VoluntaryPublicId == id));
            }

            sessionVoluntaryPublicList.Add(new VoluntaryPublicModel
            {
                VoluntaryPublicId = id,
                FromYear = string.IsNullOrWhiteSpace(fromYear) ? (int?)null : Convert.ToInt32(fromYear),
                Institution = institution,
                ToYear = string.IsNullOrWhiteSpace(toYear) ? (int?)null : Convert.ToInt32(toYear),
                Position = position
            });
            HttpContext.Session.Set("VoluntaryPublicList", sessionVoluntaryPublicList);

            return PartialView("_VoluntaryPublicTablePartial", sessionVoluntaryPublicList);
        }

        [HttpPost]
        public async Task<IActionResult> VoluntaryPublicListDelete(string id)
        {
            var sessionVoluntaryPublicList = HttpContext.Session.Get<List<VoluntaryPublicModel>>("VoluntaryPublicList") ?? new List<VoluntaryPublicModel>();
            sessionVoluntaryPublicList.Remove(sessionVoluntaryPublicList.Find(e => e.VoluntaryPublicId == id));
            HttpContext.Session.Set("VoluntaryPublicList", sessionVoluntaryPublicList);

            return PartialView("_VoluntaryPublicTablePartial", new List<VoluntaryPublicModel>());
        }

        [HttpPost]
        public async Task<IActionResult> EmploymentListDelete(string id)
        {
            var sessionEmploymentList = HttpContext.Session.Get<List<EmploymentModel>>("EmploymentList") ?? new List<EmploymentModel>();
            sessionEmploymentList.Remove(sessionEmploymentList.Find(e => e.EmploymentId == id));
            HttpContext.Session.Set("EmploymentList", sessionEmploymentList);

            return PartialView("_EmploymentTablePartial", sessionEmploymentList);
        }

        [HttpPost]
        public async Task<IActionResult> EmploymentListAdd(string id, string nameOfOrganization, string designation,
            string location, string employmentEmailAddress, string employmentTelephone, string typeOfBusiness,
            string natureOfBusiness, string natureOfBusinessOther, string employmentStartDate, string employmentEndDate)
        {
            var sessionEmploymentList = HttpContext.Session.Get<List<EmploymentModel>>("EmploymentList") ?? new List<EmploymentModel>();

            if (string.IsNullOrWhiteSpace(id))
            {
                id = Guid.NewGuid().ToString();
            }
            else
            {
                sessionEmploymentList.Remove(sessionEmploymentList.Find(e => e.EmploymentId == id));
            }

            sessionEmploymentList.Add(new EmploymentModel
            {
                EmploymentId = id,
                NameOfOrganization = nameOfOrganization,
                Designation = designation,
                Location = location,
                TypeOfBusiness = string.IsNullOrWhiteSpace(typeOfBusiness) ? string.Empty : typeOfBusiness.Split('-')[0],
                TypeOfBusinessName = string.IsNullOrWhiteSpace(typeOfBusiness) ? string.Empty : typeOfBusiness.Split('-')[1],
                EmploymentEmailAddress = employmentEmailAddress,
                EmploymentEndDate = string.IsNullOrWhiteSpace(employmentStartDate) ? (DateTime?)null : Convert.ToDateTime(employmentStartDate),
                NatureOfBusiness = string.IsNullOrWhiteSpace(natureOfBusiness) ? string.Empty : natureOfBusiness.Split('-')[0],
                NatureOfBusinessName = string.IsNullOrWhiteSpace(natureOfBusiness) ? string.Empty : natureOfBusiness.Split('-')[1],
                EmploymentStartDate = string.IsNullOrWhiteSpace(employmentStartDate) ? (DateTime?)null : Convert.ToDateTime(employmentEndDate),
                EmploymentTelephone = employmentTelephone,
                NatureOfBusinessOther = natureOfBusinessOther
            });
            HttpContext.Session.Set("EmploymentList", sessionEmploymentList);

            return PartialView("_EmploymentTablePartial", sessionEmploymentList);
        }

        [HttpPost]
        public async Task<IActionResult> FamilyRelationListAdd(string id, string relativeCnic, string relativeSalutation,
            string relativeFirstName, string relativeFathersName, string relativeFamilyName, string relativeJamatiTitle,
            string relativeDateOfBirth, string relativeRelation)
        {
            var sessionFamilyRelationList = HttpContext.Session.Get<List<FamilyRelationModel>>("FamilyRelationList") ?? new List<FamilyRelationModel>();

            if (string.IsNullOrWhiteSpace(id))
            {
                id = Guid.NewGuid().ToString();
            }
            else
            {
                sessionFamilyRelationList.Remove(sessionFamilyRelationList.Find(e => e.FamilyRelationId == id));
            }

            sessionFamilyRelationList.Add(new FamilyRelationModel
            {
                FamilyRelationId = id,
                Cnic = relativeCnic,
                DateOfBirth = Convert.ToDateTime(relativeDateOfBirth),
                FathersName = relativeFathersName,
                FirstName = relativeFirstName,
                RelationName = string.IsNullOrWhiteSpace(relativeRelation) ? string.Empty : relativeRelation.Split('-')[1],
                FamilyName = relativeFamilyName,
                JamatiTitle = relativeJamatiTitle,
                Relation = string.IsNullOrWhiteSpace(relativeRelation) ? string.Empty : relativeRelation.Split('-')[0],
                Salutation = relativeSalutation
            });
            HttpContext.Session.Set("FamilyRelationList", sessionFamilyRelationList);

            return PartialView("_FamilyRelationTablePartial", sessionFamilyRelationList);
        }

        [HttpPost]
        public async Task<IActionResult> FamilyRelationListDelete(string id)
        {
            var sessionFamilyRelationList = HttpContext.Session.Get<List<FamilyRelationModel>>("FamilyRelationList") ?? new List<FamilyRelationModel>();
            sessionFamilyRelationList.Remove(sessionFamilyRelationList.Find(e => e.FamilyRelationId == id));
            HttpContext.Session.Set("FamilyRelationList", sessionFamilyRelationList);

            return PartialView("_FamilyRelationTablePartial", sessionFamilyRelationList);
        }

        public async Task<IActionResult> ValidateCnic(string cnic)
        {
            bool success =await RestfulClient.searchByCNIC(cnic);
            return Json(success == false ? "true" : string.Format("A record against {0} already exists.", cnic));
        }
    }
}