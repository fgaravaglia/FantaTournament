using System;
using System.Collections.Generic;
using FantaTournament.Domain.Forecast.Entities;
using Umbrella.Core;

namespace FantaTournament.Domain.Forecast.Abstractions
{
    public interface IMatchForecastRepository
    {
        /// <summary>
        /// Gets MatchForecast by its identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Result<MatchForecast?> GetById(string id);
        /// <summary>
        /// Gets MatchForecast by User Forecast ID
        /// </summary>
        /// <param name="forecastID"></param>
        /// <returns></returns>
        Result<IEnumerable<MatchForecast>> GetMatchResultByUserForecast(Guid forecastID);
        /// <summary>
        /// Saves a MatchForecast
        /// </summary>
        /// <param name="dto"></param>
        void Save(MatchForecast dto);
        /// <summary>
        /// Deletes a MatchForecast by its Id
        /// </summary>
        /// <param name="id"></param>
        void Delete(string id);
        /// <summary>
        /// Deletes MatchForecasts by User Forecast ID
        /// </summary>
        /// <param name="forecastID"></param>
        void DeleteByUserForecast(Guid forecastID);
    }

}