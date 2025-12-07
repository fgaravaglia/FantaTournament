using Umbrella.Core;

namespace FantaTournament.Domain.Boards
{
    /// <summary>
    /// Abstraction for persistence on Match entity
    /// </summary>
    public interface IMatchRepository
    {
        /// <summary>
        /// queries for all mathes on the board
        /// </summary>
        /// <param name="boardId">
        /// <returns></returns>
        Task<Result<IEnumerable<Match>>> GetAllAsync(string boardId);
        /// <summary>
        /// Queries for a specific match by its identifier
        /// </summary>
        /// <param name="boardId"></param>
        /// <param name="matchId"></param>
        /// <returns></returns>
        Task<Result<Match>> GetMatchAsync(string boardId, string matchId);
        /// <summary>
        /// Queries for all results on the board    
        /// </summary>
        /// <param name="boardId"></param>
        Task<Result<IEnumerable<MatchResult>>> GetAllResultsAsync(string boardId);
    }
}