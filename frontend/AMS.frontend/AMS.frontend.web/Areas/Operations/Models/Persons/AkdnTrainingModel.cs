using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace AMS.frontend.web.Areas.Operations.Models.Persons
{
    public class AkdnTrainingModel
    {
        [JsonProperty(PropertyName = "trainingId")]
        public string TrainingId { get; set; }

        [JsonProperty(PropertyName = "training")]
        public string Training { get; set; }

        [JsonProperty(PropertyName = "trainingName")]
        [JsonIgnore] public string TrainingName { get; set; }

        [JsonProperty(PropertyName = "countryOfTraining")]
        [Display(Name = "Country of Training")]
        public string CountryOfTraining { get; set; }

        [JsonIgnore] public string CountryOfTrainingName { get; set; }

        [JsonProperty(PropertyName = "year")]
        [DataType(DataType.Date)]
        [Display(Name = "Year")]
        public int? Year { get; set; }

        [JsonProperty(PropertyName = "month")]
        public string Month { get; set; }

        [JsonIgnore]
        public string MonthName { get; set; }
    }
}