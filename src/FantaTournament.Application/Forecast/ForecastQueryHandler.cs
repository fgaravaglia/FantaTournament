using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FantaTournament.Application.Forecast.Models;
using FantaTournament.Domain.Forecast.Abstractions;
using FantaTournament.Domain.Forecast.Entities;
using Umbrella.Mapper;

namespace FantaTournament.Application.Forecast
{
    /// <summary>
    /// Implementation of <see cref="IForecastQueryHandler"/> to earch for data about forecasts
    /// </summary>
    public class ForecastQueryHandler : IForecastQueryHandler
    {
        #region Fields
        readonly IForecastRepository _ForecastRepository;
        IMapperRegistry _MapperRegistry;
        #endregion

        #region Constructor
        public ForecastQueryHandler(IForecastRepository forecastRepo, IMapperRegistry mapperRegistry)
        {
            this._ForecastRepository = forecastRepo ?? throw new ArgumentNullException(nameof(forecastRepo));
            this._MapperRegistry = mapperRegistry ?? throw new ArgumentNullException(nameof(mapperRegistry));
        }
        #endregion

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public IEnumerable<ForecastDTO> GetAllUserForecasts()
        {
            var queryResult = this._ForecastRepository.GetAllAsync().Result;

            // ensure correctness of results
            if (!queryResult.Succeeded)
                throw new InvalidOperationException("Unexpected error during data query! " + string.Join(",", queryResult.Errors));

            // fill display name then map to DTOs
            var mapper = this._MapperRegistry.GetRequiredMapper<Domain.Forecast.Entities.Forecast, ForecastDTO>();
            return queryResult?.Data?.Select(f => mapper.Map(f)!).ToList() ?? [];
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public IEnumerable<ForecastMatchCalculationDetail> GetCalculationDetails(Guid forecastId)
        {
            var queryResult = this._ForecastRepository.GetCalculationDetails(forecastId);

            // ensure correctness of results
            if (!queryResult.Succeeded)
                throw new InvalidOperationException("Unexpected error during data query! " + string.Join(",", queryResult.Errors));

            return queryResult?.Data ?? [];
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public ForecastDTO GetForecastByUser(string userName)
        {
            var queryResult = this._ForecastRepository.GetForecastByUserAsync(userName).Result;

            // ensure correctness of results
            if (!queryResult.Succeeded)
                throw new InvalidOperationException("Unexpected error during data query! " + string.Join(",", queryResult.Errors));

            // fill display name then map to DTOs
            var mapper = this._MapperRegistry.GetMapper<Domain.Forecast.Entities.Forecast, ForecastDTO>();
            return mapper.Map(queryResult.Data) ?? new ForecastDTO
            {
                User = userName
            };
        }
    }
}