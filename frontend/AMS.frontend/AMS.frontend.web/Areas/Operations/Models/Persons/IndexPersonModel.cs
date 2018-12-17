using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AMS.frontend.web.Areas.Operations.Models.Persons
{
    public class IndexPersonModel
    {
        #region Public Properties

        public string AcademicInstitution { get; set; }
        public string Cnic { get; set; }
        public string Degree { get; set; }
        public string FormNumber { get; set; }
        public string JamatiTitle { get; set; }
        public string MajorAreaOfStudy { get; set; }
        public string Name { get; set; }
        public List<PersonModel> Persons { get; set; }

        [Display(Name = "Date of Birth")]
        public string DOB { get; set; }

        #endregion Public Properties
    }
}