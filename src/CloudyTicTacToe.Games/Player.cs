namespace CloudyTicTacToe.Games;

public readonly record struct Player(int Number)
{
    public static readonly Player NoOne = new(0);
    
    public static implicit operator Player(int number) => new(number);

    public static explicit operator int(Player player) => player.Number;
}