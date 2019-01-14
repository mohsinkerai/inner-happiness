using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AMS.frontend.web.Areas.Administration.Models
{
    public class CityModel : CrudModel
    {
        [Display(Name = "Country")]
        [JsonProperty(PropertyName = "countryId")]
        public string CountryId { get; set; }
    }
}
