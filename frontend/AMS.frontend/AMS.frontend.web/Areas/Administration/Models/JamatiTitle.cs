using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AMS.frontend.web.Areas.Administration.Models
{
    public class JamatiTitle : CrudModel
    {
        [JsonProperty(PropertyName = "gender")]
        [Required]
        public string Gender { get; set; }
    }
}
