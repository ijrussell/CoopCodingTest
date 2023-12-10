using Coop.Interview.Api.Core.Games;

namespace Coop.Interview.Api.Http.Games;

public static class GameEndpoints
{
    public static void MapGameEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("game");
        
        group.MapPost("play", Play);
        group.MapPost("playmultiple", PlayMultiple);
    }

    private static async Task<IResult> Play(
        PlayRequest request,
        GameEngine game)
    {
        var playerAction = new PlayerAction(request.Action);
        var result = await game.Play(playerAction);

        return result.Match<IResult>(
            success => Results.Ok(success.Message),
            failure => Results.BadRequest(failure.Error));
    }

    private static async Task<IResult> PlayMultiple(
        MultiplePlayRequest request,
        GameEngine game)
    {
        var tasks = request.Actions.Select(action =>
        {
            var playerAction = new PlayerAction(action);
            return game.Play(playerAction);
        }).ToList();
        var results = await Task.WhenAll(tasks);
        var messages = results.Select(result =>
        {
            return result.Match<string>(
                success => success.Message,
                failure => failure.Error);
        });
        return Results.Ok(messages);
    }
}