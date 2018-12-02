using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Newtonsoft.Json;

namespace AMS.frontend.web.Areas.Operations.Models.Persons
{
    public class EmploymentModel
    {
        #region Public Properties

        [JsonProperty(PropertyName = "employmentCategory")]
        public string Category { get; set; }

        [JsonProperty(PropertyName = "designation")]
        public string Designation { get; set; }

        [JsonProperty(PropertyName = "employmentEmailAddress")]
        [EmailAddress]
        public string EmploymentEmailAddress { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}")]
        [JsonProperty(PropertyName = "employmentEndDate")]
        public DateTime? EmploymentEndDate { get; set; }

        [JsonProperty(PropertyName = "employmentId")]
        public string EmploymentId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}")]
        [JsonProperty(PropertyName = "employmentStartDate")]
        public DateTime? EmploymentStartDate { get; set; }

        [JsonProperty(PropertyName = "employmentTelephone")]
        public string EmploymentTelephone { get; set; }

        [JsonProperty(PropertyName = "location")]
        public string Location { get; set; }

        [JsonProperty(PropertyName = "nameOfOrganization")]
        public string NameOfOrganization { get; set; }

        [JsonProperty(PropertyName = "businessNature")]
        public string NatureOfBusiness { get; set; }

        public string NatureOfBusinessName { get; set; }

        [JsonProperty(PropertyName = "natureOfBusinessOther")]
        public string NatureOfBusinessOther { get; set; }

        [JsonProperty(PropertyName = "priority")]
        public int Priority { get; set; }

        [JsonProperty(PropertyName = "businessType")]
        public string TypeOfBusiness { get; set; }

        public string TypeOfBusinessName { get; set; }

        [JsonIgnore]
        public string StartDateForDisplay
        {
            get
            {
                string format = "MM-yyyy";
                string newDateFormat = "";
                
                var date = EmploymentStartDate.Equals(DateTime.MinValue) ? null : EmploymentStartDate.ToString();
                if (date != null && date != "") {
                    DateTime dateTime = (DateTime) EmploymentStartDate;
                    newDateFormat = dateTime.ToString(format);   
                }

                return newDateFormat;
            }
        }

        [JsonIgnore]
        public string EndDateForDisplay
        {
            get
            {
                string format = "MM-yyyy";
                string newDateFormat = null;

                var date = EmploymentEndDate.Equals(DateTime.MinValue) ? null : EmploymentEndDate.ToString();
                if (date != null && date != "")
                {
                    DateTime dateTime = (DateTime)EmploymentEndDate;
                    newDateFormat = dateTime.ToString(format);
                }
                return newDateFormat;
            }
        }

        #endregion Public Properties
    }
}