using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AMS.frontend.web.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AMS.frontend.web.Areas.Operations.Models.Persons
{
    public class PersonModel
    {
        #region Public Methods

        public static implicit operator PersonModel(JToken v)
        {
            throw new NotImplementedException();
        }

        #endregion Public Methods

        #region Public Properties

        public int Age
        {
            get
            {
                if (DateOfBirth != null) return DateOfBirth.Value.GetAge();

                return 0;
            }
        }

        [JsonIgnore]
        [Display(Name = "Training")]
        public string AkdnTraining { get; set; }

        [JsonIgnore]
        [Display(Name = "Country")]
        public string AkdnTrainingCountry { get; set; }

        [JsonIgnore]
        [Display(Name = "Month and Year")]
        public DateTime? AkdnTrainingDate { get; set; }

        [JsonIgnore] [Display(Name = "Month")] public string AkdnTrainingMonth { get; set; }

        [JsonProperty(PropertyName = "akdnTrainings")]
        public List<AkdnTrainingModel> AkdnTrainings { get; set; }

        [JsonIgnore] [Display(Name = "Year")] public string AkdnTrainingYear { get; set; }

        [JsonProperty(PropertyName = "areaOfOrigin")]
        [Display(Name = "Area of Origin")]
        public string AreaOfOrigin { get; set; }

        public string AreaOfOriginForDisplay { get; set; }

        public string AreaOfOriginForUi
        {
            get
            {
                if (string.IsNullOrWhiteSpace(AreaOfOriginForDisplay))
                    return "No origin specified";
                return AreaOfOriginForDisplay;
            }
        }

        [JsonProperty(PropertyName = "city")]
        [Display(Name = "City or Village")]
        public string City { get; set; }

        //[Required]
        [JsonProperty(PropertyName = "cnic")]
        [Display(Name = "CNIC")]
        [Remote("ValidateCnic", "Persons", "Operations")]
        [RegularExpression("^[0-9+]{5}-[0-9+]{7}-[0-9]{1}$")]
        public string Cnic { get; set; }

        [JsonIgnore]
        [Display(Name = "Country of Study")]
        public string CountryOfStudy { get; set; }

        [Required]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{yyyy-MM-dd}")]
        [JsonProperty(PropertyName = "dateOfBirth")]
        [Display(Name = "Date of Birth")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? DateOfBirth { get; set; }

        [JsonIgnore] public string Designation { get; set; }

        [JsonProperty(PropertyName = "educations")]
        public List<EducationModel> Educations { get; set; }

        [JsonProperty(PropertyName = "emailAddress")]
        [Display(Name = "Email Address")]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [JsonIgnore] public string EmploymentCategory { get; set; }
        [JsonIgnore] public string EmploymentEmailAddress { get; set; }
        [JsonIgnore] public DateTime? EmploymentEndDate { get; set; }

        [JsonProperty(PropertyName = "employments")]
        public List<EmploymentModel> Employments { get; set; }

        [JsonIgnore] public DateTime? EmploymentStartDate { get; set; }

        [JsonIgnore] public string EmploymentTelephone { get; set; }

        [JsonProperty(PropertyName = "familyName")]
        [Display(Name = "Last Name")]
        public string FamilyName { get; set; }

        [JsonProperty(PropertyName = "familyRelations")]
        public List<FamilyRelationModel> FamilyRelations { get; set; }

        [JsonProperty(PropertyName = "fathersName")]
        [Display(Name = "Father/Husband Name")]
        public string FathersName { get; set; }

        [JsonProperty(PropertyName = "fieldOfExpertise")]
        [Display(Name = "Field of Expertise")]
        public List<string> FieldOfExpertise { get; set; }

        [JsonProperty(PropertyName = "fieldOfInterest")]
        [Display(Name = "Fields of Interest (in order of preference)")]
        public List<string> FieldOfInterest { get; set; }

        [Required]
        [JsonProperty(PropertyName = "firstName")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        //[Required]
        [JsonProperty(PropertyName = "formnumber")]
        [Display(Name = "From Number")]
        [Remote("ValidateFormNumber", "Persons", "Operations")]
        public string FormNumber { get; set; }

        [JsonIgnore]
        [Display(Name = "From Year")]
        public int? FromYear { get; set; }

        [JsonIgnore]
        public string FullName
        {
            get
            {
                var salutation = !string.IsNullOrWhiteSpace(JamatiTitleForDisplay)
                    ? JamatiTitleForDisplay
                    : SalutationForDisplay;
                return $"{salutation} {FirstName} {FathersName} {FamilyName}";
            }
        }

        [Required]
        [JsonProperty(PropertyName = "gender")]
        public Gender? Gender { get; set; }

        [JsonProperty(PropertyName = "highestLevelOfStudy")]
        [Display(Name = "Highest Level of Study")]
        public string HighestLevelOfStudy { get; set; }

        [JsonProperty(PropertyName = "highestLevelOfStudyOther")]
        [Display(Name = "Others")]
        public string HighestLevelOfStudyOther { get; set; }

        [JsonProperty(PropertyName = "hoursPerWeek")]
        [Display(Name = "Hours per Week")]
        public double? HoursPerWeek { get; set; }

        //[Remote("ValidateId", "Persons", "Operations")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "image")] public string Image { get; set; }

        [JsonIgnore] public IFormFile ImageUpload { get; set; }

        [JsonIgnore] public string Institution { get; set; }

        [JsonProperty(PropertyName = "jamatiTitle")]
        [Display(Name = "Jamati Title")]
        public string JamatiTitle { get; set; }

        public string JamatiTitleForDisplay { get; set; }

        [Required]
        [JsonProperty(PropertyName = "jamatkhana")]
        public string Jamatkhana { get; set; }

        [JsonIgnore] public string Language { get; set; }

        [JsonProperty(PropertyName = "languageProficiencies")]
        public List<LanguageProficiencyModel> LanguageProficiencies { get; set; }

        public string LatestEducation
        {
            get
            {
                var education = Educations?.FirstOrDefault();
                if (education != null)
                    return $"{education?.NameOfDegreeName}, {education?.InstitutionName} {education?.YearForDisplay}";

                return string.Empty;
            }
        }

        public string LatestEmplopyment
        {
            get
            {
                var employment = Employments?.FirstOrDefault();
                if (employment != null) return $"{employment?.Designation}, {employment?.NameOfOrganization}";

                return string.Empty;
            }
        }

        [Required]
        [JsonProperty(PropertyName = "localCouncil")]
        [Display(Name = "Local Council")]
        public string LocalCouncil { get; set; }

        [JsonIgnore] public string Location { get; set; }

        [JsonIgnore]
        [Display(Name = "Major Area of Study")]
        public string MajorAreaOfStudy { get; set; }

        [JsonProperty(PropertyName = "maritalStatus")]
        [Display(Name = "Marital Status")]
        public string MaritalStatus { get; set; }

        public string MaritalStatusForDisplay { get; set; }

        [JsonProperty(PropertyName = "mobilePhone")]
        [Display(Name = "Mobile No")]
        public string MobilePhone { get; set; }

        [JsonIgnore]
        [Display(Name = "Name of Degree")]
        public string NameOfDegree { get; set; }

        [JsonIgnore] public string NameOfOrganization { get; set; }
        [JsonIgnore] public string NatureOfBusiness { get; set; }
        [JsonIgnore] public string NatureOfBusinessOther { get; set; }

        [JsonProperty(PropertyName = "occupationType")]
        [Display(Name = "Occupation Type")]
        public string OccupationType { get; set; }

        [JsonIgnore] public string OccupationTypeName { get; set; }

        [JsonProperty(PropertyName = "occupationOthers")]
        [Display(Name = "Others")]
        public string OccupationTypeOther { get; set; }

        [JsonProperty(PropertyName = "oldCnic")]
        [Display(Name = "Old CNIC")]
        public string OldCnic { get; set; }

        [JsonProperty(PropertyName = "passportNumber")]
        [Display(Name = "Passport Number")]
        public string PassportNumber { get; set; }

        [JsonProperty(PropertyName = "planToRelocate")]
        [Display(Name = "Plan to Relocate")]
        public bool PlanToRelocate { get; set; }

        [JsonIgnore]
        [Display(Name = "Training")]
        public string ProfesisonalTraining { get; set; }

        [JsonProperty(PropertyName = "professionalMemberships")]
        public List<string> ProfessionalMemberships { get; set; }

        [JsonIgnore]
        [Display(Name = "Country")]
        public string ProfessionalTrainingCountry { get; set; }

        [JsonIgnore]
        [Display(Name = "Month and Year")]
        public DateTime? ProfessionalTrainingDate { get; set; }

        [JsonIgnore]
        [Display(Name = "Institution")]
        public string ProfessionalTrainingInstitution { get; set; }

        [JsonIgnore] [Display(Name = "Month")] public string ProfessionalTrainingMonth { get; set; }

        [JsonProperty(PropertyName = "professionalTrainings")]
        public List<ProfessionalTrainingModel> ProfessionalTrainings { get; set; }

        [JsonIgnore] [Display(Name = "Year")] public string ProfessionalTrainingYear { get; set; }
        [JsonIgnore] public string Read { get; set; }

        [Required]
        [JsonProperty(PropertyName = "regionalCouncil")]
        [Display(Name = "Regional Council")]
        public string RegionalCouncil { get; set; }

        [Display(Name = "Cnic")] [JsonIgnore] public string RelativeCnic { get; set; }

        [Display(Name = "Cycle")] [JsonIgnore] public string RelativeCycle { get; set; }

        [Display(Name = "Date of Birth")] public DateTime? RelativeDateOfBirth { get; set; }
        [Display(Name = "Last Name")] public string RelativeFamilyName { get; set; }
        [Display(Name = "Father Name")] public string RelativeFathersName { get; set; }
        [Display(Name = "First Name")] public string RelativeFirstName { get; set; }

        [Display(Name = "Form Number")]
        [JsonIgnore]
        public string RelativeFormNumber { get; set; }

        [Display(Name = "Institution")]
        [JsonIgnore]
        public string RelativeInstitution { get; set; }

        [Display(Name = "Jamati Title")] public string RelativeJamatiTitle { get; set; }

        [Display(Name = "Relation")]
        [JsonIgnore]
        public string RelativePersonId { get; set; }

        [Display(Name = "Position")]
        [JsonIgnore]
        public string RelativePosition { get; set; }

        [Display(Name = "Relation")]
        [JsonIgnore]
        public string RelativeRelation { get; set; }

        [Display(Name = "Salutation")]
        [JsonIgnore]
        public string RelativeSalutation { get; set; }

        [JsonProperty(PropertyName = "religiousEducation")]
        [Display(Name = "Religious Education")]
        public string ReligiousEducation { get; set; }

        [JsonProperty(PropertyName = "relocateLocation")]
        [Display(Name = "Relocate Location")]
        public string RelocateLocation { get; set; }

        [JsonProperty(PropertyName = "relocationDateTime")]
        [Display(Name = "Relocation Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? RelocationDateTime { get; set; }

        [JsonProperty(PropertyName = "residenceTelephone")]
        [Display(Name = "Residence Telephone")]
        public string ResidenceTelephone { get; set; }

        [JsonProperty(PropertyName = "residentialAddress")]
        [Display(Name = "Residental Address")]
        public string ResidentalAddress { get; set; }

        [JsonProperty(PropertyName = "salutation")]
        [Required]
        public string Salutation { get; set; }

        public string SalutationForDisplay { get; set; }

        [JsonProperty(PropertyName = "skills")]
        public List<string> Skills { get; set; }

        [JsonIgnore] public string Speak { get; set; }

        [JsonIgnore]
        [Display(Name = "To Year")]
        public int? ToYear { get; set; }

        [JsonIgnore] public string TypeOfBusiness { get; set; }

        [JsonIgnore] [Display(Name = "Cycle")] public string VoluntaryCommunityCycle { get; set; }

        [JsonIgnore]
        [Display(Name = "From Year")]
        public int? VoluntaryCommunityFromYear { get; set; }

        [JsonIgnore]
        [Display(Name = "Institution")]
        public string VoluntaryCommunityInstitution { get; set; }

        [JsonIgnore]
        [Display(Name = "Appointment Type")]
        //[Required]
        public bool VoluntaryCommunityIsImamatAppointment { get; set; }

        [JsonIgnore]
        [Display(Name = "Position")]
        public string VoluntaryCommunityPosition { get; set; }

        [JsonProperty(PropertyName = "voluntaryCommunityServices")]
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

        [JsonProperty(PropertyName = "voluntaryPublicServices")]
        public List<VoluntaryPublicModel> VoluntaryPublicServices { get; set; }

        [JsonIgnore]
        [Display(Name = "To Year")]
        public int? VoluntaryPublicToYear { get; set; }

        [JsonProperty(PropertyName = "willingnessToDevoteTimeInFuture")]
        [Display(Name = "Willingness to Devote Time in Future")]
        public string WillingnessToDevoteTimeInFuture { get; set; }

        [JsonIgnore] public string Write { get; set; }

        #endregion Public Properties
    }
}