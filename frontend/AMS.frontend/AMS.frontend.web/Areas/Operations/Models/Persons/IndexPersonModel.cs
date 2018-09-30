using System.Collections.Generic;

namespace AMS.frontend.web.Areas.Operations.Models.Persons
{
    public class IndexPersonModel
    {
        #region Public Properties

        public string AcademicInstitution { get; set; }
        public string Cnic { get; set; }
        public string Degree { get; set; }
        public string FirstName { get; set; }

        public string FormNumber { get; set; }
        public string JamatiTitle { get; set; }
        public string LastName { get; set; }

        public string MajorAreaOfStudy { get; set; }
        public List<PersonModel> Persons { get; set; }

        #endregion Public Properties
    }
}