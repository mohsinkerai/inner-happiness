﻿using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace AMS.frontend.web.Models.Authenticate
{
    public class LoginModel
    {
        #region Public Properties

        [JsonProperty(PropertyName = "companyId")]
        [Required] [Display(Name = "Company")] public string Company { get; set; }

        [Required]
        [Display(Name = "Username")]
        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }

        [JsonProperty(PropertyName = "password")]
        [Required] public string Password { get; set; }

        [JsonIgnore]
        [Display(Name = "Remember Me")] public bool RememberMe { get; set; }

        #endregion Public Properties
    }
}