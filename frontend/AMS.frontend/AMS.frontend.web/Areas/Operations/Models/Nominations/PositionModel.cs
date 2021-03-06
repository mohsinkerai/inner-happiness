﻿using System;
using System.Collections.Generic;
using AMS.frontend.web.Areas.Operations.Models.Persons;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace AMS.frontend.web.Areas.Operations.Models.Nominations
{
    public class PositionModel
    {
        #region Public Methods

        public static implicit operator PositionModel(JToken v)
        {
            throw new NotImplementedException();
        }

        #endregion Public Methods

        #region Public Properties

        public string CurrentCycle { get; set; }
        public string CycleStatus { get; set; }

        public string CycleStatusForDisplay => CycleStatus?.ToLower();
        public string FullName { get; set; }
        public string State { get; set; }

        public string StateForDisplay => State?.ToLower();
        public string Id { get; set; }
        public PersonModel Incubment { get; set; }
        public IncumbentDetail incumbentDetail { get; set; }
        public List<NominationModel> Nominations { get; set; }

        public string NominationsMessage
        {
            get
            {
                if (Required == Nominations?.Count) return "Nominations completed";

                if (Nominations?.Count < Required)
                {
                    var count = Required - Nominations.Count;
                    return count > 1 ? $"{count} nominations required" : $"{count} nomination required";
                }

                return $"{Required} nominations required";
            }
        }

        public string PositionId { get; set; }
        public string PositionName { get; set; }
        public string PreviousCycle { get; set; }
        public int Rank { get; set; }
        public int Required { get; set; }

        public string Name { get; set; }

        public string SeatId { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }

        [JsonIgnore] public string ErrorMessage { get; set; }
        [JsonIgnore] public bool IsError { get; set; }

        #endregion Public Properties
    }


}