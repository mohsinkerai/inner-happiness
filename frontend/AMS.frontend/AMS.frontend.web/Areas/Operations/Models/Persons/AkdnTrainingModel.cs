using System.ComponentModel.DataAnnotations;

namespace AMS.frontend.web.Areas.Operations.Models.Persons
{
    public class AkdnTrainingModel
    {
        public string TrainingId { get; set; }

        public string Training { get; set; }
        public string TrainingName { get; set; }

        [Display(Name = "Country of Training")]
        public string CountryOfTraining { get; set; }

        public string CountryOfTrainingName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Year")]
        public int? Year { get; set; }

        public string Month { get; set; }
    }
}