using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace AMS.frontend.web.Areas.Operations.Models.Persons
{
    public class PersonModel
    {
        #region Public Properties

        [JsonIgnore]
        [Display(Name = "Training")]
        public string AkdnTraining { get; set; }

        [JsonIgnore]
        [Display(Name = "Country")]
        public string AkdnTrainingCountry { get; set; }

        [JsonIgnore] [Display(Name = "Month")] public string AkdnTrainingMonth { get; set; }
        public List<AkdnTrainingModel> AkdnTrainings { get; set; }
        [JsonIgnore] [Display(Name = "Year")] public string AkdnTrainingYear { get; set; }
        [Display(Name = "Area of Origin")] public string AreaOfOrigin { get; set; }
        [Display(Name = "City or Village")] public string City { get; set; }
        [Display(Name = "CNIC")] public string Cnic { get; set; }

        [JsonIgnore]
        [Display(Name = "Country of Study")]
        public string CountryOfStudy { get; set; }

        [Display(Name = "Date of Birth")] public DateTime DateOfBirth { get; set; }
        [JsonIgnore] public string Designation { get; set; }
        public List<EducationModel> Educations { get; set; }
        [Display(Name = "Email Address")] public string EmailAddress { get; set; }
        [JsonIgnore] public string EmploymentEmailAddress { get; set; }
        [JsonIgnore] public DateTime? EmploymentEndDate { get; set; }
        public List<EmploymentModel> Employments { get; set; }
        [JsonIgnore] public DateTime? EmploymentStartDate { get; set; }
        [JsonIgnore] public string EmploymentTelephone { get; set; }
        [Display(Name = "Last Name")] public string FamilyName { get; set; }
        public List<FamilyRelationModel> FamilyRelations { get; set; }
        [Display(Name = "Father Name")] public string FathersName { get; set; }
        [Display(Name = "Field of Expertise")] public string FieldOfExpertise { get; set; }

        [Display(Name = "Fields of Interest (in order of preference)")]
        public List<string> FieldOfInterest { get; set; }

        [Display(Name = "First Name")] public string FirstName { get; set; }

        [JsonIgnore]
        [Display(Name = "From Year")]
        public int? FromYear { get; set; }

        public Gender Gender { get; set; }

        [Display(Name = "Highest Level of Study")]
        public string HighestLevelOfStudy { get; set; }

        [Display(Name = "Others")] public string HighestLevelOfStudyOther { get; set; }
        [Display(Name = "Hours per Week")] public double? HoursPerWeek { get; set; }
        public string Id { get; set; }
        public string Image { get; set; }
        [JsonIgnore] public string Institution { get; set; }
        [Display(Name = "Jamati Title")] public string JamatiTitle { get; set; }
        public string Jamatkhana { get; set; }
        [JsonIgnore] public string Language { get; set; }
        public List<LanguageProficiencyModel> LanguageProficiencies { get; set; }
        [Display(Name = "Local Council")] public string LocalCouncil { get; set; }
        [JsonIgnore] public string Location { get; set; }

        [JsonIgnore]
        [Display(Name = "Major Area of Study")]
        public string MajorAreaOfStudy { get; set; }

        [Display(Name = "Marital Status")] public string MaritalStatus { get; set; }
        [Display(Name = "Mobile No")] public string MobilePhone { get; set; }

        [JsonIgnore]
        [Display(Name = "Name of Degree")]
        public string NameOfDegree { get; set; }

        [JsonIgnore] public string NameOfOrganization { get; set; }
        [JsonIgnore] public string NatureOfBusiness { get; set; }
        [JsonIgnore] public string NatureOfBusinessOther { get; set; }
        [Display(Name = "Occupation Type")] public string OccupationType { get; set; }
        [Display(Name = "Others")] public string OccupationTypeOther { get; set; }

        [Display(Name = "Passport Number (for foreign nationals)")]
        public string PassportNumber { get; set; }

        [Display(Name = "Plan to Relocate")] public bool PlanToRelocate { get; set; }

        [JsonIgnore]
        [Display(Name = "Training")]
        public string ProfesisonalTraining { get; set; }

        public List<string> ProfessionalMemberships { get; set; }

        [JsonIgnore]
        [Display(Name = "Country")]
        public string ProfessionalTrainingCountry { get; set; }

        [JsonIgnore]
        [Display(Name = "Institution")]
        public string ProfessionalTrainingInstitution { get; set; }

        [JsonIgnore] [Display(Name = "Month")] public string ProfessionalTrainingMonth { get; set; }
        public List<ProfessionalTrainingModel> ProfessionalTrainings { get; set; }
        [JsonIgnore] [Display(Name = "Year")] public string ProfessionalTrainingYear { get; set; }
        [JsonIgnore] public string Read { get; set; }
        [Display(Name = "Regional Council")] public string RegionalCouncil { get; set; }
        [Display(Name = "Cnic")] [JsonIgnore] public string RelativeCnic { get; set; }
        [Display(Name = "Date of Birth")] public DateTime RelativeDateOfBirth { get; set; }
        [Display(Name = "Last Name")] public string RelativeFamilyName { get; set; }
        [Display(Name = "Father Name")] public string RelativeFathersName { get; set; }
        [Display(Name = "First Name")] public string RelativeFirstName { get; set; }
        [Display(Name = "Jamati Title")] public string RelativeJamatiTitle { get; set; }

        [Display(Name = "Relation")]
        [JsonIgnore]
        public string RelativeRelation { get; set; }

        [Display(Name = "Salutation")]
        [JsonIgnore]
        public string RelativeSalutation { get; set; }

        [Display(Name = "Religious Education")]
        public string ReligiousEducation { get; set; }

        [Display(Name = "Relocate Location")] public string RelocateLocation { get; set; }

        [Display(Name = "Relocation Date Time")]
        public DateTime? RelocationDateTime { get; set; }

        [Display(Name = "Residence Telephone")]
        public string ResidenceTelephone { get; set; }

        [Display(Name = "Residental Address")] public string ResidentalAddress { get; set; }
        public string Salutation { get; set; }
        public List<string> Skills { get; set; }

        [JsonIgnore] public string Speak { get; set; }

        [JsonIgnore]
        [Display(Name = "To Year")]
        public int? ToYear { get; set; }

        [JsonIgnore] public string TypeOfBusiness { get; set; }

        [JsonIgnore]
        [Display(Name = "From Year")]
        public int? VoluntaryCommunityFromYear { get; set; }

        [JsonIgnore]
        [Display(Name = "Institution")]
        public string VoluntaryCommunityInstitution { get; set; }

        [JsonIgnore]
        [Display(Name = "Position")]
        public string VoluntaryCommunityPosition { get; set; }

        public List<VoluntaryCommunityModel> VoluntaryCommunityServices { get; set; }

        [JsonIgnore]
        [Display(Name = "To Year")]
        public int? VoluntaryCommunityToYear { get; set; }

        [JsonIgnore]
        [Display(Name = "From Year")]
        public int? VoluntaryPublicFromYear { get; set; }

        [JsonIgnore]
        [Display(Name = "Institution")]
        public string VoluntaryPublicInstitution { get; set; }

        [JsonIgnore]
        [Display(Name = "Position")]
        public string VoluntaryPublicPosition { get; set; }

        public List<VoluntaryPublicModel> VoluntaryPublicServices { get; set; }

        [JsonIgnore]
        [Display(Name = "To Year")]
        public int? VoluntaryPublicToYear { get; set; }

        [Display(Name = "Willingness to Devote Time in Future")]
        public string WillingnessToDevoteTimeInFuture { get; set; }

        [JsonIgnore] public string Write { get; set; }

        #endregion Public Properties
    }
}