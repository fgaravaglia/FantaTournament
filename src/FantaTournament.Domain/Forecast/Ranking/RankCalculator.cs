using FantaTournament.Domain.Boards;
using FantaTournament.Domain.Forecast.Entities;
using FantaTournament.Domain.Forecast.Ranking;
using FantaTournament.Domain.Forecast.Ranking.Rules;

namespace FantaTournament.Domain.Forecast.Ranking
{
    internal class RankCalculator
    {
        #region Fields
        readonly List<MatchResult> _MatchResults;
        readonly List<BasicRule> _Rules;
        #endregion

        public RankCalculator()
        {
            this._MatchResults = new List<MatchResult>();
            this._Rules = new List<BasicRule>()
            {
                new ExactResultRule(),
                new RegularScoreRule(),
                new ExtendedScoreRule(),
                new MatchWinnerRule()
            };
        }

        public RankCalculator GivenTheseResults(IEnumerable<MatchResult> matchResults)
        {
            matchResults.ToList().ForEach(x => this._MatchResults.Add(x));
            return this;
        }

        public List<ForecastMatchCalculationDetail> CalculatePointsWithDetails(IEnumerable<MatchForecast> forecasts)
        {
            Guid correlationId = Guid.NewGuid();
            LogDebug("Start Calculation", "TIME", correlationId.ToString());

            double total = 0.0;
            var details = new List<ForecastMatchCalculationDetail>();
            foreach (var dto in forecasts)
            {
                // get result
                var result = this._MatchResults.SingleOrDefault(x => (x.Match?.Id ?? "").Equals(dto.MatchKey, StringComparison.OrdinalIgnoreCase));

                //get the rules to be applied
                var rules = this._Rules.Where(x => x.CanBeApplied(result?.Match)).ToList();
                LogDebug($"Found {rules.Count} to apply", "", correlationId.ToString());

                // calculate the points and sum to total of user forecast
                var detail = new ForecastMatchCalculationDetail(dto.MatchKey, dto.UserForecastID);
                var matchPoints = 0.0;
                rules.ForEach(r =>
                {
                    LogDebug($"Rule {r.Code} @ Match {result?.Match?.Id}: {result?.NGoalFinalA ?? 0}-{result?.NGoalFinalB ?? 0} [Forecast:{dto.NGoalFinalA}-{dto.NGoalFinalB}]", "", correlationId.ToString());
                    var points = r.GetPoints(result, dto);
                    detail.AddRulePoints(r, points);
                    LogDebug($"\t{r.Code}={points} points", "", correlationId.ToString());
                    matchPoints += points;
                });
                LogDebug($"Match {result?.Match?.Id}: {matchPoints} points", "", correlationId.ToString());

                total += matchPoints;
                details.Add(detail);
            }

            LogDebug("End Calculation", "TIME", correlationId.ToString());
            return details;
        }


        public double CalculatePoints(IEnumerable<MatchForecast> forecasts)
        {
            Guid correlationId = Guid.NewGuid();
            LogDebug("Start Calculation", "TIME", correlationId.ToString());

            double total = 0.0;
            foreach (var dto in forecasts)
            {
                // get result
                var result = this._MatchResults.SingleOrDefault(x => (x.Match?.Id ?? "") == dto.MatchKey);

                //get the rules to be applied
                var rules = this._Rules.Where(x => x.CanBeApplied(result?.Match)).ToList();
                // calculate the points and sum to total of user forecast
                var matchPoints = rules.Select(x => x.GetPoints(result, dto)).Sum();
                LogDebug($"Match {result?.Match?.Id}: {matchPoints} points", "TIME", correlationId.ToString());

                total += matchPoints;
            }

            LogDebug("End Calculation", "TIME", correlationId.ToString());
            return total;
        }

        #region Private Methods

        private void LogDebug(string message, string category, string correationId)
        {
            Console.WriteLine($"[DEBUG] [{category}] [{correationId}] [{this.GetType().FullName}] {message}");
        }

        #endregion
    }
}