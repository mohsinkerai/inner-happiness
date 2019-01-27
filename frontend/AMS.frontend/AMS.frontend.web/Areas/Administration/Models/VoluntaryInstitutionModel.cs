using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AMS.frontend.web.Areas.Administration.Models
{
    public class VoluntaryInstitutionModel : CrudModel
    {
        [JsonProperty(PropertyName = "fullName")]
        [Required]

        public string FullName { get; set; }

        [JsonProperty(PropertyName = "levelId")]
        public string LevelId { get; set; }

        [NotMapped]
        public string Level { get; set; }

        [NotMapped]
        public string Region { get; set; }

        [NotMapped]
        public string Local { get; set; }
    }
}
