using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AMS.frontend.web.Areas.Administration.Models;
using AMS.frontend.web.Areas.Operations.Models.Cycle;
using AMS.frontend.web.Areas.Operations.Models.Nominations;
using AMS.frontend.web.Areas.Operations.Models.Persons;
using AMS.frontend.web.Models.Authenticate;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AMS.frontend.web.Areas.Operations.Models
{
    public class AuthenticationResponse
    {
        #region Public Properties

        [JsonProperty("active")] public bool Active { get; set; }

        [JsonProperty("authenticated")] public bool Authenticated { get; set; }

        [JsonProperty("authorities")] public Authority[] Authorities { get; set; }

        [JsonProperty("credentials")] public string Credentials { get; set; }

        [JsonProperty("details")] public object Details { get; set; }

        [JsonProperty("expiry")] public DateTime Expiry { get; set; }

        [JsonProperty("id")] public long Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("principal")] public string Principal { get; set; }

        [JsonProperty("roles")] public string[] Roles { get; set; }

        [JsonProperty("token")] public string Token { get; set; }

        [JsonProperty("user")] public string User { get; set; }

        #endregion Public Properties
    }

    public class Authority
    {
        #region Public Properties

        [JsonProperty("amsAuthority")] public string AmsAuthority { get; set; }

        [JsonProperty("authority")] public string AuthorityAuthority { get; set; }

        #endregion Public Properties
    }

    public class CycleId
    {
        #region Public Properties

        [JsonProperty("endDate")] public DateTime EndDate { get; set; }

        [JsonProperty("id")] public long Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("startDate")] public DateTime StartDate { get; set; }

        #endregion Public Properties
    }

    public class Institution
    {
        #region Public Properties

        [JsonProperty("id")] public long Id { get; set; }

        [JsonProperty("levelId")] public long LevelId { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        #endregion Public Properties
    }

    public class PastAppointment
    {
        #region Public Properties

        [JsonProperty("active")] public bool Active { get; set; }

        [JsonProperty("cycleId")] public CycleId CycleId { get; set; }

        [JsonIgnore]
        public string CycleName => $"{CycleId.StartDate.Year.ToString()} - {CycleId.EndDate.Year.ToString()}";

        [JsonProperty("id")] public long Id { get; set; }

        [JsonProperty("institution")] public Institution Institution { get; set; }

        [JsonProperty("mowlaAppointee")] public bool MowlaAppointee { get; set; }

        [JsonProperty("nominationsRequired")] public long NominationsRequired { get; set; }

        [JsonProperty("position")] public Position Position { get; set; }

        [JsonIgnore] public string PositionName => $"{Position.Name} - {Institution.Name}";

        [JsonProperty("seatNo")] public long SeatNo { get; set; }

        #endregion Public Properties
    }

    public class Position
    {
        #region Public Properties

        [JsonProperty("id")] public long Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        #endregion Public Properties
    }

    public class RestfulClient
    {
        #region Public Constructors

        public RestfulClient(string token)
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(_baseUrl)
            };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        #endregion Public Constructors

        #region Private Methods

        private static void SetDetails(List<SelectListItem> list, List<SelectListItem> listAreaOfOrigin,
            List<SelectListItem> salutationList, List<SelectListItem> jamatiTitleList,
            List<SelectListItem> nameOfDegreeList, List<SelectListItem> voluntaryCommunityInstitutionList,
            List<SelectListItem> occupationTypeList,
            List<SelectListItem> institutionList, PersonModel personModel)
        {
            try
            {
                if (personModel.MaritalStatus != null)
                {
                    var item = list.Find(x => x.Value == personModel.MaritalStatus);
                    personModel.MaritalStatusForDisplay = item.Text;
                }

                if (personModel.AreaOfOrigin != null)
                {
                    var item1 = listAreaOfOrigin.Find(x => x.Value == personModel.AreaOfOrigin);
                    personModel.AreaOfOriginForDisplay = item1.Text;
                }

                if (personModel.Salutation != null)
                {
                    var salutation = salutationList.Find(x => x.Value == personModel.Salutation);
                    personModel.SalutationForDisplay = salutation.Text;
                }

                if (personModel.JamatiTitle != null)
                {
                    var jamatiTitle = jamatiTitleList.Find(x => x.Value == personModel.JamatiTitle);
                    personModel.JamatiTitleForDisplay = jamatiTitle.Text;
                }

                if (personModel.Educations != null)
                    foreach (var education in personModel.Educations)
                    {
                        var edu = nameOfDegreeList.Find(x => x.Value.StartsWith($"{education.NameOfDegree}-"));
                        education.NameOfDegreeName = edu.Text;

                        var institute = institutionList.Find(x => x.Value.StartsWith($"{education.Institution}-"));
                        education.InstitutionName = institute.Text;
                    }

                if (personModel.VoluntaryCommunityServices != null)
                    foreach (var communityInstituion in personModel.VoluntaryCommunityServices)
                    {
                        var institution =
                            voluntaryCommunityInstitutionList.Find(x => x.Value == communityInstituion.Institution);
                        communityInstituion.InstitutionName = institution.Text;
                    }

                if (personModel.OccupationType != null)
                {
                    var ocupation = occupationTypeList.Find(x => x.Value == personModel.OccupationType);
                    personModel.OccupationTypeName = ocupation.Text;
                }
            }
            catch (Exception)
            {
            }
        }

        #endregion Private Methods

        #region Private Fields

        private readonly string _baseUrl = "http://is.bismagreens.com/";

        //http://localhost:8080/

        private readonly HttpClient _client;

        #endregion Private Fields

        #region Public Methods

        public async Task<bool> EditPersonData(PersonModel personModel)
        {
            var json = JsonConvert.SerializeObject(personModel);

            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var content = new StringContent(json);
            var res = await _client.PutAsync("person/one/" + personModel.Id, httpContent);

            return res.IsSuccessStatusCode;
        }

        public async Task<List<SelectListItem>> GetAkdnTraining()
        {
            var res = await _client.GetAsync("constants/akdn-training/all");
            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                var list = new List<SelectListItem>();

                foreach (var item in myObject)
                {
                    var id = $"{Convert.ToString(item.id)}-{Convert.ToString(item.name)}";
                    var name = Convert.ToString(item.name);

                    list.Add(new SelectListItem { Text = name, Value = id });
                }

                return list;
            }

            return null;
        }

        public async Task<List<SelectListItem>> GetAllCompanies()
        {
            var res = await _client.GetAsync("company/all");
            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                var list = new List<SelectListItem>();

                foreach (var item in myObject)
                {
                    var id = Convert.ToString(item.id);
                    var name = Convert.ToString(item.name);

                    list.Add(new SelectListItem { Text = name, Value = id });
                }

                return list;
            }

            return null;
        }

        public async Task<List<SelectListItem>> GetAllCountries()
        {
            var res = await _client.GetAsync("constants/country/all");
            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                var list = new List<SelectListItem>();

                foreach (var item in myObject)
                {
                    var id = $"{Convert.ToString(item.id)}-{Convert.ToString(item.name)}";
                    var name = Convert.ToString(item.name);

                    list.Add(new SelectListItem { Text = name, Value = id });
                }

                return list;
            }

            return null;
        }

        public async Task<List<SelectListItem>> GetAllInstitutions()
        {
            var res = await _client.GetAsync("constants/educational-publicserviceinstitution/all");
            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                var list = new List<SelectListItem>();

                foreach (var item in myObject)
                {
                    var id = $"{Convert.ToString(item.id)}-{Convert.ToString(item.name)}";
                    var name = Convert.ToString(item.name);

                    list.Add(new SelectListItem { Text = name, Value = id });
                }

                return list;
            }

            return null;
        }

        public async Task<List<SelectListItem>> GetAllRelatives()
        {
            var res = await _client.GetAsync("constants/relation/all");
            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                var list = new List<SelectListItem>();

                foreach (var item in myObject)
                {
                    var id = $"{Convert.ToString(item.id)}-{Convert.ToString(item.name)}";
                    var name = Convert.ToString(item.name);

                    list.Add(new SelectListItem { Text = name, Value = id });
                }

                return list;
            }

            return null;
        }

        public async Task<List<PastAppointment>> GetAppointments(string personId, bool isMawlaAppointee)
        {
            var res = await _client.GetAsync(
                $"appointment-position/search/findAppointmentOfPersonIdAndIsMowlaAppointee?personId={personId}&isMowlaAppointee={isMawlaAppointee}");

            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;

                var appointments = new List<PastAppointment>();

                appointments = JsonConvert.DeserializeObject<List<PastAppointment>>(json);

                return appointments;
            }

            return null;
        }

        public async Task<List<SelectListItem>> GetAreaOfOrigin()
        {
            var res = await _client.GetAsync("constants/area-of-origin/all");
            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                var list = new List<SelectListItem>();

                foreach (var item in myObject)
                {
                    var id = Convert.ToString(item.id);
                    var name = Convert.ToString(item.name);

                    list.Add(new SelectListItem { Text = name, Value = id });
                }

                return list;
            }

            return null;
        }

        public async Task<List<SelectListItem>> GetBussinessNature()
        {
            var res = await _client.GetAsync("constants/business-nature/all");
            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                var list = new List<SelectListItem>();

                foreach (var item in myObject)
                {
                    var id = $"{Convert.ToString(item.id)}-{Convert.ToString(item.name)}";
                    var name = Convert.ToString(item.name);

                    list.Add(new SelectListItem { Text = name, Value = id });
                }

                return list;
            }

            return null;
        }

        public async Task<List<SelectListItem>> GetBussinessType()
        {
            var res = await _client.GetAsync("constants/business-type/all");
            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                var list = new List<SelectListItem>();

                foreach (var item in myObject)
                {
                    var id = $"{Convert.ToString(item.id)}-{Convert.ToString(item.name)}";
                    var name = Convert.ToString(item.name);

                    list.Add(new SelectListItem { Text = name, Value = id });
                }

                return list;
            }

            return null;
        }

        public async Task<List<SelectListItem>> GetCities()
        {
            var res = await _client.GetAsync("constants/city/all");
            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                var list = new List<SelectListItem>();

                foreach (var item in myObject)
                {
                    var id = Convert.ToString(item.id);
                    var name = Convert.ToString(item.name);

                    list.Add(new SelectListItem { Text = name, Value = id });
                }

                return list;
            }

            return null;
        }

        public async Task<List<SelectListItem>> GetCycles()
        {
            var res = await _client.GetAsync("cycle/all");
            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                var list = new List<SelectListItem>();

                foreach (var item in myObject)
                {
                    var id = Convert.ToString(item.id);
                    var name = Convert.ToString(item.name);

                    list.Add(new SelectListItem { Text = name, Value = id });
                }

                return list;
            }

            return null;
        }

        public async Task<List<SelectListItem>> GetEducationalDegree()
        {
            var res = await _client.GetAsync("constants/educational-degree/all");
            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                var list = new List<SelectListItem>();

                foreach (var item in myObject)
                {
                    var id = $"{Convert.ToString(item.id)}-{Convert.ToString(item.name)}";
                    var name = Convert.ToString(item.name);

                    list.Add(new SelectListItem { Text = name, Value = id });
                }

                return list;
            }

            return null;
        }

        public async Task<List<SelectListItem>> GetFieldOfExpertise()
        {
            //var res = await _client.GetAsync("constants/field-of-expertise/all");
            var res = await _client.GetAsync("constants/field-of-expertise/all");

            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                var list = new List<SelectListItem>();

                foreach (var item in myObject)
                {
                    var id = Convert.ToString(item.id);
                    var name = Convert.ToString(item.name);

                    list.Add(new SelectListItem { Text = name, Value = id });
                }

                return list;
            }

            return null;
        }

        public async Task<List<SelectListItem>> GetFieldOfInterests()
        {
            var res = await _client.GetAsync("constants/field-of-interest/all");
            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                var list = new List<SelectListItem>();

                foreach (var item in myObject)
                {
                    var id = Convert.ToString(item.id);
                    var name = Convert.ToString(item.name);

                    list.Add(new SelectListItem { Text = name, Value = id });
                }

                return list;
            }

            return null;
        }

        public async Task<List<SelectListItem>> GetHighestLevelOfStudy()
        {
            var res = await _client.GetAsync("constants/secular-study-level/all");
            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                var list = new List<SelectListItem>();

                foreach (var item in myObject)
                {
                    var id = Convert.ToString(item.id);
                    var name = Convert.ToString(item.name);

                    list.Add(new SelectListItem { Text = name, Value = id });
                }

                return list;
            }

            return null;
        }

        public async Task<NominationDetailModel> GetInstitutionDetails(string id, string cycle)
        {
            var nominationDetailModel = new NominationDetailModel
            {
                Positions = new List<PositionModel>(),
                Institution = new InstitutionModel()
            };

            var list = await GetMartialStatuses();
            var listAreaOfOrigin = await GetAreaOfOrigin();
            var salutationList = await GetSalutation();
            var jamatiTitleList = await GetJamatiTitles();
            var institutionList = await GetAllInstitutions();
            var nameOfDegreeList = await GetEducationalDegree();
            var religiousEducationList = await GetReligiousEducation();
            var regionalCouncilList = await GetRegionalCouncil();
            var listOfCountries = await GetAllCountries();
            var listOfLanguageProficiency = await GetLanguageProficiency();
            var voluntaryCommunityPositionList = await GetPositions();
            var highestLevelOfStudyList = await GetHighestLevelOfStudy();
            var akdnTrainingList = await GetAkdnTraining();
            var voluntaryCommunityInstitutionList = await GetPositionInstitution();
            var fieldOfInterestsList = await GetFieldOfInterests();
            var occupationTypeList = await GetOcupations();
            var typeOfBusinessList = await GetBussinessType();
            var natureOfBusinessList = await GetBussinessNature();
            var professionalMembershipsList = await GetProfessionalMemeberShipDetails();
            var languageList = await GetLanguages();
            var skillsList = await GetSkills();
            var relationList = await GetAllRelatives();
            var majorAreaOfStudy = await GetMajorAreaOfStudy();
            var fieldOfExpertiseList = await GetFieldOfExpertise();

            try
            {
                //HttpResponseMessage res = await _client.GetAsync("position/search/findByInstitutionId?institutionId=5");
                var res = await _client.GetAsync("appointment-position/search/findByCycleIdAndInstitutionId?cycleId=" +
                                                 cycle + "&institutionId=" + id);

                if (res.IsSuccessStatusCode)
                {
                    var json = res.Content.ReadAsStringAsync().Result;

                    var arr = JArray.Parse(json);

                    var listPositions = new List<PositionModel>();

                    foreach (var jToken1 in arr)
                    {
                        var positionArray = (JObject) jToken1;
                        var listNominations = new List<NominationModel>();
                        var positionModel = new PositionModel();
                        var incumbentDetail = new IncumbentDetail();


                        var personAppointmentList = positionArray["personAppointmentList"];
                        var currentCycle = positionArray["cycle"];
                        var positionName = positionArray["position"];
                        var instituion = positionArray["institution"];

                        positionModel.Id = Convert.ToString(positionArray["appointmentPositionId"]);
                        positionModel.CurrentCycle = Convert.ToString(currentCycle["name"]);
                        positionModel.PositionName = Convert.ToString(positionName["name"]);
                        positionModel.PositionId = Convert.ToString(positionName["id"]);
                        positionModel.Required = Convert.ToInt32(positionArray["nominationsRequired"]);
                        positionModel.Rank = Convert.ToInt32(positionArray["rank"].HasValues ? positionArray["rank"] : 0);
                        positionModel.SeatId = Convert.ToString(positionArray["seatId"]);
                        positionModel.State = Convert.ToString(positionArray["state"]);
                        positionModel.From = string.IsNullOrWhiteSpace(Convert.ToString(positionArray["from"]))
                            ? (DateTime?) null
                            : Convert.ToDateTime(positionArray["from"]);
                        positionModel.To = string.IsNullOrWhiteSpace(Convert.ToString(positionArray["to"]))
                            ? (DateTime?) null
                            : Convert.ToDateTime(positionArray["to"]);
                        positionModel.CycleStatus = Convert.ToString(currentCycle["state"]);

                        //int index = 0;
                        foreach (var jToken in personAppointmentList)
                        {
                            var personsAppointed = (JObject) jToken;
                            var nominationModel = new NominationModel();

                            var incumbent = personsAppointed["person"];
                            if (Convert.ToInt32(personsAppointed["priority"]) == 0)
                            {
                                positionModel.Incubment = incumbent.ToObject<PersonModel>();
                                SetDetails(list, listAreaOfOrigin, salutationList, jamatiTitleList, nameOfDegreeList,
                                    voluntaryCommunityInstitutionList,
                                    occupationTypeList, institutionList, positionModel.Incubment);

                                incumbentDetail.Priority = Convert.ToInt32(personsAppointed["priority"]);
                                incumbentDetail.IsAppointed = Convert.ToBoolean(personsAppointed["appointed"]);
                                incumbentDetail.IsRecommended = Convert.ToBoolean(personsAppointed["recommended"]);
                                incumbentDetail.personAppointmentId = Convert.ToString(personsAppointed["personAppointmentId"]);

                                positionModel.incumbentDetail = incumbentDetail;
                            }
                            else
                            {
                                nominationModel.Priority = Convert.ToInt32(personsAppointed["priority"]);
                                nominationModel.IsAppointed = Convert.ToBoolean(personsAppointed["appointed"]);
                                nominationModel.IsRecommended = Convert.ToBoolean(personsAppointed["recommended"]);
                                nominationModel.personAppointmentId =
                                    Convert.ToString(personsAppointed["personAppointmentId"]);
                                var person = personsAppointed["person"];
                                nominationModel.Person = person.ToObject<PersonModel>();

                                SetDetails(list, listAreaOfOrigin, salutationList, jamatiTitleList, nameOfDegreeList,
                                    voluntaryCommunityInstitutionList,
                                    occupationTypeList, institutionList, nominationModel.Person);

                                listNominations.Add(nominationModel);
                            }

                            //index++;
                        }

                        listNominations.Sort((a, b) => a.Priority.CompareTo(b.Priority));
                        positionModel.Nominations = listNominations;

                        listPositions.Add(positionModel);

                        nominationDetailModel.Institution.Id = Convert.ToString(instituion["id"]);
                        nominationDetailModel.Institution.Name = Convert.ToString(instituion["name"]);
                    }

                    listPositions.Sort((a, b) => a.Rank.CompareTo(b.Rank));
                    nominationDetailModel.Positions = listPositions;
                }
            }
            catch (Exception ex)
            {
            }

            return nominationDetailModel;
        }

        public async Task<List<PositionModel>> GetInstitutionTypes(string level, string subLevel)
        {
            //var Res = await client.GetAsync("institution/search/findByLevelType?levelTypeId="+subLevel);
            //var Res = await client.GetAsync("institution/search/findByLevelId?levelId=" + subLevel);

            if (level == null) return null;

            var res = await _client.GetAsync(string.IsNullOrWhiteSpace(subLevel)
                ? "institution/search/findByLevelType?levelTypeId=1"
                : "institution/search/findByLevelId?levelId=" + subLevel);

            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                var list = new List<PositionModel>();

                foreach (var item in myObject)
                {
                    var id = Convert.ToString(item.id);
                    var name = Convert.ToString(item.name);
                    var fullName = Convert.ToString(item.fullName);

                    var positionModel = new PositionModel
                    {
                        Id = id,
                        PositionName = name,
                        FullName = fullName
                    };

                    list.Add(positionModel);
                }

                return list;
            }

            return null;
        }

        public async Task<List<SelectListItem>> GetJamatiTitles()
        {
            var res = await _client.GetAsync("constants/jamati-title/all");
            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                var list = new List<SelectListItem>();

                foreach (var item in myObject)
                {
                    var id = Convert.ToString(item.id);
                    var name = Convert.ToString(item.name);

                    list.Add(new SelectListItem { Text = name, Value = id });
                }

                return list;
            }

            return null;
        }

        public async Task<List<SelectListItem>> GetJamatkhana(string id = "")
        {
            var res = await _client.GetAsync(string.IsNullOrWhiteSpace(id)
                ? "level/search/type?value=4"
                : "level/search/parent?value=" + id);
            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                var list = new List<SelectListItem>();

                foreach (var item in myObject)
                {
                    var id1 = Convert.ToString(item.id);
                    var name = Convert.ToString(item.name);

                    list.Add(new SelectListItem { Text = name, Value = id1 });
                }

                return list;
            }

            return null;
        }

        public async Task<List<SelectListItem>> GetLanguageProficiency()
        {
            var res = await _client.GetAsync("/constants/language-proficiency/all");
            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                var list = new List<SelectListItem>();

                foreach (var item in myObject)
                {
                    var id = $"{Convert.ToString(item.id)}-{Convert.ToString(item.name)}";
                    var name = Convert.ToString(item.name);

                    list.Add(new SelectListItem { Text = name, Value = id });
                }

                return list;
            }

            return null;
        }

        public async Task<List<SelectListItem>> GetLanguages()
        {
            var res = await _client.GetAsync("constants/language/all");
            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                var list = new List<SelectListItem>();

                foreach (var item in myObject)
                {
                    var id = $"{Convert.ToString(item.id)}-{Convert.ToString(item.name)}";
                    var name = Convert.ToString(item.name);

                    list.Add(new SelectListItem { Text = name, Value = id });
                }

                return list;
            }

            return null;
        }

        public async Task<List<SelectListItem>> GetLocalCouncil(string uid = "")
        {
            var res = await _client.GetAsync(string.IsNullOrWhiteSpace(uid)
                ? "level/search/type?value=3"
                : "level/search/parent?value=" + uid);
            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                var list = new List<SelectListItem>();

                foreach (var item in myObject)
                {
                    var id = Convert.ToString(item.id);
                    var name = Convert.ToString(item.name);

                    list.Add(new SelectListItem { Text = name, Value = id });
                }

                return list;
            }

            return null;
        }

        public async Task<List<SelectListItem>> GetLocalInstitutions()
        {
            var res = await _client.GetAsync("institution/search/findByLevelType?levelTypeId=3");

            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                var list = new List<SelectListItem>();

                foreach (var item in myObject)
                {
                    var id = Convert.ToString(item.id);
                    var name = Convert.ToString(item.name);

                    list.Add(new SelectListItem { Text = name, Value = id });
                }

                return list;
            }

            return null;
        }

        public async Task<List<SelectListItem>> GetMajorAreaOfStudy()
        {
            var res = await _client.GetAsync("constants/area-of-study/all");
            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                var list = new List<SelectListItem>();

                foreach (var item in myObject)
                {
                    var id = $"{Convert.ToString(item.id)}-{Convert.ToString(item.name)}";
                    var name = Convert.ToString(item.name);

                    list.Add(new SelectListItem { Text = name, Value = id });
                }

                return list;
            }

            return null;
        }

        public async Task<List<SelectListItem>> GetMartialStatuses()
        {
            var res = await _client.GetAsync("constants/marital-status/all");
            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                var list = new List<SelectListItem>();

                foreach (var item in myObject)
                {
                    var id = Convert.ToString(item.id);
                    var name = Convert.ToString(item.name);

                    list.Add(new SelectListItem { Text = name, Value = id });
                }

                return list;
            }

            return null;
        }

        public async Task<List<SelectListItem>> GetOcupations()
        {
            var res = await _client.GetAsync("constants/occupation/all");
            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                var list = new List<SelectListItem>();

                foreach (var item in myObject)
                {
                    var id = Convert.ToString(item.id);
                    var name = Convert.ToString(item.name);

                    list.Add(new SelectListItem { Text = name, Value = id });
                }

                return list;
            }

            return null;
        }

        public async Task<List<PersonModel>> GetPersonDetails()
        {
            var res = await _client.GetAsync("person/all");
            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;

                var person = JsonConvert.DeserializeObject<List<PersonModel>>(json);

                return person;
            }

            return null;
        }

        public async Task<PersonModel> GetPersonDetailsById(string id)
        {
            var res = await _client.GetAsync("person/one/" + id);
            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;

                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                };

                var person = JsonConvert.DeserializeObject<PersonModel>(json, settings);

                return person;
            }

            return null;
        }

        public async Task<Tuple<List<PersonModel>, int>> GetPersonDetailsThroughPagging(string firstName,
            string cnic, string formNo, string jamatiTitle, string degree, string majorAreaOfStudy,
            string academicInstitution,
            int pageNumber, int pageSize, string dob)
        {
            //var Res = await client.GetAsync("/person/search/findByCnicOrFirstNameOrLastName?firstName&cnic&lastName&page=1&size=1");

            var url = "";
            if (string.IsNullOrWhiteSpace(firstName) && string.IsNullOrWhiteSpace(cnic) &&
                string.IsNullOrWhiteSpace(formNo) && string.IsNullOrWhiteSpace(jamatiTitle) &&
                string.IsNullOrWhiteSpace(degree) &&
                string.IsNullOrWhiteSpace(majorAreaOfStudy) && string.IsNullOrWhiteSpace(academicInstitution) && string.IsNullOrWhiteSpace(dob))
                url = "/person/all/paginated?page=" + pageNumber + "&size=" + pageSize;
            else
                url = "/person/search/findByCnicAndNameAndIdAndDegreeAndAcadInstAndJamatiTitleAndMaosAndDob?cnic=" + cnic +
                      "&name=" + firstName + "&id=" + formNo + "&degree=" +
                      (string.IsNullOrWhiteSpace(degree) ? degree : degree.Split('-')[0]) +
                      "&inst=" +
                      (string.IsNullOrWhiteSpace(academicInstitution)
                          ? academicInstitution
                          : academicInstitution.Split('-')[0]) + "&jamatiTitle=" + jamatiTitle + "&maos=" +
                      (string.IsNullOrWhiteSpace(majorAreaOfStudy)
                          ? majorAreaOfStudy
                          : majorAreaOfStudy.Split('-')[0]) + "&dob=" + dob +
                      "&page=" + pageNumber + "&size=" + pageSize;

            var res = await _client.GetAsync(url);
            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;

                var person = new List<PersonModel>();

                var jsonObject = JObject.Parse(json);

                try
                {
                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    };

                    person = JsonConvert.DeserializeObject<List<PersonModel>>(jsonObject["content"].ToString(),
                        settings);
                }
                catch (Exception)
                {
                }

                var totalElements = Convert.ToInt32(jsonObject["totalElements"]);

                return new Tuple<List<PersonModel>, int>(person, totalElements);
            }

            return null;
        }

        public async Task<List<SelectListItem>> GetPositionInstitution()
        {
            var res = await _client.GetAsync("institution/all");
            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                var list = new List<SelectListItem>();

                foreach (var item in myObject)
                {
                    var id = $"{Convert.ToString(item.id)}-{Convert.ToString(item.name)}";
                    var name = Convert.ToString(item.name);

                    list.Add(new SelectListItem { Text = name, Value = id });
                }

                return list;
            }

            return null;
        }

        public async Task<List<SelectListItem>> GetPositions()
        {
            var res = await _client.GetAsync("position/all");
            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                var list = new List<SelectListItem>();

                foreach (var item in myObject)
                {
                    var id = $"{Convert.ToString(item.id)}-{Convert.ToString(item.name)}";
                    var name = Convert.ToString(item.name);

                    list.Add(new SelectListItem { Text = name, Value = id });
                }

                return list;
            }

            return null;
        }

        public async Task<List<SelectListItem>> GetProfessionalMemeberShipDetails()
        {
            var res = await _client.GetAsync("constants/professional-membership/all");
            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                var list = new List<SelectListItem>();

                foreach (var item in myObject)
                {
                    var id = Convert.ToString(item.id);
                    var name = Convert.ToString(item.name);

                    list.Add(new SelectListItem { Text = name, Value = id });
                }

                return list;
            }

            return null;
        }

        public async Task<List<SelectListItem>> GetRegionalCouncil()
        {
            var res = await _client.GetAsync("institution/search/findByLevelType?levelTypeId=2");
            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                var list = new List<SelectListItem>();

                foreach (var item in myObject)
                {
                    var id = Convert.ToString(item.id);
                    var name = Convert.ToString(item.name);

                    list.Add(new SelectListItem { Text = name, Value = id });
                }

                return list;
            }

            return null;
        }

        public async Task<List<SelectListItem>> GetRegionalInstitutions()
        {
            var res = await _client.GetAsync("institution/search/findByLevelType?levelTypeId=2");
            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                var list = new List<SelectListItem>();

                foreach (var item in myObject)
                {
                    var id = Convert.ToString(item.id);
                    var name = Convert.ToString(item.name);

                    list.Add(new SelectListItem { Text = name, Value = id });
                }

                return list;
            }

            return null;
        }

        public async Task<List<SelectListItem>> GetReligiousEducation()
        {
            var res = await _client.GetAsync("constants/religious-qualification/all");
            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                var list = new List<SelectListItem>();

                foreach (var item in myObject)
                {
                    var id = Convert.ToString(item.id);
                    var name = Convert.ToString(item.name);

                    list.Add(new SelectListItem { Text = name, Value = id });
                }

                return list;
            }

            return null;
        }

        public async Task<List<SelectListItem>> GetSalutation()
        {
            try
            {
                var res = await _client.GetAsync("constants/salutation/all");
                if (res.IsSuccessStatusCode)
                {
                    var json = res.Content.ReadAsStringAsync().Result;
                    dynamic myObject = JArray.Parse(json);
                    var list = new List<SelectListItem>();

                    foreach (var item in myObject)
                    {
                        var id = Convert.ToString(item.id);
                        var name = Convert.ToString(item.name);
                        list.Add(new SelectListItem { Text = name, Value = id });
                    }

                    return list;
                }

                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        /*public async Task<List<SelectListItem>> GetInstitutions(string level = "", string subLevel = "" , string type = "")
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(BASE_URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var Res = await client.GetAsync("institution/all");
            if (Res.IsSuccessStatusCode)
            {
                var json = Res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                var list = new List<SelectListItem>();

                foreach (var item in myObject)
                {
                    var id = Convert.ToString(item.id);
                    var name = Convert.ToString(item.name);

                    list.Add(new SelectListItem { Text = name, Value = id });
                }

                return list;
            }
            return null;
        }*/

        public async Task<List<SelectListItem>> GetSkills()
        {
            //var res = await _client.GetAsync("constants/field-of-expertise/all");
            var res = await _client.GetAsync("constants/skill/all");

            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                var list = new List<SelectListItem>();

                foreach (var item in myObject)
                {
                    var id = Convert.ToString(item.id);
                    var name = Convert.ToString(item.name);

                    list.Add(new SelectListItem { Text = name, Value = id });
                }

                return list;
            }

            return null;
        }

        public async Task<AuthenticationResponse> GetToken(LoginModel model)
        {
            var json = JsonConvert.SerializeObject(model);

            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var content = new StringContent(json);
            var res = await _client.PostAsync("auth/login", httpContent);

            if (res.IsSuccessStatusCode)
            {
                var responseJson = res.Content.ReadAsStringAsync().Result;

                var response = new AuthenticationResponse();

                response = JsonConvert.DeserializeObject<AuthenticationResponse>(responseJson);

                return response;
            }

            return null;
        }

        public async Task<List<SelectListItem>> GetVoluntaryInstitution()
        {
            var res = await _client.GetAsync("constants/voluntary-publicserviceinstitution/all");
            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                var list = new List<SelectListItem>();

                foreach (var item in myObject)
                {
                    var id = $"{Convert.ToString(item.id)}-{Convert.ToString(item.name)}";
                    var name = Convert.ToString(item.name);

                    list.Add(new SelectListItem { Text = name, Value = id });
                }

                return list;
            }

            return null;
        }

        public async Task<PositionModel> MapSinglePosition(JObject positionArray)
        {
            var list = await GetMartialStatuses();
            var listAreaOfOrigin = await GetAreaOfOrigin();
            var salutationList = await GetSalutation();
            var jamatiTitleList = await GetJamatiTitles();
            var institutionList = await GetAllInstitutions();
            var nameOfDegreeList = await GetEducationalDegree();
            var religiousEducationList = await GetReligiousEducation();
            var regionalCouncilList = await GetRegionalCouncil();
            var listOfCountries = await GetAllCountries();
            var listOfLanguageProficiency = await GetLanguageProficiency();
            var voluntaryCommunityPositionList = await GetPositions();
            var highestLevelOfStudyList = await GetHighestLevelOfStudy();
            var akdnTrainingList = await GetAkdnTraining();
            var voluntaryCommunityInstitutionList = await GetPositionInstitution();
            var fieldOfInterestsList = await GetFieldOfInterests();
            var occupationTypeList = await GetOcupations();
            var typeOfBusinessList = await GetBussinessType();
            var natureOfBusinessList = await GetBussinessNature();
            var professionalMembershipsList = await GetProfessionalMemeberShipDetails();
            var languageList = await GetLanguages();
            var skillsList = await GetSkills();
            var relationList = await GetAllRelatives();
            var majorAreaOfStudy = await GetMajorAreaOfStudy();
            var fieldOfExpertiseList = await GetFieldOfExpertise();

            var listNominations = new List<NominationModel>();
            var positionModel = new PositionModel();

            try
            {
                var personAppointmentList = positionArray["personAppointmentList"];
                var currentCycle = positionArray["cycle"];
                var positionName = positionArray["position"];
                var instituion = positionArray["institution"];

                positionModel.Id = Convert.ToString(positionArray["appointmentPositionId"]);
                positionModel.CurrentCycle = Convert.ToString(currentCycle["name"]);
                positionModel.PositionName = Convert.ToString(positionName["name"]);
                positionModel.PositionId = Convert.ToString(positionName["id"]);
                positionModel.Required = Convert.ToInt32(positionArray["nominationsRequired"]);
                positionModel.Rank = Convert.ToInt32(positionArray["rank"]);
                positionModel.SeatId = Convert.ToString(positionArray["seatId"]);

                foreach (var jToken in personAppointmentList)
                {
                    var personsAppointed = (JObject)jToken;
                    var nominationModel = new NominationModel();

                    var incumbent = personsAppointed["person"];
                    if (Convert.ToInt32(personsAppointed["priority"]) == 0)
                    {
                        positionModel.Incubment = incumbent.ToObject<PersonModel>();
                        SetDetails(list, listAreaOfOrigin, salutationList, jamatiTitleList, nameOfDegreeList,
                            voluntaryCommunityInstitutionList,
                            occupationTypeList, institutionList, positionModel.Incubment);

                        positionModel.incumbentDetail.Priority = Convert.ToInt32(personsAppointed["priority"]);
                        positionModel.incumbentDetail.IsAppointed = Convert.ToBoolean(personsAppointed["appointed"]);
                        positionModel.incumbentDetail.IsRecommended = Convert.ToBoolean(personsAppointed["recommended"]);
                        positionModel.incumbentDetail.personAppointmentId = Convert.ToString(personsAppointed["personAppointmentId"]);
                        
                    }
                    else
                    {
                        nominationModel.Priority = Convert.ToInt32(personsAppointed["priority"]);
                        nominationModel.IsAppointed = Convert.ToBoolean(personsAppointed["appointed"]);
                        nominationModel.IsRecommended = Convert.ToBoolean(personsAppointed["recommended"]);
                        nominationModel.personAppointmentId = Convert.ToString(personsAppointed["personAppointmentId"]);
                        var person = personsAppointed["person"];
                        nominationModel.Person = person.ToObject<PersonModel>();

                        SetDetails(list, listAreaOfOrigin, salutationList, jamatiTitleList, nameOfDegreeList,
                            voluntaryCommunityInstitutionList,
                            occupationTypeList, institutionList, nominationModel.Person);

                        listNominations.Add(nominationModel);
                    }
                }

                listNominations.Sort((a, b) => a.Priority.CompareTo(b.Priority));
                positionModel.Nominations = listNominations;
            }
            catch (Exception)
            {
            }

            return positionModel;
        }

        public async Task<PositionModel> Nominate(string personId, string appointmentPositionId, int priority,
            string institutionId, string positionId, string cycleId,
            string seatNo)
        {
            PositionModel positionModel = null;

            try
            {
                var jObject = new JObject
                {
                    {"appointmentPositionId", appointmentPositionId},
                    {"isAppointed", false},
                    {"isRecommended", false},
                    {"personId", personId},
                    {"priority", priority + 1},
                    {"remarks", ""}
                };

                var json = JsonConvert.SerializeObject(jObject);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                var res = await _client.PostAsync("/person/appointment", httpContent);

                if (res.IsSuccessStatusCode)
                {
                    var response = await _client.GetAsync(
                        "/appointment-position/search/findByCycleIdAndInstitutionIdAndPositionIdAndSeatNo?cycleId=" +
                        cycleId +
                        "&institutionId=" + institutionId + "&positionId=" + positionId + "&seatNo=" + seatNo);

                    if (response.IsSuccessStatusCode)
                    {
                        var newJson = response.Content.ReadAsStringAsync().Result;
                        var obj = JArray.Parse(newJson);
                        foreach (var position in obj)
                        {
                            if (Convert.ToString(position["state"]) == "CREATED")
                            {
                                positionModel = await MapSinglePosition((JObject)position);
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }

            return positionModel;
        }

        public async Task<PositionModel> RemoveNomination(string personAppointmentId, string cycleId,
            string institutionId, string positionId, string seatNo,
            PositionModel position)
        {
            PositionModel positionModel = null;

            try
            {
                //removing nomination
                var res = await _client.DeleteAsync("/person/appointment/one/%7Bid%7D?entityId=" + personAppointmentId);

                if (res.IsSuccessStatusCode)
                {
                    //Reordering priority after removing nomination
                    var nominations = position.Nominations;
                    if (nominations != null) nominations.Sort((a, b) => a.Priority.CompareTo(b.Priority));

                    //this loop set the prioirty in a sequence for the remaining nominations.
                    var priorityIndex = 1;
                    var sortPriority = false;
                    foreach (var data in nominations)
                    {
                        if (data.personAppointmentId == personAppointmentId)
                        {
                            sortPriority = true;
                            continue;
                        }

                        if (sortPriority)
                            if (data.Priority != priorityIndex)
                            {
                                var jObject = new JObject
                                {
                                    {"appointmentPositionId", position.Id},
                                    {"isAppointed", false},
                                    {"isRecommended", false},
                                    {"personId", data.Person.Id},
                                    {"priority", priorityIndex},
                                    {"remarks", ""}
                                };

                                var json = JsonConvert.SerializeObject(jObject);
                                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                                var httpResponse =
                                    await _client.PutAsync("/person/appointment/one/" + data.personAppointmentId,
                                        httpContent);
                            }

                        priorityIndex++;
                    }

                    //getting updated data of nominations.

                    var response = await _client.GetAsync(
                        "/appointment-position/search/findByCycleIdAndInstitutionIdAndPositionIdAndSeatNo?cycleId=" +
                        cycleId +
                        "&institutionId=" + institutionId + "&positionId=" + positionId + "&seatNo=" + seatNo);

                    if (response.IsSuccessStatusCode)
                    {
                        var newJson = response.Content.ReadAsStringAsync().Result;
                        var obj = JArray.Parse(newJson);
                        foreach (var objPosition in obj)
                        {
                            if (Convert.ToString(objPosition["state"]) == "CREATED")
                            {
                                positionModel = await MapSinglePosition((JObject)objPosition);
                                break;
                            }
                        }
                    }

                    //if (response.IsSuccessStatusCode)
                    //{
                    //    var newJson = response.Content.ReadAsStringAsync().Result;
                    //    var obj = JObject.Parse(newJson);
                    //    positionModel = await MapSinglePosition(obj);
                    //}
                }
            }
            catch (Exception)
            {
            }

            return positionModel;
        }

        public async Task<PositionModel> reOrderNomination(List<NominationModel> nominationModel, string positionId,
            string cycleId, string institutionId,
            string seatNo, string id)
        {
            PositionModel positionModel = null;

            try
            {
                foreach (var data in nominationModel)
                {
                    var jObject = new JObject
                    {
                        {"appointmentPositionId", positionId},
                        {"isAppointed", false},
                        {"isRecommended", false},
                        {"personId", data.Person.Id},
                        {"priority", data.Priority},
                        {"remarks", ""}
                    };

                    var json = JsonConvert.SerializeObject(jObject);
                    var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                    var httpResponse = await _client.PutAsync("/person/appointment/one/" + data.personAppointmentId,
                        httpContent);
                }

                var response = await _client.GetAsync(
                    "/appointment-position/search/findByCycleIdAndInstitutionIdAndPositionIdAndSeatNo?cycleId=" +
                    cycleId +
                    "&institutionId=" + institutionId + "&positionId=" + id + "&seatNo=" + seatNo);

                if (response.IsSuccessStatusCode)
                {
                    var newJson = response.Content.ReadAsStringAsync().Result;
                    var obj = JArray.Parse(newJson);
                    foreach (var objPosition in obj)
                    {
                        if (Convert.ToString(objPosition["state"]) == "CREATED")
                        {
                            positionModel = await MapSinglePosition((JObject)objPosition);
                            break;
                        }
                    }
                }

                //if (response.IsSuccessStatusCode)
                //{
                //    var newJson = response.Content.ReadAsStringAsync().Result;
                //    var obj = JObject.Parse(newJson);
                //    positionModel = await MapSinglePosition(obj);
                //}
            }
            catch (Exception)
            {
            }

            return positionModel;
        }

        public async Task<PositionModel> Recommend(NominationModel nominationModel, PositionModel positionModel, string cycleId, string institutionId, bool isIncumbent)
        {
            PositionModel updatedPosition = null;

            try
            {
                JObject jObject = null;
                if (!isIncumbent)
                {
                    jObject = new JObject
                    {
                        {"appointmentPositionId", positionModel.Id},
                        {"isAppointed", false},
                        {"isRecommended", true},
                        {"personId", nominationModel.Person.Id},
                        {"priority", nominationModel.Priority},
                        {"remarks", ""}
                    };
                }
                else
                {
                    jObject = new JObject
                    {
                        {"appointmentPositionId", positionModel.Id},
                        {"isAppointed", true},
                        {"isRecommended", true},
                        {"personId", nominationModel.Person.Id},
                        {"priority", nominationModel.Priority},
                        {"remarks", ""}
                    };

                }

                var json = JsonConvert.SerializeObject(jObject);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _client.PutAsync("/person/appointment/one/" + nominationModel.personAppointmentId,
                    httpContent);

                if (response.IsSuccessStatusCode)
                {
                    var resp = await _client.GetAsync(
                        "/appointment-position/search/findByCycleIdAndInstitutionIdAndPositionIdAndSeatNo?cycleId=" +
                        cycleId +
                        "&institutionId=" + institutionId + "&positionId=" + positionModel.PositionId + "&seatNo=" + positionModel.SeatId);

                    if (response.IsSuccessStatusCode)
                    {
                        var newJson = resp.Content.ReadAsStringAsync().Result;
                        var obj = JArray.Parse(newJson);
                        foreach (var objPosition in obj)
                        {
                            if (Convert.ToString(objPosition["state"]) == "CREATED")
                            {
                                updatedPosition = await MapSinglePosition((JObject)objPosition);
                                break;
                            }
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {

            }
            
            return updatedPosition;
        }

        public async Task<bool> SavePersonData(PersonModel personModel)
        {
            var json = JsonConvert.SerializeObject(personModel);

            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var content = new StringContent(json);
            var res = await _client.PostAsync("person", httpContent);

            return res.StatusCode == HttpStatusCode.OK ? true : false;
        }

        public bool SearchByCnic(string cnic, out PersonModel model)
        {
            model = null;

            var res = _client.GetAsync("person/search/cnic?cnic=" + cnic).Result;
            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;

                var familyRelation = JsonConvert.DeserializeObject<FamilyRelationModel>(json);
                model = new PersonModel
                {
                    RelativeCnic = familyRelation.Cnic,
                    RelativeSalutation = familyRelation.Salutation,
                    RelativeFirstName = familyRelation.FirstName,
                    RelativeDateOfBirth = familyRelation.DateOfBirth,
                    RelativeFamilyName = familyRelation.FamilyName,
                    RelativeFathersName = familyRelation.FathersName,
                    RelativeJamatiTitle = familyRelation.JamatiTitle,
                    RelativeRelation = familyRelation.Relation
                };

                return true;
            }

            return false;
        }

        public async Task<List<PersonModel>> SearchPerson(string cnic, string firstName, string lastName)
        {
            var res = await _client.GetAsync("person/search/findByCnicOrFirstNameOrLastName?cnic=" + cnic +
                                             "&firstName=" + firstName + "&lastName=" + lastName);
            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;

                var person = new List<PersonModel>();

                person = JsonConvert.DeserializeObject<List<PersonModel>>(json);

                return person;
            }

            return null;
        }

        public async Task<PersonModel> SearchPersonByFormNumber(string formNumber, string personId, string id)
        {
            var res = await _client.GetAsync("person/search/findByFormNo?formNo=" + formNumber);
            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;

                return null;
            }

            return null;
        }
        
        public async Task<bool> AddNewData(CrudModel model)
        {
            model.IsActive = true;
            var json = JsonConvert.SerializeObject(model);

            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var content = new StringContent(json);
            var res = await _client.PostAsync(model.Url, httpContent);

            return res.StatusCode == HttpStatusCode.OK ? true : false;
        }

        public async Task<bool> AddNewCountry(CountryModel model)
        {
            model.IsActive = true;
            var json = JsonConvert.SerializeObject(model);

            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var content = new StringContent(json);
            var res = await _client.PostAsync(model.Url, httpContent);

            return res.StatusCode == HttpStatusCode.OK ? true : false;
        }

        public async Task<bool> AddNewCity(CityModel model)
        {
            model.CountryId = model.CountryId.Split('-')[0];

            model.IsActive = true;
            var json = JsonConvert.SerializeObject(model);

            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var content = new StringContent(json);
            var res = await _client.PostAsync(model.Url, httpContent);

            return res.StatusCode == HttpStatusCode.OK ? true : false;
        }

        public async Task<bool> AddNewJamatiTitle(JamatiTitle model)
        {
            model.IsActive = true;
            var json = JsonConvert.SerializeObject(model);

            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var content = new StringContent(json);
            var res = await _client.PostAsync(model.Url, httpContent);

            return res.StatusCode == HttpStatusCode.OK ? true : false;
        }

        public async Task<List<CycleModel>> GetAllCycles()
        {
            var res = await _client.GetAsync("cycle/all");
            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                var list = new List<CycleModel>();

                foreach (var item in myObject)
                {
                    var id = Convert.ToString(item.id);
                    var name = Convert.ToString(item.name);
                    var startDate = Convert.ToDateTime(item.startDate);
                    var endDate = Convert.ToDateTime(item.endDate);

                    list.Add(new CycleModel {Id = id, Name = name, StartDate = startDate, EndDate = endDate });
                }

                return list;
            }

            return null;
        }

        public async Task<bool> CreateNewCycle(CycleModel model)
        {
            try
            {
                JObject jObject = new JObject();
                jObject.Add("previousCycleId", model.PreviousCycle);
                jObject.Add("startDate", model.StartDate);

                JObject cycleDetail = new JObject();
                cycleDetail.Add("createdBy", "");
                cycleDetail.Add("endDate", model.EndDate);
                cycleDetail.Add("id", 0);
                cycleDetail.Add("isActive", true);
                cycleDetail.Add("midtermCycle", false);
                cycleDetail.Add("name", model.CycleNameForDisplay);
                cycleDetail.Add("nominatedCount", 0);
                cycleDetail.Add("parentCycle", 0);
                cycleDetail.Add("previousCycle", model.PreviousCycle);
                cycleDetail.Add("recommendedCount", 0);
                cycleDetail.Add("startDate", model.StartDate);
                cycleDetail.Add("state", "");
                cycleDetail.Add("updatedBy", "");

                jObject.Add("cycleDetails", cycleDetail);

                var json = JsonConvert.SerializeObject(jObject);

                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                var res = await _client.PostAsync("cycle/create", httpContent);

                return res.StatusCode == HttpStatusCode.OK ? true : false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<SelectListItem>> findPositionByInstituionId(string InstitutionId)
        {
            var res = await _client.GetAsync("position/search/findByInstitutionId?institutionId="+InstitutionId);
            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                var list = new List<SelectListItem>();

                foreach (var item in myObject)
                {
                    var id = $"{Convert.ToString(item.id)}-{Convert.ToString(item.name)}";
                    var name = Convert.ToString(item.name);

                    list.Add(new SelectListItem { Text = name, Value = id });
                }

                return list;
            }

            return null;
        }

        public async Task<bool> appoint(string id)
        {
            var httpContent = new StringContent(id, Encoding.UTF8, "application/json");
            var res = await _client.PostAsync("cycle/appoint", httpContent);

            return res.StatusCode == HttpStatusCode.OK ? true : false;
        }

        public async Task<bool> close(MidTermCycle model)
        {
            JObject jObject = new JObject();
            jObject.Add("CycleId",model.CycleId);
            jObject.Add("endingDate", model.StartDate);

            var json = JsonConvert.SerializeObject(jObject);

            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var res = await _client.PostAsync("cycle/close", httpContent);

            return res.StatusCode == HttpStatusCode.OK ? true : false;
        }
        
        #endregion Public Methods
    }
}