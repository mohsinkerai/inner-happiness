using AMS.frontend.web.Areas.Operations.Models;
using AMS.frontend.web.Areas.Operations.Models.Persons;
using AMS.frontend.web.Extensions;
using AMS.frontend.web.Helpers.Constants;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using AMS.frontend.web.Areas.Operations.Models.Nominations;

namespace AMS.frontend.web.Areas.Operations.Controllers
{
    [Area(AreaNames.Operations)]
    public class NominationsController : Controller
    {
        #region Private Fields

        private readonly Configuration _configuration;

        private readonly IMapper _mapper;

        #endregion Private Fields

        #region Public Constructors

        public NominationsController(IMapper mapper, IOptions<Configuration> configuration)
        {
            _mapper = mapper;
            _configuration = configuration.Value;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<IActionResult> Index()
        {
            ViewBag.MessageType = TempData["MessageType"];
            ViewBag.Message = TempData["Message"];
            //return View(new List<PersonModel>());

            ViewBag.CompanyList = await RestfulClient.getAllCompanies();
            ViewBag.RegionList = await RestfulClient.getRegionalCouncil();
            ViewBag.LocalList = await RestfulClient.getLocalCouncil();
            ViewBag.JamatkhanaList = await RestfulClient.getJamatkhana();
            ViewBag.InstitutionList = await RestfulClient.getPositionInstitution();

            return View(new IndexNominationModel{Company = "1", Positions = new List<Position>()});
        }

        [HttpPost]
        public async Task<IActionResult> Index(string company, string region, string local, string jamatkhana, string institution)
        {
            return RedirectToAction("Index");
        }

        #endregion Public Methods
    }
}