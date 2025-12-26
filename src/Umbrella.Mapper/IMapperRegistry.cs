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
        /// <summary>
        /// Gets the mapper for the specified source and destination types.
        /// </summary>
        /// <typeparam name="Tsource"></typeparam>
        /// <typeparam name="Tdest"></typeparam>
        /// <returns></returns>
        /// <exception cref="NUllReferenceException">Thrown if no mapper is found for the specified types.</exception>
        IMapper<Tsource, Tdest> GetRequiredMapper<Tsource, Tdest>()
            where Tsource : class, new()
            where Tdest : class, new();
    }
}