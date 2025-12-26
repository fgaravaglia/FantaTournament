using FantaTournament.Domain.Forecast.Ranking.Rules;
using Umbrella.Core.Domain;

namespace FantaTournament.Domain.Forecast.Entities
{
    /// <summary>
    /// Details of calculation
    /// </summary>
    public class ForecastMatchCalculationDetail : Entity
    {
        public string ID { get { return ForecastId.ToString() + "|" + this.MatchKey; } }

        public Guid ForecastId { get; set; }

        public string MatchKey { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? LastUpdatedOn { get; set; }

        public List<KeyValuePair<string, string>> RuleDescriptions { get; private set; }

        public List<KeyValuePair<string, double>> Points { get; private set; }

        public string PointsPerRule { get; private set; }

        public ForecastMatchCalculationDetail()
        {
            this.MatchKey = "";
            this.CreatedOn = DateTime.Now;
            this.RuleDescriptions = new List<KeyValuePair<string, string>>();
            this.Points = new List<KeyValuePair<string, double>>();
            this.PointsPerRule = "";
        }

        public ForecastMatchCalculationDetail(string matchKey, Guid forecastId) : this()
        {
            if (String.IsNullOrEmpty(matchKey))
                throw new ArgumentNullException(nameof(matchKey));

            this.MatchKey = matchKey;
            this.ForecastId = forecastId;
        }

        internal void AddRulePoints(BasicRule rule, double points)
        {
            if (rule == null)
                throw new ArgumentNullException(nameof(rule));

            if (this.RuleDescriptions.Exists(x => x.Key == rule.Code))
                throw new InvalidOperationException("Unable to add rule " + rule.Code + ": item already added");

            this.RuleDescriptions.Add(new KeyValuePair<string, string>(rule.Code, rule.Description));
            this.Points.Add(new KeyValuePair<string, double>(rule.Code, points));
            this.PointsPerRule = FlattenedRulesAndPoints();
        }

        public double GetPoints()
        {
            return this.Points.Select(x => x.Value).Sum();
        }

        string FlattenedRulesAndPoints()
        {
            var rules = "";

            foreach (var r in this.RuleDescriptions)
            {
                var index = this.RuleDescriptions.IndexOf(r);
                var point = this.Points[index];
                rules += $"{r.Key}|{r.Value}|{point}@@";
            }
            return rules;

        }
    }
}