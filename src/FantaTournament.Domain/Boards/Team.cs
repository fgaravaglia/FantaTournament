using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Umbrella.Core.Domain;

namespace FantaTournament.Domain.Boards
{
    /// <summary>  
    /// This entity maps a specific Team jioining the Tournament
    /// </summary>   
    [ExcludeFromCodeCoverage]
    public class Team : Entity
    {
        /// <summary>
        /// 
        /// </summary>
        public string Code { get; set; } = "";
        /// <summary>
        /// 
        /// </summary>
        public string DisplayName { get; set; } = "";
    }
}
