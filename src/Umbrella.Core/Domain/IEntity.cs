namespace Umbrella.Core.Domain
{
    /// <summary>
    /// Abstraction of a simple Entity of the Domain
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Unique identifier for the entity
        /// </summary>
        public string Id { get; set; }
    }
}