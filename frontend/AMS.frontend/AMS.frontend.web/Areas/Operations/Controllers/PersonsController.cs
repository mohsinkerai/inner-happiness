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
            //return View(new List<PersonModel>());
            return View(await RestfulClient.getPersonDetails());
        }

        [HttpPost]
        public async Task<IActionResult> Index(string cnic, string firstName, string lastName)
        {
            return View(new List<PersonModel>());
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

            sessionEducationList.Add(new EducationModel
            {
                EducationId = id,
                CountryOfStudy = string.IsNullOrWhiteSpace(countryOfStudy) ? string.Empty : countryOfStudy.Split('-')[0],
                CountryOfStudyName = string.IsNullOrWhiteSpace(countryOfStudy) ? string.Empty : countryOfStudy.Split('-')[1],
                FromYear = string.IsNullOrWhiteSpace(fromYear) ? (int?)null : Convert.ToInt32(fromYear),
                Institution = institution,
                MajorAreaOfStudy = majorAreaOfStudy,
                NameOfDegree = nameOfDegree,
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
            sessionAkdnTrainingList.Add(new AkdnTrainingModel
            {
                TrainingId = id,
                CountryOfTraining = countryOfTarining,
                Month = month,
                Training = training,
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
            sessionProfessionalTrainingList.Add(new ProfessionalTrainingModel
            {
                TrainingId = id,
                CountryOfTraining = countryOfTarining,
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
            sessionLanguageList.Add(new LanguageProficiencyModel
            {
                LanguageProficiencyId = id,
                Language = language,
                Read = read,
                Speak = speak,
                Write = write
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
            sessionVoluntaryCommunityList.Add(new VoluntaryCommunityModel
            {
                VoluntaryCommunityId = id,
                FromYear = string.IsNullOrWhiteSpace(fromYear) ? (int?)null : Convert.ToInt32(fromYear),
                Institution = institution,
                ToYear = string.IsNullOrWhiteSpace(toYear) ? (int?)null : Convert.ToInt32(toYear),
                Position = position
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
            sessionEmploymentList.Add(new EmploymentModel
            {
                EmploymentId = id,
                NameOfOrganization = nameOfOrganization,
                Designation = designation,
                Location = location,
                TypeOfBusiness = typeOfBusiness,
                EmploymentEmailAddress = employmentEmailAddress,
                EmploymentEndDate = string.IsNullOrWhiteSpace(employmentStartDate) ? (DateTime?)null : Convert.ToDateTime(employmentStartDate),
                NatureOfBusiness = natureOfBusiness,
                EmploymentStartDate = string.IsNullOrWhiteSpace(employmentStartDate) ? (DateTime?)null : Convert.ToDateTime(employmentEndDate),
                EmploymentTelephone = employmentTelephone,
                NatureOfBusinessOther = natureOfBusinessOther
            });
            HttpContext.Session.Set("EmploymentList", sessionEmploymentList);

            return PartialView("_EmploymentTablePartial", sessionEmploymentList);
        }
    }
}