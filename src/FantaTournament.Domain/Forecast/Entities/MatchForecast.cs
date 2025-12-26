using System;
using System.Collections.Generic;
using FantaTournament.Domain.Boards;
using Umbrella.Core.Domain;

namespace FantaTournament.Domain.Forecast.Entities
{
    public class MatchForecast : Entity
    {
        public string MatchKey => this.TargetMatch.Id;
        public string MatchType => this.TargetMatch.MatchType;

        public Match TargetMatch { get; set; } = new Match();

        public Guid UserForecastID { get; set; }

        public int NGoalA { get; set; }
        public int NGoalB { get; set; }
        public int NGoalFinalA { get; set; }
        public int NGoalFinalB { get; set; }

        public DateTime CreationDate { get; set; }

        public MatchForecast()
        {
            this.UserForecastID = Guid.Empty;
            this.CreationDate = DateTime.Now;
        }
    }
}