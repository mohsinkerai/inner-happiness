namespace AMS.frontend.web.Models
{
    public class ErrorViewModel
    {
        #region Public Properties

        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        #endregion Public Properties
    }
}