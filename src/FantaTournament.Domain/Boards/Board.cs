using System.Diagnostics.CodeAnalysis;
using Umbrella.Core.Domain;

namespace FantaTournament.Domain.Boards
{
    /// <summary>
    /// Entity to map the board of the trournament, with all matches
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Board : Entity, IAuditableEntity
    {
        /// <summary>
        /// List of matches in the tournament board
        /// </summary>
        public List<Match> Matches { get; set; } = [];

        #region IAuditableEntity Implementation

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string CreatedBy { get; set; } = "";
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string? UpdatedBy { get; set; }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public DateTime? UpdatedDate { get; set; }

        #endregion
    }
}