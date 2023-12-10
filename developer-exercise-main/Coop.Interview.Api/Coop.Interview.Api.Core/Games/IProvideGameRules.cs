namespace Coop.Interview.Api.Core.Games;

public interface IProvideGameRules
{
    IDictionary<string, List<string>> WinningGames { get; }
    IEnumerable<string> ValidChoices { get; }
}