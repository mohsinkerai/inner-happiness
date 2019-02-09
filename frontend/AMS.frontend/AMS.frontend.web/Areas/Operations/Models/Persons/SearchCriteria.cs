using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AMS.frontend.web.Areas.Operations.Models.Persons
{
    public class SearchCriteria
    {
        public string Condition { get; set; }

        public Gender Gender { get; set; }

        [Display(Name = "Employment Category")]
        public string EmploymentCategory { get; set; }

        [Display(Name = "Employment - Name of Organization")]
        public string EmploymentNameOfOrganization { get; set; }

        [Display(Name = "From - Date of Birth")]
        public DateTime DateOfBirthFrom { get; set; }

        [Display(Name = "Till - Date of Birth")]
        public DateTime DateOfBirthTo { get; set; }

        [Display(Name = "Regional Council")]
        public List<string> RegionalCouncil { get; set; }

        [Display(Name = "Local Council")]
        public List<string> LocalCouncil { get; set; }

        [Display(Name = "Jamat Khana")]
        public List<string> JamatKhana { get; set; }

        [Display(Name = "Jamati Title")]
        public List<string> JamatiTitle { get; set; }

        [Display(Name = "City")]
        public List<string> City { get; set; }

        [Display(Name = "Area of Origin")]
        public List<string> AreaOfOrigin { get; set; }

        [Display(Name = "HighestLevel of Study")]
        public List<string> HighestLevelOfStudy { get; set; }

        [Display(Name = "Field of Interest")]
        public List<string> FiledOfInterest { get; set; }

        [Display(Name = "Occupation Type")]
        public List<string> OccupationType { get; set; }

        [Display(Name = "Educational Institution")]
        public List<string> EducationalInstitution { get; set; }

        [Display(Name = "Name of Degree")]
        public List<string> NameOfDegree { get; set; }

        [Display(Name = "Major Area of Study")]
        public List<string> MajorAreaOfStudy { get; set; }

        [Display(Name = "Field  of Expertise")]
        public List<string> FieldOfExpertise { get; set; }

        [Display(Name = "Religious Education")]
        public List<string> ReligiousEducation { get; set; }

        [Display(Name = "Akdn Training")]
        public List<string> AkdnTraining { get; set; }

        [Display(Name = "Professional Training")]
        public List<string> ProfessionalTraining { get; set; }

        [Display(Name = "Skills")]
        public List<string> Skills { get; set; }

        [Display(Name = "Professional Membership")]
        public List<string> ProfessionalMembership { get; set; }

        [Display(Name = "Type Of Buisness")]
        public List<string> TypeOfBuisness { get; set; }

        [Display(Name = "Nature Of Buisness")]
        public List<string> NatureOfBuisness { get; set; }

        [Display(Name = "Area Of Expertise")]
        public List<string> AreaOfExpertise { get; set; }

        public List<PersonModel> persons { get; set; }

        public SearchCriteria() {
            persons = new List<PersonModel>();
        }

    }
}
