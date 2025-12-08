using System;
using FantaTournament.Application.Boards.Models;
using FantaTournament.Domain.Boards;
using Umbrella.Mapper;

namespace FantaTournament.Application.Boards.Mappers
{
    public class TeamMapper : Umbrella.Mapper.BaseMapper<Team, TeamDTO>
    {
        public TeamMapper()
        { }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public override TeamDTO? Map(Team? source)
        {
            if (source == null)
                return null;

            // Create a new instance of the destination type and red property list
            TeamDTO dest = this.MapByName(source ?? new Team());
            dest.BoardCode = source?.Id ?? "";

            return dest;
        }
    }
}