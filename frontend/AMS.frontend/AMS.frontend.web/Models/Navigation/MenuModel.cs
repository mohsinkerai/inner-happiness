using System.Collections.Generic;

namespace AMS.frontend.web.Models.Navigation
{
    public class MenuModel
    {
        #region Public Properties

        public string Action { get; set; }
        public string Area { get; set; }
        public string Controller { get; set; }
        public string ImageClass { get; set; }
        public int MenuId { get; set; }
        public List<MenuModel> SubMenu { get; set; }
        public string Title { get; set; }

        #endregion Public Properties
    }
}