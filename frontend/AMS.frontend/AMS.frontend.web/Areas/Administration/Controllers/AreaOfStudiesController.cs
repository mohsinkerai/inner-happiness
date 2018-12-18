using AMS.frontend.web.Areas.Administration.Models.AreaOfStudies;
using AMS.frontend.web.Helpers.Constants;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AMS.frontend.web.Areas.Administration.Controllers
{
    [Area(AreaNames.Administration)]
    public class AreaOfStudiesController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(AreaOfStudyModel model)
        {
            if (ModelState.IsValid)
            {
            }

            return View();
        }
    }
}