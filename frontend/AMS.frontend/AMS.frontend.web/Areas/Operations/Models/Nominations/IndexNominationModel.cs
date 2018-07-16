using System.Collections.Generic;

namespace AMS.frontend.web.Areas.Operations.Models.Nominations
{
    public class IndexNominationModel
    {
        #region Public Properties

        public string Company { get; set; }
        public string Institution { get; set; }
        public string Jamatkhana { get; set; }
        public string Local { get; set; }
        public List<PositionModel> Positions { get; set; }
        public string Region { get; set; }

        #endregion Public Properties
    }
}