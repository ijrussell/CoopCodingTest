namespace Coop.Interview.Api.Core.Tests;

public class GameEngineTests
{
    [Theory]
    [InlineData("kcor")]
    public async Task Unexpected_Input(string? action)
    {
        var rules = new RockPaperScissors();
        var random = Substitute.For<IMoveGenerator>();
        var game = new GameEngine(random, rules);

        var actual = await game.Play(new PlayerAction(action));

        Assert.Equal(actual, new ValidationFailed(game.ErrorMessage));
    }
}