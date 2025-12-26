using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace FantaTournament.Application.Forecast.Models
{
    [ExcludeFromCodeCoverage]
    public class MatchForecastDTO
    {
        public string MatchKey { get; set; }
        public string MatchType { get; set; }

        public Guid UserForecastID { get; set; }

        public int NGoalA { get; set; }
        public int NGoalB { get; set; }
        public int NGoalFinalA { get; set; }
        public int NGoalFinalB { get; set; }

        public DateTime CreationDate { get; set; }

        public MatchForecastDTO()
        {
            this.MatchKey = "";
            this.MatchType = "";
            this.UserForecastID = Guid.Empty;
            this.CreationDate = DateTime.Now;
        }
    }
}