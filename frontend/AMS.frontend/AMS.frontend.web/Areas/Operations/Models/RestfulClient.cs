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
        private static string BASE_URL = "http://13.93.85.18:8080/constants/";

        public static async Task<List<SelectListItem>> getSalutation()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(BASE_URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                HttpResponseMessage Res = await client.GetAsync("salutatuions");
                if (Res.IsSuccessStatusCode)
                {
                    var json = Res.Content.ReadAsStringAsync().Result.ToString();
                    //var result = await Res.Content.ReadAsStringAsync();
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
            }

            return null;
        }
    }
}
