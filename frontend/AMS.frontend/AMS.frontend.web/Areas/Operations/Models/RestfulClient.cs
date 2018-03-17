using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AMS.frontend.web.Areas.Operations.Models
{
    public class RestfulClient
    {
        private HttpClient client;
        private static string BASE_URL = "http://13.93.85.18:8080/constants/";

        public async Task<string> getSalutation()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(BASE_URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try {
                HttpResponseMessage Res = await client.GetAsync("salutatuions");
                if (Res.IsSuccessStatusCode)
                {
                    return Res.Content.ReadAsStringAsync().Result.ToString();
                }
                else
                {
                    return null;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }

            return null;
        }
    }
}
