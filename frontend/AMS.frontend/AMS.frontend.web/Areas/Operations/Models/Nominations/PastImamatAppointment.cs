using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AMS.frontend.web.Areas.Operations.Models.Nominations
{
    public class PastImamatAppointment
    {
        public string Position { get; set; }
        public string Cycle { get; set; }
        public string RawPosition { get; set; }
        public string RawInstitution { get; set; }
        public string FromYear { get; set; }
        public string ToYear { get; set; }
    }
}
