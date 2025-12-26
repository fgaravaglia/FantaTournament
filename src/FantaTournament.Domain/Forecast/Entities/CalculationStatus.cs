

using Umbrella.Core.Domain;

namespace FantaTournament.Domain.Forecast.Entities
{
    public class CalculationStatus : ValueObject, IKeyValuePairObject
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
        /// Initializes a new instance of the <see cref="CalculationStatus"/> class for To Start status
        /// </summary>
        public static CalculationStatus ToStart = new("TOSTART", "To Start");
        /// <summary>
        /// Initializes a new instance of the <see cref="CalculationStatus"/> class for In Progress status
        /// </summary>
        public static CalculationStatus InProgress = new("PROGRESS", "In Progress");
        /// <summary>
        ///     Initializes a new instance of the <see cref="CalculationStatus"/> class for Completed status
        /// </summary>
        public static CalculationStatus Completed = new("COMPLETED", "Completed");

        /// <summary>
        ///     
        /// </summary>
        public CalculationStatus() : base()
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="CalculationStatus"/> class
        /// </summary>
        /// <param name="code"></param>
        /// <param name="value"></param>
        private CalculationStatus(string code, string value)
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
        public static CalculationStatus FromCode(string code)
        {
            if (String.IsNullOrEmpty(code))
                throw new ArgumentNullException(nameof(code));

            switch (code)
            {
                case "TOSTART":
                    return CalculationStatus.ToStart;
                case "PROGRESS":
                    return CalculationStatus.InProgress;
                case "COMPLETED":
                    return CalculationStatus.Completed;
                default:
                    throw new NotImplementedException("Unable to cast " + code + " into CalculationStatus ValueObject");
            }
        }

    }
}