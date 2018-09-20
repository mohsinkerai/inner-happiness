using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AMS.frontend.web.Areas.Operations.Models;
using AMS.frontend.web.Areas.Operations.Models.Persons;
using AMS.frontend.web.Extensions;
using AMS.frontend.web.Helpers.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;

namespace AMS.frontend.web.Areas.Operations.Controllers
{
    [Area(AreaNames.Operations)]
    public class PersonsController : BaseController
    {
        #region Public Constructors

        public PersonsController(IOptions<Configuration> configuration)
        {
            _configuration = configuration.Value;
        }

        #endregion Public Constructors

        #region Private Fields

        private const string SessionKeyDoNotValidateCnicOnEditPage = "_DoNotValidateCnicOnEditPage";
        private const string SessionKeyDoNotValidateFormNumberOnEditPage = "_DoNotValidateFormNumberOnEditPage";
        private readonly Configuration _configuration;

        #endregion Private Fields

        #region Public Methods

        public async Task<IActionResult> Add()
        {
            try
            {
                await InitializePerson();

                HttpContext.Session.Set("EducationList", new List<EducationModel>());
                HttpContext.Session.Set("AkdnTrainingList", new List<AkdnTrainingModel>());
                HttpContext.Session.Set("ProfessionalTrainingList", new List<ProfessionalTrainingModel>());
                HttpContext.Session.Set("LanguageList", new List<LanguageProficiencyModel>());
                HttpContext.Session.Set("VoluntaryCommunityList", new List<VoluntaryCommunityModel>());
                HttpContext.Session.Set("VoluntaryPublicList", new List<VoluntaryPublicModel>());
                HttpContext.Session.Set("EmploymentList", new List<EmploymentModel>());
                HttpContext.Session.Set("FamilyRelationList", new List<FamilyRelationModel>());

                HttpContext.Session.SetString(SessionKeyDoNotValidateCnicOnEditPage, "false");
                HttpContext.Session.SetString(SessionKeyDoNotValidateFormNumberOnEditPage, "false");
            }
            catch
            {
            }

            return View();
        }

        private async Task InitializePerson()
        {
            ViewBag.SalutationList = await RestfulClient.GetSalutation();
            ViewBag.JamatiTitleList = await RestfulClient.GetJamatiTitles();
            ViewBag.MaritalStatusList = await RestfulClient.GetMartialStatuses();
            ViewBag.CityList = await RestfulClient.GetCities();
            ViewBag.AreaOfOriginList = await RestfulClient.GetAreaOfOrigin();
            ViewBag.InstitutionList = await RestfulClient.GetAllInstitutions();
            ViewBag.NameOfDegreeList = await RestfulClient.GetEducationalDegree();
            ViewBag.ReligiousEducationList = await RestfulClient.GetReligiousEducation();
            ViewBag.RegionalCouncilList = await RestfulClient.GetRegionalCouncil();

            var listOfCountries = await RestfulClient.GetAllCountries();
            ViewBag.CountryOfStudyList = listOfCountries;
            ViewBag.AkdnTrainingCountryList = listOfCountries;
            ViewBag.ProfessionalTrainingCountryList = listOfCountries;

            var listOfLanguageProficiency = await RestfulClient.GetLanguageProficiency();
            ViewBag.Proficiency = listOfLanguageProficiency;

            ViewBag.VoluntaryCommunityPositionList = await RestfulClient.GetPositions();
            ViewBag.HighestLevelOfStudyList = await RestfulClient.GetHighestLevelOfStudy();
            ViewBag.AkdnTrainingList = await RestfulClient.GetAkdnTraining();
            //ViewBag.VoluntaryCommunityInstitutionList = await RestfulClient.GetVoluntaryInstitution();
            ViewBag.VoluntaryCommunityInstitutionList = await RestfulClient.GetPositionInstitution();
            ViewBag.FieldOfInterestsList = await RestfulClient.GetFieldOfInterests();
            ViewBag.OccupationTypeList = await RestfulClient.GetOcupations();
            ViewBag.TypeOfBusinessList = await RestfulClient.GetBussinessType();
            ViewBag.NatureOfBusinessList = await RestfulClient.GetBussinessNature();
            ViewBag.ProfessionalMembershipsList = await RestfulClient.GetProfessionalMemeberShipDetails();
            ViewBag.LanguageList = await RestfulClient.GetLanguages();
            ViewBag.SkillsList = await RestfulClient.GetSkills();
            ViewBag.RelationList = await RestfulClient.GetAllRelatives();
            ViewBag.MajorAreaOfStudy = await RestfulClient.GetMajorAreaOfStudy();
            ViewBag.FieldOfExpertiseList = await RestfulClient.GetFieldOfExpertise();
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] PersonModel model)
        {
            try
            {
                var formCollection = await HttpContext.Request.ReadFormAsync().ConfigureAwait(false);

                if (!string.IsNullOrWhiteSpace(formCollection["RelocationDateTime"]))
                {
                    model.RelocationDateTime =
                        DateTime.ParseExact(formCollection["RelocationDateTime"], "MM/dd/yyyy", null);
                }

                if (!string.IsNullOrWhiteSpace(formCollection["DateOfBirth"]))
                {
                    model.DateOfBirth = DateTime.ParseExact(formCollection["DateOfBirth"], "MM/dd/yyyy", null);
                }

                if (ModelState.IsValid)
                {
                    if (model.Image != null)
                        using (var memoryStream = new MemoryStream())
                        {
                            await model.ImageUpload.CopyToAsync(memoryStream);
                            model.Image = Convert.ToBase64String(memoryStream.ToArray());
                        }

                    RestoreSessionDataToModel(model);

                    var success = await RestfulClient.SavePersonData(model);

                    //var success = await RestfulClient.savePersonData(PersonDummyData(model.Image));

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
            catch (Exception)
            {
                ViewBag.MessageType = MessageTypes.Error;
                ViewBag.Message = Messages.GeneralError;
            }
            finally
            {
                await InitializePerson();
                RestoreSessionDataToModel(model);
            }

            return View(model);
        }

        private void RestoreSessionDataToModel(PersonModel model)
        {
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
            var sessionFamilyRelationList =
                HttpContext.Session.Get<List<FamilyRelationModel>>("FamilyRelationList") ??
                new List<FamilyRelationModel>();

            model.AkdnTrainings = sessionAkdnTrainingList;
            model.Educations = sessionEducationList;
            model.ProfessionalTrainings = sessionProfessionalTrainingList;
            model.LanguageProficiencies = sessionLanguageList;
            model.VoluntaryCommunityServices = sessionVoluntaryCommunityList;
            model.VoluntaryPublicServices = sessionVoluntaryPublicList;
            model.Employments = sessionEmploymentList;
            model.FamilyRelations = sessionFamilyRelationList;
        }

        public List<AkdnTrainingModel> AddAkdnTrainingToSession(string id, string training, string countryOfTarining,
            string month, string year, string date)
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
                Month = month.Contains('-') ? month.Split('-')[0] : month,
                MonthName = GetMonthName(month.Contains('-') ? month.Split('-')[0] : month),
                Training = string.IsNullOrWhiteSpace(training) ? string.Empty : training.Split('-')[0],
                TrainingName = string.IsNullOrWhiteSpace(training) ? string.Empty : training.Split('-')[1],
                Year = string.IsNullOrWhiteSpace(year) ? (int?) null : Convert.ToInt32(year),
                Date = string.IsNullOrWhiteSpace(date) ? DateTime.ParseExact("00/00/00", "MM/dd/yyyy", null) : DateTime.ParseExact(date, "MM/dd/yyyy", null)


        });

