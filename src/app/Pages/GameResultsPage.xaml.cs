using CommunityToolkit.Maui.Views;

namespace CloudyTicTacToe;

public partial class GameResultsPage : Popup
{
	public GameResultsPage()
	{
		InitializeComponent();
	}

	public string? State { get; set; }

	private async void OnNewGameClicked(object sender, EventArgs e)
	{
		await CloseAsync();
		await Shell.Current.GoToAsync("..?newgame=true");
	}

	private async void OnHomeClicked(object sender, EventArgs e)
	{
		await CloseAsync();
		await Shell.Current.GoToAsync("//home");
	}
}

