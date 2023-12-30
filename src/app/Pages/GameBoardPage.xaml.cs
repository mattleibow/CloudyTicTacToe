using CommunityToolkit.Maui.Views;

namespace CloudyTicTacToe;

[QueryProperty(nameof(NewGame), "newgame")]
public partial class GameBoardPage : ContentPage
{
	public GameBoardPage()
	{
		InitializeComponent();
	}
	
	public bool NewGame { get; set; }
	
	private async void OnWinClicked(object sender, EventArgs e)
	{
		await this.ShowPopupAsync(new GameResultsPage
		{
			State = "Win",
			Size = new Size(
				Math.Max(320, Width * 0.85),
				Math.Max(480, Height * 0.85))
		});
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
