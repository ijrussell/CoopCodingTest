namespace Coop.Interview.Api.Core.Games;

public class RandomMoveGenerator : IMoveGenerator
{
    public int Next(int maxValue) => Random.Shared.Next(maxValue);
}