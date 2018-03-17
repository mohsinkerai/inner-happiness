using System.ComponentModel.DataAnnotations;

namespace AMS.frontend.web.Areas.Operations.Models.Persons
{
    public class LanguageProficiencyModel
    {
        public string LanguageProficiencyId { get; set; }

        public string Language { get; set; }

        public string Read { get; set; }

        public string Write { get; set; }

        public string Speak { get; set; }
    }
}