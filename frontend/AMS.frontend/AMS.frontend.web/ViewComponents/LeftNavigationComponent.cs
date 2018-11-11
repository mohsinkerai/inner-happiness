using System.Collections.Generic;
using System.Linq;
using AMS.frontend.web.Areas.Operations.Models;
using AMS.frontend.web.Extensions;
using AMS.frontend.web.Helpers.Constants;
using AMS.frontend.web.Models.Navigation;
using Microsoft.AspNetCore.Mvc;

namespace AMS.frontend.web.ViewComponents
{
    [ViewComponent(Name = "LeftNavigation")]
    public class LeftNavigationComponent : ViewComponent
    {
        #region Public Methods

        public IViewComponentResult Invoke()
        {
            var authReponse = HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse");
            var menuModel = new List<MenuModel>();

            foreach (var role in authReponse.Roles)
            {
                if (role == "PIF" && menuModel.All(mm => mm.MenuId != 1))
                    menuModel.Add(new MenuModel
                    {
                        MenuId = 1,
                        Action = ActionNames.Index,
                        Controller = ControllerNames.Persons,
                        SubMenu = null,
                        Title = "Basic Information Form",
                        Area = AreaNames.Operations,
                        ImageClass = "flaticon-information"
                    });

                if ((role == "NOM" || role == "REC") && menuModel.All(mm => mm.MenuId != 2))
                    menuModel.Add(new MenuModel
                    {
                        MenuId = 2,
                        Action = ActionNames.Index,
                        Controller = ControllerNames.Nominations,
                        SubMenu = null,
                        Title = "Nominations",
                        Area = AreaNames.Operations,
                        ImageClass = "flaticon-network"
                    });
            }

            return View(menuModel);
        }

        #endregion Public Methods
    }
}