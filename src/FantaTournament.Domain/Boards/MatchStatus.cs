using System.Diagnostics.CodeAnalysis;
using Umbrella.Core.Domain;

namespace FantaTournament.Domain.Boards
{
    /// <summary>
    /// Match Status Value Object
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class MatchStatus : ValueObject, IKeyValuePairObject
    {
        /// <summary>
        /// The code of Item
        /// </summary>
        public string Code { get; set; } = "";
        /// <summary>
        /// The value of Item
        /// </summary>
        public string Value { get; set; } = "";

        /// <summary>
        /// Initializes a new instance of the <see cref="MatchStatus"/> class for Planend match
        /// </summary>
        public static MatchStatus Planned = new("PLANNED", "Planned");
        /// <summary>
        /// Initializes a new instance of the <see cref="MatchStatus"/> class for started  match
        /// </summary>
        public static MatchStatus Started = new("STARTED", "Started");
        /// <summary>
        /// Initializes a new instance of the <see cref="MatchStatus"/> class for palyed match
        /// </summary>
        public static MatchStatus Played = new("PLAYED", "Played");

        private MatchStatus(string code, string value)
        {
            Code = code;
            Value = value;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected override IEnumerable<object> GetEqualityComponents()
        {
            return [this.Code];
        }
        /// <summary>
        /// instanntiate a MatchStatus from its code
        /// </summary>
        public static MatchStatus FromCode(string code)
        {
            if (String.IsNullOrEmpty(code))
                throw new ArgumentNullException(nameof(code));

            switch (code)
            {
                case "PLANNED":
                    return MatchStatus.Planned;
                case "STARTED":
                    return MatchStatus.Started;
                case "PLAYED":
                    return MatchStatus.Played;
                default:
                    throw new NotImplementedException("Unabel to cast " + code + " into MatchType ValueObject");
            }
        }
    }
}