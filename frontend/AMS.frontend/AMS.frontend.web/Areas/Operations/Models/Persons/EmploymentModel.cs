﻿using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace AMS.frontend.web.Areas.Operations.Models.Persons
{
    public class EmploymentModel
    {
        [JsonProperty(PropertyName = "employmentId")]
        public string EmploymentId { get; set; }

        [JsonProperty(PropertyName = "nameOfOrganization")]
        public string NameOfOrganization { get; set; }

        [JsonProperty(PropertyName = "designation")]
        public string Designation { get; set; }

        [JsonProperty(PropertyName = "location")]
        public string Location { get; set; }

        [JsonProperty(PropertyName = "employmentEmailAddress")]
        [EmailAddress] public string EmploymentEmailAddress { get; set; }

        [JsonProperty(PropertyName = "employmentTelephone")]
        public string EmploymentTelephone { get; set; }

        [JsonProperty(PropertyName = "typeOfBusiness")]
        public string TypeOfBusiness { get; set; }
        
        [JsonIgnore] public string TypeOfBusinessName { get; set; }

        [JsonProperty(PropertyName = "natureOfBusiness")]
        public string NatureOfBusiness { get; set; }

        [JsonIgnore] public string NatureOfBusinessName { get; set; }

        [JsonProperty(PropertyName = "natureOfBusinessOther")]
        public string NatureOfBusinessOther { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}")]
        [JsonProperty(PropertyName = "employmentStartDate")]
        public DateTime? EmploymentStartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}")]
        [JsonProperty(PropertyName = "employmentEndDate")]
        public DateTime? EmploymentEndDate { get; set; }
    }
}