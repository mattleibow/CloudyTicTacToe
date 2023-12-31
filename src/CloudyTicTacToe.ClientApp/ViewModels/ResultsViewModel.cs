using CloudyTicTacToe.Games;

namespace CloudyTicTacToe.ClientApp;

public class ResultsViewModel : BindableObject
{
    public ResultsViewModel(Player winner, WinningPosition position)
    {
        Winner = winner;
        Position = position;
    }
    
    public Player? Winner { get; }

    public WinningPosition Position { get; }
}
