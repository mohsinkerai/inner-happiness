using AMS.frontend.web.Areas.Operations.Models.Persons;

namespace AMS.frontend.web.Areas.Operations.Models.Nominations
{
    public class NominationModel
    {
        #region Public Properties

        public string Id { get; set; }
        public bool IsAppointed { get; set; }
        public bool IsRecommended { get; set; }
        public PersonModel Person { get; set; }
        public string personAppointmentId { get; set; }
        public int Priority { get; set; }
        public string RowClass => IsRecommended ? "background: rgba(38, 194, 129, .3) !important" : string.Empty;

        #endregion Public Properties
    }
}