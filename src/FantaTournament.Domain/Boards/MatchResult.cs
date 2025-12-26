using System.Diagnostics.CodeAnalysis;
using Umbrella.Core.Domain;

namespace FantaTournament.Domain.Boards
{
    /// <summary>
    /// Entity to map the Result of a specific Match
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class MatchResult : Entity, IAuditableEntity
    {
        /// <summary>
        /// The Match related to this Result
        /// </summary>
        public Match? Match { get; set; }

        /// <summary>
        /// Number of goals of Team A, after regular time
        /// </summary>
        public int NGoalA { get; set; }
        /// <summary>
        /// Number of goals of Team B, after regular time
        /// </summary>
        public int NGoalB { get; set; }
        /// <summary>
        /// Number of goals of Team A, after final time (including extra time and penalties)
        /// </summary>
        public int NGoalFinalA { get; set; }
        /// <summary>
        /// Number of goals of Team B, after final time (including extra time and penalties)
        /// 
        public int NGoalFinalB { get; set; }

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