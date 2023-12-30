using Xunit;

namespace CloudyTicTacToe.Games.Tests;

public class GameBoardTests
{
    private static readonly List<char> PlayerNumbers = ['-', 'X', 'O'];
    
    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public void DimensionConstructorThrowsOnInvalidDimension(int dimension)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new GameBoard(dimension));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    public void DimensionConstructorIsCorrect(int dimension)
    {
        var board = new GameBoard(dimension);

        Assert.Equal(dimension, board.Dimension);
    }

    [Fact]
    public void EmptyBoardIsValid()
    {
        var board = Generate(
            """
            - - -
            - - -
            - - -
            """);
        
        Assert.Equal(3, board.Dimension);
        
        var state = board.CalculateState();

        Assert.Equal(GameState.Default, state);
    }

    [Fact]
    public void IncompleteGameIsDetected()
    {
        var board = Generate(
            """
            O - -
            - X -
            - - X
            """);
        
        Assert.Equal(3, board.Dimension);
        
        var state = board.CalculateState();

        Assert.Equal(GameState.Default, state);
    }

    [Fact]
    public void WinningBoardIsDetected()
    {
        var board = Generate(
            """
            X X X
            - O O
            - O -
            """);
        
        Assert.Equal(3, board.Dimension);

        var state = board.CalculateState();

        var expectedState = new GameState(
            true,
            PlayerNumbers.IndexOf('X'),
            new WinningPosition(WinningType.Horizontal, 0));

        Assert.Equal(expectedState, state);
    }

    private static GameBoard Generate(string board)
    {
        var rows = board.Split(new[] { '\n', '\r' },
            StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        Assert.True(rows.Length > 0,
            $"Expected at least 1 row, but found {rows.Length} instead.");

        var b = new Player[rows.Length, rows.Length];

        for (var y = 0; y < rows.Length; y++)
        {
            var row = rows[y].Replace(" ", "");

            Assert.True(row.Length == rows.Length,
                $"Expected row {y} to have {rows.Length} columns, but found {row.Length} instead.");

            for (var x = 0; x < rows.Length; x++)
            {
                var pos = row[x];
                var player = new Player(PlayerNumbers.IndexOf(pos));

                Assert.True(player.Number != -1,
                    $"Expected a valid player, but found {pos} instead.");

                b[x, y] = player;
            }
        }

        var gb = new GameBoard(b);

        return gb;
    }
}
