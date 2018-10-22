using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace AMS.frontend.web.Areas.Operations.Models.Persons
{
    public class VoluntaryCommunityModel
    {
        #region Public Properties

        [JsonProperty(PropertyName = "fromYear")]
        [Display(Name = "From Year")]
        public int? FromYear { get; set; }

        [JsonProperty(PropertyName = "institution")]
        public string Institution { get; set; }

        public string InstitutionName { get; set; }

        [JsonProperty(PropertyName = "position")]
        public string Position { get; set; }

        [JsonProperty(PropertyName = "positionName")]
        public string PositionName { get; set; }

        [JsonProperty(PropertyName = "toYear")]
        [Display(Name = "To Year")]
        public int? ToYear { get; set; }

        [JsonProperty(PropertyName = "voluntaryCommunityId")]
        public string VoluntaryCommunityId { get; set; }

        [JsonProperty(PropertyName = "priority")]
        public int Priority { get; set; }

        [JsonProperty(PropertyName = "cycleId")]
        public string Cycle { get; set; }

        [JsonProperty(PropertyName = "cycleName")]
        public string CycleName { get; set; }

        [JsonProperty(PropertyName = "isImamatAppointee")]
        public bool IsImamatAppointee { get; set; }

        #endregion Public Properties
    }
}