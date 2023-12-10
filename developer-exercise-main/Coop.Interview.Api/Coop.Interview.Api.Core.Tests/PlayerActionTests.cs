namespace Coop.Interview.Api.Core.Tests;

public class PlayerActionTests
{
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Invalid_Input(string? action)
    {
        Assert.Throws<ArgumentNullException>(() => new PlayerAction(action));
    }
}