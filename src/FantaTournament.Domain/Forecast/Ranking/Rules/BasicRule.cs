using System;
using FantaTournament.Domain.Boards;
using FantaTournament.Domain.Forecast.Entities;

namespace FantaTournament.Domain.Forecast.Ranking.Rules
{
    /// <summary>
    /// Base class for Ranking Rules
    /// </summary>
    internal abstract class BasicRule
    {
        public string Code { get; private set; }
        public string Description { get; private set; }

        protected BasicRule(string code, string descr)
        {
            if (string.IsNullOrEmpty(code))
                throw new ArgumentNullException(nameof(code));
            if (string.IsNullOrEmpty(descr))
                throw new ArgumentNullException(nameof(descr));

            this.Code = code;
            this.Description = descr;
        }

        public virtual bool CanBeApplied(Match? dto)
        {
            if (dto == null)
                return false;
            return dto.Status.Equals(MatchStatus.Played);
        }

        protected void LogDebug(string message, string category, string correationId)
        {
            Console.WriteLine($"[DEBUG] [{category}] [{correationId}] [{this.GetType().FullName}] {message}");
        }

        public double GetPoints(MatchResult? result, MatchForecast forecast)
        {
            if (result == null)
                throw new ArgumentNullException(nameof(result));
            if (result.Match == null)
                throw new ArgumentNullException(nameof(result), "Match is mandatory");
            if (forecast == null)
                throw new ArgumentNullException(nameof(forecast));

            if (!CanBeApplied(result.Match))
                return 0.0;
            return GetPointsForGivenMatch(result, forecast);
        }

        protected abstract double GetPointsForGivenMatch(MatchResult result, MatchForecast forecast);
    }
}