using FantaTournament.Domain.Boards;
using FantaTournament.Domain.Forecast.Entities;

namespace FantaTournament.Domain.Forecast.Ranking.Rules
{
    internal class RegularScoreRule : BasicRule
    {
        public RegularScoreRule() : base("REGULAR_SCORE", "Pronostico per il risultato al termine dei tempi regolamentari")
        {

        }

        protected override double GetPointsForGivenMatch(MatchResult result, MatchForecast forecast)
        {
            if (result.Match is null)
                throw new InvalidOperationException($"Rule {this.Code} has invalid Input: match is null");

            if (String.IsNullOrEmpty(result.Match.MatchType))
                throw new InvalidOperationException($"Rule {this.Code} has invalid Input: match {result.Match.Id} has null MatchType");

            LogDebug($"{result.Match.Id}: {forecast.NGoalA}-{forecast.NGoalB}", "", "");

            if (result.NGoalA == forecast.NGoalA && result.NGoalB == forecast.NGoalB)
                return 3.0;
            else if ((result.NGoalA - result.NGoalB) == (forecast.NGoalA - forecast.NGoalB))
                return 1.5;
            else
                return 0.0;
        }
    }
}