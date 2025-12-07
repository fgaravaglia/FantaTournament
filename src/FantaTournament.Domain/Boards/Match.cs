using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Umbrella.Core.Domain;

namespace FantaTournament.Domain.Boards
{
    ///<summary>
    /// Entity to map a specific match of the tournament board
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Match : Entity, IAuditableEntity
    {
        /// <summary>
        /// Date of the match
        /// <summary/>
        public DateTime MatchDate { get; set; }

        /// <summary>
        /// First Team playing the match
        /// </summary>
        public Team TeamA { get; set; } = new Team();

        /// <summary>
        /// Second Team playing the match
        /// </summary>
        public Team TeamB { get; set; } = new Team();
        /// <summary>
        /// Type of match (Group, Round of 16, Quarters, etc)
        /// </summary>
        public string MatchType { get; set; } = "";
        /// <summary>
        /// Container of match. it is the Group name for Round Matches, Null for others
        /// </summary>
        public string MatchContainer { get; set; } = "";
        ///<summary>
        /// Status of the match (Scheduled, Completed, etc)
        ///</summary>
        public string Status { get; set; } = "TO_PLAY";


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
