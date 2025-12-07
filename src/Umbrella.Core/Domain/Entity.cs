using System.Diagnostics.CodeAnalysis;


namespace Umbrella.Core.Domain
{
    /// <summary>
    /// Base implementation for a given entity
    /// </summary>
    [ExcludeFromCodeCoverage]
    public abstract class Entity : IEntity
    {
        /// <summary>
        /// < inheritdoc/>
        /// </summary>
        public string Id { get; set; } = Guid.NewGuid().ToString();
    }
}