using System;
using System.Collections.Generic;
using AMS.frontend.web.Areas.Operations.Models.Persons;
using Newtonsoft.Json.Linq;

namespace AMS.frontend.web.Areas.Operations.Models.Nominations
{
    public class PositionModel
    {
        #region Public Properties

        public string CurrentCycle { get; set; }
        public string CycleStatus { get; set; }
        public string Id { get; set; }
        public PersonModel Incubment { get; set; }
        public List<NominationModel> Nominations { get; set; }

        public string NominationsMessage
        {
            get
            {
                if (Required == Nominations?.Count)
                {
                    return "Nominations completed";
                }

                if (Nominations?.Count < Required)
                {
                    var count = Required - Nominations.Count;
                    return count > 1 ? $"{count} nominations required" : $"{count} nomination required";
                }

                return $"{Required} nominations required";
            }
        }

        public string PositionName { get; set; }
        public string FullName { get; set; }
        public string PreviousCycle { get; set; }
        public int Required { get; set; }

        public static implicit operator PositionModel(JToken v)
        {
            throw new NotImplementedException();
        }

        public int Rank { get; set; }

        public string PositionId { get; set; }

        public string SeatId { get; set; }

        #endregion Public Properties
    }
}