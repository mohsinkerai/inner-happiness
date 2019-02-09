using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AMS.frontend.web.Areas.Operations.Models.Nominations;

namespace AMS.frontend.web.Areas.Operations.Models.Persons
{
    public class CycleOutlook
    {
        public PersonModel person { get; set; }
        public int personId { get; set; }
        public int appointmentPositionId { get; set; }
        public int personAppointmentId { get; set; }
        public int priority { get; set; }
        public object remarks { get; set; }
        public int cycleId { get; set; }
        public int institutionId { get; set; }
        public int positionId { get; set; }
        public int seatId { get; set; }
        public PositionModel position { get; set; }
        public InstitutionModel institution { get; set; }
        public object cycle { get; set; }
        public bool recommended { get; set; }
        public bool appointed { get; set; }
    }
}
