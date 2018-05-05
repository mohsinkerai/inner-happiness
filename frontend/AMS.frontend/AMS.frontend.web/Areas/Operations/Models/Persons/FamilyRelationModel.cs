using Newtonsoft.Json;
using System;

namespace AMS.frontend.web.Areas.Operations.Models.Persons
{
    public class FamilyRelationModel
    {
        #region Public Properties

        public string Cnic { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string FamilyName { get; set; }
        public string FamilyRelationId { get; set; }
        public string FathersName { get; set; }
        public string FirstName { get; set; }
        public string JamatiTitle { get; set; }
        public string Relation { get; set; }
        [JsonIgnore] public string RelationName { get; set; }
        public string Salutation { get; set; }

        #endregion Public Properties
    }
}