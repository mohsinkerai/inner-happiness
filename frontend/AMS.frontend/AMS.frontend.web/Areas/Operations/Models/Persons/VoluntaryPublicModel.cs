using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace AMS.frontend.web.Areas.Operations.Models.Persons
{
    public class VoluntaryPublicModel
    {
        #region Public Properties

        [JsonProperty(PropertyName = "fromYear")]
        [Display(Name = "From Year")]
        public int? FromYear { get; set; }

        [JsonProperty(PropertyName = "institution")]
        public string Institution { get; set; }

        [JsonProperty(PropertyName = "position")]
        public string Position { get; set; }

        [JsonProperty(PropertyName = "priority")]
        public int Priority { get; set; }

        [JsonProperty(PropertyName = "toYear")]
        [Display(Name = "To Year")]
        public int? ToYear { get; set; }

        [JsonProperty(PropertyName = "voluntaryPublicId")]
        public string VoluntaryPublicId { get; set; }

        #endregion Public Properties
    }
}