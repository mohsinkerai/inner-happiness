using AMS.frontend.web.Areas.Operations.Models;
using AMS.frontend.web.Extensions;
using AMS.frontend.web.Helpers.Constants;
using AMS.frontend.web.Models.Navigation;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace AMS.frontend.web.ViewComponents
{
    [ViewComponent(Name = "LeftNavigation")]
    public class LeftNavigationComponent : ViewComponent
    {
        #region Public Methods

        public IViewComponentResult Invoke()
        {
            AuthenticationResponse authReponse = HttpContext.Session.Get<AuthenticationResponse>("AuthenticationResponse");
            List<MenuModel> menuModel = new List<MenuModel>();

            foreach (string role in authReponse.Roles)
            {

                if (role == "DB" && menuModel.All(mm => mm.MenuId != 1))
                {
                    menuModel.Add(new MenuModel
                    {
                        MenuId = 1,
                        Action = ActionNames.Index,
                        Controller = ControllerNames.Dashboard,
                        SubMenu = null,
                        Title = "Dashboard",
                        Area = AreaNames.Blank,
                        ImageClass = "flaticon-analytics"
                    });
                }

                if (role == "PIF" && menuModel.All(mm => mm.MenuId != 2))
                {
                    menuModel.Add(new MenuModel
                    {
                        MenuId = 2,
                        Action = ActionNames.Index,
                        Controller = ControllerNames.Persons,
                        SubMenu = null,
                        Title = "Basic Information Form",
                        Area = AreaNames.Operations,
                        ImageClass = "flaticon-information"
                    });
                }

                if ((role == "NOM" || role == "REC") && menuModel.All(mm => mm.MenuId != 3))
                {
                    menuModel.Add(new MenuModel
                    {
                        MenuId = 3,
                        Action = ActionNames.Index,
                        Controller = ControllerNames.Nominations,
                        SubMenu = null,
                        Title = "Nominations",
                        Area = AreaNames.Operations,
                        ImageClass = "flaticon-network"
                    });

                    menuModel.Add(new MenuModel
                    {
                        MenuId = 4,
                        Action = string.Empty,
                        Controller = string.Empty,
                        SubMenu = new List<MenuModel> {
                            new MenuModel
                            {
                                MenuId = 5,
                                Action = ActionNames.AreaOfStudy,
                                Controller = ControllerNames.LookupCrud,
                                SubMenu = null,
                                Title = "Area of Study",
                                Area = AreaNames.Administration,
                                ImageClass = "flaticon-network"
                            },
                            new MenuModel
                            {
                                MenuId = 6,
                                Action = ActionNames.Index,
                                Controller = ControllerNames.EducationalInstituion,
                                SubMenu = null,
                                Title = "Educational Institution",
                                Area = AreaNames.Administration,
                                ImageClass = "flaticon-network"
                            },
                            new MenuModel
                            {
                                MenuId = 7,
                                Action = ActionNames.Index,
                                Controller = ControllerNames.Country,
                                SubMenu = null,
                                Title = "Country",
                                Area = AreaNames.Administration,
                                ImageClass = "flaticon-network"
                            },
                            new MenuModel
                            {
                                MenuId = 8,
                                Action = ActionNames.AkdnTraining,
                                Controller = ControllerNames.LookupCrud,
                                SubMenu = null,
                                Title = "Akdn Training",
                                Area = AreaNames.Administration,
                                ImageClass = "flaticon-network"
                            },
                            new MenuModel
                            {
                                MenuId = 9,
                                Action = ActionNames.Index,
                                Controller = ControllerNames.AreaOfOrigin,
                                SubMenu = null,
                                Title = "Area Of Origin",
                                Area = AreaNames.Administration,
                                ImageClass = "flaticon-network"
                            },
                            new MenuModel
                            {
                                MenuId = 10,
                                Action = ActionNames.Index,
                                Controller = ControllerNames.BusinessNature,
                                SubMenu = null,
                                Title = "Business Nature",
                                Area = AreaNames.Administration,
                                ImageClass = "flaticon-network"
                            },
                            new MenuModel
                            {
                                MenuId = 11,
                                Action = ActionNames.Index,
                                Controller = ControllerNames.BusinessType,
                                SubMenu = null,
                                Title = "Business Type",
                                Area = AreaNames.Administration,
                                ImageClass = "flaticon-network"
                            },
                            new MenuModel
                            {
                                MenuId = 12,
                                Action = ActionNames.Index,
                                Controller = ControllerNames.EducationalDegree,
                                SubMenu = null,
                                Title = "Educational Degree",
                                Area = AreaNames.Administration,
                                ImageClass = "flaticon-network"
                            },
                            new MenuModel
                            {
                                MenuId = 13,
                                Action = ActionNames.Index,
                                Controller = ControllerNames.FieldOfExpertise,
                                SubMenu = null,
                                Title = "Field of Expertise",
                                Area = AreaNames.Administration,
                                ImageClass = "flaticon-network"
                            },
                            new MenuModel
                            {
                                MenuId = 14,
                                Action = ActionNames.Index,
                                Controller = ControllerNames.FieldOfInterest,
                                SubMenu = null,
                                Title = "Field of Interest",
                                Area = AreaNames.Administration,
                                ImageClass = "flaticon-network"
                            }
                        },
                        Title = "Master Data",
                        Area = string.Empty,
                        ImageClass = "flaticon-network"
                    });
                }
            }

            return View(menuModel);
        }

        #endregion Public Methods
    }
}