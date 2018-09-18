using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace AMS.frontend.web.Areas.Operations.Models.Persons
{
    public class AkdnTrainingModel
    {
        #region Public Properties

        [JsonProperty(PropertyName = "countryOfTraining")]
        [Display(Name = "Country of Training")]
        public string CountryOfTraining { get; set; }

        public string CountryOfTrainingName { get; set; }

        [JsonProperty(PropertyName = "month")] public string Month { get; set; }

        public string MonthName { get; set; }

        [JsonProperty(PropertyName = "training")]
        public string Training { get; set; }

        [JsonProperty(PropertyName = "trainingId")]
        public string TrainingId { get; set; }

        [JsonProperty(PropertyName = "trainingName")]
        public string TrainingName { get; set; }

        [JsonProperty(PropertyName = "year")]
        [DataType(DataType.Date)]
        [Display(Name = "Year")]
        public int? Year { get; set; }

        [JsonProperty(PropertyName = "priority")]
        public int Priority { get; set; }

        #endregion Public Properties
    }
}