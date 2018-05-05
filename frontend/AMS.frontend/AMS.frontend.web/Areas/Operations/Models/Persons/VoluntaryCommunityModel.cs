using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace AMS.frontend.web.Areas.Operations.Models.Persons
{
    public class VoluntaryCommunityModel
    {
        [JsonProperty(PropertyName = "voluntaryCommunityId")]
        public string VoluntaryCommunityId { get; set; }

        [JsonProperty(PropertyName = "institution")]
        public string Institution { get; set; }
        
        [JsonIgnore] public string InstitutionName { get; set; }

        [JsonProperty(PropertyName = "fromYear")]
        [Display(Name = "From Year")] public int? FromYear { get; set; }

        [JsonProperty(PropertyName = "toYear")]
        [Display(Name = "To Year")] public int? ToYear { get; set; }

        [JsonProperty(PropertyName = "position")]
        public string Position { get; set; }

        [JsonProperty(PropertyName = "positionName")]
        [JsonIgnore] public string PositionName { get; set; }
    }
}