using CloudyTicTacToe.Games;

namespace CloudyTicTacToe.ClientApp;

public class GameOverEventArgs : EventArgs
{
    public GameOverEventArgs(GameState state)
    {
        State = state;
    }

    public GameState State { get; }
}
