using FantaTournament.Domain.Forecast.Entities;
using Umbrella.Core;
using ForecastEntity = FantaTournament.Domain.Forecast.Entities.Forecast;

namespace FantaTournament.Domain.Forecast.Abstractions
{
    /// <summary>
    /// Interface to hide persistence
    /// </summary>
    public interface IForecastRepository
    {
        /// <summary>
        /// Gets all Forecast
        /// </summary>
        /// <returns></returns>
        Task<Result<IEnumerable<ForecastEntity>>> GetAllAsync();
        /// <summary>
        /// Gets a Forecast by its Key
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        Task<Result<ForecastEntity>> GetByKeyAsync(Guid keyValue);
        /// <summary>
        ///     Gets the latest Forecast for a given user
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<Result<ForecastEntity>> GetForecastByUserAsync(string userName);
        /// <summary>
        /// Saves all forecasts
        /// </summary>
        /// <param name="forecasts"></param>
        /// <param name="ignoreLastUpdateDate"></param>
        void SaveAll(IEnumerable<ForecastEntity> forecasts, bool ignoreLastUpdateDate);
        /// <summary>
        /// Saves a Forecast
        /// </summary>
        /// <param name="forecast"></param>
        /// <returns></returns>
        Result<Guid> Save(ForecastEntity forecast);
        /// <summary>
        /// Deletes a Forecast by its Id    
        /// </summary>
        void Delete(Guid id);
        /// <summary>
        /// Gets the calculation details
        /// </summary>
        Result<IEnumerable<ForecastMatchCalculationDetail>> GetCalculationDetails(Guid forecastId);
        /// <summary>
        /// Saves the calculation details
        /// </summary>
        /// <param name="details"></param>
        void SaveCalculationDetails(IEnumerable<ForecastMatchCalculationDetail> details);
    }
}