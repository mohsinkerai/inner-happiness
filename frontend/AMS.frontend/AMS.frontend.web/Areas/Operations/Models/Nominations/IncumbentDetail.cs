using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AMS.frontend.web.Areas.Operations.Models.Nominations
{
    public class IncumbentDetail
    {
        public string Id { get; set; }
        public bool IsAppointed { get; set; }
        public bool IsRecommended { get; set; }
        public string personAppointmentId { get; set; }
        public int Priority { get; set; }
        public string RowClass => IsRecommended ? "background: #bec9f57a !important" : string.Empty;

        public string Remarks { get; set; }
    }
}
