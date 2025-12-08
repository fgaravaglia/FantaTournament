using Umbrella.Core;

namespace FantaTournament.Domain.Boards.Abstractions
{
    /// <summary>
    /// Abstraction for persistence on Team entity
    /// </summary>
    public interface ITeamRepository
    {
        ///<summary>
        /// Queries for all teams in the tournament
        ///</summary>
        Task<Result<IEnumerable<Team>>> GetAllAsync();
        /// <summary>
        /// Queries for a specific team by its identifier
        /// </summary>
        Task<Result<Team>> GetByIdAsync(string keyValue);
    }
}