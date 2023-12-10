namespace Coop.Interview.Api.Core.Games;

public interface IMoveGenerator
{
    int Next(int maxValue);
}