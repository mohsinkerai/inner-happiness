﻿using AMS.frontend.web.Areas.Operations.Models.Nominations;
using AMS.frontend.web.Areas.Operations.Models.Persons;
using AMS.frontend.web.Models.Authenticate;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AMS.frontend.web.Areas.Operations.Models
{
    public class RestfulClient
    {
        #region Private Fields

        private readonly string _baseUrl = "http://is.bismagreens.com:8080/";

        //http://localhost:8080/

        private readonly HttpClient _client;

        #endregion Private Fields

        public RestfulClient(string token)
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(_baseUrl)
            };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

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

        public async Task<List<PositionModel>> GetInstitutionTypes(string level, string subLevel)
        {

            //var Res = await client.GetAsync("institution/search/findByLevelType?levelTypeId="+subLevel);
            //var Res = await client.GetAsync("institution/search/findByLevelId?levelId=" + subLevel);

            if (level == null)
            {
                return null;
            }

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
            string lastName, string cnic, string formNo, string jamatiTitle, string degree, string majorAreaOfStudy, string academicInstitution,
            int pageNumber, int pageSize)
        {

            //var Res = await client.GetAsync("/person/search/findByCnicOrFirstNameOrLastName?firstName&cnic&lastName&page=1&size=1");

            var url = "";
            if (string.IsNullOrWhiteSpace(firstName) && string.IsNullOrWhiteSpace(lastName) && string.IsNullOrWhiteSpace(cnic) &&
                string.IsNullOrWhiteSpace(formNo) && string.IsNullOrWhiteSpace(jamatiTitle) && string.IsNullOrWhiteSpace(degree) &&
                string.IsNullOrWhiteSpace(majorAreaOfStudy) && string.IsNullOrWhiteSpace(academicInstitution))
            {
                url = "/person/all/paginated?page=" + pageNumber + "&size=" + pageSize;
            }
            else
            {
                url = "/person/search/findByCnicOrFNameOrLNameOrIdOrDegreeOrAcadInstOrJamatiTitleOrMaos?cnic=" + cnic + "&firstName=" + firstName + "&lastName=" + lastName + "&id=" + formNo + "&degree=" + degree +
                    "&inst=" + academicInstitution + "&jamatiTitle=" + jamatiTitle + "&maos=" + majorAreaOfStudy + "&page=" + pageNumber + "&size=" + pageSize;
            }

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

                    person = JsonConvert.DeserializeObject<List<PersonModel>>(jsonObject["content"].ToString(), settings);
                }
                catch (Exception)
                { }

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

        public async Task<bool> SavePersonData(PersonModel personModel)
        {

            var json = JsonConvert.SerializeObject(personModel);

            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var content = new StringContent(json);
            var res = await _client.PostAsync("person", httpContent);

            return res.StatusCode == HttpStatusCode.OK ? true : false;
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
                var res = await _client.GetAsync("appointment-position/search/findByCycleIdAndInstitutionId?cycleId=" + cycle + "&institutionId=" + id);

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

                        var personAppointmentList = positionArray["personAppointmentList"];
                        var currentCycle = positionArray["cycle"];
                        var positionName = positionArray["position"];
                        var instituion = positionArray["institution"];

                        positionModel.Id = Convert.ToString(positionArray["appointmentPositionId"]);
                        positionModel.CurrentCycle = Convert.ToString(currentCycle["name"]);
                        positionModel.PositionName = Convert.ToString(positionName["name"]);
                        positionModel.Required = Convert.ToInt32(positionArray["nominationsRequired"]);

                        //int index = 0;
                        foreach (var jToken in personAppointmentList)
                        {
                            var personsAppointed = (JObject) jToken;
                            var nominationModel = new NominationModel();

                            var incumbent = personsAppointed["person"];
                            if (Convert.ToInt32(personsAppointed["priority"]) == 0)
                            {
                                positionModel.Incubment = incumbent.ToObject<PersonModel>();
                                SetDetails(list, listAreaOfOrigin, salutationList, jamatiTitleList, nameOfDegreeList, voluntaryCommunityInstitutionList,
                                    occupationTypeList, institutionList, positionModel.Incubment);
                            }
                            else
                            {
                                nominationModel.Priority = Convert.ToInt32(personsAppointed["priority"]);
                                nominationModel.IsAppointed = Convert.ToBoolean(personsAppointed["appointed"]);
                                nominationModel.IsRecommended = Convert.ToBoolean(personsAppointed["recommended"]);
                                var person = personsAppointed["person"];
                                nominationModel.Person = person.ToObject<PersonModel>();

                                SetDetails(list, listAreaOfOrigin, salutationList, jamatiTitleList, nameOfDegreeList, voluntaryCommunityInstitutionList,
                                occupationTypeList, institutionList, nominationModel.Person);
                               
                                listNominations.Add(nominationModel);
                            }
                            //index++;
                        }
                        listNominations.Sort((a, b) => (a.Priority.CompareTo(b.Priority)));
                        positionModel.Nominations = listNominations;

                        listPositions.Add(positionModel);

                        nominationDetailModel.Institution.Id = Convert.ToString(instituion["id"]);
                        nominationDetailModel.Institution.Name = Convert.ToString(instituion["name"]);
                    }

                    nominationDetailModel.Positions = listPositions;
                }
            }
            catch (Exception ex)
            {
 
            }
            return nominationDetailModel;
        }

        private static void SetDetails(List<SelectListItem> list, List<SelectListItem> listAreaOfOrigin, List<SelectListItem> salutationList, List<SelectListItem> jamatiTitleList,
            List<SelectListItem> nameOfDegreeList,List<SelectListItem> voluntaryCommunityInstitutionList, List<SelectListItem> occupationTypeList,
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
                {
                    foreach (var education in personModel.Educations)
                    {

                        var edu = nameOfDegreeList.Find(x => x.Value.StartsWith($"{education.NameOfDegree}-"));
                        education.NameOfDegreeName = edu.Text;

                        var institute = institutionList.Find(x => x.Value.StartsWith($"{education.Institution}-"));
                        education.InstitutionName = institute.Text;
                    }
                }
                if (personModel.VoluntaryCommunityServices != null)
                {
                    foreach (var communityInstituion in personModel.VoluntaryCommunityServices)
                    {
                        var institution = voluntaryCommunityInstitutionList.Find(x => x.Value == communityInstituion.Institution);
                        communityInstituion.InstitutionName = institution.Text;
                    }
                }
                if (personModel.OccupationType != null)
                {
                    var ocupation = occupationTypeList.Find(x => x.Value == personModel.OccupationType);
                    personModel.OccupationTypeName = ocupation.Text;
                }
            }
            catch (Exception ex)
            {

            }
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

        public async Task<PersonModel> Nominate(string personId, string positionId)
        {

            var jObject = new JObject
            {
                { "appointed", true },
                { "cpiId", positionId },
                { "id", 0 },
                { "personId", personId },
                { "priority", 0 },
                { "recommended", true }
            };

            var json = JsonConvert.SerializeObject(jObject);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var res = await _client.PostAsync("/person/cpi", httpContent);

            if (res.IsSuccessStatusCode)
            {
                var response = res.Content.ReadAsStringAsync().Result;

                return null;
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

        #endregion Public Methods
    }

    public partial class AuthenticationResponse
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("user")]
        public string User { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("expiry")]
        public DateTime Expiry { get; set; }

        [JsonProperty("roles")]
        public string[] Roles { get; set; }

        [JsonProperty("authenticated")]
        public bool Authenticated { get; set; }

        [JsonProperty("principal")]
        public string Principal { get; set; }

        [JsonProperty("details")]
        public object Details { get; set; }

        [JsonProperty("authorities")]
        public Authority[] Authorities { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("credentials")]
        public string Credentials { get; set; }
    }

    public partial class Authority
    {
        [JsonProperty("amsAuthority")]
        public string AmsAuthority { get; set; }

        [JsonProperty("authority")]
        public string AuthorityAuthority { get; set; }
    }

    public class PastAppointment
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("position")]
        public Position Position { get; set; }

        [JsonProperty("institution")]
        public Institution Institution { get; set; }

        [JsonProperty("seatNo")]
        public long SeatNo { get; set; }

        [JsonProperty("cycleId")]
        public CycleId CycleId { get; set; }

        [JsonProperty("nominationsRequired")]
        public long NominationsRequired { get; set; }

        [JsonProperty("mowlaAppointee")]
        public bool MowlaAppointee { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonIgnore]
        public string PositionName => $"{Position.Name} - {Institution.Name}";

        [JsonIgnore]
        public string CycleName => $"{CycleId.StartDate.Year.ToString()} - {CycleId.EndDate.Year.ToString()}";
    }

    public class CycleId
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("startDate")]
        public DateTime StartDate { get; set; }

        [JsonProperty("endDate")]
        public DateTime EndDate { get; set; }
    }

    public class Institution
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("levelId")]
        public long LevelId { get; set; }
    }

    public class Position
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}