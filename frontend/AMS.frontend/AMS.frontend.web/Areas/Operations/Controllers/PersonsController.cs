using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AMS.frontend.web.Areas.Operations.Models;
using AMS.frontend.web.Areas.Operations.Models.Nominations;
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

        private readonly RestfulClient _restfulClient;

        #endregion Private Fields

        #region Public Methods

        public async Task<IActionResult> Add()
        {
            try
            {
                await InitializePerson();

                ResetSession();

                HttpContext.Session.SetString(SessionKeyDoNotValidateCnicOnEditPage, "false");
                HttpContext.Session.SetString(SessionKeyDoNotValidateFormNumberOnEditPage, "false");
            }
            catch
            {
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] PersonModel model)
        {
            try
            {
                var formCollection = await HttpContext.Request.ReadFormAsync().ConfigureAwait(false);

                if (!string.IsNullOrWhiteSpace(formCollection["RelocationDateTime"]))
                    model.RelocationDateTime =
                        DateTime.ParseExact(formCollection["RelocationDateTime"], "MM/dd/yyyy", null);

                if (!string.IsNullOrWhiteSpace(formCollection["DateOfBirth"]))
                    model.DateOfBirth = DateTime.ParseExact(formCollection["DateOfBirth"], "MM/dd/yyyy", null);

                if (ModelState.IsValid)
                {
                    if (model.ImageUpload != null)
                        using (var memoryStream = new MemoryStream())
                        {
                            await model.ImageUpload.CopyToAsync(memoryStream);
                            model.Image = Convert.ToBase64String(memoryStream.ToArray());
                        }

                    RestoreSessionDataToModel(model);

                    var success =
                        await new RestfulClient(HttpContext.Session
                            .Get<AuthenticationResponse>("AuthenticationResponse")?.Token).SavePersonData(model);

                    //var success = await new RestfulClient(HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).savePersonData(PersonDummyData(model.Image));

                    if (success)
                    {
                        TempData["MessageType"] = MessageTypes.Success;
                        TempData["Message"] = Messages.SuccessfulUserAdd;

                        ViewBag.MessageType = MessageTypes.Success;
                        ViewBag.Message = Messages.SuccessfulUserAdd;

                        return RedirectToAction("Edit", "Persons", new {area = AreaNames.Operations, id = model.Id});

                        //return RedirectToAction("Index");
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

        public List<AkdnTrainingModel> AddAkdnTrainingToSession(string id, string training, string countryOfTarining,
            string date)
        {
            var sessionAkdnTrainingList = HttpContext.Session.Get<List<AkdnTrainingModel>>("AkdnTrainingList") ??
                                          new List<AkdnTrainingModel>();

            if (string.IsNullOrWhiteSpace(id))
                id = Guid.NewGuid().ToString();
            else
                sessionAkdnTrainingList.Remove(sessionAkdnTrainingList.Find(e => e.TrainingId == id));

            DateTime? dt = null;
            try
            {
                if (!string.IsNullOrWhiteSpace(date)) dt = DateTime.ParseExact(date, "MMMM-yyyy", null);
            }
            catch (Exception)
            {
                dt = Convert.ToDateTime(date);
            }

            sessionAkdnTrainingList.Add(new AkdnTrainingModel
            {
                TrainingId = id,
                CountryOfTraining = string.IsNullOrWhiteSpace(countryOfTarining)
                    ? string.Empty
                    : countryOfTarining.Split('-')[0],
                CountryOfTrainingName = string.IsNullOrWhiteSpace(countryOfTarining)
                    ? string.Empty
                    : countryOfTarining.Split('-')[1],
                //Month = month.Contains('-') ? month.Split('-')[0] : month,
                //MonthName = GetMonthName(month.Contains('-') ? month.Split('-')[0] : month),
                Training = string.IsNullOrWhiteSpace(training) ? string.Empty : training.Split('-')[0],
                TrainingName = string.IsNullOrWhiteSpace(training) ? string.Empty : training.Split('-')[1],
                //Year = string.IsNullOrWhiteSpace(year) ? (int?) null : Convert.ToInt32(year),
                //Date = string.IsNullOrWhiteSpace(date) ? DateTime.ParseExact("00/00/00", "MM/dd/yyyy", null) : DateTime.ParseExact(date, "MM/dd/yyyy", null)
                Date = dt
            });

            for (var counter = 0; counter < sessionAkdnTrainingList.Count; counter++)
                sessionAkdnTrainingList[counter].Priority = counter + 1;

            HttpContext.Session.Set("AkdnTrainingList", sessionAkdnTrainingList);

            return sessionAkdnTrainingList;
        }

        [HttpPost]
        public IActionResult AkdnTrainingListAdd(string id, string training, string countryOfTarining,
            string date)
        {
            var sessionAkdnTrainingList = AddAkdnTrainingToSession(id, training, countryOfTarining, date);

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

                ResetSession();
            }
            catch (Exception)
            {
            }

            var person =
                await new RestfulClient(
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token)
                    .GetPersonDetailsById(id);

            if (person.RegionalCouncil != null)
            {
                ViewBag.LocalCouncilList =
                    await new RestfulClient(HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")
                        ?.Token).GetLocalCouncil(person.RegionalCouncil);
                ViewBag.JamatkhanaList =
                    await new RestfulClient(HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")
                        ?.Token).GetJamatkhana(person.LocalCouncil);

                var appointments = await GetPastImamatAppointments(person.Id);
                foreach (var appointment in appointments)
                {
                    if (person.VoluntaryCommunityServices == null)
                        person.VoluntaryCommunityServices = new List<VoluntaryCommunityModel>();

                    if (!person.VoluntaryCommunityServices.Any(v =>
                        v.Position == appointment.RawPosition && v.Institution == appointment.RawInstitution &&
                        v.FromYear == Convert.ToInt32(appointment.FromYear) &&
                        v.ToYear == Convert.ToInt32(appointment.ToYear)))
                        person.VoluntaryCommunityServices.Add(new VoluntaryCommunityModel
                        {
                            Position = appointment.RawPosition,
                            Institution = appointment.RawInstitution,
                            FromYear = Convert.ToInt32(appointment.FromYear),
                            ToYear = Convert.ToInt32(appointment.ToYear),
                            Priority = person.VoluntaryCommunityServices.Select(vc => vc.Priority).FirstOrDefault() + 1,
                            Cycle = appointment.CycleId,
                            IsImamatAppointee = true
                        });
                }
            }

            return View(await MapPerson(person));
        }

        public async Task<IActionResult> Edit(string id)
        {
            ViewBag.MessageType = TempData["MessageType"];
            ViewBag.Message = TempData["Message"];

            try
            {
                await InitializePerson();

                ResetSession();

                HttpContext.Session.SetString(SessionKeyDoNotValidateCnicOnEditPage, "true");
                HttpContext.Session.SetString(SessionKeyDoNotValidateFormNumberOnEditPage, "true");
            }
            catch (Exception)
            {
            }

            var person =
                await new RestfulClient(
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token)
                    .GetPersonDetailsById(id);

            ViewBag.LocalCouncilList =
                await new RestfulClient(
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token)
                    .GetLocalCouncil(person.RegionalCouncil);
            ViewBag.JamatkhanaList =
                await new RestfulClient(
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token)
                    .GetJamatkhana(person.LocalCouncil);

            var appointments = await GetPastImamatAppointments(person.Id);
            foreach (var appointment in appointments)
            {
                if (person.VoluntaryCommunityServices == null)
                    person.VoluntaryCommunityServices = new List<VoluntaryCommunityModel>();

                if (!person.VoluntaryCommunityServices.Any(v =>
                    v.Position == appointment.RawPosition && v.Institution == appointment.RawInstitution &&
                    v.FromYear == Convert.ToInt32(appointment.FromYear) &&
                    v.ToYear == Convert.ToInt32(appointment.ToYear)))
                    person.VoluntaryCommunityServices.Add(new VoluntaryCommunityModel
                    {
                        Position = appointment.RawPosition,
                        Institution = appointment.RawInstitution,
                        FromYear = Convert.ToInt32(appointment.FromYear),
                        ToYear = Convert.ToInt32(appointment.ToYear),
                        Priority = person.VoluntaryCommunityServices.Select(vc => vc.Priority).FirstOrDefault() + 1,
                        Cycle = appointment.CycleId,
                        IsImamatAppointee = true
                    });
            }

            return View(await MapPerson(person));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PersonModel model)
        {
            try
            {
                var formCollection = await HttpContext.Request.ReadFormAsync().ConfigureAwait(false);
                if (!string.IsNullOrWhiteSpace(formCollection["RelocationDateTime"]))
                    model.RelocationDateTime =
                        DateTime.ParseExact(formCollection["RelocationDateTime"], "MM/dd/yyyy", null);

                if (!string.IsNullOrWhiteSpace(formCollection["DateOfBirth"]))
                    model.DateOfBirth = DateTime.ParseExact(formCollection["DateOfBirth"], "MM/dd/yyyy", null);

                if (ModelState.IsValid)
                {
                    if (model.ImageUpload != null)
                        using (var memoryStream = new MemoryStream())
                        {
                            await model.ImageUpload.CopyToAsync(memoryStream);
                            model.Image = Convert.ToBase64String(memoryStream.ToArray());
                        }

                    RestoreSessionDataToModel(model);

                    var success =
                        await new RestfulClient(HttpContext.Session
                            .Get<AuthenticationResponse>("AuthenticationResponse")?.Token).EditPersonData(model);
                    if (success)
                    {
                        TempData["MessageType"] = MessageTypes.Success;
                        TempData["Message"] = Messages.SuccessUserUpdate;

                        ViewBag.MessageType = MessageTypes.Success;
                        ViewBag.Message = Messages.SuccessUserUpdate;

                        return RedirectToAction("Edit", "Persons", new {area = AreaNames.Operations, id = model.Id});

                        //return RedirectToAction("Index");
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
        public IActionResult EmploymentListAdd(string id, string nameOfOrganization, string category,
            string designation,
            string location, string employmentEmailAddress, string employmentTelephone, string typeOfBusiness,
            string natureOfBusiness, string natureOfBusinessOther, string employmentStartDate, string employmentEndDate)
        {
            var sessionEmploymentList = AddEmploymentToSession(id, nameOfOrganization, category, designation, location,
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
        public async Task<IActionResult> FamilyRelationListAdd(string id, string relativeCnic,
            string relativeSalutation,
            string relativeFirstName, string relativeFathersName, string relativeFamilyName, string relativeJamatiTitle,
            string relativeDateOfBirth, string relativeRelation, string personId, string relativeCycle,
            string relativeInstitution, string relativePosition)
        {
            ViewBag.Cycle =
                await new RestfulClient(
                    HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetCycles();
            ViewBag.VoluntaryCommunityPositionList =
                await new RestfulClient(
                    HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetPositions();
            ViewBag.VoluntaryCommunityInstitutionList =
                await new RestfulClient(
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token)
                    .GetPositionInstitution();

            var sessionFamilyRelationList = await AddFamilyRelationToSession(id, relativeCnic, relativeSalutation,
                relativeFirstName, relativeFathersName,
                relativeFamilyName, relativeJamatiTitle, relativeDateOfBirth, relativeRelation, personId, string.Empty,
                string.Empty, string.Empty, relativeCycle, relativeInstitution, relativePosition);

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
            var list = await new RestfulClient(HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")
                ?.Token).GetJamatkhana(uid);

            return new JsonResult(list);
        }

        public async Task<JsonResult> GetLocalCouncil(string uid)
        {
            //var list = new List<SelectListItem> {new SelectListItem {Text = "Karimabad", Value = "Karimabad"}};
            var list = await new RestfulClient(HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")
                ?.Token).GetLocalCouncil(uid);

            return new JsonResult(list);
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.MessageType = TempData["MessageType"];
            ViewBag.Message = TempData["Message"];
            //return View(new List<PersonModel>());

            HttpContext.Session.SetString(SessionKeyDoNotValidateCnicOnEditPage, "false");
            HttpContext.Session.SetString(SessionKeyDoNotValidateFormNumberOnEditPage, "false");

            ViewBag.JamatiTitleList =
                await new RestfulClient(
                    HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetJamatiTitles();
            ViewBag.InstitutionList =
                await new RestfulClient(
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token)
                    .GetAllInstitutions();
            ViewBag.NameOfDegreeList =
                await new RestfulClient(
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token)
                    .GetEducationalDegree();
            ViewBag.MajorAreaOfStudy =
                await new RestfulClient(
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token)
                    .GetMajorAreaOfStudy();

            //return View(new IndexPersonModel { Persons = await new RestfulClient(HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).getPersonDetails() });
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string formNumber, string cnic, string name, string jamatiTitle,
            string degree,
            string majorAreaOfStudy, string academicInstitution)
        {
            if (cnic == null && name == null && formNumber == null)
            {
            }

            ViewBag.JamatiTitleList =
                await new RestfulClient(
                    HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetJamatiTitles();
            ViewBag.InstitutionList =
                await new RestfulClient(
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token)
                    .GetAllInstitutions();
            ViewBag.NameOfDegreeList =
                await new RestfulClient(
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token)
                    .GetEducationalDegree();
            ViewBag.MajorAreaOfStudy =
                await new RestfulClient(
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token)
                    .GetMajorAreaOfStudy();

            ViewBag.Search = true;

            //var persons = await new RestfulClient(HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).getPersonDetailsThroughPagging(firstName,lastName,cnic,1, 1);
            return View(new IndexPersonModel
            {
                //Persons = await new RestfulClient(HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).searchPerson(cnic, firstName, lastName),
                //Persons = persons.Item1,
                Cnic = cnic,
                Name = name,
                FormNumber = formNumber,
                JamatiTitle = jamatiTitle,
                Degree = degree,
                MajorAreaOfStudy = majorAreaOfStudy,
                AcademicInstitution = academicInstitution
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
            string countryOfTarining, string month, string year, string date)
        {
            var sessionProfessionalTrainingList =
                AddProfessionalTrainingToSession(id, training, institution, countryOfTarining, month, year, date);

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

        [HttpPost]
        public IActionResult ReorderAkdnTraining(string primaryId, string primaryPosition, string secondaryId,
            string secondaryPosition)
        {
            var sessionAkdnTrainingList =
                ReOrderAkdnTrainingInSession(primaryId, primaryPosition, secondaryId, secondaryPosition);

            return PartialView("_AkdnTrainingTablePartial", sessionAkdnTrainingList);
        }

        [HttpPost]
        public IActionResult ReorderEducation(string primaryId, string primaryPosition, string secondaryId,
            string secondaryPosition)
        {
            var sessionEducationList =
                ReOrderEducationInSession(primaryId, primaryPosition, secondaryId, secondaryPosition);

            return PartialView("_EducationTablePartial", sessionEducationList);
        }

        [HttpPost]
        public IActionResult ReorderEmployment(string primaryId, string primaryPosition, string secondaryId,
            string secondaryPosition)
        {
            var employmentInSession =
                ReorderEmploymentInSession(primaryId, primaryPosition, secondaryId, secondaryPosition);

            return PartialView("_EmploymentTablePartial", employmentInSession);
        }

        [HttpPost]
        public IActionResult ReorderLanguage(string primaryId, string primaryPosition, string secondaryId,
            string secondaryPosition)
        {
            var sessionLanguageList =
                ReOrderLanguageInSession(primaryId, primaryPosition, secondaryId, secondaryPosition);

            return PartialView("_LanguageTablePartial", sessionLanguageList);
        }

        [HttpPost]
        public IActionResult ReorderProfessionalTraining(string primaryId, string primaryPosition, string secondaryId,
            string secondaryPosition)
        {
            var sessionProfessionalTrainingList =
                ReOrderProfessionalTrainingInSession(primaryId, primaryPosition, secondaryId, secondaryPosition);

            return PartialView("_ProfessionalTrainingTablePartial", sessionProfessionalTrainingList);
        }

        [HttpPost]
        public IActionResult ReorderVoluntaryCommunityService(string primaryId, string primaryPosition,
            string secondaryId, string secondaryPosition)
        {
            var voluntaryCommunityServiceInSession =
                ReorderVoluntaryCommunityServiceInSession(primaryId, primaryPosition, secondaryId, secondaryPosition);

            return PartialView("_VoluntaryCommunityTablePartial", voluntaryCommunityServiceInSession);
        }

        [HttpPost]
        public IActionResult ReorderVoluntaryPublicService(string primaryId, string primaryPosition, string secondaryId,
            string secondaryPosition)
        {
            var voluntaryPublicServiceInSession =
                ReorderVoluntaryPublicServiceInSession(primaryId, primaryPosition, secondaryId, secondaryPosition);

            return PartialView("_VoluntaryPublicTablePartial", voluntaryPublicServiceInSession);
        }

        public async Task<IActionResult> ServerSideAjaxHandler(IndexPersonModel searchingData)
        {
            try
            {
                var name = searchingData.Name;
                var cnic = searchingData.Cnic;
                var formNumber = searchingData.FormNumber;
                var jamatiTitle = searchingData.JamatiTitle;
                var degree = searchingData.Degree;
                var majorAreaOfStudy = searchingData.MajorAreaOfStudy;
                var academicIstitution = searchingData.AcademicInstitution;

                var queryCollection = Request.Query; //HttpContext.Request.Query;
                // Initialization.
                var search = queryCollection["search[value]"][0];
                var draw = queryCollection["draw"][0];
                //string order = form["order[0][column]"][0];
                //string orderDir = form["order[0][dir]"][0];
                var startRec = Convert.ToInt32(queryCollection["start"][0]);
                var pageSize = Convert.ToInt32(queryCollection["length"][0]);

                var tupleData =
                    await new RestfulClient(HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")
                        ?.Token).GetPersonDetailsThroughPagging(name, cnic,
                        formNumber, jamatiTitle, degree, majorAreaOfStudy, academicIstitution, startRec / pageSize + 1,
                        pageSize);

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
                        n.Id,
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

        public async Task<IActionResult> ValidateCnic(string cnic)
        {
            var doNotValidateCnic = HttpContext.Session.GetString(SessionKeyDoNotValidateCnicOnEditPage);

            if (doNotValidateCnic == "true") return Json("true");

            var success =
                await new RestfulClient(
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token)
                    .GetPersonDetailsThroughPagging(string.Empty, cnic, string.Empty, string.Empty, string.Empty,
                        string.Empty, string.Empty, 1, 1);
            var list = success.Item1;

            return Json(!list.Any() ? "true" : string.Format("A record against {0} already exists.", cnic));
        }

        public async Task<IActionResult> ValidateFormNumber(string formnumber)
        {
            var doNotValidateFormNumber = HttpContext.Session.GetString(SessionKeyDoNotValidateFormNumberOnEditPage);

            //if (doNotValidateFormNumber == "true")
            //{
            //return Json("true");
            //}
            //else
            //{
            //    var success = new RestfulClient(HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).searchByFormNumber(cnic, out var person);
            //    return Json(!success ? "true" : string.Format("A record against {0} already exists.", formnumber));
            //}

            if (doNotValidateFormNumber == "true") return Json("true");

            var success =
                await new RestfulClient(
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token)
                    .GetPersonDetailsThroughPagging(string.Empty, string.Empty, formnumber, string.Empty, string.Empty,
                        string.Empty, string.Empty, 1, 1);
            var list = success.Item1;

            return Json(!list.Any() ? "true" : string.Format("A record against {0} already exists.", formnumber));
        }

        public async Task<IActionResult> ValidateId(string id)
        {
            var doNotValidateFormNumber = HttpContext.Session.GetString(SessionKeyDoNotValidateFormNumberOnEditPage);

            //if (doNotValidateFormNumber == "true")
            //{
            //return Json("true");
            //}
            //else
            //{
            //    var success = new RestfulClient(HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).searchByFormNumber(cnic, out var person);
            //    return Json(!success ? "true" : string.Format("A record against {0} already exists.", formnumber));
            //}

            if (doNotValidateFormNumber == "true") return Json("true");

            var success =
                await new RestfulClient(
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token)
                    .GetPersonDetailsThroughPagging(string.Empty, string.Empty, id, string.Empty, string.Empty,
                        string.Empty, string.Empty, 1, 1);
            var list = success.Item1;

            return Json(!list.Any() ? "true" : string.Format("A record against {0} already exists.", id));
        }

        [HttpPost]
        public async Task<IActionResult> VerifyCnic(string cnic)
        {
            var success =
                await new RestfulClient(
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token)
                    .GetPersonDetailsThroughPagging(string.Empty, cnic, string.Empty, string.Empty, string.Empty,
                        string.Empty, string.Empty, 1, 1);
            ViewBag.SalutationList =
                await new RestfulClient(
                    HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetSalutation();
            ViewBag.JamatiTitleList =
                await new RestfulClient(
                    HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetJamatiTitles();
            ViewBag.RelationList =
                await new RestfulClient(
                    HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetAllRelatives();
            ViewBag.Cycle =
                await new RestfulClient(
                    HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetCycles();
            ViewBag.VoluntaryCommunityPositionList =
                await new RestfulClient(
                    HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetPositions();
            ViewBag.VoluntaryCommunityInstitutionList =
                await new RestfulClient(
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token)
                    .GetPositionInstitution();

            var person = success.Item1.FirstOrDefault();
            if (person != null)
            {
                person.RelativeCnic = person.Cnic;
                person.RelativeSalutation = person.Salutation;
                person.RelativeFirstName = person.FirstName;
                person.RelativeFathersName = person.FathersName;
                person.RelativeFamilyName = person.FamilyName;
                person.RelativeJamatiTitle = person.JamatiTitle;
                person.RelativeDateOfBirth = person.DateOfBirth;
                person.RelativePersonId = person.Id;
                person.RelativeFormNumber = person.Id;
            }

            return PartialView("_FamilyRelationPartial", person == null ? new PersonModel {RelativeCnic = cnic} : null);
        }

        [HttpPost]
        public IActionResult VoluntaryCommunityListAdd(string id, string institution, string fromYear,
            string toYear, string position, string cycle)
        {
            var sessionVoluntaryCommunityList =
                AddVoluntaryCommunityToSession(id, institution, fromYear, toYear, position, cycle);

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
                MajorAreaOfStudy = string.IsNullOrWhiteSpace(majorAreaOfStudy)
                    ? string.Empty
                    : majorAreaOfStudy.Split('-')[0],
                MajorAreaOfStudyName = string.IsNullOrWhiteSpace(majorAreaOfStudy)
                    ? string.Empty
                    : majorAreaOfStudy.Split('-')[1],
                NameOfDegree = string.IsNullOrWhiteSpace(nameOfDegree) ? string.Empty : nameOfDegree.Split('-')[0],
                NameOfDegreeName = string.IsNullOrWhiteSpace(nameOfDegree) ? string.Empty : nameOfDegree.Split('-')[1],
                ToYear = string.IsNullOrWhiteSpace(toYear) ? (int?) null : Convert.ToInt32(toYear)
            });

            for (var counter = 0; counter < sessionEducationList.Count; counter++)
                sessionEducationList[counter].Priority = counter + 1;

            HttpContext.Session.Set("EducationList", sessionEducationList);
            return sessionEducationList;
        }

        private List<EmploymentModel> AddEmploymentToSession(string id, string nameOfOrganization, string category,
            string designation,
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
                Category = category,
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

        private async Task<List<FamilyRelationModel>> AddFamilyRelationToSession(string id, string relativeCnic,
            string relativeSalutation,
            string relativeFirstName, string relativeFathersName, string relativeFamilyName, string relativeJamatiTitle,
            string relativeDateOfBirth, string relativeRelation, string personId, string position = "",
            string cycle = "", string institution = "", string relativeCycle = "", string relativeInstitution = "",
            string relativePosition = "")
        {
            var sessionFamilyRelationList = HttpContext.Session.Get<List<FamilyRelationModel>>("FamilyRelationList") ??
                                            new List<FamilyRelationModel>();

            if (string.IsNullOrWhiteSpace(id))
                id = Guid.NewGuid().ToString();
            else
                sessionFamilyRelationList.Remove(sessionFamilyRelationList.Find(e => e.FamilyRelationId == id));

            if (!string.IsNullOrWhiteSpace(personId))
                if (string.IsNullOrWhiteSpace(position) && string.IsNullOrWhiteSpace(cycle))
                {
                    var appointment = (await GetPastImamatAppointments(personId, true)).OrderByDescending(a => a.ToYear)
                        .FirstOrDefault();
                    if (appointment != null)
                    {
                        cycle = appointment.Cycle;
                        position = appointment.Position;
                        institution = appointment.Institution;
                    }
                }

            try
            {
                string institutionName = GetText(relativeInstitution, ViewBag.VoluntaryCommunityInstitutionList);
                string positionName = GetText(relativePosition, ViewBag.VoluntaryCommunityPositionList);
                string cycleName = GetText(relativeCycle, ViewBag.Cycle);

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
                    Relation = string.IsNullOrWhiteSpace(relativeRelation)
                        ? string.Empty
                        : relativeRelation.Split('-')[0],
                    Salutation = relativeSalutation,
                    Id = personId,
                    Cycle = string.IsNullOrWhiteSpace(relativeCycle) ? cycle : cycleName,
                    Position = string.IsNullOrWhiteSpace(relativePosition) ? position : positionName,
                    Institution = string.IsNullOrWhiteSpace(relativeInstitution) ? institution : institutionName,
                    VoluntaryCommunityServices = !string.IsNullOrWhiteSpace(personId)
                        ? null
                        : new List<VoluntaryCommunityModel>
                        {
                            new VoluntaryCommunityModel
                            {
                                Priority = 1,
                                IsImamatAppointee = true,
                                Cycle = relativeCycle,
                                Institution = relativeInstitution.Split('-')[0],
                                Position = relativePosition.Split('-')[0],
                                FromYear = Convert.ToInt32(cycleName.Split('-')[0]),
                                ToYear = Convert.ToInt32(cycleName.Split('-')[1])
                            }
                        }
                });
            }
            catch (Exception)
            {
            }

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
            string countryOfTarining, string month, string year, string date)
        {
            var sessionProfessionalTrainingList =
                HttpContext.Session.Get<List<ProfessionalTrainingModel>>("ProfessionalTrainingList") ??
                new List<ProfessionalTrainingModel>();

            if (string.IsNullOrWhiteSpace(id))
                id = Guid.NewGuid().ToString();
            else
                sessionProfessionalTrainingList.Remove(sessionProfessionalTrainingList.Find(e => e.TrainingId == id));

            DateTime? dt = null;
            try
            {
                if (!string.IsNullOrWhiteSpace(date)) dt = DateTime.ParseExact(date, "MMMM-yyyy", null);
            }
            catch (Exception)
            {
                dt = Convert.ToDateTime(date);
            }

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
                //Month = month.Contains('-') ? month.Split('-')[0] : month,
                //MonthName = GetMonthName(month.Contains('-') ? month.Split('-')[0] : month),
                Training = training,
                //Year = string.IsNullOrWhiteSpace(year) ? (int?) null : Convert.ToInt32(year),
                Date = dt
            });

            for (var counter = 0; counter < sessionProfessionalTrainingList.Count; counter++)
                sessionProfessionalTrainingList[counter].Priority = counter + 1;

            HttpContext.Session.Set("ProfessionalTrainingList", sessionProfessionalTrainingList);

            return sessionProfessionalTrainingList;
        }

        private List<VoluntaryCommunityModel> AddVoluntaryCommunityToSession(string id, string institution,
            string fromYear,
            string toYear, string position, string cycle)
        {
            var sessionVoluntaryCommunityList =
                HttpContext.Session.Get<List<VoluntaryCommunityModel>>("VoluntaryCommunityList") ??
                new List<VoluntaryCommunityModel>();

            if (cycle.StartsWith("0")) cycle = string.Empty;

            if (string.IsNullOrWhiteSpace(id))
                id = Guid.NewGuid().ToString();
            else
                sessionVoluntaryCommunityList.Remove(
                    sessionVoluntaryCommunityList.Find(e => e.VoluntaryCommunityId == id));

            sessionVoluntaryCommunityList.Add(new VoluntaryCommunityModel
            {
                VoluntaryCommunityId = id,
                FromYear = string.IsNullOrWhiteSpace(fromYear)
                    ? string.IsNullOrWhiteSpace(cycle)
                        ? (int?) null
                        : Convert.ToInt32(cycle.Split('|')[1].Split('-')[0])
                    : Convert.ToInt32(fromYear),
                Institution = string.IsNullOrWhiteSpace(institution) ? string.Empty : institution.Split('-')[0],
                InstitutionName = string.IsNullOrWhiteSpace(institution) ? string.Empty : institution.Split('-')[1],
                ToYear = string.IsNullOrWhiteSpace(toYear)
                    ? string.IsNullOrWhiteSpace(cycle)
                        ? (int?) null
                        : Convert.ToInt32(cycle.Split('|')[1].Split('-')[1])
                    : Convert.ToInt32(toYear),
                Position = string.IsNullOrWhiteSpace(position) ? string.Empty : position.Split('-')[0],
                PositionName = string.IsNullOrWhiteSpace(position) ? string.Empty : position.Split('-')[1],
                IsImamatAppointee = !string.IsNullOrWhiteSpace(cycle),
                Cycle = string.IsNullOrWhiteSpace(cycle) ? string.Empty : cycle.Split('|')[0],
                CycleName = string.IsNullOrWhiteSpace(cycle) ? string.Empty : cycle.Split('|')[1]
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
            if (id == "1") return "January";

            if (id == "2") return "February";

            if (id == "3") return "March";

            if (id == "4") return "April";

            if (id == "5") return "May";

            if (id == "6") return "June";

            if (id == "7") return "July";

            if (id == "8") return "August";

            if (id == "9") return "September";

            if (id == "10") return "October";

            if (id == "11") return "November";

            return id == "12" ? "December" : "";
        }

        private async Task<List<PastImamatAppointment>> GetPastImamatAppointments(string personId,
            bool forFamily = false)
        {
            var pastAppointments = new List<PastImamatAppointment>();

            var appointments =
                await new RestfulClient(
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token)
                    .GetAppointments(personId, true);
            if (appointments?.Count > 0 && appointments.Any(a => a.Active))
                pastAppointments.AddRange(appointments.Where(a => a.Active)
                    .Select(pastAppointment => new PastImamatAppointment
                    {
                        Position = pastAppointment?.Position?.Name,
                        Cycle = pastAppointment?.CycleName,
                        CycleId = pastAppointment?.CycleId?.Id.ToString(),
                        RawPosition = pastAppointment?.Position?.Id.ToString(),
                        RawInstitution = pastAppointment?.Institution?.Id.ToString(),
                        FromYear = pastAppointment?.CycleId?.StartDate.Year.ToString(),
                        ToYear = pastAppointment?.CycleId?.EndDate.Year.ToString(),
                        Institution = pastAppointment?.Institution?.Name
                    }));

            if (forFamily)
            {
                var person =
                    await new RestfulClient(HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")
                        ?.Token).GetPersonDetailsById(personId);

                if (person.VoluntaryCommunityServices != null)
                    pastAppointments.AddRange(
                        from personVoluntaryCommunityService in person.VoluntaryCommunityServices?.Where(vcs =>
                            vcs.IsImamatAppointee || !string.IsNullOrWhiteSpace(vcs.Cycle))
                        select new PastImamatAppointment
                        {
                            Position = GetText(personVoluntaryCommunityService.Position,
                                ViewBag.VoluntaryCommunityPositionList),
                            Cycle =
                                $"{personVoluntaryCommunityService.FromYear}-{personVoluntaryCommunityService.ToYear}",
                            CycleId = personVoluntaryCommunityService.Cycle,
                            RawPosition = personVoluntaryCommunityService.Position,
                            RawInstitution = personVoluntaryCommunityService.Institution,
                            FromYear = personVoluntaryCommunityService.FromYear?.ToString(),
                            ToYear = personVoluntaryCommunityService.ToYear?.ToString(),
                            Institution = GetText(personVoluntaryCommunityService.Institution,
                                ViewBag.VoluntaryCommunityInstitutionList)
                        });
            }

            return pastAppointments;
        }

        private string GetText(string id, List<SelectListItem> list)
        {
            if (list == null) return string.Empty;

            if (id.Contains('-')) id = id.Split('-')[0];

            return list.FirstOrDefault(l =>
                (string.IsNullOrWhiteSpace(l.Value) ? string.Empty : l.Value.Split('-')[0]) == id)?.Text;
        }

        private async Task InitializePerson()
        {
            ViewBag.SalutationList =
                await new RestfulClient(
                    HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetSalutation();
            ViewBag.JamatiTitleList =
                await new RestfulClient(
                    HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetJamatiTitles();
            ViewBag.MaritalStatusList =
                await new RestfulClient(
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token)
                    .GetMartialStatuses();
            ViewBag.CityList =
                await new RestfulClient(
                    HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetCities();
            ViewBag.AreaOfOriginList =
                await new RestfulClient(
                    HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetAreaOfOrigin();
            ViewBag.InstitutionList =
                await new RestfulClient(
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token)
                    .GetAllInstitutions();
            ViewBag.NameOfDegreeList =
                await new RestfulClient(
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token)
                    .GetEducationalDegree();
            ViewBag.ReligiousEducationList =
                await new RestfulClient(
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token)
                    .GetReligiousEducation();
            ViewBag.RegionalCouncilList =
                await new RestfulClient(
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token)
                    .GetRegionalCouncil();

            var listOfCountries =
                await new RestfulClient(
                    HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetAllCountries();
            ViewBag.CountryOfStudyList = listOfCountries;
            ViewBag.AkdnTrainingCountryList = listOfCountries;
            ViewBag.ProfessionalTrainingCountryList = listOfCountries;

            var listOfLanguageProficiency =
                await new RestfulClient(
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token)
                    .GetLanguageProficiency();
            ViewBag.Proficiency = listOfLanguageProficiency;

            ViewBag.VoluntaryCommunityPositionList =
                await new RestfulClient(
                    HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetPositions();
            ViewBag.HighestLevelOfStudyList =
                await new RestfulClient(
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token)
                    .GetHighestLevelOfStudy();
            ViewBag.AkdnTrainingList =
                await new RestfulClient(
                    HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetAkdnTraining();
            //ViewBag.VoluntaryCommunityInstitutionList = await new RestfulClient(HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetVoluntaryInstitution();
            ViewBag.VoluntaryCommunityInstitutionList =
                await new RestfulClient(
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token)
                    .GetPositionInstitution();
            ViewBag.FieldOfInterestsList =
                await new RestfulClient(
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token)
                    .GetFieldOfInterests();
            ViewBag.OccupationTypeList =
                await new RestfulClient(
                    HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetOcupations();
            ViewBag.TypeOfBusinessList =
                await new RestfulClient(
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token)
                    .GetBussinessType();
            ViewBag.NatureOfBusinessList =
                await new RestfulClient(
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token)
                    .GetBussinessNature();
            ViewBag.ProfessionalMembershipsList =
                await new RestfulClient(
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token)
                    .GetProfessionalMemeberShipDetails();
            ViewBag.LanguageList =
                await new RestfulClient(
                    HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetLanguages();
            ViewBag.SkillsList =
                await new RestfulClient(
                    HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetSkills();
            ViewBag.RelationList =
                await new RestfulClient(
                    HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetAllRelatives();
            ViewBag.MajorAreaOfStudy =
                await new RestfulClient(
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token)
                    .GetMajorAreaOfStudy();
            ViewBag.FieldOfExpertiseList =
                await new RestfulClient(
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token)
                    .GetFieldOfExpertise();

            ViewBag.Cycle =
                await new RestfulClient(
                    HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetCycles();
        }

        private async Task<PersonModel> MapPerson(PersonModel person)
        {
            ResetSession();
            //saif ali write mapping here
            if (person != null)
            {
                try
                {
                    HttpContext.Session.Set("Image", person.Image);

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

                        person.Educations = HttpContext.Session.Get<List<EducationModel>>("EducationList");
                    }
                }
                catch (Exception)
                {
                }

                try
                {
                    if (person.AkdnTrainings != null)
                    {
                        foreach (var akdnTraining in person.AkdnTrainings)
                        {
                            string training = GetText(akdnTraining.Training, ViewBag.AkdnTrainingList);
                            string country = GetText(akdnTraining.CountryOfTraining, ViewBag.AkdnTrainingCountryList);
                            var month = GetMonthName(akdnTraining.Month);

                            akdnTraining.TrainingName = training;
                            akdnTraining.CountryOfTrainingName = country;
                            //akdnTraining.MonthName = month;

                            AddAkdnTrainingToSession(akdnTraining.TrainingId,
                                akdnTraining.Training + "-" + akdnTraining.TrainingName,
                                akdnTraining.CountryOfTraining + "-" + akdnTraining.CountryOfTrainingName,
                                akdnTraining.Date?.ToString());
                        }

                        person.AkdnTrainings = HttpContext.Session.Get<List<AkdnTrainingModel>>("AkdnTrainingList");
                    }
                }
                catch (Exception)
                {
                }

                try
                {
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

                            AddProfessionalTrainingToSession(professionalTraining.TrainingId,
                                professionalTraining.Training,
                                professionalTraining.Institution,
                                professionalTraining.CountryOfTraining + "-" +
                                professionalTraining.CountryOfTrainingName,
                                professionalTraining.Month + "-" + professionalTraining.MonthName,
                                professionalTraining.Year?.ToString(), professionalTraining.Date.ToString());
                        }

                        person.ProfessionalTrainings =
                            HttpContext.Session.Get<List<ProfessionalTrainingModel>>("ProfessionalTrainingList");
                    }
                }
                catch (Exception)
                {
                }

                try
                {
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
                                language.Language + "-" + language.LanguageName,
                                language.Read + "-" + language.ReadName,
                                language.Write + "-" + language.WriteName, language.Speak + "-" + language.SpeakName);
                        }

                        person.LanguageProficiencies =
                            HttpContext.Session.Get<List<LanguageProficiencyModel>>("LanguageList");
                    }
                }
                catch (Exception)
                {
                }

                try
                {
                    if (person.VoluntaryCommunityServices != null)
                    {
                        foreach (var voluntaryService in person.VoluntaryCommunityServices)
                        {
                            string institutionName = GetText(voluntaryService.Institution,
                                ViewBag.VoluntaryCommunityInstitutionList);
                            string position = GetText(voluntaryService.Position,
                                ViewBag.VoluntaryCommunityPositionList);

                            voluntaryService.InstitutionName = institutionName;
                            voluntaryService.PositionName = position;

                            var cycle = string.IsNullOrWhiteSpace(voluntaryService.Cycle)
                                ? string.Empty
                                : $"{voluntaryService.Cycle}|{voluntaryService.FromYear}-{voluntaryService.ToYear}";

                            AddVoluntaryCommunityToSession(voluntaryService.VoluntaryCommunityId,
                                voluntaryService.Institution + "-" + voluntaryService.InstitutionName,
                                voluntaryService.FromYear?.ToString(), voluntaryService.ToYear?.ToString(),
                                voluntaryService.Position + "-" + voluntaryService.PositionName, cycle);
                        }

                        person.VoluntaryCommunityServices =
                            HttpContext.Session.Get<List<VoluntaryCommunityModel>>("VoluntaryCommunityList");
                    }
                }
                catch (Exception)
                {
                }

                try
                {
                    if (person.VoluntaryPublicServices != null)
                    {
                        foreach (var voluntaryService in person.VoluntaryPublicServices)
                            AddVoluntaryPublicToSession(voluntaryService.VoluntaryPublicId,
                                voluntaryService.Institution,
                                voluntaryService.FromYear?.ToString(), voluntaryService.ToYear?.ToString(),
                                voluntaryService.Position);

                        person.VoluntaryPublicServices =
                            HttpContext.Session.Get<List<VoluntaryPublicModel>>("VoluntaryPublicList");
                    }
                }
                catch (Exception)
                {
                }

                try
                {
                    if (person.Employments != null)
                    {
                        foreach (var employment in person.Employments)
                        {
                            string businessNature = GetText(employment.NatureOfBusiness, ViewBag.NatureOfBusinessList);
                            string businessType = GetText(employment.TypeOfBusiness, ViewBag.TypeOfBusinessList);

                            employment.TypeOfBusinessName = businessType;
                            employment.NatureOfBusinessName = businessNature;

                            AddEmploymentToSession(employment.EmploymentId, employment.NameOfOrganization,
                                employment.Category,
                                employment.Designation, employment.Location,
                                employment.EmploymentEmailAddress, employment.EmploymentTelephone,
                                employment.TypeOfBusiness + "-" + employment.TypeOfBusinessName,
                                employment.NatureOfBusiness + "-" + employment.NatureOfBusinessName,
                                employment.NatureOfBusinessOther,
                                employment.EmploymentStartDate?.ToString(), employment.EmploymentEndDate?.ToString());
                        }

                        person.Employments = HttpContext.Session.Get<List<EmploymentModel>>("EmploymentList");
                    }
                }
                catch (Exception)
                {
                }

                try
                {
                    if (person.FamilyRelations != null)
                    {
                        foreach (var relation in person.FamilyRelations)
                        {
                            string relationName = GetText(relation.Relation, ViewBag.RelationList);
                            relation.RelationName = relationName;

                            var appointment = (await GetPastImamatAppointments(relation.Id, true))
                                .OrderByDescending(a => a.ToYear).FirstOrDefault();
                            if (appointment != null)
                            {
                                var cycle = appointment.Cycle;
                                var position = appointment.Position;
                                var institution = appointment.Institution;

                                relation.Position = position;
                                relation.Cycle = cycle;
                                relation.Institution = institution;
                            }

                            await AddFamilyRelationToSession(relation.FamilyRelationId, relation.Cnic,
                                relation.Salutation,
                                relation.FirstName, relation.FathersName,
                                relation.FamilyName, relation.JamatiTitle, relation.DateOfBirth.ToString(),
                                relation.Relation + "-" + relation.RelationName, relation.Id, relation.Position,
                                relation.Cycle, relation.Institution);
                        }

                        person.FamilyRelations =
                            HttpContext.Session.Get<List<FamilyRelationModel>>("FamilyRelationList");
                    }
                }
                catch (Exception)
                {
                }
            }

            return person;
        }

        private List<AkdnTrainingModel> ReOrderAkdnTrainingInSession(string primaryId, string primaryPosition,
            string secondaryId, string secondaryPosition)
        {
            var sessionAkdnTrainingList = HttpContext.Session.Get<List<AkdnTrainingModel>>("AkdnTrainingList") ??
                                          new List<AkdnTrainingModel>();

            sessionAkdnTrainingList.FirstOrDefault(e => e.TrainingId == primaryId).Priority =
                Convert.ToInt32(primaryPosition);
            sessionAkdnTrainingList.FirstOrDefault(e => e.TrainingId == secondaryId).Priority =
                Convert.ToInt32(secondaryPosition);

            var counter = Convert.ToInt32(secondaryPosition) + 1;
            foreach (var sessionValue in sessionAkdnTrainingList.Where(se =>
                se.Priority >= Convert.ToInt32(secondaryPosition) && se.TrainingId != secondaryId))
                sessionValue.Priority = counter++;

            HttpContext.Session.Set("AkdnTrainingList", sessionAkdnTrainingList);
            return sessionAkdnTrainingList;
        }

        private List<EducationModel> ReOrderEducationInSession(string primaryId, string primaryPosition,
            string secondaryId, string secondaryPosition)
        {
            var sessionEducationList = HttpContext.Session.Get<List<EducationModel>>("EducationList") ??
                                       new List<EducationModel>();

            sessionEducationList.FirstOrDefault(e => e.EducationId == primaryId).Priority =
                Convert.ToInt32(primaryPosition);
            sessionEducationList.FirstOrDefault(e => e.EducationId == secondaryId).Priority =
                Convert.ToInt32(secondaryPosition);

            var counter = Convert.ToInt32(secondaryPosition) + 1;
            foreach (var sessionValue in sessionEducationList.Where(se =>
                se.Priority >= Convert.ToInt32(secondaryPosition) && se.EducationId != secondaryId))
                sessionValue.Priority = counter++;

            HttpContext.Session.Set("EducationList", sessionEducationList);
            return sessionEducationList;
        }

        private List<EmploymentModel> ReorderEmploymentInSession(string primaryId, string primaryPosition,
            string secondaryId, string secondaryPosition)
        {
            var employmentModels = HttpContext.Session.Get<List<EmploymentModel>>("EmploymentList") ??
                                   new List<EmploymentModel>();

            employmentModels.FirstOrDefault(e => e.EmploymentId == primaryId).Priority =
                Convert.ToInt32(primaryPosition);
            employmentModels.FirstOrDefault(e => e.EmploymentId == secondaryId).Priority =
                Convert.ToInt32(secondaryPosition);

            var counter = Convert.ToInt32(secondaryPosition) + 1;
            foreach (var sessionValue in employmentModels.Where(se =>
                se.Priority >= Convert.ToInt32(secondaryPosition) && se.EmploymentId != secondaryId))
                sessionValue.Priority = counter++;

            HttpContext.Session.Set("EmploymentList", employmentModels);
            return employmentModels;
        }

        private List<LanguageProficiencyModel> ReOrderLanguageInSession(string primaryId, string primaryPosition,
            string secondaryId, string secondaryPosition)
        {
            var sessionLanguageList = HttpContext.Session.Get<List<LanguageProficiencyModel>>("LanguageList") ??
                                      new List<LanguageProficiencyModel>();

            sessionLanguageList.FirstOrDefault(e => e.LanguageProficiencyId == primaryId).Priority =
                Convert.ToInt32(primaryPosition);
            sessionLanguageList.FirstOrDefault(e => e.LanguageProficiencyId == secondaryId).Priority =
                Convert.ToInt32(secondaryPosition);

            var counter = Convert.ToInt32(secondaryPosition) + 1;
            foreach (var sessionValue in sessionLanguageList.Where(se =>
                se.Priority >= Convert.ToInt32(secondaryPosition) && se.LanguageProficiencyId != secondaryId))
                sessionValue.Priority = counter++;

            HttpContext.Session.Set("LanguageList", sessionLanguageList);
            return sessionLanguageList;
        }

        private List<ProfessionalTrainingModel> ReOrderProfessionalTrainingInSession(string primaryId,
            string primaryPosition, string secondaryId, string secondaryPosition)
        {
            var sessionProfessionalTrainingList =
                HttpContext.Session.Get<List<ProfessionalTrainingModel>>("ProfessionalTrainingList") ??
                new List<ProfessionalTrainingModel>();

            sessionProfessionalTrainingList.FirstOrDefault(e => e.TrainingId == primaryId).Priority =
                Convert.ToInt32(primaryPosition);
            sessionProfessionalTrainingList.FirstOrDefault(e => e.TrainingId == secondaryId).Priority =
                Convert.ToInt32(secondaryPosition);

            var counter = Convert.ToInt32(secondaryPosition) + 1;
            foreach (var sessionValue in sessionProfessionalTrainingList.Where(se =>
                se.Priority >= Convert.ToInt32(secondaryPosition) && se.TrainingId != secondaryId))
                sessionValue.Priority = counter++;

            HttpContext.Session.Set("ProfessionalTrainingList", sessionProfessionalTrainingList);
            return sessionProfessionalTrainingList;
        }

        private List<VoluntaryCommunityModel> ReorderVoluntaryCommunityServiceInSession(string primaryId,
            string primaryPosition, string secondaryId, string secondaryPosition)
        {
            var voluntaryCommunityModels =
                HttpContext.Session.Get<List<VoluntaryCommunityModel>>("VoluntaryCommunityList") ??
                new List<VoluntaryCommunityModel>();

            voluntaryCommunityModels.FirstOrDefault(e => e.VoluntaryCommunityId == primaryId).Priority =
                Convert.ToInt32(primaryPosition);
            voluntaryCommunityModels.FirstOrDefault(e => e.VoluntaryCommunityId == secondaryId).Priority =
                Convert.ToInt32(secondaryPosition);

            var counter = Convert.ToInt32(secondaryPosition) + 1;
            foreach (var sessionValue in voluntaryCommunityModels.Where(se =>
                se.Priority >= Convert.ToInt32(secondaryPosition) && se.VoluntaryCommunityId != secondaryId))
                sessionValue.Priority = counter++;

            HttpContext.Session.Set("VoluntaryCommunityList", voluntaryCommunityModels);
            return voluntaryCommunityModels;
        }

        private List<VoluntaryPublicModel> ReorderVoluntaryPublicServiceInSession(string primaryId,
            string primaryPosition, string secondaryId, string secondaryPosition)
        {
            var voluntaryPublicModels = HttpContext.Session.Get<List<VoluntaryPublicModel>>("VoluntaryPublicList") ??
                                        new List<VoluntaryPublicModel>();

            voluntaryPublicModels.FirstOrDefault(e => e.VoluntaryPublicId == primaryId).Priority =
                Convert.ToInt32(primaryPosition);
            voluntaryPublicModels.FirstOrDefault(e => e.VoluntaryPublicId == secondaryId).Priority =
                Convert.ToInt32(secondaryPosition);

            var counter = Convert.ToInt32(secondaryPosition) + 1;
            foreach (var sessionValue in voluntaryPublicModels.Where(se =>
                se.Priority >= Convert.ToInt32(secondaryPosition) && se.VoluntaryPublicId != secondaryId))
                sessionValue.Priority = counter++;

            HttpContext.Session.Set("VoluntaryPublicList", voluntaryPublicModels);
            return voluntaryPublicModels;
        }

        private void ResetSession()
        {
            HttpContext.Session.Set("EducationList", new List<EducationModel>());
            HttpContext.Session.Set("AkdnTrainingList", new List<AkdnTrainingModel>());
            HttpContext.Session.Set("ProfessionalTrainingList", new List<ProfessionalTrainingModel>());
            HttpContext.Session.Set("LanguageList", new List<LanguageProficiencyModel>());
            HttpContext.Session.Set("VoluntaryCommunityList", new List<VoluntaryCommunityModel>());
            HttpContext.Session.Set("VoluntaryPublicList", new List<VoluntaryPublicModel>());
            HttpContext.Session.Set("EmploymentList", new List<EmploymentModel>());
            HttpContext.Session.Set("FamilyRelationList", new List<FamilyRelationModel>());
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

            if (string.IsNullOrWhiteSpace(model.Image)) model.Image = HttpContext.Session.Get<string>("Image");
        }

        [HttpPost]
        public async Task<IActionResult> SearchPerson(string formNumber)
        {
            Console.WriteLine("id="+formNumber);
            ViewBag.MessageType = TempData["MessageType"];
            ViewBag.Message = TempData["Message"];

            var success =
                await new RestfulClient(
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token)
                    .GetPersonDetailsThroughPagging(string.Empty, string.Empty, formNumber, string.Empty, string.Empty,
                        string.Empty, string.Empty, 1, 1);
            var list = success.Item1;
            if (list.Count > 0)
            {
                return RedirectToAction("Detail", "Persons", new { id = formNumber });
            }
            else
            {
                ViewBag.MessageType = MessageTypes.Error;
                ViewBag.Message = "Person not found";
                return RedirectToAction("Index", "Home", new { area = "" });
            }
        }

        #endregion Private Methods
    }
}