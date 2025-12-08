namespace Umbrella.Mapper
{
    /// <summary>
    /// Abstraction for mapper registry
    /// </summary>
    public interface IMapperRegistry
    {
        /// <summary>
        /// Gets the mapper for the specified source and destination types
        /// </summary>
        /// <typeparam name="Tsource"></typeparam>
        /// <typeparam name="Tdest"></typeparam>
        /// <returns></returns>
        IMapper<Tsource, Tdest>? GetMapper<Tsource, Tdest>()
            where Tsource : class, new() where Tdest : class, new();
    }
}