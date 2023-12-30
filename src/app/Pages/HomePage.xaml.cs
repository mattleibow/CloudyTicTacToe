namespace CloudyTicTacToe;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
	}

	private async void OnPlayClicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("//game");
	}
}
