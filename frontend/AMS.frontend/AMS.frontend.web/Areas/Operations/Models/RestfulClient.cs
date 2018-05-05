using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AMS.frontend.web.Areas.Operations.Models.Persons;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AMS.frontend.web.Areas.Operations.Models
{
    public static class RestfulClient
    {
        private static HttpClient client;
        private static readonly string BASE_URL = "http://13.93.85.18:8080/";
       
        public static async Task<List<SelectListItem>> getSalutation()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(BASE_URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                var Res = await client.GetAsync("constants/salutation/all");
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
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public static async Task<List<SelectListItem>> getJamatiTitles()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(BASE_URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var Res = await client.GetAsync("constants/jamati-title/all");
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
        }

        public static async Task<List<SelectListItem>> getMartialStatuses()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(BASE_URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var Res = await client.GetAsync("constants/marital-status/all");
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
        }

        public static async Task<List<SelectListItem>> getCities()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(BASE_URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var Res = await client.GetAsync("constants/city/all");
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
        }

        public static async Task<List<SelectListItem>> getAreaOfOrigin()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(BASE_URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var Res = await client.GetAsync("constants/area-of-origin/all");
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
        }

        public static async Task<List<SelectListItem>> getAllInstitutions()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(BASE_URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var Res = await client.GetAsync("constants/educational-publicserviceinstitution/all");
            if (Res.IsSuccessStatusCode)
            {
                var json = Res.Content.ReadAsStringAsync().Result;
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

        public static async Task<List<SelectListItem>> getAllCountries()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(BASE_URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var Res = await client.GetAsync("constants/country/all");
            if (Res.IsSuccessStatusCode)
            {
                var json = Res.Content.ReadAsStringAsync().Result;
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

        public static async Task<List<SelectListItem>> getEducationalDegree()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(BASE_URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var Res = await client.GetAsync("constants/educational-degree/all");
            if (Res.IsSuccessStatusCode)
            {
                var json = Res.Content.ReadAsStringAsync().Result;
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

        public static async Task<List<SelectListItem>> getReligiousEducation()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(BASE_URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var Res = await client.GetAsync("constants/religious-qualification/all");
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
        }

        public static async Task<List<SelectListItem>> getPositions()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(BASE_URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var Res = await client.GetAsync("position/all");
            if (Res.IsSuccessStatusCode)
            {
                var json = Res.Content.ReadAsStringAsync().Result;
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

        public static async Task<List<SelectListItem>> getRegionalCouncil()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(BASE_URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var Res = await client.GetAsync("level/search/parent?value=1");
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
        }

        public static async Task<List<SelectListItem>> getLocalCouncil(string uid)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(BASE_URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var Res = await client.GetAsync("level/search/parent?value=" + uid);
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
        }

        public static async Task<List<SelectListItem>> getJamatkhana(string uid)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(BASE_URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var Res = await client.GetAsync("level/search/parent?value=" + uid);
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
        }

        public static async Task<List<SelectListItem>> getHighestLevelOfStudy()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(BASE_URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var Res = await client.GetAsync("constants/highest-level-of-study/all");
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
        }

        public static async Task<List<SelectListItem>> getAkdnTraining()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(BASE_URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var Res = await client.GetAsync("constants/akdn-training/all");
            if (Res.IsSuccessStatusCode)
            {
                var json = Res.Content.ReadAsStringAsync().Result;
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

        public static async Task<List<SelectListItem>> getVoluntaryInstitution()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(BASE_URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var Res = await client.GetAsync("constants/voluntary-publicserviceinstitution/all");
            if (Res.IsSuccessStatusCode)
            {
                var json = Res.Content.ReadAsStringAsync().Result;
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

        public static async Task<List<SelectListItem>> getFieldOfInterests()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(BASE_URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var Res = await client.GetAsync("constants/field-of-interest/all");
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
        }

        public static async Task<List<SelectListItem>> getOcupations()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(BASE_URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var Res = await client.GetAsync("constants/occupation/all");
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
        }

        public static async Task<List<SelectListItem>> getBussinessType()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(BASE_URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var Res = await client.GetAsync("constants/business-type/all");
            if (Res.IsSuccessStatusCode)
            {
                var json = Res.Content.ReadAsStringAsync().Result;
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

        public static async Task<List<SelectListItem>> getBussinessNature()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(BASE_URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var Res = await client.GetAsync("constants/business-nature/all");
            if (Res.IsSuccessStatusCode)
            {
                var json = Res.Content.ReadAsStringAsync().Result;
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

        public static async Task<List<SelectListItem>> getProfessionalMemeberShipDetails()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(BASE_URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var Res = await client.GetAsync("constants/professional-membership/all");
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
        }

        public static async Task<List<SelectListItem>> getLanguages()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(BASE_URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var Res = await client.GetAsync("constants/language/all");
            if (Res.IsSuccessStatusCode)
            {
                var json = Res.Content.ReadAsStringAsync().Result;
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

        public static async Task<List<SelectListItem>> getSkills()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(BASE_URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var Res = await client.GetAsync("constants/field-of-expertise/all");
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
        }
        
        public static async Task<List<PersonModel>> getPersonDetails()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(BASE_URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var Res = await client.GetAsync("person/all");
            if (Res.IsSuccessStatusCode)
            {
                var json = Res.Content.ReadAsStringAsync().Result;

                List<PersonModel> person = new List<PersonModel>();

                person = JsonConvert.DeserializeObject<List<PersonModel>>(json);
                
                return person;
            }
            return null;
        }
        
        public static async Task<PersonModel> getPersonDetailsById(string id)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(BASE_URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var Res = await client.GetAsync("person/one/"+id);
            if (Res.IsSuccessStatusCode)
            {
                var json = Res.Content.ReadAsStringAsync().Result;

                PersonModel person = new PersonModel();

                person = JsonConvert.DeserializeObject<PersonModel>(json);

                return person;
            }
            return null;
        }

        public static async Task<List<SelectListItem>> getLanguageProficiency()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(BASE_URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var Res = await client.GetAsync("/constants/language-proficiency/all");
            if (Res.IsSuccessStatusCode)
            {
                var json = Res.Content.ReadAsStringAsync().Result;  
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

        public static async Task<List<PersonModel>> searchPerson(string cnic, string firstName, string lastName)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(BASE_URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var Res = await client.GetAsync("person/search/findByCnicOrFirstNameOrLastName?cnic="+cnic+"&firstName="+firstName+"&lastName="+lastName);
            if (Res.IsSuccessStatusCode)
            {
                var json = Res.Content.ReadAsStringAsync().Result;

                List<PersonModel> person = new List<PersonModel>();

                person = JsonConvert.DeserializeObject<List<PersonModel>>(json);

                return person;
            }
            return null;
        }

        public static async Task savePersonData(PersonModel personModel) {
            client = new HttpClient();
            client.BaseAddress = new Uri(BASE_URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string json = JsonConvert.SerializeObject(personModel);

            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var content = new StringContent(json.ToString());
            var Res = await client.PostAsync("person", httpContent);

            if (Res.StatusCode == HttpStatusCode.OK) {

            }
            
        }
        

    }
}
