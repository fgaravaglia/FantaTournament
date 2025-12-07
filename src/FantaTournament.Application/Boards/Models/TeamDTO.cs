namespace FantaTournament.Application.Boards.Models
{
    public class TeamDTO
    {
        public string Code { get; set; }

        public string DisplayName { get; set; }

        public string BoardCode { get; set; }

        public TeamDTO()
        {
            this.Code = "";
            this.DisplayName = "";
            this.BoardCode = "";

        }
    }
}