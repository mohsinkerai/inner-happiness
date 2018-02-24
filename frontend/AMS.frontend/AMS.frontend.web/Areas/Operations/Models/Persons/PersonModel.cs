using System;

namespace AMS.frontend.web.Areas.Operations.Models.Persons
{
    public class PersonModel
    {
        public string ImageUrl { get; set; }
        public string Id { get; set; }
        public string Cnic { get; set; }
        public string Salutation { get; set; }
        public string FirstName { get; set; }
        public string FathersName { get; set; }
        public string FamilyName { get; set; }
        public string JamatiTitle { get; set; }
        public bool Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string MaritalStatus { get; set; }
        public string ResidentalAddress { get; set; }
        public string City { get; set; }
        public string ResidenceTelephone { get; set; }
        public string MobilePhone { get; set; }
        public string EmailAddress { get; set; }
        public string AreaOfOrigin { get; set; }
        public string LocalCouncil { get; set; }
        public string Jamatkhana { get; set; }
        public bool PlantToRelocate { get; set; }
        public string RelocateLocation { get; set; }
        public DateTime RelocationDateTime { get; set; }
    }
}