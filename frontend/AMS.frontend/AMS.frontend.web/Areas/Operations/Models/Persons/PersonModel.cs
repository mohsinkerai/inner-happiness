using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace AMS.frontend.web.Areas.Operations.Models.Persons
{
    public class PersonModel
    {
        public string Id { get; set; }

        [Required]
        [JsonProperty(PropertyName = "cnic")]
        [Display(Name = "CNIC")] public string Cnic { get; set; }

        [JsonProperty(PropertyName = "salutation")]
        public string Salutation { get; set; }

        [Required]
        [JsonProperty(PropertyName = "firstName")]
        [Display(Name = "First Name")] public string FirstName { get; set; }

        [JsonProperty(PropertyName = "fathersName")]
        [Display(Name = "Father Name")] public string FathersName { get; set; }

        [JsonProperty(PropertyName = "familyName")]
        [Display(Name = "Last Name")] public string FamilyName { get; set; }

        [JsonProperty(PropertyName = "jamatiTitle")]
        [Display(Name = "Jamati Title")] public string JamatiTitle { get; set; }

        [Required]
        [JsonProperty(PropertyName = "gender")]
        public Gender Gender { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}")]
        [JsonProperty(PropertyName = "dateOfBirth")]
        [Display(Name = "Date of Birth")] public DateTime DateOfBirth { get; set; }

        [JsonProperty(PropertyName = "maritalStatus")]
        [Display(Name = "Marital Status")] public string MaritalStatus { get; set; }

        [JsonProperty(PropertyName = "residentialAddress")]
        [Display(Name = "Residental Address")] public string ResidentalAddress { get; set; }

        [JsonProperty(PropertyName = "city")]
        [Display(Name = "City or Village")] public string City { get; set; }

        [JsonProperty(PropertyName = "residenceTelephone")]
        [Display(Name = "Residence Telephone")]
        public string ResidenceTelephone { get; set; }

        [JsonProperty(PropertyName = "mobilePhone")]
        [Display(Name = "Mobile No")] public string MobilePhone { get; set; }

        [JsonProperty(PropertyName = "emailAddress")]
        [Display(Name = "Email Address")] public string EmailAddress { get; set; }

        [JsonProperty(PropertyName = "areaOfOrigin")]
        [Display(Name = "Area of Origin")] public string AreaOfOrigin { get; set; }

        [Required]
        [JsonProperty(PropertyName = "localCouncil")]
        [Display(Name = "Local Council")] public string LocalCouncil { get; set; }

        [Required]
        [JsonProperty(PropertyName = "regionalCouncil")]
        [Display(Name = "Regional Council")] public string RegionalCouncil { get; set; }

        [Required]
        [JsonProperty(PropertyName = "jamatkhana")]
        public string Jamatkhana { get; set; }

        [JsonProperty(PropertyName = "planToRelocate")]
        [Display(Name = "Plan to Relocate")] public bool PlanToRelocate { get; set; }

        [JsonProperty(PropertyName = "relocateLocation")]
        [Display(Name = "Relocate Location")] public string RelocateLocation { get; set; }

        [JsonProperty(PropertyName = "relocationDateTime")]
        [Display(Name = "Relocation Date Time")]
        public DateTime? RelocationDateTime { get; set; }

        [JsonProperty(PropertyName = "passportNumber")]
        [Display(Name = "Passport Number (for foreign nationals)")]
        public string PassportNumber { get; set; }

        [JsonProperty(PropertyName = "image")]
        public string Image { get; set; }

        [JsonProperty(PropertyName = "highestLevelOfStudy")]
        [Display(Name = "Highest Level of Study")]
        public string HighestLevelOfStudy { get; set; }

        [JsonProperty(PropertyName = "highestLevelOfStudyOther")]
        [Display(Name = "Others")] public string HighestLevelOfStudyOther { get; set; }
        
        [JsonIgnore] public string Institution { get; set; }

        [JsonIgnore]
        [Display(Name = "Country of Study")]
        public string CountryOfStudy { get; set; }

        [JsonIgnore]
        [Display(Name = "From Year")]
        public int? FromYear { get; set; }

        [JsonIgnore]
        [Display(Name = "To Year")]
        public int? ToYear { get; set; }

        [JsonIgnore]
        [Display(Name = "Name of Degree")]
        public string NameOfDegree { get; set; }

        [JsonIgnore]
        [Display(Name = "Major Area of Study")]
        public string MajorAreaOfStudy { get; set; }

        [JsonProperty(PropertyName = "educations")]
        public List<EducationModel> Educations { get; set; }

        [JsonProperty(PropertyName = "fieldOfExpertise")]
        [Display(Name = "Field of Expertise")] public string FieldOfExpertise { get; set; }

        [JsonProperty(PropertyName = "religiousEducation")]
        [Display(Name = "Religious Education")]
        public string ReligiousEducation { get; set; }

        [JsonIgnore]
        [Display(Name = "Training")]
        public string AkdnTraining { get; set; }

        [JsonIgnore]
        [Display(Name = "Country")]
        public string AkdnTrainingCountry { get; set; }

        [JsonIgnore] [Display(Name = "Month")] public string AkdnTrainingMonth { get; set; }

        [JsonIgnore] [Display(Name = "Year")] public string AkdnTrainingYear { get; set; }

        [JsonProperty(PropertyName = "akdnTrainings")]
        public List<AkdnTrainingModel> AkdnTrainings { get; set; }

        [JsonIgnore]
        [Display(Name = "Training")]
        public string ProfesisonalTraining { get; set; }

        [JsonIgnore]
        [Display(Name = "Institution")]
        public string ProfessionalTrainingInstitution { get; set; }

        [JsonIgnore]
        [Display(Name = "Country")]
        public string ProfessionalTrainingCountry { get; set; }

        [JsonIgnore] [Display(Name = "Month")] public string ProfessionalTrainingMonth { get; set; }

        [JsonIgnore] [Display(Name = "Year")] public string ProfessionalTrainingYear { get; set; }

        [JsonProperty(PropertyName = "professionalTrainings")]
        public List<ProfessionalTrainingModel> ProfessionalTrainings { get; set; }

        [JsonProperty(PropertyName = "skills")]
        public List<string> Skills { get; set; }

        [JsonProperty(PropertyName = "professionalMemberships")]
        public List<string> ProfessionalMemberships { get; set; }

        [JsonIgnore] public string Language { get; set; }

        [JsonIgnore] public string Read { get; set; }

        [JsonIgnore] public string Write { get; set; }

        [JsonIgnore] public string Speak { get; set; }

        [JsonProperty(PropertyName = "languageProficiencies")]
        public List<LanguageProficiencyModel> LanguageProficiencies { get; set; }

        [JsonIgnore]
        [Display(Name = "Institution")]
        public string VoluntaryCommunityInstitution { get; set; }

        [JsonIgnore]
        [Display(Name = "From Year")]
        public int? VoluntaryCommunityFromYear { get; set; }

        [JsonIgnore]
        [Display(Name = "To Year")]
        public int? VoluntaryCommunityToYear { get; set; }

        [JsonIgnore]
        [Display(Name = "Position")]
        public string VoluntaryCommunityPosition { get; set; }

        [JsonProperty(PropertyName = "voluntaryCommunityServices")]
        public List<VoluntaryCommunityModel> VoluntaryCommunityServices { get; set; }

        [JsonIgnore]
        [Display(Name = "Institution")]
        public string VoluntaryPublicInstitution { get; set; }

        [JsonIgnore]
        [Display(Name = "From Year")]
        public int? VoluntaryPublicFromYear { get; set; }

        [JsonIgnore]
        [Display(Name = "To Year")]
        public int? VoluntaryPublicToYear { get; set; }

        [JsonIgnore]
        [Display(Name = "Position")]
        public string VoluntaryPublicPosition { get; set; }

        [JsonProperty(PropertyName = "voluntaryPublicServices")]
        public List<VoluntaryPublicModel> VoluntaryPublicServices { get; set; }

        [JsonProperty(PropertyName = "willingnessToDevoteTimeInFuture")]
        [Display(Name = "Willingness to Devote Time in Future")]
        public string WillingnessToDevoteTimeInFuture { get; set; }

        [JsonProperty(PropertyName = "fieldOfInterest")]
        [Display(Name = "Fields of Interest (in order of preference)")]
        public List<string> FieldOfInterest { get; set; }

        [JsonProperty(PropertyName = "hoursPerWeek")]
        [Display(Name = "Hours per Week")] public double? HoursPerWeek { get; set; }

        [JsonProperty(PropertyName = "occupationType")]
        [Display(Name = "Occupation Type")] public string OccupationType { get; set; }

        [JsonProperty(PropertyName = "occupationTypeOther")]
        [Display(Name = "Others")] public string OccupationTypeOther { get; set; }

        [JsonIgnore] public string NameOfOrganization { get; set; }

        [JsonIgnore] public string Designation { get; set; }

        [JsonIgnore] public string Location { get; set; }

        [JsonIgnore] public string EmploymentEmailAddress { get; set; }

        [JsonIgnore] public string EmploymentTelephone { get; set; }

        [JsonIgnore] public string TypeOfBusiness { get; set; }

        [JsonIgnore] public string NatureOfBusiness { get; set; }

        [JsonIgnore] public string NatureOfBusinessOther { get; set; }

        [JsonIgnore] public DateTime? EmploymentStartDate { get; set; }

        [JsonIgnore] public DateTime? EmploymentEndDate { get; set; }

        [JsonProperty(PropertyName = "employments")]
        public List<EmploymentModel> Employments { get; set; }

        [JsonProperty(PropertyName = "relativeCnic")]
        [Display(Name = "Cnic")] [JsonIgnore] public string RelativeCnic { get; set; }

        [JsonProperty(PropertyName = "relativeSalutation")]
        [Display(Name = "Salutation")]
        [JsonIgnore]
        public string RelativeSalutation { get; set; }
    }
}