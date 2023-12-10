namespace Coop.Interview.Api.Core.Games;

public class GameEngine
{
    private readonly IProvideGameRules _rules;
    private readonly IMoveGenerator _moveGenerator;

    public GameEngine(
        IMoveGenerator moveGenerator,
        IProvideGameRules rules)
    {
        _rules = rules;
        _moveGenerator = moveGenerator;
    }
    
    public async Task<Result<GameResponse, ValidationFailed>> Play(PlayerAction action)
    {
        var playerMove = action.Value;

        if (!_rules.ValidChoices.Contains(playerMove))
            return await Task.FromResult(new ValidationFailed(ErrorMessage));
        
        var myMove = GetMyMove();

        var gameResult = (action, myMove) switch
        {
            _ when playerMove == myMove => "It's a draw",
            _ when _rules.WinningGames[playerMove].Contains(myMove) => "Winner",
            _ => "Loser"
        };

        return await Task.FromResult(new GameResponse($"I play {myMove}. {gameResult}!"));
    }

    private string GetMyMove()
    {
        IList<string> validChoices = _rules.ValidChoices.ToList();
        return validChoices[_moveGenerator.Next(validChoices.Count)];
    }

    public string ErrorMessage
    {
        get
        {
            var actions = string.Join(", ", _rules.ValidChoices);
            var lastComma = string.Join(", ", _rules.ValidChoices).LastIndexOf(',');
            if (lastComma != -1) actions = actions.Remove(lastComma, 1).Insert(lastComma, " or");
            return $"You have to play {actions}";
        }
    }
}