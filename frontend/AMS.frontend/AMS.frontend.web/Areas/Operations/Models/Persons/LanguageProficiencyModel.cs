using Newtonsoft.Json;

namespace AMS.frontend.web.Areas.Operations.Models.Persons
{
    public class LanguageProficiencyModel
    {
        #region Public Properties

        [JsonProperty(PropertyName = "language")]
        public string Language { get; set; }

        public string LanguageName { get; set; }

        [JsonProperty(PropertyName = "languageProficiencyId")]
        public string LanguageProficiencyId { get; set; }

        [JsonProperty(PropertyName = "read")] public string Read { get; set; }

        public string ReadName { get; set; }

        [JsonProperty(PropertyName = "speak")] public string Speak { get; set; }

        public string SpeakName { get; set; }

        [JsonProperty(PropertyName = "write")] public string Write { get; set; }

        public string WriteName { get; set; }

        #endregion Public Properties
    }
}