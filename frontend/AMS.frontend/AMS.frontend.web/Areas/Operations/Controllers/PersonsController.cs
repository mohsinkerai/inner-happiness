using System.Collections.Generic;
using AMS.frontend.web.Helpers.Constants;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using AMS.frontend.web.Areas.Operations.Models.Persons;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AMS.frontend.web.Areas.Operations.Controllers
{
    [Area(AreaNames.Operations)]
    public class PersonsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly Configuration _configuration;

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