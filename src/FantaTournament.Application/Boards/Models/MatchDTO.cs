using System;

namespace FantaTournament.Application.Boards.Models
{
    /// <summary>
    /// DTO to model a Match
    /// </summary>
    public class MatchDTO
    {
        public DateTime MatchDate { get; set; } = DateTime.UtcNow;

        public string TeamA { get; set; } = "";

        public string TeamB { get; set; } = "";

        public string DisplayTeamA { get; set; } = "";
        public string DisplayTeamB { get; set; } = "";

        public string MatchType { get; set; } = FantaTournament.Domain.Boards.MatchType.Round.Code;
        /// <summary>
        /// Container of match. it is the Group name for Round Matches, Null for others
        /// </summary>
        /// <value></value>
        public string MatchContainer { get; set; } = FantaTournament.Domain.Boards.MatchType.Round.Code;

        public string Status { get; set; } = FantaTournament.Domain.Boards.MatchStatus.Planned.Code;

        public string ID { get; set; } = Guid.NewGuid().ToString();

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public DateTime? LastUpdatedOn { get; set; }
    }
}