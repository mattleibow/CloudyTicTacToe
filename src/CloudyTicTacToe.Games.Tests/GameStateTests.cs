using Xunit;

namespace CloudyTicTacToe.Games.Tests;

public class GameStateTests
{
    [Theory]
    [InlineData(true, 0, true)]
    [InlineData(true, 1, false)]
    [InlineData(false, 0, false)]
    [InlineData(false, 1, false)]
    public void IsDrawIsCorrect(bool isOver, int player, bool expectedIsDraw)
    {
        var gs = new GameState(isOver, player);

        Assert.Equal(expectedIsDraw, gs.IsDraw);
    }
}
