using Microsoft.Maui.Controls;
using System;
using System.IO;
using DukaInventory.Data;
using Microsoft.Maui;

namespace DukaInventory;

public partial class App : Application
{
	public static DatabaseService Database { get; private set; }

	public App()
	{
		InitializeComponent();

		string dbPath = Path.Combine(FileSystem.AppDataDirectory, "duka.db3");
        Database = new DatabaseService(dbPath);

    	MainPage = new NavigationPage(new MainPage());
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		return new Window(new AppShell());
	}
}