using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace AMS.frontend.web.Areas.Operations.Models.Persons
{
    public class VoluntaryCommunityModel
    {
        public string VoluntaryCommunityId { get; set; }

        public string Institution { get; set; }

        [JsonIgnore] public string InstitutionName { get; set; }

        [Display(Name = "From Year")] public int? FromYear { get; set; }

        [Display(Name = "To Year")] public int? ToYear { get; set; }
        public string Position { get; set; }

        [JsonIgnore] public string PositionName { get; set; }
    }
}