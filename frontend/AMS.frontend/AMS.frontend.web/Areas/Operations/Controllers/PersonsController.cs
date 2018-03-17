using System.Collections.Generic;
using AMS.frontend.web.Helpers.Constants;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using AMS.frontend.web.Areas.Operations.Models.Persons;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http;
using System;
using System.Net.Http.Headers;
using AMS.frontend.web.Areas.Operations.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AMS.frontend.web.Areas.Operations.Controllers
{
    [Area(AreaNames.Operations)]
    public class PersonsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly Configuration _configuration;

        string Baseurl = "http://13.93.85.18:8080/constants/";
        
        public PersonsController(IMapper mapper, IOptions<Configuration> configuration)
        {
            this._mapper = mapper;
            this._configuration = configuration.Value;
        }

        public async Task<IActionResult> Index()
        {
            return View(new List<PersonModel>());
        }

        public async Task<IActionResult> Add()
        {
            //RestfulClient restfulClient = new RestfulClient();

            using (var client = new HttpClient()) {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("salutatuions");

                if (Res.IsSuccessStatusCode)
                {
                    var result = await Res.Content.ReadAsStringAsync();
                    dynamic myObject = JArray.Parse(result);
                    var list = new List<SelectListItem>();
                    
                    foreach (var item in myObject) {
                        var id = Convert.ToString(item.id);
                        var salutation = Convert.ToString(item.salutation);
                        list.Add(new SelectListItem { Text = salutation, Value = id });
                    }


                    ViewBag.SalutationList = list;
                }
            
            }

            //var getSalutation = restfulClient.getSalutation();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Verify(string cnic)
        {
            return Json(true);
        }

        public async Task<JsonResult> GetLocalCouncil(string uid)
        {
            var list = new List<SelectListItem> {new SelectListItem {Text = "Karimabad", Value = "Karimabad"}};

            return new JsonResult(list);
        }

        public async Task<JsonResult> GetJamatkhana(string uid)
        {
            var list = new List<SelectListItem> { new SelectListItem { Text = "Karimabad", Value = "Karimabad" } };

            return new JsonResult(list);
        }
    }
}