using System.Collections.Generic;

namespace AMS.frontend.web.Areas.Operations.Models.Nominations
{
    public class NominationDetailModel
    {
        #region Public Properties

        public InstitutionModel Institution { get; set; }
        public List<PositionModel> Positions { get; set; }

        #endregion Public Properties
    }
}