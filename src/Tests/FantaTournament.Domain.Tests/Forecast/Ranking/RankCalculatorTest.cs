using NUnit.Framework;
using FantaTournament.Domain.Boards;
using FantaTournament.Domain.Forecast.Entities;
using FantaTournament.Domain.Forecast.Ranking;
using DomainModels = FantaTournament.Domain.Forecast.Entities;

namespace FantaTournament.Domain.Tests.Forecast.Ranking
{
    public class RankCalculatorTest
    {
        RankCalculator _Calculator;
        List<MatchResult> _Results;

        [SetUp]
        public void Setup()
        {
            // var dataFolder = Path.Combine(Environment.CurrentDirectory, @"..\..\..\..\..\UI.Web.MVCPortal\StaticData\FT\");
            // var boardFileName = "BOARD-2022.csv";
            // Console.WriteLine(@"Data Folder ..........> {Path.GetFullPath(StaticDataFolder)}");
            // var matchRepository = new CSVMatchRepository(dataFolder, boardFileName);
            this._Results = new List<MatchResult>();
            this._Calculator = new RankCalculator();
        }

        static Match NewRoundMatch()
        {
            return new Match()
            {
                MatchContainer = "A",
                MatchType = FantaTournament.Domain.Boards.MatchType.Round.Code,
                TeamA = new Team() { Code = "X", DisplayName = "Team X" },
                TeamB = new Team() { Code = "Y", DisplayName = "Team Y" },
                Id = "A-X-Y",
                Status = MatchStatus.Played,
            };
        }

        [Test]
        [Category(TestCategories.INTEGRATION)]
        public void GuessFinalResult_Gives6Points_ForRounds()
        {
            //********** GIVEN
            var matches = new List<MatchResult>()
            {
                new MatchResult()
                {
                    Match = NewRoundMatch(),
                    NGoalA = 0,
                    NGoalB = 1,
                    NGoalFinalA = 2,
                    NGoalFinalB = 2
                }
            };
            var forecast = new MatchForecast()
            {
                TargetMatch = matches[0].Match,
                NGoalA = 0,
                NGoalB = 0,
                NGoalFinalA = 2,
                NGoalFinalB = 2
            };
            this._Calculator.GivenTheseResults(matches);

            //********** WHEN
            var details = this._Calculator.CalculatePointsWithDetails([forecast]);
            var forecastEntity = new DomainModels.Forecast();
            forecastEntity.SetPoints(details);

            //********** WHEN
            Assert.That(details[0].RuleDescriptions.Count, Is.EqualTo(3), "Expected only EXACT_RESULT, REGULAR_SCORE, MATCH_RESULT applied");
            Assert.That(details[0].Points.Exists(x => x.Key == "EXACT_RESULT"), Is.True);
            Assert.That(details[0].Points.Single(x => x.Key == "EXACT_RESULT").Value, Is.EqualTo(3.0));
            Assert.That(details[0].Points.Exists(x => x.Key == "MATCH_RESULT"), Is.True);
            Assert.That(details[0].Points.Single(x => x.Key == "MATCH_RESULT").Value, Is.EqualTo(3.0));
            Assert.That(forecastEntity.Points, Is.EqualTo(6.0));
            Assert.Pass();
        }
    }
}