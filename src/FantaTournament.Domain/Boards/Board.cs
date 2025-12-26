using System.Diagnostics.CodeAnalysis;
using Umbrella.Core.Domain;

namespace FantaTournament.Domain.Boards
{
    /// <summary>
    /// Entity to map the board of the trournament, with all matches
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Board : AuditableEntity
    {
        /// <summary>
        /// List of matches in the tournament board
        /// </summary>
        public List<Match> Matches { get; set; } = [];

    }
}