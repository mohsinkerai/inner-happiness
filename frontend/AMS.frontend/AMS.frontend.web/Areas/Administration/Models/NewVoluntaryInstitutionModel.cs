using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AMS.frontend.web.Areas.Administration.Models
{
    public class NewVoluntaryInstitutionModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string FullName { get; set; }

        public string Population { get; set; }

        public string HouseHold { get; set; }

        public string JamatKhana { get; set; }

        public string Level { get; set; }

        public string SubLevel { get; set; }

        [JsonIgnore]
        public List<SelectListItem> lookUpList { get; set; }

        public NewVoluntaryInstitutionModel()
        {
            lookUpList = new List<SelectListItem>();
        }

        [JsonIgnore]
        public string Title { get; set; }

    }
}
