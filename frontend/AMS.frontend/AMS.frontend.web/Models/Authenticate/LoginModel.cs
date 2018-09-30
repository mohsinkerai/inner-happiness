using System.ComponentModel.DataAnnotations;

namespace AMS.frontend.web.Models.Authenticate
{
    public class LoginModel
    {
        #region Public Properties

        [Required] [Display(Name = "Company")] public string Company { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Required] public string Password { get; set; }

        [Display(Name = "Remember Me")] public bool RememberMe { get; set; }

        #endregion Public Properties
    }
}