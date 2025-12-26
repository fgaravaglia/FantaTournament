
using FantaTournament.Application.Boards.Models;
using Umbrella.Core;

namespace FantaTournament.Application.Boards
{
    /// <summary>
    /// Abstraction to manage the query on aggregate Borad and its entities
    /// </summary>
    public interface IBoardQueryHandler : IQueryHandler
    {
        /// <summary>
        /// Queries to retrieve all amtches of tournament
        /// </summary>
        /// <returns></returns>
        Task<Result<IEnumerable<MatchDTO>>> GetAllMatches();
        ///<summary>
        /// Queries all teams in the Board
        /// </summary>
        Task<Result<IEnumerable<TeamDTO>>> GetTeamsAsync();

        /// <summary>
        /// Gets the math results
        /// </summary>
        /// <returns></returns>
        Task<Result<IEnumerable<MatchResultDTO>>> GetMatchResultsAsync();
    }
}