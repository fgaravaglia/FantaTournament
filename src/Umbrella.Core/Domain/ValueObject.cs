using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Umbrella.Core.Domain
{
    /// <summary>
    /// Base implementation for a Value Object
    /// </summary>
    [ExcludeFromCodeCoverage]
    public abstract class ValueObject
    {
        #region Protected Methods

        ///<summary>
        /// Helper method to compare two value objects
        /// </summary>
        protected static bool EqualOperator(ValueObject left, ValueObject right)
        {
            if (left is null ^ right is null)
            {
                return false;
            }

            return left?.Equals(right!) != false;
        }
        /// <summary>
        /// Helper method to compare two value objects
        ///     </summary>
        protected static bool NotEqualOperator(ValueObject left, ValueObject right)
        {
            return !(EqualOperator(left, right));
        }
        /// <summary>
        ///     When implemented in derived classes, returns the components of the value object that are used to determine equality.
        /// </summary>
        /// <returns></returns>
        protected abstract IEnumerable<object> GetEqualityComponents();
        #endregion

        /// <summary>
        /// Overrides Equals to compare value objects based on their components
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            var other = (ValueObject)obj;
            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }
        /// <summary>
        /// Overrides GetHashCode to generate a hash code based on the value object's components
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }
    }
}
