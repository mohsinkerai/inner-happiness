using AMS.frontend.web.Areas.Operations.Models;
using AMS.frontend.web.Areas.Operations.Models.Nominations;
using AMS.frontend.web.Extensions;
using AMS.frontend.web.Helpers.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AMS.frontend.web.Areas.Operations.Controllers
{
    [Area(AreaNames.Operations)]
    public class NominationsController : BaseController
    {
        #region Public Constructors

        public NominationsController(IOptions<Configuration> configuration)
        {
            _configuration = configuration.Value;
            //RestfulClient = new RestfulClient(HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token);
        }

        #endregion Public Constructors

        #region Private Fields

        private readonly Configuration _configuration;
        private readonly RestfulClient _restfulClient;
        private const string SelectedCycle = "_cycle";
        private const string SessionNominationModel = "_sessionNominationModel";

        #endregion Private Fields

        #region Public Methods

        [HttpPost]
        public async Task<IActionResult> Nominate(string id, string personId)
        {
            //getting required data from session
            var json = HttpContext.Session.GetString(SessionNominationModel);
            NominationDetailModel model = JsonConvert.DeserializeObject<NominationDetailModel>(json);

            List<NominationModel> nominations = null;
            string positionId = null;
            string institutionId = null;
            string cycleId = null;
            string seatNo = null;
            int pos = -1;

            for(int index = 0; index < model.Positions.Count; index++)
            {
                PositionModel position = model.Positions[index];
                if(position.Id == id)
                {
                    pos = index;
                    index++;
                    nominations = position.Nominations;
                    nominations.OrderByDescending(i => i.Priority);
                    positionId = position.PositionId;
                    seatNo = position.SeatId;
                }
            }
            institutionId = model.Institution.Id;
            cycleId = HttpContext.Session.GetString(SelectedCycle);

            //api call for nominate
            var positionModel = await new RestfulClient(HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).Nominate(id, personId,
                nominations[0].Priority, institutionId, positionId, cycleId, seatNo);

            //update data in session
            if(pos != -1)
            {
                model.Positions[pos] = positionModel;
                var updatedJson = JsonConvert.SerializeObject(model);
                HttpContext.Session.SetString(SessionNominationModel, updatedJson);
            }
            
            return PartialView("_NominationsTablePartial", positionModel);

            //saif integration goes here
            /*return PartialView("_NominationsTablePartial", new PositionModel
            {
                PositionName = "President",
                CurrentCycle = "2018 - 2020",
                CycleStatus = "On going",
                PreviousCycle = "2015 - 2018",
                Required = 3,
                Id = "123",
                Incubment = PersonDummyData(string.Empty),
                Nominations = new List<NominationModel>
                {
                    new NominationModel
                    {
                        IsAppointed = false,
                        IsRecommended = false,
                        Priority = 1,
                        Person = PersonDummyData(string.Empty),
                        Id = "1111"
                    },
                    new NominationModel
                    {
                        IsAppointed = false,
                        IsRecommended = false,
                        Priority = 2,
                        Person = PersonDummyData(string.Empty),
                        Id = "2222"
                    }
                }
            });*/
        }

        [HttpPost]
        public IActionResult ReOrderNominations(string positionId, string primaryId, string primaryPosition, string secondaryId, string secondaryPosition)
        {
       
            //saif integration goes here
            return PartialView("_NominationsTablePartial", new PositionModel
            {
                PositionName = "President",
                CurrentCycle = "2018 - 2020",
                CycleStatus = "On going",
                PreviousCycle = "2015 - 2018",
                Required = 3,
                Id = "123",
                Incubment = PersonDummyData(string.Empty),
                Nominations = new List<NominationModel>
                {
                    new NominationModel
                    {
                        IsAppointed = false,
                        IsRecommended = false,
                        Priority = 1,
                        Person = PersonDummyData(string.Empty),
                        Id = "1111"
                    },
                    new NominationModel
                    {
                        IsAppointed = false,
                        IsRecommended = false,
                        Priority = 2,
                        Person = PersonDummyData(string.Empty),
                        Id = "2222"
                    }
                }
            });
        }
        [HttpPost]
        public IActionResult RemoveNomination(string positionId, string personId)
        {
            //saif integration goes here
            return PartialView("_NominationsTablePartial", new PositionModel
            {
                PositionName = "President",
                CurrentCycle = "2018 - 2020",
                CycleStatus = "On going",
                PreviousCycle = "2015 - 2018",
                Required = 3,
                Id = "123",
                Incubment = PersonDummyData(string.Empty),
                Nominations = new List<NominationModel>
                {
                    new NominationModel
                    {
                        IsAppointed = false,
                        IsRecommended = false,
                        Priority = 1,
                        Person = PersonDummyData(string.Empty),
                        Id = "1111"
                    }
                }
            });
        }
        public async Task<JsonResult> GetPersons(string id)
        {
            var personTuple =
                await new RestfulClient(
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token)
                    .GetPersonDetailsThroughPagging(string.Empty, string.Empty, id, string.Empty, string.Empty,
                        string.Empty, string.Empty, 1, 10);
            var persons = personTuple.Item1.Select(p => new { Name = $"{p.FormNumber}-{p.FullName}" })
                .Select(p => p.Name);

            //var persons = new List<string> {"Naveed", "Mohsin", "Saif"};

            return Json(persons);
        }

        public async Task<IActionResult> Detail(string uid)
        {

            var model = new NominationDetailModel
            {
                Institution = new InstitutionModel
                {
                    Name = "Council for Karimabad"
                },
                Positions = new List<PositionModel>
                {
                    new PositionModel
                    {
                        PositionName = "President",
                        CurrentCycle = "2018 - 2020",
                        CycleStatus = "On going",
                        PreviousCycle = "2015 - 2018",
                        Required = 3,
                        Id = "123",
                        Incubment = PersonDummyData(string.Empty),
                        Nominations = new List<NominationModel>
                        {
                            new NominationModel
                            {
                                IsAppointed = false,
                                IsRecommended = false,
                                Priority = 1,
                                Person = PersonDummyData(string.Empty),
                                Id = "1111"
                            },

                            new NominationModel
                            {
                                IsAppointed = false,
                                IsRecommended = false,
                                Priority = 2,
                                Person = PersonDummyData(string.Empty),
                                Id = "2222"
                            },

                            new NominationModel
                            {
                                IsAppointed = false,
                                IsRecommended = false,
                                Priority = 3,
                                Person = PersonDummyData(string.Empty),
                                Id = "3333"
                            }
                        }
                    },
                    new PositionModel
                    {
                        PositionName = "Chairman Education Board",
                        CurrentCycle = "2018 - 2020",
                        CycleStatus = "On going",
                        PreviousCycle = "2015 - 2018",
                        Required = 3,
                        Id = "456",
                        Incubment = PersonDummyData(string.Empty),
                        Nominations = new List<NominationModel>
                        {
                            new NominationModel
                            {
                                IsAppointed = false,
                                IsRecommended = false,
                                Priority = 1,
                                Person = PersonDummyData(string.Empty),
                                Id = "4444"
                            },

                            new NominationModel
                            {
                                IsAppointed = false,
                                IsRecommended = false,
                                Priority = 2,
                                Person = PersonDummyData(string.Empty),
                                Id = "5555"
                            },

                            new NominationModel
                            {
                                IsAppointed = false,
                                IsRecommended = false,
                                Priority = 3,
                                Person = PersonDummyData(string.Empty),
                                Id = "6666"
                            }
                        }
                    }
                }
            };

            var cycle = HttpContext.Session.GetString(SelectedCycle);

            var nominationModel = await new RestfulClient(HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetInstitutionDetails(uid,cycle);

            var json = JsonConvert.SerializeObject(nominationModel);
            HttpContext.Session.SetString(SessionNominationModel, json);

            return View(nominationModel);
        }

        public async Task<JsonResult> GetInstitutionTypes(string level, string subLevel)
        {
            var list = await _restfulClient.GetInstitutionTypes(level, subLevel);

            return new JsonResult(list);
        }

        public async Task<JsonResult> GetLocalInstitutions()
        {
            var list = await new RestfulClient(HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetLocalInstitutions();

            return new JsonResult(list);
        }

        public async Task<JsonResult> GetRegionalInstitutions()
        {
            var list = await new RestfulClient(HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetRegionalInstitutions();

            return new JsonResult(list);
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.MessageType = TempData["MessageType"];
            ViewBag.Message = TempData["Message"];

            ViewBag.Cycle = await new RestfulClient(HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetCycles();
            //return View(new List<PersonModel>());

            //ViewBag.CompanyList = await RestfulClient.getAllCompanies();
            //ViewBag.RegionList = await RestfulClient.getRegionalCouncil();
            //ViewBag.LocalList = await RestfulClient.getLocalCouncil();
            //ViewBag.JamatkhanaList = await RestfulClient.getJamatkhana();
            //ViewBag.InstitutionList = await RestfulClient.getPositionInstitution();

            return View(new IndexNominationModel { Positions = new List<PositionModel>() });
        }

        [HttpPost]
        public async Task<IActionResult> Index(IndexNominationModel indexNominationModel)
        {
            ViewBag.Cycle = await new RestfulClient(HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetCycles();
            if (ModelState.IsValid)
            {
                //Store Cycle in session
                HttpContext.Session.SetString(SelectedCycle, indexNominationModel.Cycle);
                
                return View(indexNominationModel);
            }
            else
            {
                return View();
            }
        }
        
        public async Task<IActionResult> ServerSideAjaxHandler(IndexNominationModel indexNominationModel)
        {
            try
            {
                var level = indexNominationModel.Level;
                var subLevel = string.IsNullOrWhiteSpace(indexNominationModel.Region)
                    ? indexNominationModel.Local
                    : indexNominationModel.Region;

                var queryCollection = Request.Query; //HttpContext.Request.Query;
                // Initialization.
                var search = queryCollection["search[value]"][0];
                var draw = queryCollection["draw"][0];
                //string order = form["order[0][column]"][0];
                //string orderDir = form["order[0][dir]"][0];
                var startRec = Convert.ToInt32(queryCollection["start"][0]);
                var pageSize = Convert.ToInt32(queryCollection["length"][0]);
                // Loading.
                //IQueryable<NiyatForm> data = null;
                //if (user.Category.Equals(UserCategory.NationalCouncil) && user.Role.Equals(UserRole.Administrator))
                //{
                //    data = _dbContext.NiyatForms
                //        .Where(n => n.Country.NationalCouncilId.Equals(user.NationalCouncilId) &&
                //                    n.Status.Equals(NiyatFormStatus
                //                        .Approved) /* && n.Category.Equals(NiyatFormCategory.Adult)*/);
                //    //.ConfigureAwait(false);
                //}
                //else if (user.Category.Equals(UserCategory.NationalCouncil) && user.Role.Equals(UserRole.Checker))
                //{
                //    data = _dbContext.NiyatForms
                //        .Where(n => n.Country.NationalCouncilId.Equals(user.NationalCouncilId) &&
                //                    n.Status.Equals(NiyatFormStatus
                //                        .Pending) /* && n.Category.Equals(NiyatFormCategory.Adult)*/);
                //    //.ConfigureAwait(false);
                //}
                //else
                //{
                //    data = _dbContext.NiyatForms
                //        .Where(n => n.Country.NationalCouncilId.Equals(user.NationalCouncilId) &&
                //                    (n.Status.Equals(NiyatFormStatus.New) ||
                //                     n.Status.Equals(NiyatFormStatus
                //                         .Rejected)) /* && n.Category.Equals(NiyatFormCategory.Adult)*/);
                //    //.ConfigureAwait(false);
                //}
                //// Total record count.
                //int totalRecords = data.Count();
                //// Verification.
                //if (!string.IsNullOrEmpty(search) &&
                //    !string.IsNullOrWhiteSpace(search))
                //{
                //    // Apply search
                //    data = data.Where(p =>
                //        p.FormNumber.ToLower().Contains(search.ToLower()) ||
                //        p.FullName.ToLower().Contains(search.ToLower()));
                //}
                //// Sorting.
                ////data = this.SortByColumnWithOrder(order, orderDir, data);
                //// Filter record count.
                //int recFilter = (string.IsNullOrEmpty(search) || string.IsNullOrWhiteSpace(search))
                //    ? totalRecords
                //    : data.Count();
                //// Apply pagination.
                //data = data.Skip(startRec).Take(pageSize);

                //var finalData = data.Include(n => n.ApprovedByUser).Include(n => n.EnteredByUser)
                //    .Include(n => n.SentForVerificationByUser).Include(n => n.VoluntaryExperiences).ToList();

                //var conditionedData = finalData.Select(n => new
                //{
                //    n.FormNumber,
                //    n.FullName,
                //    n.EnteredOnForDisplay,
                //    n.EnteredByUserForDisplay,
                //    n.SubmittedForVerificationOnForDisplay,
                //    n.SentForVerificationByUserForDisplay,
                //    n.VerifiedOnForDisplay,
                //    n.ApprovedByUserForDisplay,
                //    //n.Comments,
                //    n.Status,
                //    n.CompletionPercentage,
                //    n.IsExported,
                //    n.ExportedOnForDisplay,
                //    DetailUrl = n.Category == NiyatFormCategory.Adult
                //        ? Url.Action(ActionNames.Detail, ControllerNames.NationalCouncil.NiyatForm,
                //            new { area = AreaNames.NationalCouncil, uid = n.UId })
                //        : Url.Action(ActionNames.Detail, ControllerNames.NationalCouncil.MinorNiyatForm,
                //            new { area = AreaNames.NationalCouncil, uid = n.UId })
                //});

                //var jamatkhanas = new List<string>();
                //var provinces = new List<string>();
                //var cities = new List<string>();
                //var volunteerRoles = new List<string>();
                //var volunteerInstitutions = new List<string>();

                //foreach (var niyatForm in finalData)
                //{
                //    if (!jamatkhanas.Contains(niyatForm.Jamatkhana))
                //    {
                //        jamatkhanas.Add(niyatForm.Jamatkhana);
                //    }
                //    if (!string.IsNullOrWhiteSpace(niyatForm.Province) && !provinces.Contains(niyatForm.Province))
                //    {
                //        provinces.Add(niyatForm.Province);
                //    }
                //    if (!cities.Contains(niyatForm.City))
                //    {
                //        cities.Add(niyatForm.City);
                //    }
                //    if (!cities.Contains(niyatForm.AddressCity))
                //    {
                //        cities.Add(niyatForm.AddressCity);
                //    }
                //    foreach (var voluntaryExperience in niyatForm.VoluntaryExperiences)
                //    {
                //        if (!volunteerInstitutions.Contains(voluntaryExperience.InstitutionOrOrganizationName))
                //        {
                //            volunteerInstitutions.Add(voluntaryExperience.InstitutionOrOrganizationName);
                //        }
                //        if (!volunteerRoles.Contains(voluntaryExperience.Role))
                //        {
                //            volunteerRoles.Add(voluntaryExperience.Role);
                //        }
                //    }
                //}

                //HttpContext.Session.Set(SessionKeys.AutoCompleteJamatkhanas, jamatkhanas);
                //HttpContext.Session.Set(SessionKeys.AutoCompleteProvinces, provinces);
                //HttpContext.Session.Set(SessionKeys.AutoCompleteReligiousQualifications, new List<string>());
                //HttpContext.Session.Set(SessionKeys.AutoCompleteVolunteerRoles, volunteerRoles);
                //HttpContext.Session.Set(SessionKeys.AutoCompleteVolunteerInstitutions, volunteerInstitutions);

                //var conditionedData = await RestfulClient.getPersonDetails();
                //var conditionedData = new List<PositionModel>();

                var conditionedData = await new RestfulClient(HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetInstitutionTypes(level, subLevel);

                // Loading drop down lists.
                return Json(new
                {
                    draw = Convert.ToInt32(draw),
                    recordsTotal = conditionedData.Count,
                    recordsFiltered = conditionedData.Count,
                    /*data = conditionedData.Select(n => new
                    {
                        n.PositionName,
                        n.Incubment,
                        n.Required,
                        n.Nominated,
                        DetailUrl = Url.Action(ActionNames.Detail, ControllerNames.Nominations,
                            new { area = AreaNames.Operations, uid = n.Id })
                    })*/
                    data = conditionedData.Select(n => new
                    {
                        n.PositionName,
                        n.FullName,
                        DetailUrl = Url.Action(ActionNames.Detail, ControllerNames.Nominations,
                            new { area = AreaNames.Operations, uid = n.Id })
                    })
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    draw = 1,
                    recordsTotal = 0,
                    recordsFiltered = 0,
                    data = new List<PositionModel>()
                });
            }
        }

        #endregion Public Methods

        /*public async Task<JsonResult> GetInstitutions(string level, string subLevel, string type)
        {
            var list = await RestfulClient.GetInstitutions(level, subLevel, type);

            return new JsonResult(list);
        }*/
    }
}