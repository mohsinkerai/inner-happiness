using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AMS.frontend.web.Areas.Administration.Models.AreaOfStudies
{
    public class AreaOfStudyModel
    {
        [Required]
        public string Name { get; set; }
    }
}
