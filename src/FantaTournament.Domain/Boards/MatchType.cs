using System.Diagnostics.CodeAnalysis;
using Umbrella.Core.Domain;

namespace FantaTournament.Domain.Boards
{
    /// <summary>
    /// Match Type Value Object
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class MatchType : ValueObject, IKeyValuePairObject
    {
        /// <summary>
        /// The code of Item
        /// </summary>
        public string Code { get; set; } = "";
        /// <summary>
        /// The value of Item
        /// </summary>
        public string Value { get; set; } = "";

        public static MatchType Round = new("ROUND", "Round");
        public static MatchType Match16th = new("16TH", "16th");
        public static MatchType Match8th = new("8TH", "8th");
        public static MatchType Match4th = new("4TH", "4th");
        public static MatchType Semifinal = new("SEMIFINAL", "Semifinal");
        public static MatchType Final3_4 = new("FINAL3-4", "Final 3rd - 4th");
        public static MatchType Final1_2 = new("FINAL1-2", "Final");

        private MatchType(string code, string value)
        {
            Code = code;
            Value = value;
        }

        /// <summary>
        /// Convert match type to integer, to order them
        /// </summary>
        /// <returns></returns>
        public int ToIntegerValue()
        {
            if (this.Code == MatchType.Round.Code)
                return 0;
            else if (this.Code == MatchType.Match16th.Code)
                return 5;
            else if (this.Code == MatchType.Match8th.Code)
                return 10;
            else if (this.Code == MatchType.Match4th.Code)
                return 15;
            else if (this.Code == MatchType.Semifinal.Code)
                return 20;
            else if (this.Code == MatchType.Final3_4.Code)
                return 25;
            else
                return 30;
        }
        /// <summary>
        /// instanntiate a MatchType from its code      
        /// </summary>
        public static MatchType FromCode(string code)
        {
            if (String.IsNullOrEmpty(code))
                throw new ArgumentNullException(nameof(code));

            switch (code.ToUpperInvariant())
            {
                case "ROUND":
                    return MatchType.Round;
                case "16TH":
                    return MatchType.Match16th;
                case "8TH":
                    return MatchType.Match8th;
                case "4TH":
                    return MatchType.Match4th;
                case "SEMIFINAL":
                    return MatchType.Semifinal;
                case "FINAL3-4":
                    return MatchType.Final3_4;
                case "FINAL1-2":
                    return MatchType.Final1_2;
                default:
                    throw new NotImplementedException("Unabel to cast " + code + " into MatchType ValueObject");
            }
        }

        /// <summary>
        /// <inheritdoc/>           
        /// </summary>
        protected override IEnumerable<object> GetEqualityComponents()
        {
            return [this.Code];
        }
    }
}