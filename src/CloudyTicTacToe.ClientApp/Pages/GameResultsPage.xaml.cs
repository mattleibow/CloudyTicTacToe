using CloudyTicTacToe.Games;
using CommunityToolkit.Maui.Views;

namespace CloudyTicTacToe.ClientApp;

public partial class GameResultsPage : Popup
{
	private readonly ResultsViewModel _results;

	public GameResultsPage(ResultsViewModel results)
	{
		InitializeComponent();
		
		_results = results;

		BindingContext = _results;
	}

	private async void OnNewGameClicked(object sender, EventArgs e)
	{
		await CloseAsync(true);
	}

	private async void OnHomeClicked(object sender, EventArgs e)
	{
		await CloseAsync(false);
	}
}
