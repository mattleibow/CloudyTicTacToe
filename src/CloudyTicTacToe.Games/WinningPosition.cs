namespace CloudyTicTacToe.Games;

public readonly record struct WinningPosition(WinningType Type, int Position)
{
    public static readonly WinningPosition None = new(WinningType.None, 0);
}