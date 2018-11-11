using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AMS.frontend.web.Areas.Operations.Models.Nominations
{
    public class IndexNominationModel
    {
        #region Public Properties

        [Required] public string Cycle { get; set; }

        public string Institution { get; set; }

        [Display(Name = "Institution Type")] public string InstitutionType { get; set; }

        public string Level { get; set; }
        public string Local { get; set; }
        public List<PositionModel> Positions { get; set; }

        public string Region { get; set; }

        #endregion Public Properties
    }
}