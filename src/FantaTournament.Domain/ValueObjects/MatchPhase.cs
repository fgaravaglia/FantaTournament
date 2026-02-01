using Umbrella.Core.Domain;

namespace FantaTournament.Domain.ValueObjects;


/// <summary>
/// Represents the different phases of the tournament.
/// </summary>
public class MatchPhase : ValueObject
{
    /// <summary>
    /// Gets the name of the phase.
    /// </summary>
    public string Name { get; }
    
    /// <summary>
    /// Gets the order of the phase.
    /// </summary>
    public int Order { get; }

    private MatchPhase(string name, int order)
    {
        Name = name;
        Order = order;
    }

    /// <summary>
    /// The initial phase where teams are divided into groups.
    /// </summary>
    public static readonly MatchPhase GroupStage = new("GroupStage", 1);
    
    /// <summary>
    /// The round of 16 knockout stage.
    /// </summary>
    public static readonly MatchPhase RoundOf16 = new("RoundOf16", 2);
    
    /// <summary>
    /// The quarter-finals knockout stage.
    /// </summary>
    public static readonly MatchPhase QuarterFinals = new("QuarterFinals", 3);
    
    /// <summary>
    /// The semi-finals knockout stage.
    /// </summary>
    public static readonly MatchPhase SemiFinals = new("SemiFinals", 4);
    
    /// <summary>
    /// The match to decide the 3rd and 4th place.
    /// </summary>
    public static readonly MatchPhase Final3_4 = new("Final3_4", 5);
    
    /// <summary>
    /// The final match to decide the champion (1st and 2nd place).
    /// </summary>
    public static readonly MatchPhase Final1_2 = new("Final1_2", 6);

    /// <inheritdoc/>
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
    }
}
