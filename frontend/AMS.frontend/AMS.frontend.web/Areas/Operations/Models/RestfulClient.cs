using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AMS.frontend.web.Areas.Operations.Models.Nominations;
using AMS.frontend.web.Areas.Operations.Models.Persons;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AMS.frontend.web.Areas.Operations.Models
{
    public static class RestfulClient
    {   
        #region Private Fields

        private static readonly string BaseUrl = "http://is.bismagreens.com:8080/";
        private static HttpClient _client;

        #endregion Private Fields

        #region Public Methods

        public static async Task<bool> EditPersonData(PersonModel personModel)
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var json = JsonConvert.SerializeObject(personModel);

            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var content = new StringContent(json);
            var res = await _client.PutAsync("person/one/" + personModel.Id, httpContent);

            return res.IsSuccessStatusCode ? true : false;
        }

        public static async Task<List<SelectListItem>> GetAkdnTraining()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

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

                    list.Add(new SelectListItem {Text = name, Value = id});
                }

                return list;
            }

            return null;
        }

        public static async Task<List<SelectListItem>> GetAllCompanies()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

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

                    list.Add(new SelectListItem {Text = name, Value = id});
                }

                return list;
            }

            return null;
        }

        public static async Task<List<SelectListItem>> GetAllCountries()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

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

                    list.Add(new SelectListItem {Text = name, Value = id});
                }

                return list;
            }

            return null;
        }

        public static async Task<List<SelectListItem>> GetAllInstitutions()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

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

                    list.Add(new SelectListItem {Text = name, Value = id});
                }

                return list;
            }

            return null;
        }

        public static async Task<List<SelectListItem>> GetAllRelatives()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

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

                    list.Add(new SelectListItem {Text = name, Value = id});
                }

                return list;
            }

            return null;
        }

        public static async Task<List<SelectListItem>> GetAreaOfOrigin()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

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

                    list.Add(new SelectListItem {Text = name, Value = id});
                }

                return list;
            }

            return null;
        }

        public static async Task<List<SelectListItem>> GetBussinessNature()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

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

                    list.Add(new SelectListItem {Text = name, Value = id});
                }

                return list;
            }

            return null;
        }

        public static async Task<List<SelectListItem>> GetBussinessType()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

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

                    list.Add(new SelectListItem {Text = name, Value = id});
                }

                return list;
            }

            return null;
        }

        public static async Task<List<SelectListItem>> GetCities()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

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

                    list.Add(new SelectListItem {Text = name, Value = id});
                }

                return list;
            }

            return null;
        }

        public static async Task<List<SelectListItem>> GetEducationalDegree()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

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

                    list.Add(new SelectListItem {Text = name, Value = id});
                }

                return list;
            }

            return null;
        }

        public static async Task<List<SelectListItem>> GetFieldOfInterests()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

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

                    list.Add(new SelectListItem {Text = name, Value = id});
                }

                return list;
            }

            return null;
        }

        public static async Task<List<SelectListItem>> GetHighestLevelOfStudy()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var res = await _client.GetAsync("constants/highest-level-of-study/all");
            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                var list = new List<SelectListItem>();

                foreach (var item in myObject)
                {
                    var id = Convert.ToString(item.id);
                    var name = Convert.ToString(item.name);

                    list.Add(new SelectListItem {Text = name, Value = id});
                }

                return list;
            }

            return null;
        }

        public static async Task<List<PositionModel>> GetInstitutionTypes(string level, string subLevel)
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

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

                    var positionModel = new PositionModel
                    {
                        Id = id,
                        PositionName = name
                    };

                    list.Add(positionModel);
                }

                return list;
            }

            return null;
        }

        public static async Task<List<SelectListItem>> GetJamatiTitles()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

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

                    list.Add(new SelectListItem {Text = name, Value = id});
                }

                return list;
            }

            return null;
        }

        public static async Task<List<SelectListItem>> GetJamatkhana(string uid = "")
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var res = await _client.GetAsync(string.IsNullOrWhiteSpace(uid)
                ? "level/search/type?value=4"
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

                    list.Add(new SelectListItem {Text = name, Value = id});
                }

                return list;
            }

            return null;
        }

        public static async Task<List<SelectListItem>> GetLanguageProficiency()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

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

                    list.Add(new SelectListItem {Text = name, Value = id});
                }

                return list;
            }

            return null;
        }

        public static async Task<List<SelectListItem>> GetLanguages()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

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

                    list.Add(new SelectListItem {Text = name, Value = id});
                }

                return list;
            }

            return null;
        }

        public static async Task<List<SelectListItem>> GetLocalCouncil(string uid = "")
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

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

                    list.Add(new SelectListItem {Text = name, Value = id});
                }

                return list;
            }

            return null;
        }

        public static async Task<List<SelectListItem>> GetLocalInstitutions(string uid = "")
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

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

                    list.Add(new SelectListItem {Text = name, Value = id});
                }

                return list;
            }

            return null;
        }

        public static async Task<List<SelectListItem>> GetMartialStatuses()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

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

                    list.Add(new SelectListItem {Text = name, Value = id});
                }

                return list;
            }

            return null;
        }

        public static async Task<List<SelectListItem>> GetOcupations()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

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

                    list.Add(new SelectListItem {Text = name, Value = id});
                }

                return list;
            }

            return null;
        }

        public static async Task<List<PersonModel>> GetPersonDetails()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var res = await _client.GetAsync("person/all");
            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;

                var person = new List<PersonModel>();

                person = JsonConvert.DeserializeObject<List<PersonModel>>(json);

                return person;
            }

            return null;
        }

        public static async Task<PersonModel> GetPersonDetailsById(string id)
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var res = await _client.GetAsync("person/one/" + id);
            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;

                var person = new PersonModel();

                person = JsonConvert.DeserializeObject<PersonModel>(json);
           
                return person;
            }

            return null;
        }

        public static async Task<Tuple<List<PersonModel>, int>> GetPersonDetailsThroughPagging(string firstName,
            string lastName, string cnic, string formNo, int pageNumber, int pageSize)
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //var Res = await client.GetAsync("/person/search/findByCnicOrFirstNameOrLastName?firstName&cnic&lastName&page=1&size=1");
            var res = await _client.GetAsync("/person/search/findByCnicAndFirstNameAndLastNameAndFormNo?cnic=" + cnic +
                                             "&firstName=" + firstName + "&lastName=" + lastName + "&formNo=" + formNo +
                                             "&page=" + pageNumber + "&size=" + pageSize);
            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;

                var person = new List<PersonModel>();

                var jsonObject = JObject.Parse(json);

                person = JsonConvert.DeserializeObject<List<PersonModel>>(jsonObject["content"].ToString());

                var totalElements = Convert.ToInt32(jsonObject["totalElements"]);

                return new Tuple<List<PersonModel>, int>(person, totalElements);
            }

            return null;
        }

        public static async Task<List<SelectListItem>> GetPositionInstitution()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

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

                    list.Add(new SelectListItem {Text = name, Value = id});
                }

                return list;
            }

            return null;
        }

        public static async Task<List<SelectListItem>> GetPositions()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

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

                    list.Add(new SelectListItem {Text = name, Value = id});
                }

                return list;
            }

            return null;
        }

        public static async Task<List<SelectListItem>> GetProfessionalMemeberShipDetails()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

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

                    list.Add(new SelectListItem {Text = name, Value = id});
                }

                return list;
            }

            return null;
        }

        public static async Task<List<SelectListItem>> GetRegionalCouncil()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var res = await _client.GetAsync("level/search/parent?value=1");
            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                var list = new List<SelectListItem>();

                foreach (var item in myObject)
                {
                    var id = Convert.ToString(item.id);
                    var name = Convert.ToString(item.name);

                    list.Add(new SelectListItem {Text = name, Value = id});
                }

                return list;
            }

            return null;
        }

        public static async Task<List<SelectListItem>> GetRegionalInstitutions()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var res = await _client.GetAsync("level/search/parent?value=1");
            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;
                dynamic myObject = JArray.Parse(json);
                var list = new List<SelectListItem>();

                foreach (var item in myObject)
                {
                    var id = Convert.ToString(item.id);
                    var name = Convert.ToString(item.name);

                    list.Add(new SelectListItem {Text = name, Value = id});
                }

                return list;
            }

            return null;
        }

        public static async Task<List<SelectListItem>> GetReligiousEducation()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

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

                    list.Add(new SelectListItem {Text = name, Value = id});
                }

                return list;
            }

            return null;
        }

        public static async Task<List<SelectListItem>> GetSalutation()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

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
                        list.Add(new SelectListItem {Text = name, Value = id});
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

        /*public static async Task<List<SelectListItem>> GetInstitutions(string level = "", string subLevel = "" , string type = "")
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

        public static async Task<List<SelectListItem>> GetSkills()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

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

                    list.Add(new SelectListItem {Text = name, Value = id});
                }

                return list;
            }

            return null;
        }

        public static async Task<List<SelectListItem>> GetVoluntaryInstitution()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

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

                    list.Add(new SelectListItem {Text = name, Value = id});
                }

                return list;
            }

            return null;
        }

        public static async Task<bool> SavePersonData(PersonModel personModel)
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var json = JsonConvert.SerializeObject(personModel);

            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var content = new StringContent(json);
            var res = await _client.PostAsync("person", httpContent);

            return res.StatusCode == HttpStatusCode.OK ? true : false;
        }

        public static bool SearchByCnic(string cnic, out PersonModel model)
        {
            model = null;
            _client = new HttpClient {BaseAddress = new Uri(BaseUrl)};
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

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

        public static async Task<List<PersonModel>> SearchPerson(string cnic, string firstName, string lastName)
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

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

        public static async Task<NominationDetailModel> GetInstitutionDetails(string id)
        {
            NominationDetailModel nominationDetailModel = new NominationDetailModel();
            nominationDetailModel.Positions = new List<PositionModel>();
            nominationDetailModel.Institution = new InstitutionModel();

            _client = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        
            var res = await _client.GetAsync("position/search/findByInstitutionId?institutionId=5");

            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;

                //JArray arr = JArray.Parse(json);
                JObject arr = JObject.Parse(json);
                var positionDeatilsDto = arr["positionDetailsDto"];

                List<PositionModel> listPositionModel = new List<PositionModel>();
               
                foreach (JObject positionArray in positionDeatilsDto)
                {
                    List<NominationModel> listNominationModel = new List<NominationModel>();
                    PositionModel positionModel = new PositionModel();

                    var person = positionArray["incumbent"]["person"];
                    var required = positionArray["poi"]["desired"];
                    var personNominated = positionArray["personsNominated"];
                    var positionId = positionArray["poi"]["positionId"];
                    var currentCycle = arr["currentCycle"]["name"];
                    
                    positionModel.Incubment = person.ToObject<PersonModel>();

                    //-----------Hard coded values need to call APi for this---------
                    positionModel.Incubment.MaritalStatusForDisplay = "Single";
                    positionModel.Incubment.AreaOfOriginForDisplay = "Karachi";
                    //---------------------------------------------------------------

                    positionModel.Id = positionId.ToString();
                    positionModel.CurrentCycle = currentCycle.ToString();
                    
                    foreach (JObject Jobj in personNominated)
                    {
                        NominationModel nominationModel = new NominationModel();

                        var nominatedPerson = Jobj["person"];
                        var priority = Jobj["personCPI"]["priority"];
                        nominationModel.Person = nominatedPerson.ToObject<PersonModel>();

                        //--------------adding maritalStatusForDisplay-----------------
                        nominationModel.Person.MaritalStatusForDisplay = "Single";
                        nominationModel.Person.AreaOfOriginForDisplay = "Karachi";
                        //-------------------------------------------------------------

                        nominationModel.Priority = Convert.ToInt32(priority);
                        listNominationModel.Add(nominationModel);
                    }

                    listNominationModel.Sort((a, b) => (a.Priority.CompareTo(b.Priority)));

                    positionModel.Nominations = listNominationModel;

                    listPositionModel.Add(positionModel);
                }

                nominationDetailModel.Positions = listPositionModel;

                var institutionId = arr["institution"]["id"];
                var institutionName = arr["institution"]["name"];
                nominationDetailModel.Institution.Id = institutionId.ToString();
                nominationDetailModel.Institution.Name = institutionName.ToString();
                
                return nominationDetailModel;
            }
            return nominationDetailModel;
        }

        public static async Task<PersonModel> searchPersonByFormNumber(string formNumber, string personId, string id)
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var res = await _client.GetAsync("person/search/findByFormNo?formNo=" + formNumber);
            if (res.IsSuccessStatusCode)
            {
                var json = res.Content.ReadAsStringAsync().Result;
                
                return null;
            }

            return null;
        }

        public static async Task<PersonModel> nominate(string personId, string positionId)
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            JObject jObject = new JObject();
            jObject.Add("appointed", true);
            jObject.Add("cpiId", positionId);
            jObject.Add("id", 0);
            jObject.Add("personId", personId);
            jObject.Add("priority", 0);
            jObject.Add("recommended", true);

            var json = JsonConvert.SerializeObject(jObject);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var res = await _client.PostAsync("/person/cpi",httpContent);

            if (res.IsSuccessStatusCode)
            {
                var response = res.Content.ReadAsStringAsync().Result;
                
                return null;
            }

            return null;
        }

        #endregion Public Methods
    }
}