namespace Coop.Interview.Api.Core.Games.Rules;

/*
Fire burns Wood
Fire melts Metal
Wood displaces Earth
Wood absorbs Water
Water extinguishes Fire
Water rusts Metal
Earth extinguishes Fire
Earth absorbs Water
Metal cuts Wood
Metal displaces Earth
*/
public class FireWoodWaterEarthMetal : IProvideGameRules
{
    public const string Fire = "fire";
    public const string Wood = "wood";
    public const string Water = "water";
    public const string Earth = "paper";
    public const string Metal = "metal";

    public IDictionary<string, List<string>> WinningGames =>
        new Dictionary<string, List<string>>
        {
            { Fire, [Wood, Metal] },
            { Wood, [Earth, Water] },
            { Water, [Fire, Metal] },
            { Earth, [Fire, Water] },
            { Metal, [Wood, Earth] }
        };

    public IEnumerable<string> ValidChoices =>
        new List<string> { Fire, Wood, Water, Earth, Metal };
}