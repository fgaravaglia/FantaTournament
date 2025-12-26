
using FantaTournament.Domain.Boards;
using FantaTournament.Domain.Forecast.Entities;

namespace FantaTournament.Domain.Forecast.Ranking.Rules
{
    internal class ExactResultRule : BasicRule
    {
        public ExactResultRule() : base("EXACT_RESULT", "Pronostico con esatto risultato")
        {

        }

        protected override double GetPointsForGivenMatch(MatchResult result, MatchForecast forecast)
        {
            if (result.Match is null)
                throw new InvalidOperationException($"Rule {this.Code} has invalid Input: match is null");
            if (String.IsNullOrEmpty(result.Match.MatchType))
                throw new InvalidOperationException($"Rule {this.Code} has invalid Input: match {result.Match.Id} has null MatchType");
            if (result.Match.MatchType == Boards.MatchType.Round.Code
                && result.NGoalFinalA == forecast.NGoalFinalA && result.NGoalFinalB == forecast.NGoalFinalB)
                return 3.0;
            else if (result.NGoalFinalA == forecast.NGoalFinalA && result.NGoalFinalB == forecast.NGoalFinalB)
                return 5.0;
            else
                return 0.0;
        }
    }
}