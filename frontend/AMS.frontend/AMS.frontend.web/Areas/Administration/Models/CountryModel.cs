using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AMS.frontend.web.Areas.Administration.Models.Country
{
    public class CountryModel : CrudModel
    {
        [JsonProperty(PropertyName = "code")]
        [Required]
        public string Code { get; set; }
    }
}
