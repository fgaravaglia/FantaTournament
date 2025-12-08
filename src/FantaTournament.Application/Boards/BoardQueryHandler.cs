using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FantaTournament.Application.Boards.Models;
using FantaTournament.Domain.Boards;
using FantaTournament.Domain.Boards.Abstractions;
using Umbrella.Core;
using Umbrella.Mapper;

namespace FantaTournament.Application.Boards
{
    /// <summary>
    /// Implementation of query handlers for Board related operations
    /// </summary>
    internal class BoardQueryHandler : IBoardQueryHandler
    {
        #region Fields

        IMatchRepository _MatchRepository;
        ITeamRepository _TeamRepository;
        IMapperRegistry _MapperRegistry;
        #endregion

        public BoardQueryHandler(IMatchRepository matchRepository,
            ITeamRepository teamRepository,
            IMapperRegistry mapperRegistry)
        {
            _MatchRepository = matchRepository ?? throw new ArgumentNullException(nameof(matchRepository));
            _TeamRepository = teamRepository ?? throw new ArgumentNullException(nameof(teamRepository));
            _MapperRegistry = mapperRegistry ?? throw new ArgumentNullException(nameof(mapperRegistry));
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async Task<Result<IEnumerable<MatchDTO>>> GetAllMatches()
        {
            var matchesResult = await this._MatchRepository.GetAllAsync("");
            var teamsResult = await this._TeamRepository.GetAllAsync();

            // ensure correctness of results
            if (!matchesResult.Succeeded)
                return Result<IEnumerable<MatchDTO>>.Failure(matchesResult.Errors);
            if (!teamsResult.Succeeded)
                return Result<IEnumerable<MatchDTO>>.Failure(teamsResult.Errors);

            // fill display name then map to DTOs
            var mapper = this._MapperRegistry.GetMapper<Domain.Boards.Match, MatchDTO>();
            List<MatchDTO> matches = [];
            foreach (var m in (matchesResult.Data ?? []))
            {
                var teamA = (teamsResult.Data ?? []).SingleOrDefault(x => x.Code.Equals(m.TeamA.Code, StringComparison.InvariantCultureIgnoreCase));
                if (teamA != null)
                    m.TeamA = teamA;
                var teamB = (teamsResult.Data ?? []).SingleOrDefault(x => x.Code.Equals(m.TeamB.Code, StringComparison.InvariantCultureIgnoreCase));
                if (teamB != null)
                    m.TeamB = teamB;

                if (DateTime.UtcNow >= m.MatchDate && m.Status.Equals(MatchStatus.Planned))
                    m.Status = MatchStatus.Started;

                var matchDto = mapper?.Map(m);
                if (matchDto != null)
                    matches.Add(matchDto);
            }
            return Result<IEnumerable<MatchDTO>>.Success(matches);
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async Task<Result<IEnumerable<TeamDTO>>> GetTeamsAsync()
        {
            var teamsResult = await this._TeamRepository.GetAllAsync();

            // ensure correctness of results
            if (!teamsResult.Succeeded)
                return Result<IEnumerable<TeamDTO>>.Failure(teamsResult.Errors);

            // then Remap on DTOs
            List<TeamDTO> teams = [];
            var mapper = this._MapperRegistry.GetMapper<Team, TeamDTO>();
            teamsResult.Data?.ToList().ForEach(t =>
            {
                var teamDto = mapper?.Map(t);
                if (teamDto != null)
                    teams.Add(teamDto);
            });
            return Result<IEnumerable<TeamDTO>>.Success(teams);
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async Task<Result<IEnumerable<MatchResultDTO>>> GetMatchResultsAsync()
        {
            var matchesResult = await this._MatchRepository.GetAllResultsAsync("");
            var teamsResult = await this._TeamRepository.GetAllAsync();

            // ensure correctness of results
            if (!matchesResult.Succeeded)
                return Result<IEnumerable<MatchResultDTO>>.Failure(matchesResult.Errors);
            if (!teamsResult.Succeeded)
                return Result<IEnumerable<MatchResultDTO>>.Failure(teamsResult.Errors);

            // fill display name
            foreach (var r in (matchesResult.Data ?? []))
            {
                // var teamA = (teamsResult.Data ?? []).SingleOrDefault(x => x.Code.Equals(r.Match.TeamA, StringComparison.InvariantCultureIgnoreCase));
                // r.Match.TeamB = teamA != null ? teamA.DisplayName : r.Match.TeamA;

                // var teamB = teams.SingleOrDefault(x => x.Code == r.Match.TeamB);
                // r.Match.DisplayTeamB = teamB != null ? teamB.DisplayName : r.Match.TeamB;

                // if (DateTime.Now >= r.Match.MatchDate && r.Match.Status == MatchStatus.Planned.Code)
                //     r.Match.Status = MatchStatus.Started.Code;
            }
            // then Remap on DTOs
            List<MatchResultDTO> matches = [];
            return Result<IEnumerable<MatchResultDTO>>.Success(matches);
        }
    }
}