using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Umbrella.Mapper
{
    /// <summary>
    /// Implementation of <see cref="IMapperRegistry"/>
    /// </summary>
    public class MapperRegistry : IMapperRegistry
    {
        readonly IEnumerable<IMapper> _Mappers;

        /// <summary>
        ///     Initializes a new instance of the <see cref="MapperRegistry"/> class.
        /// </summary>
        /// <param name="mappers"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public MapperRegistry(IEnumerable<IMapper> mappers)
        {
            this._Mappers = mappers ?? throw new ArgumentNullException(nameof(mappers));
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <typeparam name="Tsource"></typeparam>
        /// <typeparam name="Tdest"></typeparam>
        /// <returns></returns>
        public IMapper<Tsource, Tdest>? GetMapper<Tsource, Tdest>()
            where Tsource : class, new() where Tdest : class, new()
        {
            return this._Mappers
                .OfType<IMapper<Tsource, Tdest>>()
                .SingleOrDefault();

        }
    }
}