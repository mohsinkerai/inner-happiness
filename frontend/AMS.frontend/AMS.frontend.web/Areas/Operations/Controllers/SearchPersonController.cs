using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AMS.frontend.web.Areas.Operations.Models;
using AMS.frontend.web.Areas.Operations.Models.Persons;
using AMS.frontend.web.Extensions;
using AMS.frontend.web.Helpers.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
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
            var query = MakeQuery(searchCriteria);
            List<PersonModel> personList = context.GetPersons(query);
            searchCriteria.persons = personList;
            searchCriteria.Query = query;

            return View(searchCriteria);
        }

        private string MakeQuery(SearchCriteria searchCriteria)
        {
            string Query(SearchCriteria searchCriteria1, bool b, string s)
            {
                if (b)
                {
                    s += $" ";
                }
                else
                {
                    s += $" {searchCriteria1.Condition} ";
                }

                b = false;
                return s;
            }

            var query =
                "SELECT DISTINCT(p.id), p.full_name FROM person AS p LEFT OUTER JOIN person_skill AS ps ON p.id = ps.person_id LEFT OUTER JOIN person_professional_membership AS ppm ON p.id = ppm.person_id LEFT OUTER JOIN person_field_of_expertise AS pfoe ON p.id = pfoe.person_id WHERE";
            var isFirstOne = true;

            if (searchCriteria.RegionalCouncil != null && searchCriteria.RegionalCouncil.Count > 0)
            {
                query = Query(searchCriteria, isFirstOne, query);
                query += $"p.regional_council IN ({string.Join(",", searchCriteria.RegionalCouncil)})";
            }

            if (searchCriteria.LocalCouncil != null && searchCriteria.LocalCouncil.Count > 0)
            {
                query = Query(searchCriteria, isFirstOne, query);
                query += $"p.local_council IN ({string.Join(",", searchCriteria.LocalCouncil)})";
            }

            if (searchCriteria.JamatKhana != null && searchCriteria.JamatKhana.Count > 0)
            {
                query = Query(searchCriteria, isFirstOne, query);
                query += $"p.jamatkhana IN ({string.Join(",", searchCriteria.JamatKhana)})";
            }

            if (searchCriteria.JamatiTitle != null && searchCriteria.JamatiTitle.Count > 0)
            {
                query = Query(searchCriteria, isFirstOne, query);
                query += $"p.jamati_title IN ({string.Join(",", searchCriteria.JamatiTitle)})";
            }

            if (searchCriteria.City != null && searchCriteria.City.Count > 0)
            {
                query = Query(searchCriteria, isFirstOne, query);
                query += $"p.city IN ({string.Join(",", searchCriteria.City)})";
            }

            if (searchCriteria.AreaOfOrigin != null && searchCriteria.AreaOfOrigin.Count > 0)
            {
                query = Query(searchCriteria, isFirstOne, query);
                query += $"p.area_of_origin IN ({string.Join(",", searchCriteria.AreaOfOrigin)})";
            }

            if (searchCriteria.HighestLevelOfStudy != null && searchCriteria.HighestLevelOfStudy.Count > 0)
            {
                query = Query(searchCriteria, isFirstOne, query);
                query += $"p.highest_level_of_study IN ({string.Join(",", searchCriteria.HighestLevelOfStudy)})";
            }

            //if (searchCriteria.FiledOfInterest != null && searchCriteria.FiledOfInterest.Count > 0)
            //{
            //    query = Query(searchCriteria, isFirstOne, query);
            //    query += $"p.field_of_interest IN ({string.Join(",", searchCriteria.FiledOfInterest)})";
            //}

            if (searchCriteria.OccupationType != null && searchCriteria.OccupationType.Count > 0)
            {
                query = Query(searchCriteria, isFirstOne, query);
                query += $"p.occupation_type IN ({string.Join(",", searchCriteria.OccupationType)})";
            }

            if (searchCriteria.EducationalInstitution != null && searchCriteria.EducationalInstitution.Count > 0)
            {
                query = Query(searchCriteria, isFirstOne, query) + "(";
                foreach (var item in searchCriteria.EducationalInstitution)
                {
                    query += " json_contains(p.educations, '{\"institution\": " + item.Split('-')[0] + "}') OR";
                }
                query = query.Substring(0, query.LastIndexOf("OR") - 1);
                query += ")";
            }

            if (searchCriteria.NameOfDegree != null && searchCriteria.NameOfDegree.Count > 0)
            {
                query = Query(searchCriteria, isFirstOne, query) + "(";
                foreach (var item in searchCriteria.NameOfDegree)
                {
                    query += " json_contains(p.educations, '{\"nameOfDegree\": " + item.Split('-')[0] + "}') OR";
                }
                query = query.Substring(0, query.LastIndexOf("OR") - 1);
                query += ")";
            }

            if (searchCriteria.MajorAreaOfStudy != null && searchCriteria.MajorAreaOfStudy.Count > 0)
            {
                query = Query(searchCriteria, isFirstOne, query) + "(";
                foreach (var item in searchCriteria.MajorAreaOfStudy)
                {
                    query += " json_contains(p.educations, '{\"majorAreaOfStudy\": " + item.Split('-')[0] + "}') OR";
                }
                query = query.Substring(0, query.LastIndexOf("OR") - 1);
                query += ")";
            }

            if (searchCriteria.FieldOfExpertise != null && searchCriteria.FieldOfExpertise.Count > 0)
            {
                query = Query(searchCriteria, isFirstOne, query);
                query += $"pfoe.id IN ({string.Join(",", searchCriteria.FieldOfExpertise)})";
            }

            if (searchCriteria.ReligiousEducation != null && searchCriteria.ReligiousEducation.Count > 0)
            {
                query = Query(searchCriteria, isFirstOne, query);
                query += $"p.religious_education IN ({string.Join(",", searchCriteria.ReligiousEducation)})";
            }

            if (searchCriteria.AkdnTraining != null && searchCriteria.AkdnTraining.Count > 0)
            {
                query = Query(searchCriteria, isFirstOne, query) + "(";
                foreach (var item in searchCriteria.AkdnTraining)
                {
                    query += " json_contains(p.akdn_trainings, '{\"training\" : " + item.Split('-')[0] + "}') OR";
                }
                query = query.Substring(0, query.LastIndexOf("OR") - 1);
                query += ")";
            }

            if (searchCriteria.Skills != null && searchCriteria.Skills.Count > 0)
            {
                query = Query(searchCriteria, isFirstOne, query);
                query += $"ps.id IN ({string.Join(",", searchCriteria.Skills)})";
            }

            if (searchCriteria.ProfessionalMembership != null && searchCriteria.ProfessionalMembership.Count > 0)
            {
                query = Query(searchCriteria, isFirstOne, query);
                query += $"ppm.id IN ({string.Join(",", searchCriteria.ProfessionalMembership)})";
            }

            if (searchCriteria.ProfessionalTraining != null)
            {
                query = Query(searchCriteria, isFirstOne, query) + "(";
                query += "json_contains(p.professional_trainings, '{\"training\": " + searchCriteria.ProfessionalTraining + "}'))";
            }

            if (searchCriteria.EmploymentNameOfOrganization != null)
            {
                query = Query(searchCriteria, isFirstOne, query) + "(";
                query += "json_contains(p.employments, '{\"nameOfOrganization\": " + searchCriteria.EmploymentNameOfOrganization + "}'))";
            }

            if (searchCriteria.TypeOfBuisness != null && searchCriteria.TypeOfBuisness.Count > 0)
            {
                query = Query(searchCriteria, isFirstOne, query) + "(";
                foreach (var item in searchCriteria.TypeOfBuisness)
                {
                    query += " json_contains(p.employments, '{\"businessType\": " + item.Split('-')[0] + "}') OR";
                }
                query = query.Substring(0, query.LastIndexOf("OR") - 1);
                query += ")";
            }

            if (searchCriteria.NatureOfBuisness != null && searchCriteria.NatureOfBuisness.Count > 0)
            {
                query = Query(searchCriteria, isFirstOne, query) + "(";
                foreach (var item in searchCriteria.NatureOfBuisness)
                {
                    query += " json_contains(p.employments, '{\"businessNature\": " + item.Split('-')[0] + "}') OR";
                }
                query = query.Substring(0, query.LastIndexOf("OR") - 1);
                query += ")";
            }

            return query;
        }
    }
}