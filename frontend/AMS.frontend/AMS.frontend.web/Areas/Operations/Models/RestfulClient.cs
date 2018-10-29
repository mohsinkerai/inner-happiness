using AMS.frontend.web.Areas.Operations.Models.Nominations;
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
            string json = JsonConvert.SerializeObject(personModel);

            StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            StringContent content = new StringContent(json);
            HttpResponseMessage res = await _client.PutAsync("person/one/" + personModel.Id, httpContent);

            return res.IsSuccessStatusCode;
        }

        public async Task<List<SelectListItem>> GetAkdnTraining()
        {
            HttpResponseMessage res = await _client.GetAsync("constants/akdn-training/all");
            if (res.IsSuccessStatusCode)
            {
                string json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                List<SelectListItem> list = new List<SelectListItem>();

                foreach (dynamic item in myObject)
                {
                    string id = $"{Convert.ToString(item.id)}-{Convert.ToString(item.name)}";
                    dynamic name = Convert.ToString(item.name);

                    list.Add(new SelectListItem { Text = name, Value = id });
                }

                return list;
            }

            return null;
        }

        public async Task<List<SelectListItem>> GetAllCompanies()
        {

            HttpResponseMessage res = await _client.GetAsync("company/all");
            if (res.IsSuccessStatusCode)
            {
                string json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                List<SelectListItem> list = new List<SelectListItem>();

                foreach (dynamic item in myObject)
                {
                    dynamic id = Convert.ToString(item.id);
                    dynamic name = Convert.ToString(item.name);

                    list.Add(new SelectListItem { Text = name, Value = id });
                }

                return list;
            }

            return null;
        }

        public async Task<List<SelectListItem>> GetAllCountries()
        {

            HttpResponseMessage res = await _client.GetAsync("constants/country/all");
            if (res.IsSuccessStatusCode)
            {
                string json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                List<SelectListItem> list = new List<SelectListItem>();

                foreach (dynamic item in myObject)
                {
                    string id = $"{Convert.ToString(item.id)}-{Convert.ToString(item.name)}";
                    dynamic name = Convert.ToString(item.name);

                    list.Add(new SelectListItem { Text = name, Value = id });
                }

                return list;
            }

            return null;
        }

        public async Task<List<SelectListItem>> GetAllInstitutions()
        {

            HttpResponseMessage res = await _client.GetAsync("constants/educational-publicserviceinstitution/all");
            if (res.IsSuccessStatusCode)
            {
                string json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                List<SelectListItem> list = new List<SelectListItem>();

                foreach (dynamic item in myObject)
                {
                    string id = $"{Convert.ToString(item.id)}-{Convert.ToString(item.name)}";
                    dynamic name = Convert.ToString(item.name);

                    list.Add(new SelectListItem { Text = name, Value = id });
                }

                return list;
            }

            return null;
        }

        public async Task<List<SelectListItem>> GetAllRelatives()
        {

            HttpResponseMessage res = await _client.GetAsync("constants/relation/all");
            if (res.IsSuccessStatusCode)
            {
                string json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                List<SelectListItem> list = new List<SelectListItem>();

                foreach (dynamic item in myObject)
                {
                    string id = $"{Convert.ToString(item.id)}-{Convert.ToString(item.name)}";
                    dynamic name = Convert.ToString(item.name);

                    list.Add(new SelectListItem { Text = name, Value = id });
                }

                return list;
            }

            return null;
        }

        public async Task<List<SelectListItem>> GetAreaOfOrigin()
        {

            HttpResponseMessage res = await _client.GetAsync("constants/area-of-origin/all");
            if (res.IsSuccessStatusCode)
            {
                string json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                List<SelectListItem> list = new List<SelectListItem>();

                foreach (dynamic item in myObject)
                {
                    dynamic id = Convert.ToString(item.id);
                    dynamic name = Convert.ToString(item.name);

                    list.Add(new SelectListItem { Text = name, Value = id });
                }

                return list;
            }

            return null;
        }

        public async Task<List<SelectListItem>> GetBussinessNature()
        {

            HttpResponseMessage res = await _client.GetAsync("constants/business-nature/all");
            if (res.IsSuccessStatusCode)
            {
                string json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                List<SelectListItem> list = new List<SelectListItem>();

                foreach (dynamic item in myObject)
                {
                    string id = $"{Convert.ToString(item.id)}-{Convert.ToString(item.name)}";
                    dynamic name = Convert.ToString(item.name);

                    list.Add(new SelectListItem { Text = name, Value = id });
                }

                return list;
            }

            return null;
        }

        public async Task<List<SelectListItem>> GetBussinessType()
        {

            HttpResponseMessage res = await _client.GetAsync("constants/business-type/all");
            if (res.IsSuccessStatusCode)
            {
                string json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                List<SelectListItem> list = new List<SelectListItem>();

                foreach (dynamic item in myObject)
                {
                    string id = $"{Convert.ToString(item.id)}-{Convert.ToString(item.name)}";
                    dynamic name = Convert.ToString(item.name);

                    list.Add(new SelectListItem { Text = name, Value = id });
                }

                return list;
            }

            return null;
        }

        public async Task<List<SelectListItem>> GetCities()
        {

            HttpResponseMessage res = await _client.GetAsync("constants/city/all");
            if (res.IsSuccessStatusCode)
            {
                string json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                List<SelectListItem> list = new List<SelectListItem>();

                foreach (dynamic item in myObject)
                {
                    dynamic id = Convert.ToString(item.id);
                    dynamic name = Convert.ToString(item.name);

                    list.Add(new SelectListItem { Text = name, Value = id });
                }

                return list;
            }

            return null;
        }

        public async Task<List<SelectListItem>> GetEducationalDegree()
        {

            HttpResponseMessage res = await _client.GetAsync("constants/educational-degree/all");
            if (res.IsSuccessStatusCode)
            {
                string json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                List<SelectListItem> list = new List<SelectListItem>();

                foreach (dynamic item in myObject)
                {
                    string id = $"{Convert.ToString(item.id)}-{Convert.ToString(item.name)}";
                    dynamic name = Convert.ToString(item.name);

                    list.Add(new SelectListItem { Text = name, Value = id });
                }

                return list;
            }

            return null;
        }

        public async Task<List<SelectListItem>> GetFieldOfInterests()
        {

            HttpResponseMessage res = await _client.GetAsync("constants/field-of-interest/all");
            if (res.IsSuccessStatusCode)
            {
                string json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                List<SelectListItem> list = new List<SelectListItem>();

                foreach (dynamic item in myObject)
                {
                    dynamic id = Convert.ToString(item.id);
                    dynamic name = Convert.ToString(item.name);

                    list.Add(new SelectListItem { Text = name, Value = id });
                }

                return list;
            }

            return null;
        }

        public async Task<List<SelectListItem>> GetHighestLevelOfStudy()
        {

            HttpResponseMessage res = await _client.GetAsync("constants/secular-study-level/all");
            if (res.IsSuccessStatusCode)
            {
                string json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                List<SelectListItem> list = new List<SelectListItem>();

                foreach (dynamic item in myObject)
                {
                    dynamic id = Convert.ToString(item.id);
                    dynamic name = Convert.ToString(item.name);

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

            HttpResponseMessage res = await _client.GetAsync(string.IsNullOrWhiteSpace(subLevel)
                ? "institution/search/findByLevelType?levelTypeId=1"
                : "institution/search/findByLevelId?levelId=" + subLevel);

            if (res.IsSuccessStatusCode)
            {
                string json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                List<PositionModel> list = new List<PositionModel>();

                foreach (dynamic item in myObject)
                {
                    dynamic id = Convert.ToString(item.id);
                    dynamic name = Convert.ToString(item.name);
                    dynamic fullName = Convert.ToString(item.fullName);

                    PositionModel positionModel = new PositionModel
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

            HttpResponseMessage res = await _client.GetAsync("constants/jamati-title/all");
            if (res.IsSuccessStatusCode)
            {
                string json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                List<SelectListItem> list = new List<SelectListItem>();

                foreach (dynamic item in myObject)
                {
                    dynamic id = Convert.ToString(item.id);
                    dynamic name = Convert.ToString(item.name);

                    list.Add(new SelectListItem { Text = name, Value = id });
                }

                return list;
            }

            return null;
        }

        public async Task<List<SelectListItem>> GetJamatkhana(string id = "")
        {

            HttpResponseMessage res = await _client.GetAsync(string.IsNullOrWhiteSpace(id)
                ? "level/search/type?value=4"
                : "level/search/parent?value=" + id);
            if (res.IsSuccessStatusCode)
            {
                string json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                List<SelectListItem> list = new List<SelectListItem>();

                foreach (dynamic item in myObject)
                {
                    dynamic id1 = Convert.ToString(item.id);
                    dynamic name = Convert.ToString(item.name);

                    list.Add(new SelectListItem { Text = name, Value = id1 });
                }

                return list;
            }

            return null;
        }

        public async Task<List<SelectListItem>> GetLanguageProficiency()
        {

            HttpResponseMessage res = await _client.GetAsync("/constants/language-proficiency/all");
            if (res.IsSuccessStatusCode)
            {
                string json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                List<SelectListItem> list = new List<SelectListItem>();

                foreach (dynamic item in myObject)
                {
                    string id = $"{Convert.ToString(item.id)}-{Convert.ToString(item.name)}";
                    dynamic name = Convert.ToString(item.name);

                    list.Add(new SelectListItem { Text = name, Value = id });
                }

                return list;
            }

            return null;
        }

        public async Task<List<SelectListItem>> GetLanguages()
        {

            HttpResponseMessage res = await _client.GetAsync("constants/language/all");
            if (res.IsSuccessStatusCode)
            {
                string json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                List<SelectListItem> list = new List<SelectListItem>();

                foreach (dynamic item in myObject)
                {
                    string id = $"{Convert.ToString(item.id)}-{Convert.ToString(item.name)}";
                    dynamic name = Convert.ToString(item.name);

                    list.Add(new SelectListItem { Text = name, Value = id });
                }

                return list;
            }

            return null;
        }

        public async Task<List<SelectListItem>> GetLocalCouncil(string uid = "")
        {
            HttpResponseMessage res = await _client.GetAsync(string.IsNullOrWhiteSpace(uid)
                ? "level/search/type?value=3"
                : "level/search/parent?value=" + uid);
            if (res.IsSuccessStatusCode)
            {
                string json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                List<SelectListItem> list = new List<SelectListItem>();

                foreach (dynamic item in myObject)
                {
                    dynamic id = Convert.ToString(item.id);
                    dynamic name = Convert.ToString(item.name);

                    list.Add(new SelectListItem { Text = name, Value = id });
                }

                return list;
            }

            return null;
        }

        public async Task<List<SelectListItem>> GetLocalInstitutions()
        {

            HttpResponseMessage res = await _client.GetAsync("institution/search/findByLevelType?levelTypeId=3");

            if (res.IsSuccessStatusCode)
            {
                string json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                List<SelectListItem> list = new List<SelectListItem>();

                foreach (dynamic item in myObject)
                {
                    dynamic id = Convert.ToString(item.id);
                    dynamic name = Convert.ToString(item.name);

                    list.Add(new SelectListItem { Text = name, Value = id });
                }

                return list;
            }

            return null;
        }

        public async Task<List<SelectListItem>> GetMartialStatuses()
        {

            HttpResponseMessage res = await _client.GetAsync("constants/marital-status/all");
            if (res.IsSuccessStatusCode)
            {
                string json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                List<SelectListItem> list = new List<SelectListItem>();

                foreach (dynamic item in myObject)
                {
                    dynamic id = Convert.ToString(item.id);
                    dynamic name = Convert.ToString(item.name);

                    list.Add(new SelectListItem { Text = name, Value = id });
                }

                return list;
            }

            return null;
        }

        public async Task<List<SelectListItem>> GetOcupations()
        {

            HttpResponseMessage res = await _client.GetAsync("constants/occupation/all");
            if (res.IsSuccessStatusCode)
            {
                string json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                List<SelectListItem> list = new List<SelectListItem>();

                foreach (dynamic item in myObject)
                {
                    dynamic id = Convert.ToString(item.id);
                    dynamic name = Convert.ToString(item.name);

                    list.Add(new SelectListItem { Text = name, Value = id });
                }

                return list;
            }

            return null;
        }

        public async Task<List<PersonModel>> GetPersonDetails()
        {

            HttpResponseMessage res = await _client.GetAsync("person/all");
            if (res.IsSuccessStatusCode)
            {
                string json = res.Content.ReadAsStringAsync().Result;

                List<PersonModel> person = JsonConvert.DeserializeObject<List<PersonModel>>(json);

                return person;
            }

            return null;
        }

        public async Task<PersonModel> GetPersonDetailsById(string id)
        {

            HttpResponseMessage res = await _client.GetAsync("person/one/" + id);
            if (res.IsSuccessStatusCode)
            {
                string json = res.Content.ReadAsStringAsync().Result;

                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                };

                PersonModel person = JsonConvert.DeserializeObject<PersonModel>(json, settings);

                return person;
            }

            return null;
        }

        public async Task<Tuple<List<PersonModel>, int>> GetPersonDetailsThroughPagging(string firstName,
            string cnic, string formNo, string jamatiTitle, string degree, string majorAreaOfStudy, string academicInstitution,
            int pageNumber, int pageSize)
        {

            //var Res = await client.GetAsync("/person/search/findByCnicOrFirstNameOrLastName?firstName&cnic&lastName&page=1&size=1");

            string url = "";
            if (string.IsNullOrWhiteSpace(firstName) && string.IsNullOrWhiteSpace(cnic) &&
                string.IsNullOrWhiteSpace(formNo) && string.IsNullOrWhiteSpace(jamatiTitle) && string.IsNullOrWhiteSpace(degree) &&
                string.IsNullOrWhiteSpace(majorAreaOfStudy) && string.IsNullOrWhiteSpace(academicInstitution))
            {
                url = "/person/all/paginated?page=" + pageNumber + "&size=" + pageSize;
            }
            else
            {
                //url = "/person/search/findByCnicOrFNameOrLNameOrIdOrDegreeOrAcadInstOrJamatiTitleOrMaos?cnic=" + cnic + "&firstName=" + firstName + "&lastName=" + lastName + "&id=" + formNo + "&degree=" + degree +
                //    "&inst=" + academicInstitution + "&jamatiTitle=" + jamatiTitle + "&maos=" + majorAreaOfStudy + "&page=" + pageNumber + "&size=" + pageSize;

                url = "/person/search/findByCnicAndNameAndIdAndDegreeAndAcadInstAndJamatiTitleAndMaos?cnic=" + cnic +
                      "&name=" + firstName + "&id=" + formNo + "&degree=" +
                      (string.IsNullOrWhiteSpace(degree) ? degree : degree.Split('-')[0]) +
                      "&inst=" +
                      (string.IsNullOrWhiteSpace(academicInstitution)
                          ? academicInstitution
                          : academicInstitution.Split('-')[0]) + "&jamatiTitle=" + jamatiTitle + "&maos=" +
                      (string.IsNullOrWhiteSpace(majorAreaOfStudy)
                          ? majorAreaOfStudy
                          : majorAreaOfStudy.Split('-')[0]) +
                      "&page=" + pageNumber + "&size=" + pageSize;
            }

            HttpResponseMessage res = await _client.GetAsync(url);
            if (res.IsSuccessStatusCode)
            {
                string json = res.Content.ReadAsStringAsync().Result;

                List<PersonModel> person = new List<PersonModel>();

                JObject jsonObject = JObject.Parse(json);

                try
                {
                    JsonSerializerSettings settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    };

                    person = JsonConvert.DeserializeObject<List<PersonModel>>(jsonObject["content"].ToString(), settings);
                }
                catch (Exception)
                { }

                int totalElements = Convert.ToInt32(jsonObject["totalElements"]);

                return new Tuple<List<PersonModel>, int>(person, totalElements);
            }

            return null;
        }

        public async Task<List<SelectListItem>> GetPositionInstitution()
        {

            HttpResponseMessage res = await _client.GetAsync("institution/all");
            if (res.IsSuccessStatusCode)
            {
                string json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                List<SelectListItem> list = new List<SelectListItem>();

                foreach (dynamic item in myObject)
                {
                    string id = $"{Convert.ToString(item.id)}-{Convert.ToString(item.name)}";
                    dynamic name = Convert.ToString(item.name);

                    list.Add(new SelectListItem { Text = name, Value = id });
                }

                return list;
            }

            return null;
        }

        public async Task<List<SelectListItem>> GetPositions()
        {

            HttpResponseMessage res = await _client.GetAsync("position/all");
            if (res.IsSuccessStatusCode)
            {
                string json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                List<SelectListItem> list = new List<SelectListItem>();

                foreach (dynamic item in myObject)
                {
                    string id = $"{Convert.ToString(item.id)}-{Convert.ToString(item.name)}";
                    dynamic name = Convert.ToString(item.name);

                    list.Add(new SelectListItem { Text = name, Value = id });
                }

                return list;
            }

            return null;
        }

        public async Task<List<SelectListItem>> GetProfessionalMemeberShipDetails()
        {

            HttpResponseMessage res = await _client.GetAsync("constants/professional-membership/all");
            if (res.IsSuccessStatusCode)
            {
                string json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                List<SelectListItem> list = new List<SelectListItem>();

                foreach (dynamic item in myObject)
                {
                    dynamic id = Convert.ToString(item.id);
                    dynamic name = Convert.ToString(item.name);

                    list.Add(new SelectListItem { Text = name, Value = id });
                }

                return list;
            }

            return null;
        }

        public async Task<List<SelectListItem>> GetRegionalCouncil()
        {

            HttpResponseMessage res = await _client.GetAsync("institution/search/findByLevelType?levelTypeId=2");
            if (res.IsSuccessStatusCode)
            {
                string json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                List<SelectListItem> list = new List<SelectListItem>();

                foreach (dynamic item in myObject)
                {
                    dynamic id = Convert.ToString(item.id);
                    dynamic name = Convert.ToString(item.name);

                    list.Add(new SelectListItem { Text = name, Value = id });
                }

                return list;
            }

            return null;
        }

        public async Task<List<SelectListItem>> GetRegionalInstitutions()
        {

            HttpResponseMessage res = await _client.GetAsync("institution/search/findByLevelType?levelTypeId=2");
            if (res.IsSuccessStatusCode)
            {
                string json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                List<SelectListItem> list = new List<SelectListItem>();

                foreach (dynamic item in myObject)
                {
                    dynamic id = Convert.ToString(item.id);
                    dynamic name = Convert.ToString(item.name);

                    list.Add(new SelectListItem { Text = name, Value = id });
                }

                return list;
            }

            return null;
        }

        public async Task<List<SelectListItem>> GetReligiousEducation()
        {

            HttpResponseMessage res = await _client.GetAsync("constants/religious-qualification/all");
            if (res.IsSuccessStatusCode)
            {
                string json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                List<SelectListItem> list = new List<SelectListItem>();

                foreach (dynamic item in myObject)
                {
                    dynamic id = Convert.ToString(item.id);
                    dynamic name = Convert.ToString(item.name);

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
                HttpResponseMessage res = await _client.GetAsync("constants/salutation/all");
                if (res.IsSuccessStatusCode)
                {
                    string json = res.Content.ReadAsStringAsync().Result;
                    dynamic myObject = JArray.Parse(json);
                    List<SelectListItem> list = new List<SelectListItem>();

                    foreach (dynamic item in myObject)
                    {
                        dynamic id = Convert.ToString(item.id);
                        dynamic name = Convert.ToString(item.name);
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
            HttpResponseMessage res = await _client.GetAsync("constants/skill/all");

            if (res.IsSuccessStatusCode)
            {
                string json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                List<SelectListItem> list = new List<SelectListItem>();

                foreach (dynamic item in myObject)
                {
                    dynamic id = Convert.ToString(item.id);
                    dynamic name = Convert.ToString(item.name);

                    list.Add(new SelectListItem { Text = name, Value = id });
                }

                return list;
            }

            return null;
        }

        public async Task<List<SelectListItem>> GetFieldOfExpertise()
        {

            //var res = await _client.GetAsync("constants/field-of-expertise/all");
            HttpResponseMessage res = await _client.GetAsync("constants/field-of-expertise/all");

            if (res.IsSuccessStatusCode)
            {
                string json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                List<SelectListItem> list = new List<SelectListItem>();

                foreach (dynamic item in myObject)
                {
                    dynamic id = Convert.ToString(item.id);
                    dynamic name = Convert.ToString(item.name);

                    list.Add(new SelectListItem { Text = name, Value = id });
                }

                return list;
            }

            return null;
        }

        public async Task<List<SelectListItem>> GetVoluntaryInstitution()
        {

            HttpResponseMessage res = await _client.GetAsync("constants/voluntary-publicserviceinstitution/all");
            if (res.IsSuccessStatusCode)
            {
                string json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                List<SelectListItem> list = new List<SelectListItem>();

                foreach (dynamic item in myObject)
                {
                    string id = $"{Convert.ToString(item.id)}-{Convert.ToString(item.name)}";
                    dynamic name = Convert.ToString(item.name);

                    list.Add(new SelectListItem { Text = name, Value = id });
                }

                return list;
            }

            return null;
        }

        public async Task<bool> SavePersonData(PersonModel personModel)
        {

            string json = JsonConvert.SerializeObject(personModel);

            StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            StringContent content = new StringContent(json);
            HttpResponseMessage res = await _client.PostAsync("person", httpContent);

            return res.StatusCode == HttpStatusCode.OK ? true : false;
        }

        public async Task<AuthenticationResponse> GetToken(LoginModel model)
        {

            string json = JsonConvert.SerializeObject(model);

            StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            StringContent content = new StringContent(json);
            HttpResponseMessage res = await _client.PostAsync("auth/login", httpContent);

            if (res.IsSuccessStatusCode)
            {
                string responseJson = res.Content.ReadAsStringAsync().Result;

                AuthenticationResponse response = new AuthenticationResponse();

                response = JsonConvert.DeserializeObject<AuthenticationResponse>(responseJson);

                return response;
            }

            return null;
        }

        public bool SearchByCnic(string cnic, out PersonModel model)
        {
            model = null;

            HttpResponseMessage res = _client.GetAsync("person/search/cnic?cnic=" + cnic).Result;
            if (res.IsSuccessStatusCode)
            {
                string json = res.Content.ReadAsStringAsync().Result;

                FamilyRelationModel familyRelation = JsonConvert.DeserializeObject<FamilyRelationModel>(json);
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

            HttpResponseMessage res = await _client.GetAsync("person/search/findByCnicOrFirstNameOrLastName?cnic=" + cnic +
                                             "&firstName=" + firstName + "&lastName=" + lastName);
            if (res.IsSuccessStatusCode)
            {
                string json = res.Content.ReadAsStringAsync().Result;

                List<PersonModel> person = new List<PersonModel>();

                person = JsonConvert.DeserializeObject<List<PersonModel>>(json);

                return person;
            }

            return null;
        }

        public async Task<NominationDetailModel> GetInstitutionDetails(string id, string cycle)
        {
            NominationDetailModel nominationDetailModel = new NominationDetailModel
            {
                Positions = new List<PositionModel>(),
                Institution = new InstitutionModel()
            };

            List<SelectListItem> list = await GetMartialStatuses();
            List<SelectListItem> listAreaOfOrigin = await GetAreaOfOrigin();
            List<SelectListItem> salutationList = await GetSalutation();
            List<SelectListItem> jamatiTitleList = await GetJamatiTitles();
            List<SelectListItem> institutionList = await GetAllInstitutions();
            List<SelectListItem> nameOfDegreeList = await GetEducationalDegree();
            List<SelectListItem> religiousEducationList = await GetReligiousEducation();
            List<SelectListItem> regionalCouncilList = await GetRegionalCouncil();
            List<SelectListItem> listOfCountries = await GetAllCountries();
            List<SelectListItem> listOfLanguageProficiency = await GetLanguageProficiency();
            List<SelectListItem> voluntaryCommunityPositionList = await GetPositions();
            List<SelectListItem> highestLevelOfStudyList = await GetHighestLevelOfStudy();
            List<SelectListItem> akdnTrainingList = await GetAkdnTraining();
            List<SelectListItem> voluntaryCommunityInstitutionList = await GetPositionInstitution();
            List<SelectListItem> fieldOfInterestsList = await GetFieldOfInterests();
            List<SelectListItem> occupationTypeList = await GetOcupations();
            List<SelectListItem> typeOfBusinessList = await GetBussinessType();
            List<SelectListItem> natureOfBusinessList = await GetBussinessNature();
            List<SelectListItem> professionalMembershipsList = await GetProfessionalMemeberShipDetails();
            List<SelectListItem> languageList = await GetLanguages();
            List<SelectListItem> skillsList = await GetSkills();
            List<SelectListItem> relationList = await GetAllRelatives();
            List<SelectListItem> majorAreaOfStudy = await GetMajorAreaOfStudy();
            List<SelectListItem> fieldOfExpertiseList = await GetFieldOfExpertise();


            try
            {
                //HttpResponseMessage res = await _client.GetAsync("position/search/findByInstitutionId?institutionId=5");
                HttpResponseMessage res = await _client.GetAsync("appointment-position/search/findByCycleIdAndInstitutionId?cycleId=" + cycle + "&institutionId=" + id);

                if (res.IsSuccessStatusCode)
                {
                    string json = res.Content.ReadAsStringAsync().Result;

                    JArray arr = JArray.Parse(json);

                    List<PositionModel> listPositions = new List<PositionModel>();

                    foreach (JToken jToken1 in arr)
                    {
                        JObject positionArray = (JObject)jToken1;
                        List<NominationModel> listNominations = new List<NominationModel>();
                        PositionModel positionModel = new PositionModel();

                        JToken personAppointmentList = positionArray["personAppointmentList"];
                        JToken currentCycle = positionArray["cycle"];
                        JToken positionName = positionArray["position"];
                        JToken instituion = positionArray["institution"];

                        positionModel.Id = Convert.ToString(positionArray["appointmentPositionId"]);
                        positionModel.CurrentCycle = Convert.ToString(currentCycle["name"]);
                        positionModel.PositionName = Convert.ToString(positionName["name"]);
                        positionModel.PositionId = Convert.ToString(positionName["id"]);
                        positionModel.Required = Convert.ToInt32(positionArray["nominationsRequired"]);
                        positionModel.Rank = Convert.ToInt32(positionArray["rank"]);
                        positionModel.SeatId = Convert.ToString(positionArray["seatId"]);

                        //int index = 0;
                        foreach (JToken jToken in personAppointmentList)
                        {
                            JObject personsAppointed = (JObject)jToken;
                            NominationModel nominationModel = new NominationModel();

                            JToken incumbent = personsAppointed["person"];
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
                                JToken person = personsAppointed["person"];
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

                    listPositions.Sort((a, b) => (a.Rank.CompareTo(b.Rank)));
                    nominationDetailModel.Positions = listPositions;
                }
            }
            catch (Exception)
            {

            }

            return nominationDetailModel;
        }

        private static void SetDetails(List<SelectListItem> list, List<SelectListItem> listAreaOfOrigin, List<SelectListItem> salutationList, List<SelectListItem> jamatiTitleList,
            List<SelectListItem> nameOfDegreeList, List<SelectListItem> voluntaryCommunityInstitutionList, List<SelectListItem> occupationTypeList,
            List<SelectListItem> institutionList, PersonModel personModel)
        {
            try
            {
                if (personModel.MaritalStatus != null)
                {
                    SelectListItem item = list.Find(x => x.Value == personModel.MaritalStatus);
                    personModel.MaritalStatusForDisplay = item.Text;
                }
                if (personModel.AreaOfOrigin != null)
                {
                    SelectListItem item1 = listAreaOfOrigin.Find(x => x.Value == personModel.AreaOfOrigin);
                    personModel.AreaOfOriginForDisplay = item1.Text;
                }
                if (personModel.Salutation != null)
                {
                    SelectListItem salutation = salutationList.Find(x => x.Value == personModel.Salutation);
                    personModel.SalutationForDisplay = salutation.Text;
                }
                if (personModel.JamatiTitle != null)
                {
                    SelectListItem jamatiTitle = jamatiTitleList.Find(x => x.Value == personModel.JamatiTitle);
                    personModel.JamatiTitleForDisplay = jamatiTitle.Text;
                }
                if (personModel.Educations != null)
                {
                    foreach (EducationModel education in personModel.Educations)
                    {

                        SelectListItem edu = nameOfDegreeList.Find(x => x.Value.StartsWith($"{education.NameOfDegree}-"));
                        education.NameOfDegreeName = edu.Text;

                        SelectListItem institute = institutionList.Find(x => x.Value.StartsWith($"{education.Institution}-"));
                        education.InstitutionName = institute.Text;
                    }
                }
                if (personModel.VoluntaryCommunityServices != null)
                {
                    foreach (VoluntaryCommunityModel communityInstituion in personModel.VoluntaryCommunityServices)
                    {
                        SelectListItem institution = voluntaryCommunityInstitutionList.Find(x => x.Value == communityInstituion.Institution);
                        communityInstituion.InstitutionName = institution.Text;
                    }
                }
                if (personModel.OccupationType != null)
                {
                    SelectListItem ocupation = occupationTypeList.Find(x => x.Value == personModel.OccupationType);
                    personModel.OccupationTypeName = ocupation.Text;
                }
            }
            catch (Exception)
            {

            }
        }

        public async Task<PersonModel> SearchPersonByFormNumber(string formNumber, string personId, string id)
        {

            HttpResponseMessage res = await _client.GetAsync("person/search/findByFormNo?formNo=" + formNumber);
            if (res.IsSuccessStatusCode)
            {
                string json = res.Content.ReadAsStringAsync().Result;

                return null;
            }

            return null;
        }

        public async Task<PositionModel> Nominate(string personId, string appointmentPositionId, int priority, string institutionId, string positionId, string cycleId,
            string seatNo)
        {

            JObject jObject = new JObject
            {
                { "appointmentPositionId", appointmentPositionId },
                { "isAppointed", false },
                { "isRecommended", false },
                { "personId", personId },
                { "priority", priority+1},
                { "remarks", "" }
            };

            string json = JsonConvert.SerializeObject(jObject);
            StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage res = await _client.PostAsync("/person/appointment", httpContent);

            PositionModel positionModel = null;
            if (res.IsSuccessStatusCode)
            {
                HttpResponseMessage response = await _client.GetAsync("/appointment-position/search/findByCycleIdAndInstitutionIdAndPositionIdAndSeatNo?cycleId=" + cycleId +
                    "&institutionId=" + institutionId + "&positionId=" + positionId + "&seatNo=" + seatNo);

                if (response.IsSuccessStatusCode)
                {
                    string newJson = res.Content.ReadAsStringAsync().Result;
                    positionModel = JsonConvert.DeserializeObject<PositionModel>(newJson);
                }
              
            }

            return positionModel;
        }

        public async Task<List<SelectListItem>> GetMajorAreaOfStudy()
        {

            HttpResponseMessage res = await _client.GetAsync("constants/area-of-study/all");
            if (res.IsSuccessStatusCode)
            {
                string json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                List<SelectListItem> list = new List<SelectListItem>();

                foreach (dynamic item in myObject)
                {
                    string id = $"{Convert.ToString(item.id)}-{Convert.ToString(item.name)}";
                    dynamic name = Convert.ToString(item.name);

                    list.Add(new SelectListItem { Text = name, Value = id });
                }

                return list;
            }

            return null;
        }

        public async Task<List<PastAppointment>> GetAppointments(string personId, bool isMawlaAppointee)
        {

            HttpResponseMessage res = await _client.GetAsync(
                $"appointment-position/search/findAppointmentOfPersonIdAndIsMowlaAppointee?personId={personId}&isMowlaAppointee={isMawlaAppointee}");

            if (res.IsSuccessStatusCode)
            {
                string json = res.Content.ReadAsStringAsync().Result;

                List<PastAppointment> appointments = new List<PastAppointment>();

                appointments = JsonConvert.DeserializeObject<List<PastAppointment>>(json);

                return appointments;
            }

            return null;
        }

        public async Task<List<SelectListItem>> GetCycles()
        {

            HttpResponseMessage res = await _client.GetAsync("cycle/all");
            if (res.IsSuccessStatusCode)
            {
                string json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                List<SelectListItem> list = new List<SelectListItem>();

                foreach (dynamic item in myObject)
                {
                    dynamic id = Convert.ToString(item.id);
                    dynamic name = Convert.ToString(item.name);

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