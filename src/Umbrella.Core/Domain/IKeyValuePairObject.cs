namespace Umbrella.Core.Domain
{
    /// <summary>
    /// Interface for Key-Value Pair Value Objects
    /// </summary>
    public interface IKeyValuePairObject
    {
        /// <summary>
        /// The code of Item
        /// </summary>
        string Code { get; set; }
        /// <summary>
        /// The value of Item
        /// </summary>
        string Value { get; set; }
    }
}