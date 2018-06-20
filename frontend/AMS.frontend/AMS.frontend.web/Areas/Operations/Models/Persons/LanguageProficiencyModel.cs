using Newtonsoft.Json;

namespace AMS.frontend.web.Areas.Operations.Models.Persons
{
    public class LanguageProficiencyModel
    {
        [JsonProperty(PropertyName = "languageProficiencyId")]
        public string LanguageProficiencyId { get; set; }

        [JsonProperty(PropertyName = "language")]
        public string Language { get; set; }
        
        [JsonIgnore] public string LanguageName { get; set; }

        [JsonProperty(PropertyName = "read")]
        public string Read { get; set; }

        [JsonIgnore] public string ReadName { get; set; }

        [JsonProperty(PropertyName = "write")]
        public string Write { get; set; }

        [JsonIgnore] public string WriteName { get; set; }

        [JsonProperty(PropertyName = "speak")]
        public string Speak { get; set; }

        [JsonIgnore] public string SpeakName { get; set; }
    }
}