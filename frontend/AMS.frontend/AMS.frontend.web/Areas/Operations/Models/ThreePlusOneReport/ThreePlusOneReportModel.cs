using AMS.frontend.web.Areas.Operations.Models.Nominations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AMS.frontend.web.Areas.Operations.Models.ThreePlusOneReport
{
    public class ThreePlusOneReportModel
    {
        public string Level { get; set; }

        [Display(Name = "Region")]
        public string RegionalInstitution { get; set; }

        public string Category { get; set; }

        [Display(Name="Start with Page Number")]
        public int PageNumber { get; set; }

        public bool Remarks { get; set; }

        [Display(Name ="Include Parent")]
        public bool IncludeParent { get; set; }

    }
}
