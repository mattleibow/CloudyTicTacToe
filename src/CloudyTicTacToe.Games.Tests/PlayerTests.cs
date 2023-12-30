using Xunit;

namespace CloudyTicTacToe.Games.Tests;

public class PlayerTests
{
    [Fact]
    public void NoOneIsOnlyPlayer0()
    {
        var noone = Player.NoOne;
        
        var player0 = new Player(0);
        var player1 = new Player(1);

        Assert.Equal(noone, player0);
        Assert.NotEqual(noone, player1);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    public void PlayerCanBeCreated(int number)
    {
        var player = new Player(number);
        
        Assert.Equal(number, player.Number);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    public void PlayerCanBeImplicitlyCreated(int number)
    {
        Player player = number;
        
        Assert.Equal(number, player.Number);
    }
}
