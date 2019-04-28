using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AMS.frontend.web.Areas.Administration.Models
{
    public class CrudModel
    {
        [JsonProperty(PropertyName = "name")]
        [Required]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "createdBy")]
        public string CreatedBy { get; set; }

        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "isActive")]
        public bool IsActive { get; set; }

        [JsonProperty(PropertyName = "updatedBy")]
        public string UpdatedBy { get; set; }

        [JsonProperty(PropertyName = "shortCode")]
        public string ShortCode { get; set; }

        [JsonIgnore]
        public string Url { get; set; }

        [JsonIgnore]
        public List<SelectListItem> lookUpList { get; set; }

        public CrudModel()
        {
            lookUpList = new List<SelectListItem>();
        }

        [JsonIgnore]
        public string Title { get; set; }

        [JsonIgnore]
        public string ActionName { get; set; }

    }
}
