using FantaTournament.Application.Forecast.Models;
using ForecastEntity = FantaTournament.Domain.Forecast.Entities.Forecast;

namespace FantaTournament.Application.Forecast.Mappers
{
    public class ForecastMapper : Umbrella.Mapper.BaseMapper<ForecastEntity, ForecastDTO>
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public override ForecastDTO? Map(ForecastEntity? source)
        {
            if (source == null)
                return null;

            // Create a new instance of the destination type and red property list
            ForecastDTO dest = this.MapByName(source ?? new ForecastEntity());
            dest.ID = source.Id;
            dest.User = source.Username;
            dest.Points = source.Points;
            dest.CalculationStatus = source.CalculationStatus.Code;
            dest.CreatedOn = source.CreatedDate;
            dest.LastUpdatedOn = source.UpdatedDate;

            return dest;
        }
    }
}