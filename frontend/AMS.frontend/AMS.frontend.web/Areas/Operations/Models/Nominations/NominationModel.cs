using AMS.frontend.web.Areas.Operations.Models.Persons;

namespace AMS.frontend.web.Areas.Operations.Models.Nominations
{
    public class NominationModel : IncumbentDetail
    {
        #region Public Properties

        public PersonModel Person { get; set; }

        #endregion Public Properties
    }
}