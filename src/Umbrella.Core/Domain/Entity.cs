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
    /// <summary>
    /// Base implementation for a given entity
    /// </summary>
    [ExcludeFromCodeCoverage]
    public abstract class AuditableEntity : Entity, IAuditableEntity
    {
        #region IAuditableEntity Implementation

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string CreatedBy { get; set; } = "";
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string? UpdatedBy { get; set; }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public DateTime? UpdatedDate { get; set; }

        #endregion

        /// <summary>
        /// Marks the entity as modified
        /// </summary>
        public void SetAsModified(string? username)
        {
            this.UpdatedDate = DateTime.UtcNow;
            this.UpdatedBy = string.IsNullOrEmpty(username) ? "System" : username;
        }
    }


}