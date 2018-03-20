using System;
using System.ComponentModel.DataAnnotations;

namespace AMS.frontend.web.Areas.Operations.Models.Persons
{
    public class EmploymentModel
    {
        public string EmploymentId { get; set; }

        public string NameOfOrganization { get; set; }
        public string Designation { get; set; }
        public string Location { get; set; }

        [EmailAddress] public string EmploymentEmailAddress { get; set; }

        public string EmploymentTelephone { get; set; }

        public string TypeOfBusiness { get; set; }

        public string NatureOfBusiness { get; set; }
        public string NatureOfBusinessOther { get; set; }
        public DateTime EmploymentStartDate { get; set; }
        public DateTime EmploymentEndDate { get; set; }
    }
}