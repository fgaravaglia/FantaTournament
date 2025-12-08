using Umbrella.Mapper;

namespace FantaTournament.Application.Tests.Mappers
{
    public abstract class BaseMapperTests<Tsource, Tdest> where Tsource : class, new() where Tdest : class, new()
    {
        protected IMapper<Tsource, Tdest> _Mapper;
        protected Tsource? _Source;

        protected abstract void InstanceSource();

        protected abstract void AssertDestinationIsMappedAsExpected(Tdest? result);

        [Test]
        public void Map_ReturnsNull_IfSourceIsNull()
        {
            //******* GIVEN
            this._Source = null;

            //******* WHEN
            var result = _Mapper.Map(this._Source);

            //******* THEN
            Assert.IsNull(result);
            Assert.Pass();
        }

        [Test]
        public void Map_Returns_Expected_Destination()
        {
            //******* GIVEN
            InstanceSource();

            //******* WHEN
            var result = _Mapper.Map(this._Source);

            //******* THEN
            AssertDestinationIsMappedAsExpected(result);
            Assert.Pass();
        }
    }
}