namespace FantaTournament.Application.Boards.Models
{
    public class MatchResultDTO
    {
        public MatchDTO Match { get; set; }

        public int NGoalA { get; set; }
        public int NGoalB { get; set; }
        public int NGoalFinalA { get; set; }
        public int NGoalFinalB { get; set; }

        public MatchResultDTO()
        {
            this.Match = new MatchDTO();
        }
    }
}