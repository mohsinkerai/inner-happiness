﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace AMS.frontend.web.Areas.Operations.Models.Persons
{
    public class FamilyRelationModel
    {
        #region Public Properties

        [JsonProperty(PropertyName = "cnic")] public string Cnic { get; set; }

        public string Cycle { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}")]
        [JsonProperty(PropertyName = "dateOfBirth")]
        public DateTime DateOfBirth { get; set; }

        [JsonIgnore]
        public string DateOfBirthForDisplay => DateOfBirth.ToString("dd/MM/yyyy");

        [JsonProperty(PropertyName = "familyName")]
        public string FamilyName { get; set; }

        [JsonProperty(PropertyName = "familyRelationId")]
        public string FamilyRelationId { get; set; }

        [JsonProperty(PropertyName = "fathersName")]
        public string FathersName { get; set; }

        [JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "id")] public string Id { get; set; }

        public string Institution { get; set; }

        [JsonProperty(PropertyName = "jamatiTitle")]
        public string JamatiTitle { get; set; }

        public string Position { get; set; }

        [JsonProperty(PropertyName = "relation")]
        public string Relation { get; set; }

        public string RelationName { get; set; }

        [JsonProperty(PropertyName = "salutation")]
        public string Salutation { get; set; }

        [JsonProperty(PropertyName = "voluntaryCommunityServices")]
        public List<VoluntaryCommunityModel> VoluntaryCommunityServices { get; set; }

        #endregion Public Properties
    }
}