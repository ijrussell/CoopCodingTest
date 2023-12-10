namespace Coop.Interview.Api.Core.Tests;

public class RockPaperScissorsGameTests
{
    [Theory]
    [MemberData(nameof(DrawnGameData))]
    public async Task Drawn_Game(string action)
    {
        var game = CreateGameHost(action);

        var actual = await game.Play(new PlayerAction(action));

        Assert.Equal(actual, new GameResponse($"I play {action}. It's a draw!"));
    }

    [Theory]
    [MemberData(nameof(PlayerLosesGameData))]
    public async Task Player_Loses_Game(string action, string myMove)
    {
        var game = CreateGameHost(myMove);

        var actual = await game.Play(new PlayerAction(action));

        Assert.Equal(actual, new GameResponse($"I play {myMove}. Loser!"));
    }

    [Theory]
    [MemberData(nameof(PlayerWinsGameData))]
    public async Task Player_Wins_Game(string action, string myMove)
    {
        var game = CreateGameHost(myMove);

        var actual = await game.Play(new PlayerAction(action));

        Assert.Equal(actual, new GameResponse($"I play {myMove}. Winner!"));
    }

    public static IEnumerable<object[]> DrawnGameData =>
        new List<object[]>
        {
            new object[] { RockPaperScissors.Rock },
            new object[] { RockPaperScissors.Paper },
            new object[] { RockPaperScissors.Scissors }
        };

    public static IEnumerable<object[]> PlayerWinsGameData =>
        new List<object[]>
        {
            new object[] { RockPaperScissors.Rock, RockPaperScissors.Scissors },
            new object[] { RockPaperScissors.Paper, RockPaperScissors.Rock },
            new object[] { RockPaperScissors.Scissors, RockPaperScissors.Paper }
        };

    public static IEnumerable<object[]> PlayerLosesGameData =>
        new List<object[]>
        {
            new object[] { RockPaperScissors.Scissors, RockPaperScissors.Rock },
            new object[] { RockPaperScissors.Rock, RockPaperScissors.Paper },
            new object[] { RockPaperScissors.Paper, RockPaperScissors.Scissors }
        };

    private static GameEngine CreateGameHost(string action)
    {
        var rules = new RockPaperScissors();
        var random = Substitute.For<IMoveGenerator>();
        var value = rules.ValidChoices.ToList().IndexOf(action);
        random.Next(Arg.Any<int>()).Returns(value);

        return new GameEngine(random, rules);
    }
}