namespace Coop.Interview.Api.Core.Games;

public readonly record struct PlayerAction
{
    public string Value { get; }
    
    public PlayerAction(string? action)
    {
        if (string.IsNullOrWhiteSpace(action)) 
            throw new ArgumentNullException(nameof(action));

        Value = action.ToLower();
    }
}