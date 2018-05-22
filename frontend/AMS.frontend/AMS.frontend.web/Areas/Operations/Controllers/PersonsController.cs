﻿using AMS.frontend.web.Areas.Operations.Models;
using AMS.frontend.web.Areas.Operations.Models.Persons;
using AMS.frontend.web.Extensions;
using AMS.frontend.web.Helpers.Constants;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace AMS.frontend.web.Areas.Operations.Controllers
{
    [Area(AreaNames.Operations)]
    public class PersonsController : Controller
    {
        #region Private Fields

        private readonly Configuration _configuration;

        private readonly IMapper _mapper;

        #endregion Private Fields

        #region Public Constructors

        public PersonsController(IMapper mapper, IOptions<Configuration> configuration)
        {
            _mapper = mapper;
            _configuration = configuration.Value;
        }

        #endregion Public Constructors

        #region Public Methods

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

                var ListOfCountries = await RestfulClient.getAllCountries();
                ViewBag.CountryOfStudyList = ListOfCountries;
                ViewBag.AkdnTrainingCountryList = ListOfCountries;
                ViewBag.ProfessionalTrainingCountryList = ListOfCountries;

                var ListOfLanguageProficiency = await RestfulClient.getLanguageProficiency();
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
            catch
            {
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm]PersonModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await model.ImageUpload.CopyToAsync(memoryStream);
                        model.Image = Convert.ToBase64String(memoryStream.ToArray());
                    }

                    var sessionAkdnTrainingList =
                        HttpContext.Session.Get<List<AkdnTrainingModel>>("AkdnTrainingList") ??
                        new List<AkdnTrainingModel>();
                    var sessionEducationList = HttpContext.Session.Get<List<EducationModel>>("EducationList") ??
                                               new List<EducationModel>();
                    var sessionProfessionalTrainingList =
                        HttpContext.Session.Get<List<ProfessionalTrainingModel>>("ProfessionalTrainingList") ??
                        new List<ProfessionalTrainingModel>();
                    var sessionLanguageList = HttpContext.Session.Get<List<LanguageProficiencyModel>>("LanguageList") ??
                                              new List<LanguageProficiencyModel>();
                    var sessionVoluntaryCommunityList =
                        HttpContext.Session.Get<List<VoluntaryCommunityModel>>("VoluntaryCommunityList") ??
                        new List<VoluntaryCommunityModel>();
                    var sessionVoluntaryPublicList =
                        HttpContext.Session.Get<List<VoluntaryPublicModel>>("VoluntaryPublicList") ??
                        new List<VoluntaryPublicModel>();
                    var sessionEmploymentList = HttpContext.Session.Get<List<EmploymentModel>>("EmploymentList") ??
                                                new List<EmploymentModel>();

                    model.AkdnTrainings = sessionAkdnTrainingList;
                    model.Educations = sessionEducationList;
                    model.ProfessionalTrainings = sessionProfessionalTrainingList;
                    model.LanguageProficiencies = sessionLanguageList;
                    model.VoluntaryCommunityServices = sessionVoluntaryCommunityList;
                    model.VoluntaryPublicServices = sessionVoluntaryPublicList;
                    model.Employments = sessionEmploymentList;

                    //var success = await RestfulClient.savePersonData(model);

                    var success = await RestfulClient.savePersonData(PersonDummyData(model.Image));

                    if (success)
                    {
                        TempData["MessageType"] = MessageTypes.Success;
                        TempData["Message"] = Messages.SuccessfulUserAdd;

                        return RedirectToAction("Index");
                    }

                    ViewBag.MessageType = MessageTypes.Error;
                    ViewBag.Message = Messages.GeneralError;
                }
            }
            catch (Exception ex)
            {
                ViewBag.MessageType = MessageTypes.Error;
                ViewBag.Message = Messages.GeneralError;
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AkdnTrainingListAdd(string id, string training, string countryOfTarining,
            string month, string year)
        {
            var sessionAkdnTrainingList = HttpContext.Session.Get<List<AkdnTrainingModel>>("AkdnTrainingList") ??
                                          new List<AkdnTrainingModel>();

            if (string.IsNullOrWhiteSpace(id))
                id = Guid.NewGuid().ToString();
            else
                sessionAkdnTrainingList.Remove(sessionAkdnTrainingList.Find(e => e.TrainingId == id));

            sessionAkdnTrainingList.Add(new AkdnTrainingModel
            {
                TrainingId = id,
                CountryOfTraining = string.IsNullOrWhiteSpace(countryOfTarining)
                    ? string.Empty
                    : countryOfTarining.Split('-')[0],
                CountryOfTrainingName = string.IsNullOrWhiteSpace(countryOfTarining)
                    ? string.Empty
                    : countryOfTarining.Split('-')[1],
                Month = month,
                MonthName = GetMonthName(month),
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
            var sessionAkdnTrainingList = HttpContext.Session.Get<List<AkdnTrainingModel>>("AkdnTrainingList") ??
                                          new List<AkdnTrainingModel>();
            sessionAkdnTrainingList.Remove(sessionAkdnTrainingList.Find(e => e.TrainingId == id));
            HttpContext.Session.Set("AkdnTrainingList", sessionAkdnTrainingList);

            return PartialView("_AkdnTrainingTablePartial", sessionAkdnTrainingList);
        }

        public async Task<IActionResult> Detail(string id)
        {
            /*try
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

                var ListOfCountries = await RestfulClient.getAllCountries();
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
            catch
            {
            }*/

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

                var ListOfCountries = await RestfulClient.getAllCountries();
                ViewBag.CountryOfStudyList = ListOfCountries;
                ViewBag.AkdnTrainingCountryList = ListOfCountries;
                ViewBag.ProfessionalTrainingCountryList = ListOfCountries;

                var ListOfLanguageProficiency = await RestfulClient.getLanguageProficiency();
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
            catch
            {
            }

            var person = await RestfulClient.getPersonDetailsById(id);
           
            return View(MapPerson(person));
        }

        public async Task<IActionResult> Edit(string id)
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

                var ListOfCountries = await RestfulClient.getAllCountries();
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
            catch
            {
            }

            var person = await RestfulClient.getPersonDetailsById(id);

            return View(person);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PersonModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await model.ImageUpload.CopyToAsync(memoryStream);
                        model.Image = Convert.ToBase64String(memoryStream.ToArray());
                    }

                    var sessionAkdnTrainingList =
                        HttpContext.Session.Get<List<AkdnTrainingModel>>("AkdnTrainingList") ??
                        new List<AkdnTrainingModel>();
                    var sessionEducationList = HttpContext.Session.Get<List<EducationModel>>("EducationList") ??
                                               new List<EducationModel>();
                    var sessionProfessionalTrainingList =
                        HttpContext.Session.Get<List<ProfessionalTrainingModel>>("ProfessionalTrainingList") ??
                        new List<ProfessionalTrainingModel>();
                    var sessionLanguageList = HttpContext.Session.Get<List<LanguageProficiencyModel>>("LanguageList") ??
                                              new List<LanguageProficiencyModel>();
                    var sessionVoluntaryCommunityList =
                        HttpContext.Session.Get<List<VoluntaryCommunityModel>>("VoluntaryCommunityList") ??
                        new List<VoluntaryCommunityModel>();
                    var sessionVoluntaryPublicList =
                        HttpContext.Session.Get<List<VoluntaryPublicModel>>("VoluntaryPublicList") ??
                        new List<VoluntaryPublicModel>();
                    var sessionEmploymentList = HttpContext.Session.Get<List<EmploymentModel>>("EmploymentList") ??
                                                new List<EmploymentModel>();

                    model.AkdnTrainings = sessionAkdnTrainingList;
                    model.Educations = sessionEducationList;
                    model.ProfessionalTrainings = sessionProfessionalTrainingList;
                    model.LanguageProficiencies = sessionLanguageList;
                    model.VoluntaryCommunityServices = sessionVoluntaryCommunityList;
                    model.VoluntaryPublicServices = sessionVoluntaryPublicList;
                    model.Employments = sessionEmploymentList;

                    var success = await RestfulClient.savePersonData(model);
                    if (success)
                    {
                        TempData["MessageType"] = MessageTypes.Success;
                        TempData["Message"] = Messages.SuccessfulUserAdd;

                        return RedirectToAction("Index");
                    }

                    ViewBag.MessageType = MessageTypes.Error;
                    ViewBag.Message = Messages.GeneralError;
                }
            }
            catch (Exception ex)
            {
                ViewBag.MessageType = MessageTypes.Error;
                ViewBag.Message = Messages.GeneralError;
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EducationListAdd(string id, string institution, string countryOfStudy,
            string fromYear,
            string toYear, string nameOfDegree, string majorAreaOfStudy)
        {
            var sessionEducationList = HttpContext.Session.Get<List<EducationModel>>("EducationList") ??
                                       new List<EducationModel>();

            if (string.IsNullOrWhiteSpace(id))
                id = Guid.NewGuid().ToString();
            else
                sessionEducationList.Remove(sessionEducationList.Find(e => e.EducationId == id));

            sessionEducationList.Add(new EducationModel
            {
                EducationId = id,
                CountryOfStudy =
                    string.IsNullOrWhiteSpace(countryOfStudy) ? string.Empty : countryOfStudy.Split('-')[0],
                CountryOfStudyName =
                    string.IsNullOrWhiteSpace(countryOfStudy) ? string.Empty : countryOfStudy.Split('-')[1],
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
            var sessionEducationList = HttpContext.Session.Get<List<EducationModel>>("EducationList") ??
                                       new List<EducationModel>();
            sessionEducationList.Remove(sessionEducationList.Find(e => e.EducationId == id));
            HttpContext.Session.Set("EducationList", sessionEducationList);

            return PartialView("_EducationTablePartial", sessionEducationList);
        }

        [HttpPost]
        public async Task<IActionResult> EmploymentListAdd(string id, string nameOfOrganization, string designation,
            string location, string employmentEmailAddress, string employmentTelephone, string typeOfBusiness,
            string natureOfBusiness, string natureOfBusinessOther, string employmentStartDate, string employmentEndDate)
        {
            var sessionEmploymentList = HttpContext.Session.Get<List<EmploymentModel>>("EmploymentList") ??
                                        new List<EmploymentModel>();

            if (string.IsNullOrWhiteSpace(id))
                id = Guid.NewGuid().ToString();
            else
                sessionEmploymentList.Remove(sessionEmploymentList.Find(e => e.EmploymentId == id));

            sessionEmploymentList.Add(new EmploymentModel
            {
                EmploymentId = id,
                NameOfOrganization = nameOfOrganization,
                Designation = designation,
                Location = location,
                TypeOfBusiness =
                    string.IsNullOrWhiteSpace(typeOfBusiness) ? string.Empty : typeOfBusiness.Split('-')[0],
                TypeOfBusinessName =
                    string.IsNullOrWhiteSpace(typeOfBusiness) ? string.Empty : typeOfBusiness.Split('-')[1],
                EmploymentEmailAddress = employmentEmailAddress,
                EmploymentEndDate = string.IsNullOrWhiteSpace(employmentStartDate)
                    ? (DateTime?)null
                    : Convert.ToDateTime(employmentStartDate),
                NatureOfBusiness = string.IsNullOrWhiteSpace(natureOfBusiness)
                    ? string.Empty
                    : natureOfBusiness.Split('-')[0],
                NatureOfBusinessName = string.IsNullOrWhiteSpace(natureOfBusiness)
                    ? string.Empty
                    : natureOfBusiness.Split('-')[1],
                EmploymentStartDate = string.IsNullOrWhiteSpace(employmentStartDate)
                    ? (DateTime?)null
                    : Convert.ToDateTime(employmentEndDate),
                EmploymentTelephone = employmentTelephone,
                NatureOfBusinessOther = natureOfBusinessOther
            });
            HttpContext.Session.Set("EmploymentList", sessionEmploymentList);

            return PartialView("_EmploymentTablePartial", sessionEmploymentList);
        }

        [HttpPost]
        public async Task<IActionResult> EmploymentListDelete(string id)
        {
            var sessionEmploymentList = HttpContext.Session.Get<List<EmploymentModel>>("EmploymentList") ??
                                        new List<EmploymentModel>();
            sessionEmploymentList.Remove(sessionEmploymentList.Find(e => e.EmploymentId == id));
            HttpContext.Session.Set("EmploymentList", sessionEmploymentList);

            return PartialView("_EmploymentTablePartial", sessionEmploymentList);
        }

        [HttpPost]
        public async Task<IActionResult> FamilyRelationListAdd(string id, string relativeCnic,
            string relativeSalutation,
            string relativeFirstName, string relativeFathersName, string relativeFamilyName, string relativeJamatiTitle,
            string relativeDateOfBirth, string relativeRelation)
        {
            var sessionFamilyRelationList = HttpContext.Session.Get<List<FamilyRelationModel>>("FamilyRelationList") ??
                                            new List<FamilyRelationModel>();

            if (string.IsNullOrWhiteSpace(id))
                id = Guid.NewGuid().ToString();
            else
                sessionFamilyRelationList.Remove(sessionFamilyRelationList.Find(e => e.FamilyRelationId == id));

            sessionFamilyRelationList.Add(new FamilyRelationModel
            {
                FamilyRelationId = id,
                Cnic = relativeCnic,
                DateOfBirth = Convert.ToDateTime(relativeDateOfBirth),
                FathersName = relativeFathersName,
                FirstName = relativeFirstName,
                RelationName = string.IsNullOrWhiteSpace(relativeRelation)
                    ? string.Empty
                    : relativeRelation.Split('-')[1],
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
            var sessionFamilyRelationList = HttpContext.Session.Get<List<FamilyRelationModel>>("FamilyRelationList") ??
                                            new List<FamilyRelationModel>();
            sessionFamilyRelationList.Remove(sessionFamilyRelationList.Find(e => e.FamilyRelationId == id));
            HttpContext.Session.Set("FamilyRelationList", sessionFamilyRelationList);

            return PartialView("_FamilyRelationTablePartial", sessionFamilyRelationList);
        }

        public async Task<JsonResult> GetJamatkhana(string uid)
        {
            //var list = new List<SelectListItem> {new SelectListItem {Text = "Karimabad", Value = "Karimabad"}};
            var list = await RestfulClient.getJamatkhana(uid);

            return new JsonResult(list);
        }

        public async Task<JsonResult> GetLocalCouncil(string uid)
        {
            //var list = new List<SelectListItem> {new SelectListItem {Text = "Karimabad", Value = "Karimabad"}};
            var list = await RestfulClient.getLocalCouncil(uid);

            return new JsonResult(list);
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
            return View(new IndexPersonModel
            {
                Persons = await RestfulClient.searchPerson(cnic, firstName, lastName),
                Cnic = cnic,
                FirstName = firstName,
                LastName = lastName
            });
        }

        [HttpPost]
        public async Task<IActionResult> LanguageListAdd(string id, string language, string read,
            string write, string speak)
        {
            var sessionLanguageList = HttpContext.Session.Get<List<LanguageProficiencyModel>>("LanguageList") ??
                                      new List<LanguageProficiencyModel>();

            if (string.IsNullOrWhiteSpace(id))
                id = Guid.NewGuid().ToString();
            else
                sessionLanguageList.Remove(sessionLanguageList.Find(e => e.LanguageProficiencyId == id));

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
            var sessionLanguageList = HttpContext.Session.Get<List<LanguageProficiencyModel>>("LanguageList") ??
                                      new List<LanguageProficiencyModel>();
            sessionLanguageList.Remove(sessionLanguageList.Find(e => e.LanguageProficiencyId == id));
            HttpContext.Session.Set("LanguageList", sessionLanguageList);

            return PartialView("_LanguageTablePartial", sessionLanguageList);
        }

        public PersonModel PersonDummyData(string image)
        {
            var person = new PersonModel{Image = image};

            person.Cnic = "42101-9999999-3";
            person.PassportNumber = "111-2222-3333";
            person.Salutation = "1";
            person.FirstName = "Saif";
            person.FathersName = "Mehboob";
            person.FamilyName = "Ali";
            person.JamatiTitle = "1";
            person.Gender = 0;
            person.DateOfBirth = Convert.ToDateTime("2018-5-20");
            person.MaritalStatus = "1";
            person.ResidentalAddress = "abc.xyz";
            person.City = "1";
            person.ResidenceTelephone = "02136998541";
            person.MobilePhone = "090078601";
            person.EmailAddress = "abc.123@gmail.com";
            person.AreaOfOrigin = "1";
            person.RegionalCouncil = "2";
            person.LocalCouncil = "3";
            person.Jamatkhana = "4";
            person.PlanToRelocate = true;
            person.RelocationDateTime = DateTime.Now;
            person.HighestLevelOfStudy = "1";
            person.HighestLevelOfStudyOther = "etc";

            //Educations
            var education = new EducationModel();
            education.EducationId = "abcdef";
            education.Institution = "1";
            education.CountryOfStudy = "1";
            education.FromYear = 2010;
            education.ToYear = 2012;
            education.NameOfDegree = "1";
            education.MajorAreaOfStudy = "AI";

            education.CountryOfStudyName = "";
            education.InstitutionName = "";
            education.NameOfDegreeName = "";

            var educationList = new List<EducationModel>();
            educationList.Add(education);

            person.Educations = educationList;

            //-------------------------------------------------

            person.FieldOfExpertise = "Teaching";
            person.ReligiousEducation = "1";

            //AkdnTraining
            var akdnTraining = new AkdnTrainingModel();
            akdnTraining.TrainingId = "abcdef";
            akdnTraining.Training = "1";
            akdnTraining.CountryOfTraining = "1";
            akdnTraining.Month = "1";
            akdnTraining.Year = 2013;

            akdnTraining.TrainingName = null;
            akdnTraining.CountryOfTrainingName = null;

            var akdnTrainingList = new List<AkdnTrainingModel>();
            akdnTrainingList.Add(akdnTraining);

            person.AkdnTrainings = akdnTrainingList;
            //-------------------------------------------------

            //ProfessionalTrainings
            var professionalTraining = new ProfessionalTrainingModel();
            professionalTraining.TrainingId = "abcdef";
            professionalTraining.Training = "Teaching";
            professionalTraining.Institution = "HRE";
            professionalTraining.CountryOfTraining = "1";
            professionalTraining.Month = "1";
            professionalTraining.Year = 2013;

            professionalTraining.CountryOfTrainingName = null;

            var professionalTrainingList = new List<ProfessionalTrainingModel>();
            professionalTrainingList.Add(professionalTraining);

            person.ProfessionalTrainings = professionalTrainingList;

            //-------------------------------------------------

            var skillList = new List<string>();
            skillList.Add("1");

            var professionalMembershipList = new List<string>();
            professionalMembershipList.Add("2");

            person.Skills = skillList;
            person.ProfessionalMemberships = professionalMembershipList;

            //Languages
            var languageProficiency = new LanguageProficiencyModel();
            languageProficiency.LanguageProficiencyId = "abcdef";
            languageProficiency.Language = "1";
            languageProficiency.Read = "1";
            languageProficiency.Write = "2";
            languageProficiency.Speak = "3";

            languageProficiency.LanguageName = null;

            var languageProficiencyList = new List<LanguageProficiencyModel>();
            languageProficiencyList.Add(languageProficiency);

            person.LanguageProficiencies = languageProficiencyList;

            //-------------------------------------------------

            //VoluntaryCommunityServices
            var voluntaryCommunity = new VoluntaryCommunityModel();
            voluntaryCommunity.VoluntaryCommunityId = "abcdef";
            voluntaryCommunity.Institution = "1";
            voluntaryCommunity.FromYear = 2006;
            voluntaryCommunity.ToYear = 2010;
            voluntaryCommunity.Position = "1";

            voluntaryCommunity.InstitutionName = null;
            voluntaryCommunity.PositionName = null;

            var voluntaryCommunityList = new List<VoluntaryCommunityModel>();
            voluntaryCommunityList.Add(voluntaryCommunity);

            person.VoluntaryCommunityServices = voluntaryCommunityList;
            //-------------------------------------------------

            //VoluntaryPublicServices
            var voluntaryPublic = new VoluntaryPublicModel();
            voluntaryPublic.VoluntaryPublicId = "abcdef";
            voluntaryPublic.Institution = "Welfare";
            voluntaryPublic.FromYear = 2013;
            voluntaryPublic.ToYear = 2014;
            voluntaryPublic.Position = "Lead";

            var voluntaryPublicList = new List<VoluntaryPublicModel>();
            voluntaryPublicList.Add(voluntaryPublic);

            person.VoluntaryPublicServices = voluntaryPublicList;

            //-------------------------------------------------

            person.WillingnessToDevoteTimeInFuture = "Continue with Present Institution";

            var fieldofInterestList = new List<string>();
            fieldofInterestList.Add("1");

            person.FieldOfInterest = fieldofInterestList;

            person.HoursPerWeek = 10;
            person.OccupationType = "1";
            person.OccupationTypeOther = "Engineer";

            //Employments
            var employment = new EmploymentModel();
            employment.EmploymentId = "abcdef";
            employment.NameOfOrganization = "Agilosoft";
            employment.Designation = "Team lead";
            employment.Location = "Gulshan Iqbal";
            employment.EmploymentEmailAddress = "jobs@agilosoft.com";
            employment.EmploymentTelephone = "02136885412";
            employment.TypeOfBusiness = "1";
            employment.NatureOfBusiness = "1";
            employment.NatureOfBusinessOther = "N/A";
            employment.EmploymentStartDate = DateTime.Now;
            employment.EmploymentEndDate = DateTime.Now;

            employment.TypeOfBusinessName = null;
            employment.NatureOfBusinessName = null;

            var employmentList = new List<EmploymentModel>();
            employmentList.Add(employment);

            person.Employments = employmentList;
            //------------------------------------------------

            //FamilyInformation
            var familyRelation = new FamilyRelationModel();
            familyRelation.FamilyRelationId = "abcdef";
            familyRelation.Cnic = "42101-9652145-9";
            familyRelation.Salutation = "1";
            familyRelation.FirstName = "abc";
            familyRelation.FathersName = "xyz";
            familyRelation.FamilyName = "abcxyz";
            familyRelation.JamatiTitle = "1";
            familyRelation.DateOfBirth = DateTime.Now;
            familyRelation.Relation = "";

            familyRelation.RelationName = null;

            var familyRelationList = new List<FamilyRelationModel>();
            familyRelationList.Add(familyRelation);

            person.FamilyRelations = familyRelationList;
            //-------------------------------------------------------

            return person;
        }

        [HttpPost]
        public async Task<IActionResult> ProfessionalTrainingListAdd(string id, string training, string institution,
            string countryOfTarining, string month, string year)
        {
            var sessionProfessionalTrainingList =
                HttpContext.Session.Get<List<ProfessionalTrainingModel>>("ProfessionalTrainingList") ??
                new List<ProfessionalTrainingModel>();

            if (string.IsNullOrWhiteSpace(id))
                id = Guid.NewGuid().ToString();
            else
                sessionProfessionalTrainingList.Remove(sessionProfessionalTrainingList.Find(e => e.TrainingId == id));

            sessionProfessionalTrainingList.Add(new ProfessionalTrainingModel
            {
                TrainingId = id,
                CountryOfTraining = string.IsNullOrWhiteSpace(countryOfTarining)
                    ? string.Empty
                    : countryOfTarining.Split('-')[0],
                CountryOfTrainingName = string.IsNullOrWhiteSpace(countryOfTarining)
                    ? string.Empty
                    : countryOfTarining.Split('-')[1],
                Institution = institution,
                Month = month,
                MonthName = GetMonthName(month),
                Training = training,
                Year = string.IsNullOrWhiteSpace(year) ? (int?)null : Convert.ToInt32(year)
            });
            HttpContext.Session.Set("ProfessionalTrainingList", sessionProfessionalTrainingList);

            return PartialView("_ProfessionalTrainingTablePartial", sessionProfessionalTrainingList);
        }

        [HttpPost]
        public async Task<IActionResult> ProfessionalTrainingListDelete(string id)
        {
            var sessionProfessionalTrainingList =
                HttpContext.Session.Get<List<ProfessionalTrainingModel>>("ProfessionalTrainingList") ??
                new List<ProfessionalTrainingModel>();
            sessionProfessionalTrainingList.Remove(sessionProfessionalTrainingList.Find(e => e.TrainingId == id));
            HttpContext.Session.Set("ProfessionalTrainingList", sessionProfessionalTrainingList);

            return PartialView("_ProfessionalTrainingTablePartial", sessionProfessionalTrainingList);
        }

        public async Task<IActionResult> ValidateCnic(string cnic)
        {
            var success = RestfulClient.searchByCNIC(cnic, out var person);
            return Json(!success ? "true" : string.Format("A record against {0} already exists.", cnic));
        }

        [HttpPost]
        public async Task<IActionResult> VerifyCnic(string cnic)
        {
            var success = RestfulClient.searchByCNIC(cnic, out var person);
            ViewBag.SalutationList = await RestfulClient.getSalutation();
            ViewBag.JamatiTitleList = await RestfulClient.getJamatiTitles();
            ViewBag.RelationList = await RestfulClient.getAllRelatives();

            return PartialView("_FamilyRelationPartial", person);
        }

        [HttpPost]
        public async Task<IActionResult> VoluntaryCommunityListAdd(string id, string institution, string fromYear,
            string toYear, string position)
        {
            var sessionVoluntaryCommunityList =
                HttpContext.Session.Get<List<VoluntaryCommunityModel>>("VoluntaryCommunityList") ??
                new List<VoluntaryCommunityModel>();

            if (string.IsNullOrWhiteSpace(id))
                id = Guid.NewGuid().ToString();
            else
                sessionVoluntaryCommunityList.Remove(
                    sessionVoluntaryCommunityList.Find(e => e.VoluntaryCommunityId == id));

            sessionVoluntaryCommunityList.Add(new VoluntaryCommunityModel
            {
                VoluntaryCommunityId = id,
                FromYear = string.IsNullOrWhiteSpace(fromYear) ? (int?)null : Convert.ToInt32(fromYear),
                Institution = string.IsNullOrWhiteSpace(institution) ? string.Empty : institution.Split('-')[0],
                InstitutionName = string.IsNullOrWhiteSpace(institution) ? string.Empty : institution.Split('-')[1],
                ToYear = string.IsNullOrWhiteSpace(toYear) ? (int?)null : Convert.ToInt32(toYear),
                Position = string.IsNullOrWhiteSpace(position) ? string.Empty : position.Split('-')[0],
                PositionName = string.IsNullOrWhiteSpace(position) ? string.Empty : position.Split('-')[1]
            });
            HttpContext.Session.Set("VoluntaryCommunityList", sessionVoluntaryCommunityList);

            return PartialView("_VoluntaryCommunityTablePartial", sessionVoluntaryCommunityList);
        }

        [HttpPost]
        public async Task<IActionResult> VoluntaryCommunityListDelete(string id)
        {
            var sessionVoluntaryCommunityList =
                HttpContext.Session.Get<List<VoluntaryCommunityModel>>("VoluntaryCommunityList") ??
                new List<VoluntaryCommunityModel>();
            sessionVoluntaryCommunityList.Remove(sessionVoluntaryCommunityList.Find(e => e.VoluntaryCommunityId == id));
            HttpContext.Session.Set("VoluntaryCommunityList", sessionVoluntaryCommunityList);

            return PartialView("_VoluntaryCommunityTablePartial", sessionVoluntaryCommunityList);
        }

        [HttpPost]
        public async Task<IActionResult> VoluntaryPublicListAdd(string id, string institution, string fromYear,
            string toYear, string position)
        {
            var sessionVoluntaryPublicList =
                HttpContext.Session.Get<List<VoluntaryPublicModel>>("VoluntaryPublicList") ??
                new List<VoluntaryPublicModel>();

            if (string.IsNullOrWhiteSpace(id))
                id = Guid.NewGuid().ToString();
            else
                sessionVoluntaryPublicList.Remove(sessionVoluntaryPublicList.Find(e => e.VoluntaryPublicId == id));

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
            var sessionVoluntaryPublicList =
                HttpContext.Session.Get<List<VoluntaryPublicModel>>("VoluntaryPublicList") ??
                new List<VoluntaryPublicModel>();
            sessionVoluntaryPublicList.Remove(sessionVoluntaryPublicList.Find(e => e.VoluntaryPublicId == id));
            HttpContext.Session.Set("VoluntaryPublicList", sessionVoluntaryPublicList);

            return PartialView("_VoluntaryPublicTablePartial", new List<VoluntaryPublicModel>());
        }

        #endregion Public Methods

        #region Private Methods

        private string GetText(string id, List<SelectListItem> list)
        {
            return list.FirstOrDefault(l => (string.IsNullOrWhiteSpace(l.Value) ? string.Empty : l.Value.Split('-')[0]) == id)?.Text;
        }

        private PersonModel MapPerson(PersonModel person)
        {
            //saif ali write mapping here
            
            foreach(var education in person.Educations)
            {
                string institutionName = GetText(education.Institution.ToString(), ViewBag.InstitutionList);
                string country = GetText(education.CountryOfStudy.ToString(), ViewBag.CountryOfStudyList);
                string nameOfDegree = GetText(education.NameOfDegree.ToString(), ViewBag.NameOfDegreeList);

                education.InstitutionName = institutionName;
                education.CountryOfStudyName = country;
                education.NameOfDegreeName = nameOfDegree;
            }

            foreach (var akdnTraining in person.AkdnTrainings)
            {
                string training = GetText(akdnTraining.Training, ViewBag.AkdnTrainingList);
                string country = GetText(akdnTraining.CountryOfTraining, ViewBag.AkdnTrainingCountryList);
                string month = GetMonthName(akdnTraining.Month);

                //akdnTraining.TrainingName = training;
                akdnTraining.CountryOfTrainingName = country;
                akdnTraining.MonthName = month;
               
            }

            foreach (var professionalTraining in person.ProfessionalTrainings)
            {
                string country = GetText(professionalTraining.CountryOfTraining, ViewBag.ProfessionalTrainingCountryList);
                string month = GetMonthName(professionalTraining.Month);

                professionalTraining.CountryOfTrainingName = country;
                //professionalTraining.CountryOfTraining = country;
                professionalTraining.MonthName = month;
            }

            foreach (var language in person.LanguageProficiencies)
            {
                string languageName = GetText(language.Language, ViewBag.LanguageList);
                string read = GetText(language.Read, ViewBag.Proficiency);
                string write = GetText(language.Write, ViewBag.Proficiency);
                string speak = GetText(language.Speak, ViewBag.Proficiency);

                language.LanguageName = languageName;
                language.Read = read;
                language.Write = write;
                language.Speak = speak;
            }

            foreach (var voluntaryService in person.VoluntaryCommunityServices)
            {
                string institutionName = GetText(voluntaryService.Institution, ViewBag.VoluntaryCommunityInstitutionList);
                string position = GetText(voluntaryService.Position, ViewBag.VoluntaryCommunityPositionList);

                voluntaryService.InstitutionName = institutionName;
                voluntaryService.PositionName = position;
            }



            return person;
        }

        private string GetMonthName(string id)
        {
            if (id == "1")
            {
                return "January";
            }
            else if (id == "2")
            {
                return "February";
            }
            else if (id == "3")
            {
                return "March";
            }
            else if (id == "4")
            {
                return "April";
            }
            else if (id == "5")
            {
                return "May";
            }
            else if (id == "6")
            {
                return "June";
            }
            else if (id == "7")
            {
                return "July";
            }
            else if (id == "8")
            {
                return "August";
            }
            else if (id == "9")
            {
                return "September";
            }
            else if (id == "10")
            {
                return "October";
            }
            else if (id == "11")
            {
                return "November";
            }
            else if (id == "12")
            {
                return "December";
            }
            else {
                return "";
            }
        }

        #endregion Private Methods
    }
}