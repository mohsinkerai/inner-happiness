using System.ComponentModel.DataAnnotations;

namespace AMS.frontend.web.Areas.Operations.Models.Persons
{
    public class EducationModel
    {
        public string EducationId { get; set; }

        public string Institution { get; set; }

        [Display(Name = "Country of Study")] public string CountryOfStudy { get; set; }

        [Display(Name = "From Year")] public int FromYear { get; set; }

        [Display(Name = "To Year")] public int ToYear { get; set; }

        [Display(Name = "Name of Degree")] public string NameOfDegree { get; set; }

        [Display(Name = "Major Area of Study")]
        public string MajorAreaOfStudy { get; set; }
    }
}