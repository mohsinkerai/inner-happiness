using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AMS.frontend.web.Areas.Operations.Models.Persons
{
    public class PersonModel
    {
        public string Id { get; set; }

        [Display(Name = "CNIC")] public string Cnic { get; set; }

        public string Salutation { get; set; }

        [Display(Name = "First Name")] public string FirstName { get; set; }

        [Display(Name = "Father Name")] public string FathersName { get; set; }

        [Display(Name = "Last Name")] public string FamilyName { get; set; }

        [Display(Name = "Jamati Title")] public string JamatiTitle { get; set; }

        public bool Gender { get; set; }

        [Display(Name = "Date of Birth")] public DateTime DateOfBirth { get; set; }

        [Display(Name = "Marital Status")] public string MaritalStatus { get; set; }

        [Display(Name = "Residental Address")] public string ResidentalAddress { get; set; }

        [Display(Name = "City or Village")] public string City { get; set; }

        [Display(Name = "Residence Telephone")]
        public string ResidenceTelephone { get; set; }

        [Display(Name = "Mobile No")] public string MobilePhone { get; set; }

        [Display(Name = "Email Address")]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Display(Name = "Area of Origin")] public string AreaOfOrigin { get; set; }

        [Display(Name = "Local Council")] public string LocalCouncil { get; set; }

        [Display(Name = "Regional Council")] public string RegionalCouncil { get; set; }

        public string Jamatkhana { get; set; }

        [Display(Name = "Plan to Relocate")] public bool PlanToRelocate { get; set; }

        [Display(Name = "Relocate Location")] public string RelocateLocation { get; set; }

        [Display(Name = "Relocation Date Time")]
        public DateTime RelocationDateTime { get; set; }

        [Display(Name = "Passport Number (for foreign nationals)")]
        public string PassportNumber { get; set; }

        public string Image { get; set; }

        [Display(Name = "Highest Level of Study")]
        public string HighestLevelOfStudy { get; set; }

        [Display(Name = "Others")] public string HighestLevelOfStudyOther { get; set; }

        public string Institution { get; set; }

        [Display(Name = "Country of Study")] public string CountryOfStudy { get; set; }

        [Display(Name = "From Year")] public int FromYear { get; set; }

        [Display(Name = "To Year")] public int ToYear { get; set; }

        [Display(Name = "Name of Degree")] public string NameOfDegree { get; set; }

        [Display(Name = "Major Area of Study")]
        public string MajorAreaOfStudy { get; set; }

        public List<EducationModel> Educations { get; set; }

        [Display(Name = "Field of Expertise")] public string FieldOfExpertise { get; set; }

        [Display(Name = "Religious Education")]
        public string ReligiousEducation { get; set; }

        [Display(Name = "Training")] public string AkdnTraining { get; set; }

        [Display(Name = "Country")] public string AkdnTrainingCountry { get; set; }

        [Display(Name = "Month")] public string AkdnTrainingMonth { get; set; }

        [Display(Name = "Year")] public string AkdnTrainingYear { get; set; }

        public List<AkdnTrainingModel> AkdnTrainings { get; set; }

        [Display(Name = "Training")] public string ProfesisonalTraining { get; set; }

        [Display(Name = "Institution")] public string ProfessionalTrainingInstitution { get; set; }

        [Display(Name = "Country")] public string ProfessionalTrainingCountry { get; set; }

        [Display(Name = "Month")] public string ProfessionalTrainingMonth { get; set; }

        [Display(Name = "Year")] public string ProfessionalTrainingYear { get; set; }

        public List<ProfessionalTrainingModel> ProfessionalTrainings { get; set; }

        public List<string> Skills { get; set; }
        public List<string> ProfessionalMemberships { get; set; }

        public string Language { get; set; }
        public string Read { get; set; }
        public string Write { get; set; }
        public string Speak { get; set; }

        public List<LanguageProficiencyModel> LanguageProficiencies { get; set; }
    }
}