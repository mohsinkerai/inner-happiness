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
                        MenuId = 25,
                        Action = ActionNames.Index,
                        Controller = ControllerNames.Cycle,
                        SubMenu = null,
                        Title = "Cycle",
                        Area = AreaNames.Operations,
                        ImageClass = "flaticon-share"
                    });

                    menuModel.Add(new MenuModel
                    {
                        MenuId = 15,
                        Action = ActionNames.Index,
                        Controller = ControllerNames.Image,
                        SubMenu = null,
                        Title = "Bulk Image Upload",
                        Area = AreaNames.Operations,
                        ImageClass = "flaticon-user"
                    });

                    menuModel.Add(new MenuModel
                    {
                        MenuId = 32,
                        Action = ActionNames.Index,
                        Controller = ControllerNames.SearchPerson,
                        SubMenu = null,
                        Title = "Search Person",
                        Area = AreaNames.Operations,
                        ImageClass = "flaticon-search"
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
                                Action = ActionNames.EducationalInstitution,
                                Controller = ControllerNames.LookupCrud,
                                SubMenu = null,
                                Title = "Educational Institution",
                                Area = AreaNames.Administration,
                                ImageClass = "flaticon-network"
                            },
                            new MenuModel
                            {
                                MenuId = 7,
                                Action = ActionNames.Country,
                                Controller = ControllerNames.LookupCrud,
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
                                Action = ActionNames.AreaOfOrigin,
                                Controller = ControllerNames.LookupCrud,
                                SubMenu = null,
                                Title = "Area Of Origin",
                                Area = AreaNames.Administration,
                                ImageClass = "flaticon-network"
                            },
                            new MenuModel
                            {
                                MenuId = 10,
                                Action = ActionNames.BusinessNature,
                                Controller = ControllerNames.LookupCrud,
                                SubMenu = null,
                                Title = "Business Nature",
                                Area = AreaNames.Administration,
                                ImageClass = "flaticon-network"
                            },
                            new MenuModel
                            {
                                MenuId = 11,
                                Action = ActionNames.BusinessType,
                                Controller = ControllerNames.LookupCrud,
                                SubMenu = null,
                                Title = "Business Type",
                                Area = AreaNames.Administration,
                                ImageClass = "flaticon-network"
                            },
                            new MenuModel
                            {
                                MenuId = 12,
                                Action = ActionNames.EducationalDegree,
                                Controller = ControllerNames.LookupCrud,
                                SubMenu = null,
                                Title = "Educational Degree",
                                Area = AreaNames.Administration,
                                ImageClass = "flaticon-network"
                            },
                            new MenuModel
                            {
                                MenuId = 13,
                                Action = ActionNames.FieldOfExpertise,
                                Controller = ControllerNames.LookupCrud,
                                SubMenu = null,
                                Title = "Field of Expertise",
                                Area = AreaNames.Administration,
                                ImageClass = "flaticon-network"
                            },
                            new MenuModel
                            {
                                MenuId = 14,
                                Action = ActionNames.FieldOfInterest,
                                Controller = ControllerNames.LookupCrud,
                                SubMenu = null,
                                Title = "Field of Interest",
                                Area = AreaNames.Administration,
                                ImageClass = "flaticon-network"
                            },
                            new MenuModel
                            {
                                MenuId = 16,
                                Action = ActionNames.Language,
                                Controller = ControllerNames.LookupCrud,
                                SubMenu = null,
                                Title = "Language",
                                Area = AreaNames.Administration,
                                ImageClass = "flaticon-network"
                            },
                            new MenuModel
                            {
                                MenuId = 17,
                                Action = ActionNames.MaritalStatus,
                                Controller = ControllerNames.LookupCrud,
                                SubMenu = null,
                                Title = "Marital Statius",
                                Area = AreaNames.Administration,
                                ImageClass = "flaticon-network"
                            },
                            new MenuModel
                            {
                                MenuId = 18,
                                Action = ActionNames.Occupation,
                                Controller = ControllerNames.LookupCrud,
                                SubMenu = null,
                                Title = "Occupation",
                                Area = AreaNames.Administration,
                                ImageClass = "flaticon-network"
                            },
                            new MenuModel
                            {
                                MenuId = 19,
                                Action = ActionNames.ProfessionalMembership,
                                Controller = ControllerNames.LookupCrud,
                                SubMenu = null,
                                Title = "Professional Membership",
                                Area = AreaNames.Administration,
                                ImageClass = "flaticon-network"
                            },
                            new MenuModel
                            {
                                MenuId = 20,
                                Action = ActionNames.PublicServiceInstitution,
                                Controller = ControllerNames.LookupCrud,
                                SubMenu = null,
                                Title = "Public Service Institution",
                                Area = AreaNames.Administration,
                                ImageClass = "flaticon-network"
                            },
                            new MenuModel
                            {
                                MenuId = 21,
                                Action = ActionNames.ReligiousQualification,
                                Controller = ControllerNames.LookupCrud,
                                SubMenu = null,
                                Title = "Religious Qualification",
                                Area = AreaNames.Administration,
                                ImageClass = "flaticon-network"
                            },
                            new MenuModel
                            {
                                MenuId = 22,
                                Action = ActionNames.Salutation,
                                Controller = ControllerNames.LookupCrud,
                                SubMenu = null,
                                Title = "Salutation",
                                Area = AreaNames.Administration,
                                ImageClass = "flaticon-network"
                            },
                            new MenuModel
                            {
                                MenuId = 23,
                                Action = ActionNames.SecularStudyLevel,
                                Controller = ControllerNames.LookupCrud,
                                SubMenu = null,
                                Title = "Secular Study Level",
                                Area = AreaNames.Administration,
                                ImageClass = "flaticon-network"
                            },
                            new MenuModel
                            {
                                MenuId = 24,
                                Action = ActionNames.Skill,
                                Controller = ControllerNames.LookupCrud,
                                SubMenu = null,
                                Title = "Skills",
                                Area = AreaNames.Administration,
                                ImageClass = "flaticon-network"
                            },
                            new MenuModel
                            {
                                MenuId = 26,
                                Action = ActionNames.City,
                                Controller = ControllerNames.LookupCrud,
                                SubMenu = null,
                                Title = "City",
                                Area = AreaNames.Administration,
                                ImageClass = "flaticon-network"
                            },
                            new MenuModel
                            {
                                MenuId = 27,
                                Action = ActionNames.JamatiTitle,
                                Controller = ControllerNames.LookupCrud,
                                SubMenu = null,
                                Title = "Jamati Title",
                                Area = AreaNames.Administration,
                                ImageClass = "flaticon-network"
                            },
                            new MenuModel
                            {
                                MenuId = 28,
                                Action = ActionNames.Position,
                                Controller = ControllerNames.LookupCrud,
                                SubMenu = null,
                                Title = "Position",
                                Area = AreaNames.Administration,
                                ImageClass = "flaticon-network"
                            },
                            new MenuModel
                            {
                                MenuId = 29,
                                Action = ActionNames.VoluntaryInstitution,
                                Controller = ControllerNames.LookupCrud,
                                SubMenu = null,
                                Title = "Institution",
                                Area = AreaNames.Administration,
                                ImageClass = "flaticon-network"
                            },
                        },
                        Title = "Master Data",
                        Area = string.Empty,
                        ImageClass = "flaticon-list"
                    });
                }

                if (role == "REP" && menuModel.All(mm => mm.MenuId != 26))
                {
                    menuModel.Add(new MenuModel
                    {
                        MenuId = 26,
                        Action = string.Empty,
                        Controller = string.Empty,
                        SubMenu = new List<MenuModel> {
                            new MenuModel
                            {
                                MenuId = 30,
                                Action = ActionNames.Index,
                                Controller = ControllerNames.Reports,
                                SubMenu = null,
                                Title = "Reports",
                                Area = AreaNames.Operations,
                                ImageClass = "flaticon-network"
                            },
                            new MenuModel
                            {
                                MenuId = 31,
                                Action = ActionNames.Index,
                                Controller = ControllerNames.ThreePlusOneReport,
                                SubMenu = null,
                                Title = "3+1 Report",
                                Area = AreaNames.Operations,
                                ImageClass = "flaticon-network"
                            }
                        },
                        Title = "Reports",
                        Area = string.Empty,
                        ImageClass = "flaticon-line-graph"
                    });
                }
            }

            return View(menuModel);
        }

        #endregion Public Methods
    }
}