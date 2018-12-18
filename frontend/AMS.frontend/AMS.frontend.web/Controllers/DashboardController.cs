using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AMS.frontend.web.Extensions;
using AMS.frontend.web.Helpers.Constants;
using AMS.frontend.web.Helpers.CustomAttributes;
using AMS.frontend.web.Models;
using Microsoft.AspNetCore.Mvc;

namespace AMS.frontend.web.Controllers
{
    [Authenticate]
    public class DashboardController : Controller
    {
        #region Public Methods

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }

        public IActionResult Index()
        {
            ViewBag.MessageType = TempData["MessageType"];
            ViewBag.Message = TempData["Message"];

            return View();
        }
        
        #endregion Public Methods
    }
}