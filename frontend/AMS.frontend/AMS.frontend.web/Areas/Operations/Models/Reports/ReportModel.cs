﻿using System.ComponentModel.DataAnnotations;

namespace AMS.frontend.web.Areas.Operations.Models.Reports
{
    public class ReportModel
    {
        #region Public Properties

        public string Category { get; set; }

        [Display(Name = "File Type")]
        public string FileType { get; set; }

        [Display(Name = "Include Locals")]
        public bool IncludeLocals { get; set; }

        [Display(Name = "Institutions")]
        public string Institution { get; set; }

        public string Layout { get; set; }
        public string Level { get; set; }

        [Display(Name = "Locals Only")]
        public bool LocalsOnly { get; set; }

        [Display(Name = "Office Bearers Only")]
        public bool OfficeBearersOnly { get; set; }

        [Display(Name = "Start with Page Number")]
        public int? PageNumber { get; set; }

        [Display(Name = "Include Recommendations")]
        public bool Recommendation { get; set; }

        [Display(Name = "Include Remarks")]
        public bool Remarks { get; set; }

        #endregion Public Properties
    }
}