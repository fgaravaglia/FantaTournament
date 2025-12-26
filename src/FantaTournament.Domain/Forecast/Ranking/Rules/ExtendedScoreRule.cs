using FantaTournament.Domain.Boards;
using FantaTournament.Domain.Forecast.Entities;

namespace FantaTournament.Domain.Forecast.Ranking.Rules
{
    internal class ExtendedScoreRule : BasicRule
    {
        public ExtendedScoreRule() : base("EXTENDED_SCORE", "Pronostico per il risultato al termine dei tempi supplementari/Rigori; Valido solo per la fase finale del torneo")
        {

        }

        public override bool CanBeApplied(Match? dto)
        {
            if (!base.CanBeApplied(dto))
                return false;

            if (String.IsNullOrEmpty(dto?.MatchType))
                throw new InvalidOperationException($"Rule {this.Code} has invalid Input: match {dto?.Id} has null MatchType");

            return dto.MatchType != Boards.MatchType.Round.Code;
        }

        protected override double GetPointsForGivenMatch(MatchResult result, MatchForecast forecast)
        {

            if (result.NGoalFinalA == forecast.NGoalFinalA && result.NGoalFinalB == forecast.NGoalFinalB)
                return 3.0;
            else if ((result.NGoalFinalA - result.NGoalFinalB) == (forecast.NGoalFinalA - forecast.NGoalFinalB))
                return 1.5;
            else
                return 0.0;
        }
    }
}