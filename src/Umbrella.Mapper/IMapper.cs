namespace Umbrella.Mapper
{
    /// <summary>
    /// Abstraction for mapping objects
    /// </summary>
    public interface IMapper
    {
        /// <summary>
        /// Maps source object to destination object
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        object? MapToObject(object? source);

    }
    /// <summary>
    /// Abstraction for mapping objects
    /// </summary>
    public interface IMapper<Tsource, Tdest> : IMapper
        where Tsource : class, new() where Tdest : class, new()
    {
        /// <summary>
        /// Maps source object to destination object
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        Tdest? Map(Tsource? source);
    }
}