﻿using AMS.frontend.web.Areas.Operations.Models.Nominations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AMS.frontend.web.Areas.Operations.Models.ThreePlusOneReport
{
    public class ThreePlusOneReportModel
    {
        public string Level { get; set; }
        public string InstitutionName { get; set; }

        public List<InstitutionModel> ListInstitution { get; set; }
        
    }
}