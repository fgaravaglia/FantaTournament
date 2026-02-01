using FantaTournament.Domain.Entities;

namespace FantaTournament.Domain.Repositories;

/// <summary>
/// Defines the contract for accessing and managing Board data within the domain.
/// </summary>
public interface IBoardRepository
{
    /// <summary>
    /// Searches for boards whose names contain the specified search string.
    /// </summary>
    /// <param name="name">The name or part of the name to search for.</param>
    /// <returns>
    /// A collection of <see cref="Board"/> entities matching the criteria.
    /// Returns an empty collection if no matches are found.
    /// </returns>
    Task<IEnumerable<Board>> SearchByNameAsync(string name);

    /// <summary>
    /// Retrieves a board by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the board.</param>
    /// <returns>
    /// The <see cref="Board"/> if found; otherwise, <see langword="null"/>.
    /// </returns>
    /// <remarks>
    /// This method may include related data depending on the aggregate root boundaries.
    /// </remarks>
    Task<Board?> GetByIdAsync(string id);

    /// <summary>
    /// Persists changes made to an existing board.
    /// </summary>
    /// <param name="board">The board entity containing the changes to be updated.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous update operation.</returns>
    Task UpdateAsync(Board board);
}
