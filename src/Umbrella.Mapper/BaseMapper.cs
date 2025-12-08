
namespace Umbrella.Mapper
{
    public abstract class BaseMapper<Tsource, Tdest> : IMapper<Tsource, Tdest>
        where Tsource : class, new() where Tdest : class, new()
    {
        /// <summary>
        /// Maps source object to destination object
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public object? MapToObject(object? source)
        {
            if (source is Tsource)
                return (Tdest?)Map((Tsource?)source);
            return null;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public abstract Tdest? Map(Tsource? source);

        /// <summary>
        /// Mpas properties by name, using reflection
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected Tdest MapByName(Tsource source)
        {
            ArgumentNullException.ThrowIfNull(source);

            // Create a new instance of the destination type and red property list
            Tdest dest = new Tdest();
            var sourceProps = typeof(Tsource).GetProperties();
            var destProps = typeof(Tdest).GetProperties();

            // then iterate over source props and map to dest when matching
            foreach (var sProp in sourceProps)
            {
                var dProp = destProps.SingleOrDefault(x => x.Name.Equals(sProp.Name, StringComparison.InvariantCultureIgnoreCase)
                                                        && x.PropertyType == sProp.PropertyType);
                if (dProp != null && dProp.CanWrite)
                {
                    var value = sProp.GetValue(source);
                    dProp.SetValue(dest, value);
                }
            }

            return dest;
        }
    }
}