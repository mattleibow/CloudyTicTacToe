﻿namespace CloudyTicTacToe;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		
		Routing.RegisterRoute("game/results", typeof(GameResultsPage));
	}
}
