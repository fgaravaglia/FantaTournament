using FantaTournament.Application.Forecast.Models;
using FantaTournament.Domain.Forecast.Entities;
using Umbrella.Core;

namespace FantaTournament.Application.Forecast
{
    /// <summary>
    /// Abstraction to manage the query on aggregate Forecast and its entities
    /// </summary>
    public interface IForecastQueryHandler : IQueryHandler
    {
        /// <summary>
        /// Get Latest Forecast for a given user
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        ForecastDTO GetForecastByUser(string userName);
        /// <summary>
        /// Gets the forecast of all users
        /// </summary>
        /// <returns></returns>
        IEnumerable<ForecastDTO> GetAllUserForecasts();
        /// <summary>
        /// Gets the calculationd etails
        /// </summary>
        /// <param name="forecastId"></param>
        /// <returns></returns>
        IEnumerable<ForecastMatchCalculationDetail> GetCalculationDetails(Guid forecastId);
    }
}