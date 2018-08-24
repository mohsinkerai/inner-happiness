using System.Diagnostics;
using AMS.frontend.web.Models;
using Microsoft.AspNetCore.Mvc;

namespace AMS.frontend.web.Controllers
{
    public class HomeController : Controller
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
            return View();
        }

        #endregion Public Methods
    }
}