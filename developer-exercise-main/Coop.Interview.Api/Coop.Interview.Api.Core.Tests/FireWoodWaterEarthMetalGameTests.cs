namespace Coop.Interview.Api.Core.Tests;

public class FireWoodWaterEarthMetalGameTests
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
            new object[] { FireWoodWaterEarthMetal.Fire },
            new object[] { FireWoodWaterEarthMetal.Wood },
            new object[] { FireWoodWaterEarthMetal.Water },
            new object[] { FireWoodWaterEarthMetal.Earth },
            new object[] { FireWoodWaterEarthMetal.Metal },
        };

    public static IEnumerable<object[]> PlayerWinsGameData =>
        new List<object[]>
        {
            new object[] { FireWoodWaterEarthMetal.Fire, FireWoodWaterEarthMetal.Wood },
            new object[] { FireWoodWaterEarthMetal.Fire, FireWoodWaterEarthMetal.Metal },
            new object[] { FireWoodWaterEarthMetal.Wood, FireWoodWaterEarthMetal.Earth },
            new object[] { FireWoodWaterEarthMetal.Wood, FireWoodWaterEarthMetal.Water },
            new object[] { FireWoodWaterEarthMetal.Water, FireWoodWaterEarthMetal.Fire },
            new object[] { FireWoodWaterEarthMetal.Water, FireWoodWaterEarthMetal.Metal },
            new object[] { FireWoodWaterEarthMetal.Earth, FireWoodWaterEarthMetal.Fire },
            new object[] { FireWoodWaterEarthMetal.Earth, FireWoodWaterEarthMetal.Water },
            new object[] { FireWoodWaterEarthMetal.Metal, FireWoodWaterEarthMetal.Wood },
            new object[] { FireWoodWaterEarthMetal.Metal, FireWoodWaterEarthMetal.Earth },
        };

    public static IEnumerable<object[]> PlayerLosesGameData =>
        new List<object[]>
        {
            new object[] { FireWoodWaterEarthMetal.Wood, FireWoodWaterEarthMetal.Fire },
            new object[] { FireWoodWaterEarthMetal.Metal, FireWoodWaterEarthMetal.Fire },
            new object[] { FireWoodWaterEarthMetal.Earth, FireWoodWaterEarthMetal.Wood },
            new object[] { FireWoodWaterEarthMetal.Water, FireWoodWaterEarthMetal.Wood },
            new object[] { FireWoodWaterEarthMetal.Fire, FireWoodWaterEarthMetal.Water },
            new object[] { FireWoodWaterEarthMetal.Metal, FireWoodWaterEarthMetal.Water },
            new object[] { FireWoodWaterEarthMetal.Fire, FireWoodWaterEarthMetal.Earth },
            new object[] { FireWoodWaterEarthMetal.Water, FireWoodWaterEarthMetal.Earth },
            new object[] { FireWoodWaterEarthMetal.Wood, FireWoodWaterEarthMetal.Metal },
            new object[] { FireWoodWaterEarthMetal.Earth, FireWoodWaterEarthMetal.Metal },
        };

    private static GameEngine CreateGameHost(string action)
    {
        var rules = new FireWoodWaterEarthMetal();
        var random = Substitute.For<IMoveGenerator>();
        var value = rules.ValidChoices.ToList().IndexOf(action);
        random.Next(Arg.Any<int>()).Returns(value);
        
        return new GameEngine(random, rules);
    }
}