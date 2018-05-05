using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace AMS.frontend.web.Areas.Operations.Models.Persons
{
    public class VoluntaryPublicModel
    {
        [JsonProperty(PropertyName = "voluntaryPublicId")]
        public string VoluntaryPublicId { get; set; }

        [JsonProperty(PropertyName = "institution")]
        public string Institution { get; set; }

        [JsonProperty(PropertyName = "fromYear")]
        [Display(Name = "From Year")] public int? FromYear { get; set; }

        [JsonProperty(PropertyName = "toYear")]
        [Display(Name = "To Year")] public int? ToYear { get; set; }

        [JsonProperty(PropertyName = "position")]
        public string Position { get; set; }
    }
}