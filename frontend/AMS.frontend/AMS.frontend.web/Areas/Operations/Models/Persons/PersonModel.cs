using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AMS.frontend.web.Areas.Operations.Models.Persons
{
    public class PersonModel
    {
        public string Id { get; set; }

        [Display(Name = "CNIC")] [Required] public string Cnic { get; set; }

        [Required] public string Salutation { get; set; }

        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Father Name")]
        [Required]
        public string FathersName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        public string FamilyName { get; set; }

        [Display(Name = "Jamati Title")]
        [Required]
        public string JamatiTitle { get; set; }

        [Required] public bool Gender { get; set; }

        [Display(Name = "Date of Birth")]
        [Required]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Marital Status")]
        [Required]
        public string MaritalStatus { get; set; }

        [Display(Name = "Residental Address")]
        [Required]
        public string ResidentalAddress { get; set; }

        [Display(Name = "City or Village")]
        [Required]
        public string City { get; set; }

        [Display(Name = "Residence Telephone")]
        public string ResidenceTelephone { get; set; }

        [Display(Name = "Mobile No")]
        [Required]
        public string MobilePhone { get; set; }

        [Display(Name = "Email Address")]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Display(Name = "Area of Origin")]
        [Required]
        public string AreaOfOrigin { get; set; }

        [Display(Name = "Local Council")]
        [Required]
        public string LocalCouncil { get; set; }

        [Display(Name = "Regional Council")]
        [Required]
        public string RegionalCouncil { get; set; }

        [Required] public string Jamatkhana { get; set; }

        [Display(Name = "Plan to Relocate")]
        [Required]
        public bool PlanToRelocate { get; set; }

        [Display(Name = "Relocate Location")] public string RelocateLocation { get; set; }

        [Display(Name = "Relocation Date Time")]
        public DateTime RelocationDateTime { get; set; }

        [Display(Name = "Passport Number (for foreign nationals)")]
        public string PassportNumber { get; set; }

        public string Image { get; set; }

        [Required]
        [Display(Name = "Highest Level of Study")]
        public string HighestLevelOfStudy { get; set; }

        [Display(Name = "Others")]
        public string HighestLevelOfStudyOther { get; set; }

        [Required] public string Institution { get; set; }

        [Required]
        [Display(Name = "Country of Study")]
        public string CountryOfStudy { get; set; }

        [Required]
        [Display(Name = "From Year")]
        public int FromYear { get; set; }

        [Required] [Display(Name = "To Year")] public int ToYear { get; set; }

        [Required]
        [Display(Name = "Name of Degree")]
        public string NameOfDegree { get; set; }

        [Required]
        [Display(Name = "Major Area of Study")]
        public string MajorAreaOfStudy { get; set; }
    }
}