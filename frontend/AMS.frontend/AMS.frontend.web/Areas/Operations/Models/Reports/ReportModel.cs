using AMS.frontend.web.Areas.Operations.Models.Nominations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AMS.frontend.web.Areas.Operations.Models.Reports
{
    public class ReportModel
    {
        public string Level { get; set; }

        [Display(Name = "Institutions")]
        public string Institution { get; set; }

        public string Layout { get; set; }

        public string Category { get; set; }

        [Display(Name="Start with Page Number")]
        public int? PageNumber { get; set; }

        [Display(Name = "Include Remarks")]
        public bool Remarks { get; set; }

        [Display(Name = "Include Recommendations")]
        public bool Recommendation { get; set; }

        [Display(Name = "Include Locals")]
        public bool IncludeLocals { get; set; }

        [Display(Name = "Locals Only")]
        public bool LocalsOnly { get; set; }

    }
}
