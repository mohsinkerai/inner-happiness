using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AMS.frontend.web.Areas.Operations.Models.Cycle
{
    public class CycleModel
    {
        public string Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        [JsonIgnore]
        public string StartDateForDisplay => StartDate?.ToString("dd/MM/yyyy");


        [JsonIgnore]
        public string EndDateForDisplay => EndDate?.ToString("dd/MM/yyyy");
    }
}
