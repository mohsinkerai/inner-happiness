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
            
            return View(new SearchCriteria());
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
            ViewBag.AreaOfStudy = await new RestfulClient(_logger,
                        HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse")?.Token).GetMajorAreaOfStudy();
        }

        [HttpPost]
        public async Task<IActionResult> Index(SearchCriteria searchCriteria)
        {
            var formCollection = await HttpContext.Request.ReadFormAsync().ConfigureAwait(false);

            if (!string.IsNullOrWhiteSpace(formCollection["DateOfBirthFrom"]))
            {
                searchCriteria.DateOfBirthFrom = DateTime.ParseExact(formCollection["DateOfBirthFrom"], "dd/MM/yyyy", null);
                ModelState.Remove("DateOfBirthFrom");
            }

            if (!string.IsNullOrWhiteSpace(formCollection["DateOfBirthTo"]))
            {
                searchCriteria.DateOfBirthTo = DateTime.ParseExact(formCollection["DateOfBirthTo"], "dd/MM/yyyy", null);
                ModelState.Remove("DateOfBirthTo");
            }

            UploadImageContext context = HttpContext.RequestServices.GetService(typeof(UploadImageContext)) as UploadImageContext;
            var query = MakeQuery(searchCriteria);
            List<PersonModel> personList = context.GetPersons(query);
            searchCriteria.persons = personList;
            searchCriteria.Query = query;
            
            await InitializeSearchCriteria();

            return View(searchCriteria);
        }

        private string MakeQuery(SearchCriteria searchCriteria)
        {
            string Query(SearchCriteria searchCriteria1, ref bool b, ref string s)
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
                "SELECT DISTINCT(p.id), '' as institution_name, p.cnic, p.full_name, p.mobile_phone, CONCAT(ed.NAME, \", \", ei.NAME, \", \", json_extract(p.educations,'$[0].fromYear'),\" - \", json_extract(p.educations,'$[0].toYear')) AS latest_education, CONCAT(TRIM(BOTH '\"' from json_extract(p.employments,'$[0].designation')),\", \",TRIM(BOTH '\"' from json_extract(p.employments,'$[0].nameOfOrganization'))) as latest_employment FROM person AS p LEFT OUTER JOIN person_skill AS ps ON p.id = ps.person_id LEFT OUTER JOIN person_professional_membership AS ppm ON p.id = ppm.person_id LEFT OUTER JOIN person_field_of_expertise AS pfoe ON p.id = pfoe.person_id LEFT OUTER JOIN educational_degree AS ed ON json_extract(p.educations,'$[0].nameOfDegree') = ed.id LEFT OUTER JOIN educational_institution AS ei ON json_extract(p.educations,'$[0].institution') = ei.id WHERE";

            if (searchCriteria.ShowRecommendation)
            {
                query =
                    $"SELECT b.id, b.cnic, b.full_name, b.mobile_phone, b.latest_education, b.latest_employment, CONCAT(pos.NAME, ' - ', l.NAME) as institution_name FROM({query}";
            }

            var isFirstOne = true;

            if (!string.IsNullOrWhiteSpace(searchCriteria.Name))
            {
                query = Query(searchCriteria, ref isFirstOne, ref query);
                query += $"p.first_name LIKE '%{searchCriteria.Name}%' OR p.fathers_name LIKE '%{searchCriteria.Name}%' OR p.family_name LIKE '%{searchCriteria.Name}%'";
            }

            if (searchCriteria.RegionalCouncil != null && searchCriteria.RegionalCouncil.Count > 0)
            {
                query = Query(searchCriteria, ref isFirstOne, ref query);
                query += $"p.regional_council IN ({string.Join(",", searchCriteria.RegionalCouncil)})";
            }

            if (searchCriteria.LocalCouncil != null && searchCriteria.LocalCouncil.Count > 0)
            {
                query = Query(searchCriteria, ref isFirstOne, ref query);
                query += $"p.local_council IN ({string.Join(",", searchCriteria.LocalCouncil)})";
            }

            if (searchCriteria.JamatKhana != null && searchCriteria.JamatKhana.Count > 0)
            {
                query = Query(searchCriteria, ref isFirstOne, ref query);
                query += $"p.jamatkhana IN ({string.Join(",", searchCriteria.JamatKhana)})";
            }

            if (searchCriteria.JamatiTitle != null && searchCriteria.JamatiTitle.Count > 0)
            {
                query = Query(searchCriteria, ref isFirstOne, ref query);
                query += $"p.jamati_title IN ({string.Join(",", searchCriteria.JamatiTitle)})";
            }

            if (searchCriteria.City != null && searchCriteria.City.Count > 0)
            {
                query = Query(searchCriteria, ref isFirstOne, ref query);
                query += $"p.city IN ({string.Join(",", searchCriteria.City)})";
            }

            if (searchCriteria.AreaOfOrigin != null && searchCriteria.AreaOfOrigin.Count > 0)
            {
                query = Query(searchCriteria, ref isFirstOne, ref query);
                query += $"p.area_of_origin IN ({string.Join(",", searchCriteria.AreaOfOrigin)})";
            }

            if (searchCriteria.HighestLevelOfStudy != null && searchCriteria.HighestLevelOfStudy.Count > 0)
            {
                query = Query(searchCriteria, ref isFirstOne, ref query);
                query += $"p.highest_level_of_study IN ({string.Join(",", searchCriteria.HighestLevelOfStudy)})";
            }

            //if (searchCriteria.FiledOfInterest != null && searchCriteria.FiledOfInterest.Count > 0)
            //{
            //    query = Query(searchCriteria, ref isFirstOne, ref query);
            //    query += $"p.field_of_interest IN ({string.Join(",", searchCriteria.FiledOfInterest)})";
            //}

            if (searchCriteria.OccupationType != null && searchCriteria.OccupationType.Count > 0)
            {
                query = Query(searchCriteria, ref isFirstOne, ref query);
                query += $"p.occupation_type IN ({string.Join(",", searchCriteria.OccupationType)})";
            }

            if (searchCriteria.EducationalInstitution != null && searchCriteria.EducationalInstitution.Count > 0)
            {
                query = Query(searchCriteria, ref isFirstOne, ref query) + "(";
                foreach (var item in searchCriteria.EducationalInstitution)
                {
                    query += " json_contains(p.educations, '{\"institution\": " + item.Split('-')[0] + "}') OR";
                }
                query = query.Substring(0, query.LastIndexOf("OR") - 1);
                query += ")";
            }

            if (searchCriteria.NameOfDegree != null && searchCriteria.NameOfDegree.Count > 0)
            {
                query = Query(searchCriteria, ref isFirstOne, ref query) + "(";
                foreach (var item in searchCriteria.NameOfDegree)
                {
                    query += " json_contains(p.educations, '{\"nameOfDegree\": " + item.Split('-')[0] + "}') OR";
                }
                query = query.Substring(0, query.LastIndexOf("OR") - 1);
                query += ")";
            }

            if (searchCriteria.MajorAreaOfStudy != null && searchCriteria.MajorAreaOfStudy.Count > 0)
            {
                query = Query(searchCriteria, ref isFirstOne, ref query) + "(";
                foreach (var item in searchCriteria.MajorAreaOfStudy)
                {
                    query += " json_contains(p.educations, '{\"majorAreaOfStudy\": " + item.Split('-')[0] + "}') OR";
                }
                query = query.Substring(0, query.LastIndexOf("OR") - 1);
                query += ")";
            }

            if (searchCriteria.FieldOfExpertise != null && searchCriteria.FieldOfExpertise.Count > 0)
            {
                query = Query(searchCriteria, ref isFirstOne, ref query);
                query += $"pfoe.id IN ({string.Join(",", searchCriteria.FieldOfExpertise)})";
            }

            if (searchCriteria.ReligiousEducation != null && searchCriteria.ReligiousEducation.Count > 0)
            {
                query = Query(searchCriteria, ref isFirstOne, ref query);
                query += $"p.religious_education IN ({string.Join(",", searchCriteria.ReligiousEducation)})";
            }

            if (searchCriteria.AkdnTraining != null && searchCriteria.AkdnTraining.Count > 0)
            {
                query = Query(searchCriteria, ref isFirstOne, ref query) + "(";
                foreach (var item in searchCriteria.AkdnTraining)
                {
                    query += " json_contains(p.akdn_trainings, '{\"training\" : " + item.Split('-')[0] + "}') OR";
                }
                query = query.Substring(0, query.LastIndexOf("OR") - 1);
                query += ")";
            }

            if (searchCriteria.Skills != null && searchCriteria.Skills.Count > 0)
            {
                query = Query(searchCriteria, ref isFirstOne, ref query);
                query += $"ps.id IN ({string.Join(",", searchCriteria.Skills)})";
            }

            if (searchCriteria.ProfessionalMembership != null && searchCriteria.ProfessionalMembership.Count > 0)
            {
                query = Query(searchCriteria, ref isFirstOne, ref query);
                query += $"ppm.id IN ({string.Join(",", searchCriteria.ProfessionalMembership)})";
            }

            if (!string.IsNullOrWhiteSpace(searchCriteria.ProfessionalTraining))
            {
                query = Query(searchCriteria, ref isFirstOne, ref query) + "(";
                query += "json_contains(p.professional_trainings, '{\"training\": " + searchCriteria.ProfessionalTraining + "}'))";
            }

            if (!string.IsNullOrWhiteSpace(searchCriteria.EmploymentNameOfOrganization))
            {
                query = Query(searchCriteria, ref isFirstOne, ref query) + "(";
                query += "json_contains(p.employments, '{\"nameOfOrganization\": " + searchCriteria.EmploymentNameOfOrganization + "}'))";
            }

            if (searchCriteria.TypeOfBuisness != null && searchCriteria.TypeOfBuisness.Count > 0)
            {
                query = Query(searchCriteria, ref isFirstOne, ref query) + "(";
                foreach (var item in searchCriteria.TypeOfBuisness)
                {
                    query += " json_contains(p.employments, '{\"businessType\": " + item.Split('-')[0] + "}') OR";
                }
                query = query.Substring(0, query.LastIndexOf("OR") - 1);
                query += ")";
            }

            if (searchCriteria.NatureOfBuisness != null && searchCriteria.NatureOfBuisness.Count > 0)
            {
                query = Query(searchCriteria, ref isFirstOne, ref query) + "(";
                foreach (var item in searchCriteria.NatureOfBuisness)
                {
                    query += " json_contains(p.employments, '{\"businessNature\": " + item.Split('-')[0] + "}') OR";
                }
                query = query.Substring(0, query.LastIndexOf("OR") - 1);
                query += ")";
            }

            if (searchCriteria.AreaOfStudy != null && searchCriteria.AreaOfStudy.Count > 0)
            {
                query = Query(searchCriteria, ref isFirstOne, ref query) + "(";
                foreach (var item in searchCriteria.AreaOfStudy)
                {
                    query += " json_contains(p.educations, '{\"majorAreaOfStudy\": " + item.Split('-')[0] + "}') OR";
                }
                query = query.Substring(0, query.LastIndexOf("OR") - 1);
                query += ")";
            }

            if (!string.IsNullOrWhiteSpace(searchCriteria.EmploymentCategory))
            {
                query = Query(searchCriteria, ref isFirstOne, ref query) + "(";
                query += "json_contains(p.employments, '{\"employmentCategory\" : \"" + searchCriteria.EmploymentCategory + "\"}'))";
            }

            if (searchCriteria.Gender != null)
            {
                query = Query(searchCriteria, ref isFirstOne, ref query);
                query += "p.gender = " + (searchCriteria.Gender == Gender.Male ? "0" : "1");
            }

            if (searchCriteria.DateOfBirthFrom != null || searchCriteria.DateOfBirthTo != null)
            {
                query = Query(searchCriteria, ref isFirstOne, ref query);
                query +=
                    $"p.date_of_birth BETWEEN '{(searchCriteria.DateOfBirthFrom == null ? DateTime.MinValue.ToString("yyyy-MM-dd HH:mm:ss") : searchCriteria.DateOfBirthFrom.Value.ToString("yyyy-MM-dd HH:mm:ss"))}' AND '{(searchCriteria.DateOfBirthTo == null ? DateTime.MaxValue.ToString("yyyy-MM-dd HH:mm:ss") : searchCriteria.DateOfBirthTo.Value.ToString("yyyy-MM-dd HH:mm:ss"))}'";
            }

            if (searchCriteria.ShowRecommendation)
            {
                return
                    $"{query}) AS b INNER JOIN person_appointment pa ON b.id = pa.person_id INNER JOIN appointment_position ap ON pa.appointment_position_id = ap.id INNER JOIN level l ON ap.institution_id = l.id INNER JOIN position pos ON ap.position_id = pos.id WHERE ap.cycle_id = 19 AND pa.is_recommended = 1";
            }

            return query;
        }
    }
}