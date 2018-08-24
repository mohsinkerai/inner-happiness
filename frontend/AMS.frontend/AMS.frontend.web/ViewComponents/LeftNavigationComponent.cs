using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AMS.frontend.web.Helpers.Constants;
using AMS.frontend.web.Models.Navigation;
using Microsoft.AspNetCore.Mvc;

namespace AMS.frontend.web.ViewComponents
{
    [ViewComponent(Name = "LeftNavigation")]
    public class LeftNavigationComponent : ViewComponent
    {
        #region Public Methods

        public async Task<IViewComponentResult> InvokeAsync()
        {
            IList menuModel = new List<MenuModel>
            {
                new MenuModel
                {
                    MenuId = 1,
                    Action = ActionNames.Index,
                    Controller = ControllerNames.Persons,
                    SubMenu = null,
                    Title = "Basic Information Form",
                    Area = AreaNames.Operations,
                    ImageClass = "flaticon-information"
                },

                new MenuModel
                {
                    MenuId = 2,
                    Action = ActionNames.Index,
                    Controller = ControllerNames.Nominations,
                    SubMenu = null,
                    Title = "Nominations",
                    Area = AreaNames.Operations,
                    ImageClass = "flaticon-network"
                }
            };

            //var menu = new MenuModel
            //{
            //    MenuId = 1,
            //    Action = "Index",
            //    Controller = "Home",
            //    SubMenu = null,
            //    Title = "Dashboard",
            //    Area = "",
            //    ImageClass = "flaticon-dashboard"
            //};
            //menuModel.Add(menu);

            //menu = new MenuModel
            //{
            //    MenuId = 2,
            //    Title = "Employee Management",
            //    Action = "Index",
            //    Controller = "Persons",
            //    SubMenu = null,
            //    Area = AreaNames.Operations,
            //    ImageClass = "flaticon-users"
            //};
            //menuModel.Add(menu);

            //menu = new MenuModel
            //{
            //    MenuId = 3,
            //    Title = "User Groups/Tags Management",
            //    Action = "List",
            //    Controller = "TagsManagement",
            //    SubMenu = null,
            //    Area = AreaNames.Operations,
            //    ImageClass = "flaticon-interface-9"
            //};
            //menuModel.Add(menu);

            //menu = new MenuModel
            //{
            //    MenuId = 4,
            //    Title = "Fee Management",
            //    Action = "List",
            //    Controller = "FeeManagement",
            //    SubMenu = null,
            //    Area = AreaNames.Operations,
            //    ImageClass = "flaticon-coins"
            //};
            //menuModel.Add(menu);

            //menu = new MenuModel {MenuId = 5, Title = "Administration", ImageClass = "flaticon-user"};

            //menu.SubMenu = new List<MenuModel>();
            //var subMenu = new MenuModel
            //{
            //    Action = "Roles",
            //    Controller = "Administration",
            //    SubMenu = null,
            //    Title = "Role Management",
            //    Area = AreaNames.Operations,
            //    ImageClass = "flaticon-user-settings"
            //};
            //menu.SubMenu.Add(subMenu);

            //subMenu = new MenuModel
            //{
            //    Action = "Users",
            //    Controller = "Administration",
            //    SubMenu = null,
            //    Title = "User Management",
            //    Area = AreaNames.Operations,
            //    ImageClass = "flaticon-user-add"
            //};
            //menu.SubMenu.Add(subMenu);

            //menuModel.Add(menu);

            //menu = new MenuModel {MenuId = 6, Title = "Reports", ImageClass = "flaticon-analytics"};
            //menuModel.Add(menu);
            //Session["ABC"] = MenuModel;

            return View(menuModel);
        }

        #endregion Public Methods
    }
}