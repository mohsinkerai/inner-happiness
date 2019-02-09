using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AMS.frontend.web.Areas.Operations.Models;
using AMS.frontend.web.Areas.Operations.Models.Persons;
using AMS.frontend.web.Extensions;
using AMS.frontend.web.Helpers.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AMS.frontend.web.Areas.Operations.Controllers
{
    [Area(AreaNames.Operations)]
    public class SearchPersonController : BaseController
    {
        public SearchPersonController(IOptions<Configuration> configuration, ILogger<PersonsController> logger)
        {
            _configuration = configuration.Value;
            _logger = logger;
        }

        private readonly ILogger<PersonsController> _logger;
        private readonly Configuration _configuration;

        public async Task<IActionResult> Index()
        {
            await InitializeSearchCriteria();
            
            return View();
        }

        public async Task InitializeSearchCriteria()
        {
            ViewBag.RegionalCouncil = await new RestfulClient(_logger,
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetRegionalCouncil();
            ViewBag.LocalCouncil = await new RestfulClient(_logger,
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetLocalInstitutions();
            ViewBag.JamatKhana = await new RestfulClient(_logger,
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetAllJamatKhana();
            ViewBag.JamatiTitle = await new RestfulClient(_logger,
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetJamatiTitles();
            ViewBag.City = await new RestfulClient(_logger,
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetCities();
            ViewBag.AreaOfOrigin = await new RestfulClient(_logger,
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetAreaOfOrigin();
            ViewBag.HighestLevelOfStudy = await new RestfulClient(_logger,
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetHighestLevelOfStudy();
            ViewBag.AkdnTrainingList = await new RestfulClient(_logger,
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetAkdnTraining();
            ViewBag.FiledOfInterest = await new RestfulClient(_logger,
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetFieldOfInterests();
            ViewBag.OccupationType = await new RestfulClient(_logger,
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetOcupations();
            ViewBag.EducationalInstitution = await new RestfulClient(_logger,
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetAllInstitutions();
            ViewBag.NameOfDegree = await new RestfulClient(_logger,
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetEducationalDegree();
            ViewBag.MajorAreaOfStudy = await new RestfulClient(_logger,
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetMajorAreaOfStudy();
            ViewBag.FieldOfExpertise = await new RestfulClient(_logger,
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetFieldOfExpertise();
            ViewBag.ReligiousEducation = await new RestfulClient(_logger,
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetReligiousEducation();
            ViewBag.AkdnTraining = await new RestfulClient(_logger,
                    HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetAkdnTraining();
            ViewBag.Skills = await new RestfulClient(_logger,
                    HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetSkills();
            ViewBag.ProfessionalMembership = await new RestfulClient(_logger,
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetProfessionalMemeberShipDetails();
            ViewBag.TypeOfBuisness = await new RestfulClient(_logger,
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetBussinessType();
            ViewBag.NatureOfBuisness = await new RestfulClient(_logger,
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetBussinessNature();
        }

        [HttpPost]
        public async Task<IActionResult> Index(SearchCriteria searchCriteria)
        {

            UploadImageContext context = HttpContext.RequestServices.GetService(typeof(UploadImageContext)) as UploadImageContext;
            List<PersonModel> personList = context.GetPersons();
            searchCriteria.persons = personList;

            return View(searchCriteria);
        }
    }
}