using FantaTournament.Application.Boards.Models;
using FantaTournament.Domain.Boards;

namespace FantaTournament.Application.Boards.Mappers
{
    public class MatchMapper : Umbrella.Mapper.BaseMapper<Match, MatchDTO>
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public override MatchDTO? Map(Match? source)
        {
            if (source == null)
                return null;

            // Create a new instance of the destination type and red property list
            MatchDTO dest = this.MapByName(source ?? new Match());
            dest.ID = source.Id;
            dest.TeamA = source.TeamA.Code;
            dest.TeamB = source.TeamB.Code;
            return dest;
        }
    }
}