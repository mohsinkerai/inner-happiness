using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AMS.frontend.web.Areas.Operations.Models.Persons;
using AMS.frontend.web.Helpers.Constants;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http;
using System;
using System.Net.Http.Headers;
using AMS.frontend.web.Areas.Operations.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Options;

namespace AMS.frontend.web.Areas.Operations.Controllers
{
    [Area(AreaNames.Operations)]
    public class PersonsController : Controller
    {
        private readonly Configuration _configuration;
        private readonly IMapper _mapper;

        string Baseurl = "http://13.93.85.18:8080/constants/";
        
        public PersonsController(IMapper mapper, IOptions<Configuration> configuration)
        {
            _mapper = mapper;
            _configuration = configuration.Value;
        }

        public async Task<IActionResult> Index()
        {
            return View(new List<PersonModel>());
        }

        public async Task<IActionResult> Add()
        {
            //RestfulClient restfulClient = new RestfulClient();

            /*using (var client = new HttpClient()) {
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
                }*/
            
            //}

            ViewBag.SalutationList = await RestfulClient.getSalutation();

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
            var list = new List<SelectListItem> {new SelectListItem {Text = "Karimabad", Value = "Karimabad"}};

            return new JsonResult(list);
        }

        [HttpPost]
        public async Task<IActionResult> EducationListAdd(string id, string institution, string countryOfStudy,
            string fromYear,
            string toYear, string nameOfDegree, string majorAreaOfStudy)
        {
            //User user = GetLoggedInUser();
            //if (user != null)
            //{
            //    IEnumerable<LanguageProficiency> languageProficiencies = await AddLanguageProficiencyInSession(languageId, reading, writing, speaking).ConfigureAwait(false);

            //    return PartialView(PartialViewNames.LanguageProficiencyTable, languageProficiencies);
            //}

            return PartialView("_EducationTablePartial",
                new List<EducationModel>
                {
                    new EducationModel
                    {
                        EducationId = id,
                        CountryOfStudy = countryOfStudy,
                        FromYear = Convert.ToInt32(fromYear),
                        Institution = institution,
                        MajorAreaOfStudy = majorAreaOfStudy,
                        NameOfDegree = nameOfDegree,
                        ToYear = Convert.ToInt32(toYear)
                    }
                });
        }

        [HttpPost]
        public async Task<IActionResult> EducationListDelete(string id)
        {
            //User user = GetLoggedInUser();
            //if (user != null)
            //{
            //    IEnumerable<LanguageProficiency> languageProficiencies = await AddLanguageProficiencyInSession(languageId, reading, writing, speaking).ConfigureAwait(false);

            //    return PartialView(PartialViewNames.LanguageProficiencyTable, languageProficiencies);
            //}

            return PartialView("_EducationTablePartial", new List<EducationModel>());
        }

        [HttpPost]
        public async Task<IActionResult> AkdnTrainingListAdd(string id, string training, string countryOfTarining,
            string month, string year)
        {
            //User user = GetLoggedInUser();
            //if (user != null)
            //{
            //    IEnumerable<LanguageProficiency> languageProficiencies = await AddLanguageProficiencyInSession(languageId, reading, writing, speaking).ConfigureAwait(false);

            //    return PartialView(PartialViewNames.LanguageProficiencyTable, languageProficiencies);
            //}

            return PartialView("_AkdnTrainingTablePartial",
                new List<AkdnTrainingModel>
                {
                    new AkdnTrainingModel
                    {
                        TrainingId = id,
                        CountryOfTraining = countryOfTarining,
                        Month = month,
                        Training = training,
                        Year = Convert.ToInt32(year)
                    }
                });
        }

        [HttpPost]
        public async Task<IActionResult> AkdnTrainingListDelete(string id)
        {
            //User user = GetLoggedInUser();
            //if (user != null)
            //{
            //    IEnumerable<LanguageProficiency> languageProficiencies = await AddLanguageProficiencyInSession(languageId, reading, writing, speaking).ConfigureAwait(false);

            //    return PartialView(PartialViewNames.LanguageProficiencyTable, languageProficiencies);
            //}

            return PartialView("_AkdnTrainingTablePartial", new List<AkdnTrainingModel>());
        }

        [HttpPost]
        public async Task<IActionResult> ProfessionalTrainingListAdd(string id, string training, string institution,
            string countryOfTarining, string month, string year)
        {
            //User user = GetLoggedInUser();
            //if (user != null)
            //{
            //    IEnumerable<LanguageProficiency> languageProficiencies = await AddLanguageProficiencyInSession(languageId, reading, writing, speaking).ConfigureAwait(false);

            //    return PartialView(PartialViewNames.LanguageProficiencyTable, languageProficiencies);
            //}

            return PartialView("_ProfessionalTrainingTablePartial",
                new List<ProfessionalTrainingModel>
                {
                    new ProfessionalTrainingModel
                    {
                        TrainingId = id,
                        CountryOfTraining = countryOfTarining,
                        Institution = institution,
                        Month = month,
                        Training = training,
                        Year = Convert.ToInt32(year)
                    }
                });
        }

        [HttpPost]
        public async Task<IActionResult> ProfessionalTrainingListDelete(string id)
        {
            //User user = GetLoggedInUser();
            //if (user != null)
            //{
            //    IEnumerable<LanguageProficiency> languageProficiencies = await AddLanguageProficiencyInSession(languageId, reading, writing, speaking).ConfigureAwait(false);

            //    return PartialView(PartialViewNames.LanguageProficiencyTable, languageProficiencies);
            //}

            return PartialView("_ProfessionalTrainingTablePartial", new List<ProfessionalTrainingModel>());
        }
        [HttpPost]
        public async Task<IActionResult> LanguageListAdd(string id, string language, string read,
            string write, string speak)
        {
            //User user = GetLoggedInUser();
            //if (user != null)
            //{
            //    IEnumerable<LanguageProficiency> languageProficiencies = await AddLanguageProficiencyInSession(languageId, reading, writing, speaking).ConfigureAwait(false);

            //    return PartialView(PartialViewNames.LanguageProficiencyTable, languageProficiencies);
            //}

            return PartialView("_LanguageTablePartial",
                new List<LanguageProficiencyModel>
                {
                    new LanguageProficiencyModel
                    {
                        LanguageProficiencyId = id,
                        Language = language,
                        Read = read,
                        Speak = speak,
                        Write = write
                    }
                });
        }

        [HttpPost]
        public async Task<IActionResult> LanguageListDelete(string id)
        {
            //User user = GetLoggedInUser();
            //if (user != null)
            //{
            //    IEnumerable<LanguageProficiency> languageProficiencies = await AddLanguageProficiencyInSession(languageId, reading, writing, speaking).ConfigureAwait(false);

            //    return PartialView(PartialViewNames.LanguageProficiencyTable, languageProficiencies);
            //}

            return PartialView("_LanguageTablePartial", new List<LanguageProficiencyModel>());
        }
    }
}