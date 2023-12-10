namespace Coop.Interview.Api.Core.Games.Rules;

public class RockPaperScissors : IProvideGameRules
{
    public const string Rock = "rock";
    public const string Paper = "paper";
    public const string Scissors = "scissors";

    public IDictionary<string, List<string>> WinningGames =>
        new Dictionary<string, List<string>>
        {
            { Rock, [Scissors] },
            { Paper, [Rock] },
            { Scissors, [Paper] }
        };

    public IEnumerable<string> ValidChoices =>
        new List<string> { Rock, Paper, Scissors };
    
}