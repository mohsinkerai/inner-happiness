using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AMS.frontend.web.Areas.Operations.Models
{
    public static class RestfulClient
    {
        private static HttpClient client;
        private static string BASE_URL = "http://13.93.85.18:8080/";

        public static async Task<List<SelectListItem>> getSalutation()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(BASE_URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                HttpResponseMessage Res = await client.GetAsync("constants/salutatuions");
                if (Res.IsSuccessStatusCode)
                {
                    var json = Res.Content.ReadAsStringAsync().Result.ToString();
                    dynamic myObject = JArray.Parse(json);
                    var list = new List<SelectListItem>();

                    foreach (var item in myObject)
                    {
                        var id = Convert.ToString(item.id);
                        var salutation = Convert.ToString(item.salutation);
                        list.Add(new SelectListItem { Text = salutation, Value = id });
                    }

                    return list;
                }
                else
                {
                    return null;
                }
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

            HttpResponseMessage Res = await client.GetAsync("constants/jamatiTitles");
            if (Res.IsSuccessStatusCode)
            {
                var json = Res.Content.ReadAsStringAsync().Result.ToString();
                dynamic myObject = JArray.Parse(json);
                var list = new List<SelectListItem>();

                foreach (var item in myObject)
                {
                    var id = Convert.ToString(item.id);
                    var title = Convert.ToString(item.title);
                    var gender = Convert.ToString(item.gender);

                    list.Add(new SelectListItem { Text = title, Value = id });
                }

                return list;
            }
            else {
                return null;
            }
        }

        public static async Task<List<SelectListItem>> getMartialStatuses() {

            client = new HttpClient();
            client.BaseAddress = new Uri(BASE_URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage Res = await client.GetAsync("constants/maritalStatuses");
            if (Res.IsSuccessStatusCode)
            {
                var json = Res.Content.ReadAsStringAsync().Result.ToString();
                dynamic myObject = JArray.Parse(json);
                var list = new List<SelectListItem>();

                foreach (var item in myObject)
                {
                    var id = Convert.ToString(item.id);
                    var status = Convert.ToString(item.status);

                    list.Add(new SelectListItem { Text = status, Value = id });
                }

                return list;
            }
            else
            {
                return null;
            }
        }

        public static async Task<List<SelectListItem>> getCities()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(BASE_URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage Res = await client.GetAsync("constants/cities");
            if (Res.IsSuccessStatusCode)
            {
                var json = Res.Content.ReadAsStringAsync().Result.ToString();
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
            else
            {
                return null;
            }
        }

        public static async Task<List<SelectListItem>> getAreaOfOrigin()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(BASE_URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage Res = await client.GetAsync("constants/areaOfOrigin");
            if (Res.IsSuccessStatusCode)
            {
                var json = Res.Content.ReadAsStringAsync().Result.ToString();
                dynamic myObject = JArray.Parse(json);
                var list = new List<SelectListItem>();

                foreach (var item in myObject)
                {
                    var id = Convert.ToString(item.id);
                    var area = Convert.ToString(item.area);

                    list.Add(new SelectListItem { Text = area, Value = id });
                }

                return list;
            }
            else
            {
                return null;
            }
        }

        public static async Task<List<SelectListItem>> getAllInstitutions()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(BASE_URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage Res = await client.GetAsync("constants/institutions");
            if (Res.IsSuccessStatusCode)
            {
                var json = Res.Content.ReadAsStringAsync().Result.ToString();
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
            else
            {
                return null;
            }
        }

        public static async Task<List<SelectListItem>> getAllCountries()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(BASE_URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage Res = await client.GetAsync("constants/countries");
            if (Res.IsSuccessStatusCode)
            {
                var json = Res.Content.ReadAsStringAsync().Result.ToString();
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
            else
            {
                return null;
            }
        }

        public static async Task<List<SelectListItem>> getEducationalDegree()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(BASE_URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage Res = await client.GetAsync("educational-degree/all");
            if (Res.IsSuccessStatusCode)
            {
                var json = Res.Content.ReadAsStringAsync().Result.ToString();
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
            else
            {
                return null;
            }
        }

        public static async Task<List<SelectListItem>> getReligiousEducation()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(BASE_URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage Res = await client.GetAsync("constants/religiousQualifications");
            if (Res.IsSuccessStatusCode)
            {
                var json = Res.Content.ReadAsStringAsync().Result.ToString();
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
            else
            {
                return null;
            }
        }
    }
}
