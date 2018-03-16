using System.ComponentModel.DataAnnotations;

namespace AMS.frontend.web.Areas.Operations.Models.Persons
{
    public class ProfessionalTrainingModel
    {
        public string TrainingId { get; set; }

        public string Training { get; set; }

        [Display(Name = "Country of Training")]
        public string CountryOfTraining { get; set; }

        public string Institution { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Year")]
        public int Year { get; set; }

        public string Month { get; set; }
    }
}