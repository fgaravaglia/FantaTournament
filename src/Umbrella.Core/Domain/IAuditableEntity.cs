using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Umbrella.Core.Domain
{
    /// <summary>
    /// Abstraction of an Auditable Entity of the Domain, to track creator and updater
    /// </summary>
    public interface IAuditableEntity : IEntity
    {
        /// <summary>
        /// User who created the entity
        /// </summary>
        string CreatedBy { get; set; }
        /// <summary>
        /// Date when the entity was created
        /// </summary>
        DateTime CreatedDate { get; set; }
        /// <summary>
        ///     User who last updated the entity
        /// </summary>
        string? UpdatedBy { get; set; }
        /// <summary>
        ///    Date when the entity was last updated
        /// </summary>
        DateTime? UpdatedDate { get; set; }
    }
}