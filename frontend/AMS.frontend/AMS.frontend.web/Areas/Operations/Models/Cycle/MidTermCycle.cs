﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AMS.frontend.web.Areas.Operations.Models.Cycle
{
    public class MidTermCycle
    {
        [Required]
        public string Institution { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]

        public DateTime? StartDate { get; set; }
    }
}
