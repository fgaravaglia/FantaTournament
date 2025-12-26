using FantaTournament.Domain.Boards;
using FantaTournament.Domain.Forecast.Entities;

namespace FantaTournament.Domain.Forecast.Ranking.Rules
{
    internal class MatchWinnerRule : BasicRule
    {
        public MatchWinnerRule() : base("MATCH_RESULT", "Pronostico vincitore")
        {

        }

        protected override double GetPointsForGivenMatch(MatchResult result, MatchForecast forecast)
        {
            if (result.Match is null)
                throw new InvalidOperationException($"Rule {this.Code} has invalid Input: match is null");

            if (String.IsNullOrEmpty(result.Match.MatchType))
                throw new InvalidOperationException($"Rule {this.Code} has invalid Input: match {result.Match.Id} has null MatchType");

            var resultType = "SAME";
            var forecastResultType = "SAME";
            if (result.NGoalFinalA > result.NGoalFinalB)
                resultType = "WINNERA";
            else if (result.NGoalFinalB > result.NGoalFinalA)
                resultType = "WINNERB";
            if (forecast.NGoalFinalA > forecast.NGoalFinalB)
                forecastResultType = "WINNERA";
            else if (forecast.NGoalFinalB > forecast.NGoalFinalA)
                forecastResultType = "WINNERB";

            if (result.Match.MatchType == Boards.MatchType.Round.Code && resultType == forecastResultType)
                return 3.0;
            else if (resultType == forecastResultType)
                return 5.0;
            else
                return 0.0;
        }
    }
}