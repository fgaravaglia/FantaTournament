using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using FantaTournament.Domain.Forecast.Entities;

namespace FantaTournament.Application.Forecast.Models
{

    [ExcludeFromCodeCoverage]
    public class ForecastDTO
    {
        public string ID { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public string User { get; set; }

        public double Points { get; set; }

        public string CalculationStatus { get; set; }

        public List<MatchForecastDTO> MatchResults { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? LastUpdatedOn { get; set; }

        public ForecastDTO()
        {
            this.ID = Guid.NewGuid().ToString();
            this.CreationDate = DateTime.Now;
            this.LastUpdateDate = null;
            this.User = "";
            this.CalculationStatus = FantaTournament.Domain.Forecast.Entities.CalculationStatus.ToStart.Code;
            this.MatchResults = new List<MatchForecastDTO>();
            this.CreatedOn = DateTime.Now;
        }
    }
}