            for (var counter = 0; counter < sessionAkdnTrainingList.Count; counter++)
                sessionAkdnTrainingList[counter].Priority = counter + 1;

            HttpContext.Session.Set("AkdnTrainingList", sessionAkdnTrainingList);

            return sessionAkdnTrainingList;
        }

        [HttpPost]
        public IActionResult AkdnTrainingListAdd(string id, string training, string countryOfTarining,
            string month, string year, string date)
        {
            var sessionAkdnTrainingList = AddAkdnTrainingToSession(id, training, countryOfTarining, month, year, date);

            return PartialView("_AkdnTrainingTablePartial", sessionAkdnTrainingList);
        }

        [HttpPost]
        public IActionResult AkdnTrainingListDelete(string id)
        {
            var sessionAkdnTrainingList = HttpContext.Session.Get<List<AkdnTrainingModel>>("AkdnTrainingList") ??
                                          new List<AkdnTrainingModel>();
            sessionAkdnTrainingList.Remove(sessionAkdnTrainingList.Find(e => e.TrainingId == id));
            HttpContext.Session.Set("AkdnTrainingList", sessionAkdnTrainingList);

            return PartialView("_AkdnTrainingTablePartial", sessionAkdnTrainingList);
        }

        public async Task<IActionResult> Detail(string id)
        {
            try
            {
                await InitializePerson();

                HttpContext.Session.Set("EducationList", new List<EducationModel>());
                HttpContext.Session.Set("AkdnTrainingList", new List<AkdnTrainingModel>());
                HttpContext.Session.Set("ProfessionalTrainingList", new List<ProfessionalTrainingModel>());
                HttpContext.Session.Set("LanguageList", new List<LanguageProficiencyModel>());
                HttpContext.Session.Set("VoluntaryCommunityList", new List<VoluntaryCommunityModel>());
                HttpContext.Session.Set("VoluntaryPublicList", new List<VoluntaryPublicModel>());
                HttpContext.Session.Set("EmploymentList", new List<EmploymentModel>());
                HttpContext.Session.Set("FamilyRelationList", new List<FamilyRelationModel>());
            }
            catch (Exception ex)
            {
            }

            var person = await RestfulClient.GetPersonDetailsById(id);

            if (person.RegionalCouncil != null)
            {
                ViewBag.LocalCouncilList = await RestfulClient.GetLocalCouncil(person.RegionalCouncil);
                ViewBag.JamatkhanaList = await RestfulClient.GetJamatkhana(person.LocalCouncil);
            }

            return View(MapPerson(person));
        }

        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                await InitializePerson();

                HttpContext.Session.Set("EducationList", new List<EducationModel>());
                HttpContext.Session.Set("AkdnTrainingList", new List<AkdnTrainingModel>());
                HttpContext.Session.Set("ProfessionalTrainingList", new List<ProfessionalTrainingModel>());
                HttpContext.Session.Set("LanguageList", new List<LanguageProficiencyModel>());
                HttpContext.Session.Set("VoluntaryCommunityList", new List<VoluntaryCommunityModel>());
                HttpContext.Session.Set("VoluntaryPublicList", new List<VoluntaryPublicModel>());
                HttpContext.Session.Set("EmploymentList", new List<EmploymentModel>());
                HttpContext.Session.Set("FamilyRelationList", new List<FamilyRelationModel>());

                HttpContext.Session.SetString(SessionKeyDoNotValidateCnicOnEditPage, "true");
                HttpContext.Session.SetString(SessionKeyDoNotValidateFormNumberOnEditPage, "true");
            }
            catch (Exception ex)
            {
            }

            var person = await RestfulClient.GetPersonDetailsById(id);

            ViewBag.LocalCouncilList = await RestfulClient.GetLocalCouncil(person.RegionalCouncil);
            ViewBag.JamatkhanaList = await RestfulClient.GetJamatkhana(person.LocalCouncil);

            return View(MapPerson(person));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PersonModel model)
        {
            try
            {
                var formCollection = await HttpContext.Request.ReadFormAsync().ConfigureAwait(false);
                if (!string.IsNullOrWhiteSpace(formCollection["RelocationDateTime"]))
                {
                    model.RelocationDateTime =
                        DateTime.ParseExact(formCollection["RelocationDateTime"], "MM/dd/yyyy", null);
                }

                if (!string.IsNullOrWhiteSpace(formCollection["DateOfBirth"]))
                {
                    model.DateOfBirth = DateTime.ParseExact(formCollection["DateOfBirth"], "MM/dd/yyyy", null);
                }

                if (ModelState.IsValid)
                {
                    if (model.ImageUpload != null)
                        using (var memoryStream = new MemoryStream())
                        {
                            await model.ImageUpload.CopyToAsync(memoryStream);
                            model.Image = Convert.ToBase64String(memoryStream.ToArray());
                        }


                    RestoreSessionDataToModel(model);

                    var success = await RestfulClient.EditPersonData(model);
                    if (success)
                    {
                        TempData["MessageType"] = MessageTypes.Success;
                        TempData["Message"] = Messages.SuccessUserUpdate;

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
            finally
            {
                await InitializePerson();
                RestoreSessionDataToModel(model);
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult EducationListAdd(string id, string institution, string countryOfStudy,
            string fromYear,
            string toYear, string nameOfDegree, string majorAreaOfStudy)
        {
            var sessionEducationList = AddEducationToSession(id, institution, countryOfStudy, fromYear, toYear,
                nameOfDegree, majorAreaOfStudy);

            return PartialView("_EducationTablePartial", sessionEducationList);
        }

        [HttpPost]
        public IActionResult EducationListDelete(string id)
        {
            var sessionEducationList = HttpContext.Session.Get<List<EducationModel>>("EducationList") ??
                                       new List<EducationModel>();
            sessionEducationList.Remove(sessionEducationList.Find(e => e.EducationId == id));
            HttpContext.Session.Set("EducationList", sessionEducationList);

            return PartialView("_EducationTablePartial", sessionEducationList);
        }

        [HttpPost]
        public IActionResult EmploymentListAdd(string id, string nameOfOrganization, string designation,
            string location, string employmentEmailAddress, string employmentTelephone, string typeOfBusiness,
            string natureOfBusiness, string natureOfBusinessOther, string employmentStartDate, string employmentEndDate)
        {
            var sessionEmploymentList = AddEmploymentToSession(id, nameOfOrganization, designation, location,
                employmentEmailAddress, employmentTelephone,
                typeOfBusiness, natureOfBusiness, natureOfBusinessOther, employmentStartDate, employmentEndDate);

            return PartialView("_EmploymentTablePartial", sessionEmploymentList);
        }

        [HttpPost]
        public IActionResult EmploymentListDelete(string id)
        {
            var sessionEmploymentList = HttpContext.Session.Get<List<EmploymentModel>>("EmploymentList") ??
                                        new List<EmploymentModel>();
            sessionEmploymentList.Remove(sessionEmploymentList.Find(e => e.EmploymentId == id));
            HttpContext.Session.Set("EmploymentList", sessionEmploymentList);

            return PartialView("_EmploymentTablePartial", sessionEmploymentList);
        }

        [HttpPost]
        public IActionResult FamilyRelationListAdd(string id, string relativeCnic,
            string relativeSalutation,
            string relativeFirstName, string relativeFathersName, string relativeFamilyName, string relativeJamatiTitle,
            string relativeDateOfBirth, string relativeRelation)
        {
            var sessionFamilyRelationList = AddFamilyRelationToSession(id, relativeCnic, relativeSalutation,
                relativeFirstName, relativeFathersName,
                relativeFamilyName, relativeJamatiTitle, relativeDateOfBirth, relativeRelation);

            return PartialView("_FamilyRelationTablePartial", sessionFamilyRelationList);
        }

        [HttpPost]
        public IActionResult FamilyRelationListDelete(string id)
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
            var list = await RestfulClient.GetJamatkhana(uid);

            return new JsonResult(list);
        }

        public async Task<JsonResult> GetLocalCouncil(string uid)
        {
            //var list = new List<SelectListItem> {new SelectListItem {Text = "Karimabad", Value = "Karimabad"}};
            var list = await RestfulClient.GetLocalCouncil(uid);

            return new JsonResult(list);
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.MessageType = TempData["MessageType"];
            ViewBag.Message = TempData["Message"];
            //return View(new List<PersonModel>());

            HttpContext.Session.SetString(SessionKeyDoNotValidateCnicOnEditPage, "false");
            HttpContext.Session.SetString(SessionKeyDoNotValidateFormNumberOnEditPage, "false");

            ViewBag.JamatiTitleList = await RestfulClient.GetJamatiTitles();
            ViewBag.InstitutionList = await RestfulClient.GetAllInstitutions();
            ViewBag.NameOfDegreeList = await RestfulClient.GetEducationalDegree();
            ViewBag.MajorAreaOfStudy = await RestfulClient.GetMajorAreaOfStudy();

            //return View(new IndexPersonModel { Persons = await RestfulClient.getPersonDetails() });
            return View();
        }

        [HttpPost]
        public IActionResult Index(string formNumber, string cnic, string firstName, string lastName)
        {
            if (cnic == null && firstName == null && lastName == null && formNumber == null)
            {
            }

            //var persons = await RestfulClient.getPersonDetailsThroughPagging(firstName,lastName,cnic,1, 1);
            return View(new IndexPersonModel
            {
                //Persons = await RestfulClient.searchPerson(cnic, firstName, lastName),
                //Persons = persons.Item1,
                Cnic = cnic,
                FirstName = firstName,
                LastName = lastName,
                FormNumber = formNumber
            });
        }

        [HttpPost]
        public IActionResult LanguageListAdd(string id, string language, string read,
            string write, string speak)
        {
            var sessionLanguageList = AddLanguageToSession(id, language, read, write, speak);

            return PartialView("_LanguageTablePartial", sessionLanguageList);
        }

        [HttpPost]
        public IActionResult ReorderEducation(string primaryId, string primaryPosition, string secondaryId, string secondaryPosition)
        {
            var sessionEducationList = ReOrderEducationInSession(primaryId, primaryPosition, secondaryId, secondaryPosition);

            return PartialView("_EducationTablePartial", sessionEducationList);
        }

        [HttpPost]
        public IActionResult ReorderLanguage(string primaryId, string primaryPosition, string secondaryId, string secondaryPosition)
        {
            var sessionLanguageList = ReOrderLanguageInSession(primaryId, primaryPosition, secondaryId, secondaryPosition);

            return PartialView("_LanguageTablePartial", sessionLanguageList);
        }

        [HttpPost]
        public IActionResult ReorderVoluntaryCommunityService(string primaryId, string primaryPosition, string secondaryId, string secondaryPosition)
        {
            var voluntaryCommunityServiceInSession = ReorderVoluntaryCommunityServiceInSession(primaryId, primaryPosition, secondaryId, secondaryPosition);

            return PartialView("_VoluntaryCommunityTablePartial", voluntaryCommunityServiceInSession);
        }

        [HttpPost]
        public IActionResult ReorderVoluntaryPublicService(string primaryId, string primaryPosition, string secondaryId, string secondaryPosition)
        {
            var voluntaryPublicServiceInSession = ReorderVoluntaryPublicServiceInSession(primaryId, primaryPosition, secondaryId, secondaryPosition);

            return PartialView("_VoluntaryPublicTablePartial", voluntaryPublicServiceInSession);
        }

        [HttpPost]
        public IActionResult ReorderEmployment(string primaryId, string primaryPosition, string secondaryId, string secondaryPosition)
        {
            var employmentInSession = ReorderEmploymentInSession(primaryId, primaryPosition, secondaryId, secondaryPosition);

            return PartialView("_EmploymentTablePartial", employmentInSession);
        }

        [HttpPost]
        public IActionResult ReorderAkdnTraining(string primaryId, string primaryPosition, string secondaryId, string secondaryPosition)
        {
            var sessionAkdnTrainingList = ReOrderAkdnTrainingInSession(primaryId, primaryPosition, secondaryId, secondaryPosition);

            return PartialView("_AkdnTrainingTablePartial", sessionAkdnTrainingList);
        }

        [HttpPost]
        public IActionResult ReorderProfessionalTraining(string primaryId, string primaryPosition, string secondaryId, string secondaryPosition)
        {
            var sessionProfessionalTrainingList = ReOrderProfessionalTrainingInSession(primaryId, primaryPosition, secondaryId, secondaryPosition);

            return PartialView("_ProfessionalTrainingTablePartial", sessionProfessionalTrainingList);
        }

        [HttpPost]
        public IActionResult LanguageListDelete(string id)
        {
            var sessionLanguageList = HttpContext.Session.Get<List<LanguageProficiencyModel>>("LanguageList") ??
                                      new List<LanguageProficiencyModel>();
            sessionLanguageList.Remove(sessionLanguageList.Find(e => e.LanguageProficiencyId == id));
            HttpContext.Session.Set("LanguageList", sessionLanguageList);

            return PartialView("_LanguageTablePartial", sessionLanguageList);
        }

        [HttpPost]
        public IActionResult ProfessionalTrainingListAdd(string id, string training, string institution,
            string countryOfTarining, string month, string year)
        {
            var sessionProfessionalTrainingList =
                AddProfessionalTrainingToSession(id, training, institution, countryOfTarining, month, year);

            return PartialView("_ProfessionalTrainingTablePartial", sessionProfessionalTrainingList);
        }

        [HttpPost]
        public IActionResult ProfessionalTrainingListDelete(string id)
        {
            var sessionProfessionalTrainingList =
                HttpContext.Session.Get<List<ProfessionalTrainingModel>>("ProfessionalTrainingList") ??
                new List<ProfessionalTrainingModel>();
            sessionProfessionalTrainingList.Remove(sessionProfessionalTrainingList.Find(e => e.TrainingId == id));
            HttpContext.Session.Set("ProfessionalTrainingList", sessionProfessionalTrainingList);

            return PartialView("_ProfessionalTrainingTablePartial", sessionProfessionalTrainingList);
        }

        public async Task<IActionResult> ServerSideAjaxHandler(IndexPersonModel searchingData)
        {
            try
            {
                var firstName = searchingData.FirstName;
                var lastName = searchingData.LastName;
                var cnic = searchingData.Cnic;
                var formNumber = searchingData.FormNumber;

                var queryCollection = Request.Query; //HttpContext.Request.Query;
                // Initialization.
                var search = queryCollection["search[value]"][0];
                var draw = queryCollection["draw"][0];
                //string order = form["order[0][column]"][0];
                //string orderDir = form["order[0][dir]"][0];
                var startRec = Convert.ToInt32(queryCollection["start"][0]);
                var pageSize = Convert.ToInt32(queryCollection["length"][0]);

                var tupleData = await RestfulClient.GetPersonDetailsThroughPagging(firstName, lastName, cnic,
                    formNumber, startRec / pageSize + 1, pageSize);
                var conditionedData = tupleData.Item1;
                var totalRecords = tupleData.Item2;

                // Loading drop down lists.
                return Json(new
                {
                    draw = Convert.ToInt32(draw),
                    recordsTotal = totalRecords,
                    recordsFiltered = totalRecords,
                    data = conditionedData.Select(n => new
                    {
                        n.FullName,
                        n.Cnic,
                        DetailUrl = Url.Action(ActionNames.Detail, ControllerNames.Persons,
                            new {area = AreaNames.Operations, id = n.Id}),
                        EditUrl = Url.Action(ActionNames.Edit, ControllerNames.Persons,
                            new {area = AreaNames.Operations, id = n.Id})
                    })
                });
            }
            catch (Exception)
            {
                return Json(new
                {
                    draw = 1,
                    recordsTotal = 0,
                    recordsFiltered = 0,
                    data = new List<PersonModel>()
                });
            }
        }

        public IActionResult ValidateCnic(string cnic)
        {
            var doNotValidateCnic = HttpContext.Session.GetString(SessionKeyDoNotValidateCnicOnEditPage);

            if (doNotValidateCnic == "true") return Json("true");

            var success = RestfulClient.SearchByCnic(cnic, out var person);
            return Json(!success ? "true" : string.Format("A record against {0} already exists.", cnic));
        }

        public IActionResult ValidateFormNumber(string formnumber)
        {
            var doNotValidateFormNumber = HttpContext.Session.GetString(SessionKeyDoNotValidateFormNumberOnEditPage);

            //if (doNotValidateFormNumber == "true")
            //{
            return Json("true");
            //}
            //else
            //{
            //    var success = RestfulClient.searchByFormNumber(cnic, out var person);
            //    return Json(!success ? "true" : string.Format("A record against {0} already exists.", formnumber));
            //}
        }

        public IActionResult ValidateId(string id)
        {
            var doNotValidateFormNumber = HttpContext.Session.GetString(SessionKeyDoNotValidateFormNumberOnEditPage);

            //if (doNotValidateFormNumber == "true")
            //{
            return Json("true");
            //}
            //else
            //{
            //    var success = RestfulClient.searchByFormNumber(cnic, out var person);
            //    return Json(!success ? "true" : string.Format("A record against {0} already exists.", formnumber));
            //}
        }

        [HttpPost]
        public async Task<IActionResult> VerifyCnic(string cnic)
        {
            var success = RestfulClient.SearchByCnic(cnic, out var person);
            ViewBag.SalutationList = await RestfulClient.GetSalutation();
            ViewBag.JamatiTitleList = await RestfulClient.GetJamatiTitles();
            ViewBag.RelationList = await RestfulClient.GetAllRelatives();

            return PartialView("_FamilyRelationPartial", person);
        }

        [HttpPost]
        public IActionResult VoluntaryCommunityListAdd(string id, string institution, string fromYear,
            string toYear, string position)
        {
            var sessionVoluntaryCommunityList =
                AddVoluntaryCommunityToSession(id, institution, fromYear, toYear, position);

            return PartialView("_VoluntaryCommunityTablePartial", sessionVoluntaryCommunityList);
        }

        [HttpPost]
        public IActionResult VoluntaryCommunityListDelete(string id)
        {
            var sessionVoluntaryCommunityList =
                HttpContext.Session.Get<List<VoluntaryCommunityModel>>("VoluntaryCommunityList") ??
                new List<VoluntaryCommunityModel>();
            sessionVoluntaryCommunityList.Remove(sessionVoluntaryCommunityList.Find(e => e.VoluntaryCommunityId == id));
            HttpContext.Session.Set("VoluntaryCommunityList", sessionVoluntaryCommunityList);

            return PartialView("_VoluntaryCommunityTablePartial", sessionVoluntaryCommunityList);
        }

        [HttpPost]
        public IActionResult VoluntaryPublicListAdd(string id, string institution, string fromYear,
            string toYear, string position)
        {
            var sessionVoluntaryPublicList = AddVoluntaryPublicToSession(id, institution, fromYear, toYear, position);

            return PartialView("_VoluntaryPublicTablePartial", sessionVoluntaryPublicList);
        }

        [HttpPost]
        public IActionResult VoluntaryPublicListDelete(string id)
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

        private List<EducationModel> AddEducationToSession(string id, string institution, string countryOfStudy,
            string fromYear, string toYear,
            string nameOfDegree, string majorAreaOfStudy)
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
                FromYear = string.IsNullOrWhiteSpace(fromYear) ? (int?) null : Convert.ToInt32(fromYear),
                Institution = string.IsNullOrWhiteSpace(institution) ? string.Empty : institution.Split('-')[0],
                InstitutionName = string.IsNullOrWhiteSpace(institution) ? string.Empty : institution.Split('-')[1],
                MajorAreaOfStudy = string.IsNullOrWhiteSpace(majorAreaOfStudy) ? string.Empty : majorAreaOfStudy.Split('-')[0],
                MajorAreaOfStudyName = string.IsNullOrWhiteSpace(majorAreaOfStudy) ? string.Empty : majorAreaOfStudy.Split('-')[1],
                NameOfDegree = string.IsNullOrWhiteSpace(nameOfDegree) ? string.Empty : nameOfDegree.Split('-')[0],
                NameOfDegreeName = string.IsNullOrWhiteSpace(nameOfDegree) ? string.Empty : nameOfDegree.Split('-')[1],
                ToYear = string.IsNullOrWhiteSpace(toYear) ? (int?) null : Convert.ToInt32(toYear)
            });

            for (var counter = 0; counter < sessionEducationList.Count; counter++)
                sessionEducationList[counter].Priority = counter + 1;

            HttpContext.Session.Set("EducationList", sessionEducationList);
            return sessionEducationList;
        }

        private List<EducationModel> ReOrderEducationInSession(string primaryId, string primaryPosition, string secondaryId, string secondaryPosition)
        {
            var sessionEducationList = HttpContext.Session.Get<List<EducationModel>>("EducationList") ??
                                       new List<EducationModel>();

            sessionEducationList.FirstOrDefault(e => e.EducationId == primaryId).Priority = Convert.ToInt32(primaryPosition);
            sessionEducationList.FirstOrDefault(e => e.EducationId == secondaryId).Priority = Convert.ToInt32(secondaryPosition);

            HttpContext.Session.Set("EducationList", sessionEducationList);
            return sessionEducationList;
        }

        private List<LanguageProficiencyModel> ReOrderLanguageInSession(string primaryId, string primaryPosition, string secondaryId, string secondaryPosition)
        {
            var sessionLanguageList = HttpContext.Session.Get<List<LanguageProficiencyModel>>("LanguageList") ??
                                       new List<LanguageProficiencyModel>();

            sessionLanguageList.FirstOrDefault(e => e.LanguageProficiencyId == primaryId).Priority = Convert.ToInt32(primaryPosition);
            sessionLanguageList.FirstOrDefault(e => e.LanguageProficiencyId == secondaryId).Priority = Convert.ToInt32(secondaryPosition);

            HttpContext.Session.Set("LanguageList", sessionLanguageList);
            return sessionLanguageList;
        }

        private List<VoluntaryCommunityModel> ReorderVoluntaryCommunityServiceInSession(string primaryId, string primaryPosition, string secondaryId, string secondaryPosition)
        {
            var voluntaryCommunityModels = HttpContext.Session.Get<List<VoluntaryCommunityModel>>("VoluntaryCommunityList") ??
                                       new List<VoluntaryCommunityModel>();

            voluntaryCommunityModels.FirstOrDefault(e => e.VoluntaryCommunityId == primaryId).Priority = Convert.ToInt32(primaryPosition);
            voluntaryCommunityModels.FirstOrDefault(e => e.VoluntaryCommunityId == secondaryId).Priority = Convert.ToInt32(secondaryPosition);

            HttpContext.Session.Set("VoluntaryCommunityList", voluntaryCommunityModels);
            return voluntaryCommunityModels;
        }

        private List<VoluntaryPublicModel> ReorderVoluntaryPublicServiceInSession(string primaryId, string primaryPosition, string secondaryId, string secondaryPosition)
        {
            var voluntaryPublicModels = HttpContext.Session.Get<List<VoluntaryPublicModel>>("VoluntaryPublicList") ??
                                       new List<VoluntaryPublicModel>();

            voluntaryPublicModels.FirstOrDefault(e => e.VoluntaryPublicId == primaryId).Priority = Convert.ToInt32(primaryPosition);
            voluntaryPublicModels.FirstOrDefault(e => e.VoluntaryPublicId == secondaryId).Priority = Convert.ToInt32(secondaryPosition);

            HttpContext.Session.Set("VoluntaryPublicList", voluntaryPublicModels);
            return voluntaryPublicModels;
        }

        private List<EmploymentModel> ReorderEmploymentInSession(string primaryId, string primaryPosition, string secondaryId, string secondaryPosition)
        {
            var employmentModels = HttpContext.Session.Get<List<EmploymentModel>>("EmploymentList") ??
                                       new List<EmploymentModel>();

            employmentModels.FirstOrDefault(e => e.EmploymentId == primaryId).Priority = Convert.ToInt32(primaryPosition);
            employmentModels.FirstOrDefault(e => e.EmploymentId == secondaryId).Priority = Convert.ToInt32(secondaryPosition);

            HttpContext.Session.Set("EmploymentList", employmentModels);
            return employmentModels;
        }

        private List<AkdnTrainingModel> ReOrderAkdnTrainingInSession(string primaryId, string primaryPosition, string secondaryId, string secondaryPosition)
        {
            var sessionAkdnTrainingList = HttpContext.Session.Get<List<AkdnTrainingModel>>("AkdnTrainingList") ??
                                       new List<AkdnTrainingModel>();

            sessionAkdnTrainingList.FirstOrDefault(e => e.TrainingId == primaryId).Priority = Convert.ToInt32(primaryPosition);
            sessionAkdnTrainingList.FirstOrDefault(e => e.TrainingId == secondaryId).Priority = Convert.ToInt32(secondaryPosition);

            HttpContext.Session.Set("AkdnTrainingList", sessionAkdnTrainingList);
            return sessionAkdnTrainingList;
        }

        private List<ProfessionalTrainingModel> ReOrderProfessionalTrainingInSession(string primaryId, string primaryPosition, string secondaryId, string secondaryPosition)
        {
            var sessionProfessionalTrainingList = HttpContext.Session.Get<List<ProfessionalTrainingModel>>("ProfessionalTrainingList") ??
                                          new List<ProfessionalTrainingModel>();

            sessionProfessionalTrainingList.FirstOrDefault(e => e.TrainingId == primaryId).Priority = Convert.ToInt32(primaryPosition);
            sessionProfessionalTrainingList.FirstOrDefault(e => e.TrainingId == secondaryId).Priority = Convert.ToInt32(secondaryPosition);

            HttpContext.Session.Set("ProfessionalTrainingList", sessionProfessionalTrainingList);
            return sessionProfessionalTrainingList;
        }

        private List<EmploymentModel> AddEmploymentToSession(string id, string nameOfOrganization, string designation,
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
                    ? (DateTime?) null
                    : Convert.ToDateTime(employmentStartDate),
                NatureOfBusiness = string.IsNullOrWhiteSpace(natureOfBusiness)
                    ? string.Empty
                    : natureOfBusiness.Split('-')[0],
                NatureOfBusinessName = string.IsNullOrWhiteSpace(natureOfBusiness)
                    ? string.Empty
                    : natureOfBusiness.Split('-')[1],
                EmploymentStartDate = string.IsNullOrWhiteSpace(employmentStartDate)
                    ? (DateTime?) null
                    : Convert.ToDateTime(employmentEndDate),
                EmploymentTelephone = employmentTelephone,
                NatureOfBusinessOther = natureOfBusinessOther
            });

            for (var counter = 0; counter < sessionEmploymentList.Count; counter++)
                sessionEmploymentList[counter].Priority = counter + 1;

            HttpContext.Session.Set("EmploymentList", sessionEmploymentList);

            return sessionEmploymentList;
        }

        private List<FamilyRelationModel> AddFamilyRelationToSession(string id, string relativeCnic,
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

            return sessionFamilyRelationList;
        }

        private List<LanguageProficiencyModel> AddLanguageToSession(string id, string language, string read,
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
                Read = string.IsNullOrWhiteSpace(read) ? string.Empty : read.Split('-')[0],
                ReadName = string.IsNullOrWhiteSpace(read) ? string.Empty : read.Split('-')[1],
                Speak = string.IsNullOrWhiteSpace(speak) ? string.Empty : speak.Split('-')[0],
                SpeakName = string.IsNullOrWhiteSpace(speak) ? string.Empty : speak.Split('-')[1],
                Write = string.IsNullOrWhiteSpace(write) ? string.Empty : write.Split('-')[0],
                WriteName = string.IsNullOrWhiteSpace(write) ? string.Empty : write.Split('-')[1]
            });

            for (var counter = 0; counter < sessionLanguageList.Count; counter++)
                sessionLanguageList[counter].Priority = counter + 1;

            HttpContext.Session.Set("LanguageList", sessionLanguageList);

            return sessionLanguageList;
        }

        private List<ProfessionalTrainingModel> AddProfessionalTrainingToSession(string id, string training,
            string institution,
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
                Month = month.Contains('-') ? month.Split('-')[0] : month,
                MonthName = GetMonthName(month.Contains('-') ? month.Split('-')[0] : month),
                Training = training,
                Year = string.IsNullOrWhiteSpace(year) ? (int?) null : Convert.ToInt32(year)
            });

            for (var counter = 0; counter < sessionProfessionalTrainingList.Count; counter++)
                sessionProfessionalTrainingList[counter].Priority = counter + 1;

            HttpContext.Session.Set("ProfessionalTrainingList", sessionProfessionalTrainingList);

            return sessionProfessionalTrainingList;
        }

        private List<VoluntaryCommunityModel> AddVoluntaryCommunityToSession(string id, string institution,
            string fromYear,
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
                FromYear = string.IsNullOrWhiteSpace(fromYear) ? (int?) null : Convert.ToInt32(fromYear),
                Institution = string.IsNullOrWhiteSpace(institution) ? string.Empty : institution.Split('-')[0],
                InstitutionName = string.IsNullOrWhiteSpace(institution) ? string.Empty : institution.Split('-')[1],
                ToYear = string.IsNullOrWhiteSpace(toYear) ? (int?) null : Convert.ToInt32(toYear),
                Position = string.IsNullOrWhiteSpace(position) ? string.Empty : position.Split('-')[0],
                PositionName = string.IsNullOrWhiteSpace(position) ? string.Empty : position.Split('-')[1]
            });

            for (var counter = 0; counter < sessionVoluntaryCommunityList.Count; counter++)
                sessionVoluntaryCommunityList[counter].Priority = counter + 1;

            HttpContext.Session.Set("VoluntaryCommunityList", sessionVoluntaryCommunityList);

            return sessionVoluntaryCommunityList;
        }

        private List<VoluntaryPublicModel> AddVoluntaryPublicToSession(string id, string institution, string fromYear,
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
                FromYear = string.IsNullOrWhiteSpace(fromYear) ? (int?) null : Convert.ToInt32(fromYear),
                Institution = institution,
                ToYear = string.IsNullOrWhiteSpace(toYear) ? (int?) null : Convert.ToInt32(toYear),
                Position = position
            });

            for (var counter = 0; counter < sessionVoluntaryPublicList.Count; counter++)
                sessionVoluntaryPublicList[counter].Priority = counter + 1;

            HttpContext.Session.Set("VoluntaryPublicList", sessionVoluntaryPublicList);

            return sessionVoluntaryPublicList;
        }

        private string GetMonthName(string id)
        {
            if (id == "1")
                return "January";
            if (id == "2")
                return "February";
            if (id == "3")
                return "March";
            if (id == "4")
                return "April";
            if (id == "5")
                return "May";
            if (id == "6")
                return "June";
            if (id == "7")
                return "July";
            if (id == "8")
                return "August";
            if (id == "9")
                return "September";
            if (id == "10")
                return "October";
            if (id == "11")
                return "November";
            return id == "12" ? "December" : "";
        }

        private string GetText(string id, List<SelectListItem> list)
        {
            return list.FirstOrDefault(l =>
                (string.IsNullOrWhiteSpace(l.Value) ? string.Empty : l.Value.Split('-')[0]) == id)?.Text;
        }

        private PersonModel MapPerson(PersonModel person)
        {
            //saif ali write mapping here
            try
            {
                if (person != null)
                {
                    if (person.Educations != null)
                    { 
                        foreach (var education in person.Educations)
                        {
                            string institutionName = GetText(education.Institution, ViewBag.InstitutionList);
                            string country = GetText(education.CountryOfStudy, ViewBag.CountryOfStudyList);
                            string nameOfDegree = GetText(education.NameOfDegree, ViewBag.NameOfDegreeList);
                            string majorAreaOfStudy = GetText(education.MajorAreaOfStudy, ViewBag.MajorAreaOfStudy);

                            education.InstitutionName = institutionName;
                            education.CountryOfStudyName = country;
                            education.NameOfDegreeName = nameOfDegree;
                            education.MajorAreaOfStudyName = majorAreaOfStudy;

                            //tejani mapping here
                            AddEducationToSession(education.EducationId,
                                education.Institution + "-" + education.InstitutionName,
                                education.CountryOfStudy + "-" + education.CountryOfStudyName,
                                education.FromYear?.ToString(), education.ToYear?.ToString(),
                                education.NameOfDegree + "-" + education.NameOfDegreeName,
                                education.MajorAreaOfStudy + "-" + education.MajorAreaOfStudyName);
                        }
                    }

                    if (person.AkdnTrainings != null)
                    {
                        foreach (var akdnTraining in person.AkdnTrainings)
                        {
                            string training = GetText(akdnTraining.Training, ViewBag.AkdnTrainingList);
                            string country = GetText(akdnTraining.CountryOfTraining, ViewBag.AkdnTrainingCountryList);
                            var month = GetMonthName(akdnTraining.Month);

                            akdnTraining.TrainingName = training;
                            akdnTraining.CountryOfTrainingName = country;
                            akdnTraining.MonthName = month;

                            AddAkdnTrainingToSession(akdnTraining.TrainingId,
                                akdnTraining.Training + "-" + akdnTraining.TrainingName,
                                akdnTraining.CountryOfTraining + "-" + akdnTraining.CountryOfTrainingName,
                                akdnTraining.Month + "-" + akdnTraining.MonthName, akdnTraining.Year?.ToString(), "");
                        }
                    }

                    if (person.ProfessionalTrainings != null)
                    {
                        foreach (var professionalTraining in person.ProfessionalTrainings)
                        {
                            string country = GetText(professionalTraining.CountryOfTraining,
                                ViewBag.ProfessionalTrainingCountryList);
                            var month = GetMonthName(professionalTraining.Month);

                            professionalTraining.CountryOfTrainingName = country;
                            //professionalTraining.CountryOfTraining = country;
                            professionalTraining.MonthName = month;

                            AddProfessionalTrainingToSession(professionalTraining.TrainingId, professionalTraining.Training,
                                professionalTraining.Institution,
                                professionalTraining.CountryOfTraining + "-" + professionalTraining.CountryOfTrainingName,
                                professionalTraining.Month + "-" + professionalTraining.MonthName,
                                professionalTraining.Year?.ToString());
                        }
                    }

                    if (person.LanguageProficiencies != null)
                    {
                        foreach (var language in person.LanguageProficiencies)
                        {
                            string languageName = GetText(language.Language, ViewBag.LanguageList);
                            string read = GetText(language.Read, ViewBag.Proficiency);
                            string write = GetText(language.Write, ViewBag.Proficiency);
                            string speak = GetText(language.Speak, ViewBag.Proficiency);

                            language.LanguageName = languageName;
                            language.ReadName = read;
                            language.WriteName = write;
                            language.SpeakName = speak;

                            AddLanguageToSession(language.LanguageProficiencyId,
                                language.Language + "-" + language.LanguageName, language.Read + "-" + language.ReadName,
                                language.Write + "-" + language.WriteName, language.Speak + "-" + language.SpeakName);
                        }
                    }

                    if (person.VoluntaryCommunityServices != null)
                    {
                        foreach (var voluntaryService in person.VoluntaryCommunityServices)
                        {
                            string institutionName = GetText(voluntaryService.Institution,
                                ViewBag.VoluntaryCommunityInstitutionList);
                            string position = GetText(voluntaryService.Position, ViewBag.VoluntaryCommunityPositionList);

                            voluntaryService.InstitutionName = institutionName;
                            voluntaryService.PositionName = position;

                            AddVoluntaryCommunityToSession(voluntaryService.VoluntaryCommunityId,
                                voluntaryService.Institution + "-" + voluntaryService.InstitutionName,
                                voluntaryService.FromYear?.ToString(), voluntaryService.ToYear?.ToString(),
                                voluntaryService.Position + "-" + voluntaryService.PositionName);
                        }
                    }

                    if (person.Employments != null)
                    {
                        foreach (var employment in person.Employments)
                        {
                            string businessNature = GetText(employment.NatureOfBusiness, ViewBag.NatureOfBusinessList);
                            string businessType = GetText(employment.TypeOfBusiness, ViewBag.TypeOfBusinessList);

                            employment.TypeOfBusinessName = businessType;
                            employment.NatureOfBusinessName = businessNature;

                            AddEmploymentToSession(employment.EmploymentId, employment.NameOfOrganization,
                                employment.Designation, employment.Location,
                                employment.EmploymentEmailAddress, employment.EmploymentTelephone,
                                employment.TypeOfBusiness + "-" + employment.TypeOfBusinessName,
                                employment.NatureOfBusiness + "-" + employment.NatureOfBusinessName,
                                employment.NatureOfBusinessOther,
                                employment.EmploymentStartDate?.ToString(), employment.EmploymentEndDate?.ToString());
                        }
                    }

                    if (person.FamilyRelations != null)
                    {
                        foreach (var relation in person.FamilyRelations)
                        {
                            string relationName = GetText(relation.Relation, ViewBag.RelationList);
                            relation.RelationName = relationName;

                            AddFamilyRelationToSession(relation.FamilyRelationId, relation.Cnic, relation.Salutation,
                                relation.FirstName, relation.FathersName,
                                relation.FamilyName, relation.JamatiTitle, relation.DateOfBirth.ToString(),
                                relation.RelationName);
                        }
                    }
                }
            }
            catch (Exception ex)
            { 
            }

            return person;
        }

        #endregion Private Methods
    }
}