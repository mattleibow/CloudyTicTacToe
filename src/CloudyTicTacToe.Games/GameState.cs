namespace CloudyTicTacToe.Games;

public readonly record struct GameState(bool IsOver, Player Winner = default, WinningPosition Position = default)
{
    public static readonly GameState Default = new(false, Player.NoOne, WinningPosition.None);

    public bool IsDraw =>
        IsOver && Winner == Player.NoOne;

    public bool IsWinner(Player player) =>
        IsOver && Winner != Player.NoOne && Winner == player;

    public bool IsLoser(Player player) =>
        IsOver && Winner != Player.NoOne && Winner != player;
}