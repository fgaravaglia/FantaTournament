using FantaTournament.Domain.Boards;
using FantaTournament.Application.Boards.Models;
using FantaTournament.Application.Boards.Mappers;

namespace FantaTournament.Application.Tests.Mappers
{
    public class MatchMapperTests : BaseMapperTests<Match, MatchDTO>
    {

        [SetUp]
        public void Setup()
        {
            _Mapper = new MatchMapper();
        }

        protected override void InstanceSource()
        {
            this._Source = new Match
            {
                TeamA = new Domain.Boards.Team { Code = "TeamA" },
                TeamB = new Domain.Boards.Team { Code = "TeamB" },
                MatchDate = DateTime.UtcNow,
                CreatedDate = DateTime.UtcNow,
                Id = Guid.NewGuid().ToString(),
                UpdatedBy = "username",
                UpdatedDate = DateTime.UtcNow,
                MatchType = Domain.Boards.MatchType.Round.Code,
                Status = Domain.Boards.MatchStatus.Planned,
                MatchContainer = "Group1"
            };
        }

        protected override void AssertDestinationIsMappedAsExpected(MatchDTO? result)
        {
            Assert.IsNotNull(result);
            Assert.That(result.ID, Is.EqualTo(this._Source.Id));
            Assert.That(result.TeamA, Is.EqualTo(this._Source.TeamA.Code));
            Assert.That(result.TeamB, Is.EqualTo(this._Source.TeamB.Code));
            Assert.That(result.MatchDate.Date, Is.EqualTo(this._Source.MatchDate.Date));
            Assert.That(result.MatchType, Is.EqualTo(this._Source.MatchType));
            Assert.That(result.Status, Is.EqualTo(this._Source.Status.Code));
            Assert.That(result.MatchContainer, Is.EqualTo(this._Source.MatchContainer));

        }


    }
}