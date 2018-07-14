using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace AMS.frontend.web.Areas.Operations.Models.Persons
{
    public class EducationModel
    {
        [JsonProperty(PropertyName = "educationId")]
        public string EducationId { get; set; }

        [JsonProperty(PropertyName = "institution")]
        public string Institution { get; set; }

        public string InstitutionName { get; set; }

        [JsonProperty(PropertyName = "countryOfStudy")]
        [Display(Name = "Country of Study")] public string CountryOfStudy { get; set; }

        public string CountryOfStudyName { get; set; }

        [JsonProperty(PropertyName = "fromYear")]
        [Display(Name = "From Year")] public int? FromYear { get; set; }

        [JsonProperty(PropertyName = "toYear")]
        [Display(Name = "To Year")] public int? ToYear { get; set; }

        [JsonProperty(PropertyName = "nameOfDegree")]
        [Display(Name = "Name of Degree")] public string NameOfDegree { get; set; }

        public string NameOfDegreeName { get; set; }

        [JsonProperty(PropertyName = "majorAreaOfStudy")]
        [Display(Name = "Major Area of Study")]
        public string MajorAreaOfStudy { get; set; }
    }
}