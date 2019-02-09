using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AMS.frontend.web.Areas.Operations.Models.Cycle
{
    public class CycleModel
    {
        [NotMapped]
        public string Id { get; set; }

        [JsonIgnore]
        public int NumericId => Convert.ToInt32(Id);

        public string Name { get; set; }

        [Required]
        public DateTime? StartDate { get; set; }

        [Required]
        public DateTime? EndDate { get; set; }

        [Required]
        [Display(Name ="Previous Cycle")]
        public string PreviousCycle { get; set; }

        [JsonIgnore]
        public string StartDateForDisplay => StartDate?.ToString("dd/MM/yyyy");


        [JsonIgnore]
        public string EndDateForDisplay => EndDate?.ToString("dd/MM/yyyy");

        [JsonIgnore]
        public string CycleNameForDisplay => StartDate?.ToString("yyyy") +"-"+ EndDate?.ToString("yyyy");
    }
}
