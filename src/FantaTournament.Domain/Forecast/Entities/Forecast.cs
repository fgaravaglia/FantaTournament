using FantaTournament.Domain.Boards;
using FantaTournament.Domain.Forecast.Ranking;
using Umbrella.Core.Domain;
using MatchType = FantaTournament.Domain.Boards.MatchType;

namespace FantaTournament.Domain.Forecast.Entities
{
    /// <summary>
    /// Entity to representa a given forecast i the domain
    /// </summary>
    public class Forecast : AuditableEntity
    {
        /// <summary>
        /// Username of the user who made the forecast
        /// </summary>
        public string Username { get; private set; } = "";
        /// <summary>
        /// Points obtained with the forecast
        /// </summary>
        public double Points { get; set; } = 0.0;
        /// <summary>
        /// Status of the calculation of the forecast points
        /// </summary>
        public CalculationStatus CalculationStatus { get; set; } = CalculationStatus.ToStart;
        /// <summary>
        /// List of match forecasts included in the forecast
        /// </summary>
        public List<MatchForecast> ForecastResults { get; private set; } = [];
        /// <summary>
        /// Details of the calculation of the forecast points
        /// </summary>
        public List<ForecastMatchCalculationDetail> CalculationDetails { get; set; } = [];

        public Forecast()
        {



        }

        /// <summary>
        /// Sets the username for the forecast
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public Forecast SetUsername(string username)
        {
            if (String.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));
            this.Username = username.Trim();
            SetAsModified(this.Username);
            return this;
        }
        /// <summary>
        /// Sets a forecast for a specific match
        /// </summary>
        /// <param name="matchKey"></param>
        /// <param name="goalA"></param>
        /// <param name="goalB"></param>
        /// <param name="finalGoalA"></param>
        /// <param name="finalGoalB"></param>
        public void SetMatchForecast(string matchKey, int goalA, int goalB, int finalGoalA, int finalGoalB)
        {
            if (String.IsNullOrEmpty(matchKey))
                throw new ArgumentNullException(nameof(matchKey));
            if (finalGoalA < goalA)
                finalGoalA = goalA;
            if (finalGoalB < goalB)
                finalGoalB = goalB;

            // identify if match has already a forecast; then update it
            var item = this.ForecastResults.SingleOrDefault(x => x.MatchKey == matchKey);
            if (item == null)
            {
                item = new MatchForecast()
                {
                    UserForecastID = Guid.Parse(this.Id),
                    TargetMatch = new Match() { Id = matchKey },
                };
                this.ForecastResults.Add(item);
                item = this.ForecastResults.Single(x => x.MatchKey == matchKey);
            }
            item.NGoalA = goalA;
            item.NGoalB = goalB;
            item.NGoalFinalA = finalGoalA;
            item.NGoalFinalB = finalGoalB;

            SetAsModified(null);
        }
        /// <summary>
        /// Sets the points for the current forecast
        /// </summary>
        public void SetPoints(List<ForecastMatchCalculationDetail> details)
        {
            if (details == null)
                throw new ArgumentNullException(nameof(details));
            this.CalculationDetails.Clear();
            this.CalculationDetails.AddRange(details);
            this.Points = 0.0;
            foreach (var detail in this.CalculationDetails)
            {
                this.Points += detail.Points.Select(x => x.Value).Sum();
            }
            SetAsModified(null);
        }
        /// <summary>
        /// Calculates the points for the current forecast
        /// </summary>
        /// <param name="matchResults"></param>
        public void CalculatePoints(IEnumerable<MatchResult> matchResults)
        {
            // instance calculator and get the points
            var details = new RankCalculator()
                            .GivenTheseResults(matchResults)
                            .CalculatePointsWithDetails(this.ForecastResults);

            this.SetPoints(details);
        }

        // public static Forecast FromDTO(ForecastDTO dto)
        // {
        //     if (dto is null)
        //         throw new ArgumentNullException(nameof(dto));
        //     Forecast entity = new Forecast(dto.User);
        //     entity.SetStatusFromDTO(dto);
        //     return entity;
        // }

        // public override void SetStatusFromDTO(ForecastDTO dto)
        // {
        //     if (dto is null)
        //         throw new ArgumentNullException(nameof(dto));
        //     if (dto.GetType() != typeof(ForecastDTO))
        //         throw new ApplicationException($"Wrong DTO type: Expected {typeof(ForecastDTO).Name} but found {dto.GetType().Name} instead");
        //     ForecastDTO forecastDto = (ForecastDTO)dto;
        //     this.ID = forecastDto.ID.ToString();
        //     this.CreatedOn = forecastDto.CreationDate;
        //     this.LastUpdatedOn = forecastDto.LastUpdateDate;
        //     this.Username = forecastDto.User;
        //     this.Points = forecastDto.Points;

        //     this.ForecastResults.Clear();
        //     this.ForecastResults.AddRange(forecastDto.MatchResults);
        //     this.CalculationDetails.Clear();
        // }

        // public override ForecastDTO ToDTO()
        // {
        //     ForecastDTO forecastDto = new ForecastDTO()
        //     {
        //         ID = this.ID,
        //         User = this.Username,
        //         Points = this.Points,
        //         CreationDate = this.CreatedOn,
        //         LastUpdateDate = this.LastUpdatedOn,
        //         CalculationStatus = this.CalculationStatus.Code
        //     };
        //     forecastDto.MatchResults.Clear();
        //     forecastDto.MatchResults.AddRange(this.ForecastResults);
        //     return forecastDto;
        // }
        /// <summary>
        /// Calculates the completeness based on the tournament phase
        /// </summary>
        /// <param name="currentPhase"></param>
        /// <returns></returns>
        public double CalculateCompleteness(string currentPhase, IEnumerable<Match> matches)
        {
            if (String.IsNullOrEmpty(currentPhase))
                throw new ArgumentNullException(nameof(currentPhase));

            float validMatches = (float)matches.Where(x => MatchType.FromCode(x.MatchType).ToIntegerValue() <= MatchType.FromCode(currentPhase).ToIntegerValue()).Count();
            float actualForecast = this.ForecastResults.Count(x => MatchType.FromCode(x.MatchType).ToIntegerValue() <= MatchType.FromCode(currentPhase).ToIntegerValue());
            double completeness = (double)(actualForecast / validMatches);
            completeness = Math.Round(completeness, 2);
            return completeness;
        }
    }
}