using CommunityToolkit.Maui.Views;

namespace CloudyTicTacToe.ClientApp;

public partial class GameBoardPage : ContentPage
{
	private GameViewModel _currentGame;

	public GameBoardPage()
	{
		InitializeComponent();

		_currentGame = new GameViewModel();
		_currentGame.GameOver += OnGameOver;
		
		BindingContext = _currentGame;
	}

	private async void OnGameOver(object? sender, GameOverEventArgs e)
	{
		var vm = new ResultsViewModel(e.State.Winner, e.State.Position);

		var result = await this.ShowPopupAsync(
			new GameResultsPage(vm)
			{
				Size = new Size(
					Math.Max(320, Width * 0.85),
					Math.Max(480, Height * 0.85))
			});

		if (result is true)
		{
			// start a new game
			_currentGame.Reset();
		}
		else
		{
			// go home
			await Shell.Current.GoToAsync("//home");
		}
	}

	private async void OnExitClicked(object sender, EventArgs e)
	{
		var canExit = false; // if not game over

		if (!canExit)
		{
			canExit = await DisplayAlert(
				"Exit Game",
				"The game is not yet over, are you sure you want to exit now?",
				"Yes, I am finished.",
				"No, continue playing.");

			// var result = await DisplayActionSheet(
			// 	"The game is not yet over, are you sure you want to exit now?",
			// 	"No, continue playing.",
			// 	"Yes, I am finished.");
		}

		if (canExit)
			await Shell.Current.GoToAsync("//home");
	}
}
