using FantaTournament.Domain.Boards;
using FantaTournament.Application.Forecast.Models;
using FantaTournament.Application.Forecast.Mappers;
using ForecastEntity = FantaTournament.Domain.Forecast.Entities.Forecast;
using System.Data.Common;

namespace FantaTournament.Application.Tests.Mappers
{
    public class ForecastMapperTests : BaseMapperTests<ForecastEntity, ForecastDTO>
    {

        [SetUp]
        public void Setup()
        {
            _Mapper = new ForecastMapper();
        }

        protected override void InstanceSource()
        {
            this._Source = new ForecastEntity().SetUsername("TestUser");
            this._Source.Points = 150.5;
        }

        protected override void AssertDestinationIsMappedAsExpected(ForecastDTO? result)
        {
            Assert.IsNotNull(result);
            Assert.That(result.ID, Is.EqualTo(this._Source.Id));
            Assert.That(result.User, Is.EqualTo(this._Source.Username));
            Assert.That(result.Points, Is.EqualTo(this._Source.Points));
            Assert.That(result.CalculationStatus, Is.EqualTo(this._Source.CalculationStatus.Code));
            Assert.That(result.CreatedOn, Is.EqualTo(this._Source.CreatedDate));
            Assert.That(result.LastUpdatedOn, Is.EqualTo(this._Source.UpdatedDate));
        }


    }
}