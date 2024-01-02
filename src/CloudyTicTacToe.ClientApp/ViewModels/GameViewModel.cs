using CloudyTicTacToe.Games;

namespace CloudyTicTacToe.ClientApp;

public class GameViewModel : BindableObject
{
    private const int GameDimension = 3;

    private readonly IReadOnlyList<Player> _players;

    public GameViewModel()
    {
        _players = new List<Player>
        {
            new Player(1),
            new Player(2)
        };
        
        MoveCommand = new Command<string>(ExecuteMove);
        
        Reset();
    }

    public Command<string> MoveCommand { get; }

    public BoardViewModel Board { get; private set; }

    public Player CurrentPlayer { get; private set; }
    
    public event EventHandler<GameOverEventArgs>? GameOver;

    private void ExecuteMove(string position) =>
        ExecuteMove(int.Parse(position));

    private void ExecuteMove(int position)
    {
        // try make the move
        if (Board.Move(CurrentPlayer, position))
        {
            // select next player
            var idx = _players.IndexOf(CurrentPlayer);
            CurrentPlayer = _players[(idx + 1) % _players.Count];
            
            // update the UI
            OnPropertyChanged(nameof(CurrentPlayer));
            OnPropertyChanged(nameof(Board));

            // trigger events for any UI operations
            if (Board.State.IsOver)
            {
                GameOver?.Invoke(this, new GameOverEventArgs(Board.State));
            }
        }
    }

    public void Reset()
    {
        CurrentPlayer = _players[0];
        Board = new BoardViewModel(GameDimension);
        
        OnPropertyChanged(nameof(CurrentPlayer));
        OnPropertyChanged(nameof(Board));
    }
}
