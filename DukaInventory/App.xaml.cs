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
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        // Wrap MainPage in a NavigationPage if you want navigation support
        return new Window(new NavigationPage(new MainPage()));
        // OR if you're using Shell, replace with:
        // return new Window(new AppShell());
    }
}
