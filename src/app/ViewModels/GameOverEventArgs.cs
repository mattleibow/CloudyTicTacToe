using CloudyTicTacToe.Games;

namespace CloudyTicTacToe;

public class GameOverEventArgs : EventArgs
{
    public GameOverEventArgs(GameState state)
    {
        State = state;
    }

    public GameState State { get; }
}
