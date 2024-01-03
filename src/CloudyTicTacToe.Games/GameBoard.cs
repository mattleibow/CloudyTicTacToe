namespace CloudyTicTacToe.Games;

public class GameBoard
{
    private readonly Player[,] _board;

    public GameBoard(int dimension)
    {
        if (dimension <= 0)
            throw new ArgumentOutOfRangeException(nameof(dimension));
        
        _board = new Player[dimension, dimension];
        Dimension = dimension;
    }

    public GameBoard(Player[,] board)
    {
        if (board.Rank != 2)
            throw new ArgumentOutOfRangeException(nameof(board),
                $"Board array should have a rank of 2, a 2-dimensional array. Board was {board.Rank}-dimensional.");

        var dimension = board.GetLength(0);

        if (board.GetLength(1) != dimension)
            throw new ArgumentOutOfRangeException(nameof(board),
                $"Board should have equal lengths for all dimensions. Board was {dimension}x{board.GetLength(1)}.");

        _board = new Player[dimension, dimension];
        for (var y = 0; y < dimension; y++)
        {
            for (var x = 0; x < dimension; x++)
            {
                _board[x, y] = board[x, y];
            }
        }

        Dimension = dimension;
    }
    
    public int Dimension { get; }

    public Player GetPlayer(int x, int y) =>
        _board[x, y];

    public void Play(Player player, int x, int y)
    {
        if (x < 0 || x > Dimension)
            throw new ArgumentOutOfRangeException(nameof(x));
        if (y < 0 || y > Dimension)
            throw new ArgumentOutOfRangeException(nameof(y));

        if (_board[x, y] != Player.NoOne)
            throw new ArgumentOutOfRangeException($"There already was a player ({_board[x, y]}) at position {x}x{y}.");

        DangerousPlay(player, x, y);
    }

    internal void DangerousPlay(Player player, int x, int y)
    {
        _board[x, y] = player;
    }

    public GameState CalculateState()
    {
        // get all the players on the board (including open places)
        var players = GetPlayers();
        
        // find if there are any winners
        foreach (var player in players)
        {
            // skip over empty player
            if (player == Player.NoOne)
                continue;
            
            // we may have a winner
            var winning = CalculateWinningPosition(player);
            if (winning.Type != WinningType.None)
                return new GameState(true, player, winning);
        }

        // no-one won, and there are empty places, thus we are still in progress
        if (players.Contains(Player.NoOne))
            return new GameState(false);
        
        // no-one won and there are no empty places, thus we have a draw
        return new GameState(true);
    }

    private WinningPosition CalculateWinningPosition(Player player)
    {
        // horizontal lines
        for (var y = 0; y < Dimension; y++)
        {
            if (DidWinHorizontal(player, y))
                return new WinningPosition(WinningType.Horizontal, y);
        }

        // vertical lines
        for (var x = 0; x < Dimension; x++)
        {
            if (DidWinVertical(player, x))
                return new WinningPosition(WinningType.Vertical, x);
        }

        // diagonals
        if (DidWinDiagonalTopLeft(player))
            return new WinningPosition(WinningType.Diagonal, 0);
        if (DidWinDiagonalTopRight(player))
            return new WinningPosition(WinningType.Diagonal, Dimension - 1);
     
        return WinningPosition.None;
    }

    private bool DidWinHorizontal(Player player, int y)
    {
        for (var x = 0; x < Dimension; x++)
        {
            if (_board[x, y] != player)
                return false;
        }

        return true;
    }

    private bool DidWinVertical(Player player, int x)
    {
        for (var y = 0; y < Dimension; y++)
        {
            if (_board[x, y] != player)
                return false;
        }

        return true;
    }

    private bool DidWinDiagonalTopLeft(Player player)
    {
        for (var d = 0; d < Dimension; d++)
        {
            if (_board[d, d] != player)
                return false;
        }

        return true;
    }

    private bool DidWinDiagonalTopRight(Player player)
    {
        var m = Dimension - 1;
        for (var d = 0; d < Dimension; d++)
        {
            if (_board[m - d, d] != player)
                return false;
        }

        return true;
    }

    private IReadOnlyCollection<Player> GetPlayers()
    {
        var players = new HashSet<Player>();
     
        foreach (var player in _board)
        {
            players.Add(player);
        }

        return players;
    }
}