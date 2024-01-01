using CloudyTicTacToe.Games;

namespace CloudyTicTacToe;

public class BoardViewModel : BindableObject
{
    private GameBoard _board;

    public BoardViewModel(int dimension)
    {
        _board = new GameBoard(dimension);
    }

    public Player this[int position] =>
        _board.GetPlayer(position / _board.Dimension, position % _board.Dimension);
    
    public GameState State { get; private set; }

    public bool Move(Player player, int position)
    {
        var x = position / _board.Dimension;
        var y = position % _board.Dimension;

        // bail out if there is a player here
        if (_board.GetPlayer(x, y) != Player.NoOne)
            return false;

        _board.Play(player, x, y);

        State = _board.CalculateState();

        OnPropertyChanged("this");
        return true;
    }
}
