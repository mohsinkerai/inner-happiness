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
        public async Task<IViewComponentResult> InvokeAsync()
        {
            IList MenuModel = new List<MenuModel>();

            MenuModel menu = new MenuModel() { MenuId = 1, Action = "Index", Controller = "Home", SubMenu = null, Title = "Dashboard", Area = "", ImageClass = "flaticon-dashboard" };
            MenuModel.Add(menu);

            menu = new MenuModel() { MenuId = 2, Title = "Employee Management", Action = "Index", Controller = "Persons", SubMenu = null, Area = AreaNames.Operations, ImageClass = "flaticon-users" };
            MenuModel.Add(menu);

            menu = new MenuModel() { MenuId = 3, Title = "User Groups/Tags Management", Action = "List", Controller = "TagsManagement", SubMenu = null, Area = AreaNames.Operations, ImageClass = "flaticon-interface-9" };
            MenuModel.Add(menu);

            menu = new MenuModel() { MenuId = 4, Title = "Fee Management", Action = "List", Controller = "FeeManagement", SubMenu = null, Area = AreaNames.Operations, ImageClass = "flaticon-coins" };
            MenuModel.Add(menu);

            menu = new MenuModel() { MenuId = 5, Title = "Administration", ImageClass = "flaticon-user" };

            menu.SubMenu = new List<MenuModel>();
            MenuModel subMenu = new MenuModel() { Action = "Roles", Controller = "Administration", SubMenu = null, Title = "Role Management", Area = AreaNames.Operations, ImageClass = "flaticon-user-settings" };
            menu.SubMenu.Add(subMenu);

            subMenu = new MenuModel() { Action = "Users", Controller = "Administration", SubMenu = null, Title = "User Management", Area = AreaNames.Operations, ImageClass = "flaticon-user-add" };
            menu.SubMenu.Add(subMenu);

            MenuModel.Add(menu);

            menu = new MenuModel() { MenuId = 6, Title = "Reports", ImageClass = "flaticon-analytics" };
            MenuModel.Add(menu);
            //Session["ABC"] = MenuModel;

            return View(MenuModel);
        }
    }
}