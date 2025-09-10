using Microsoft.Maui.Controls;
using DukaInventory.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace DukaInventory
{
    public partial class MainPage : ContentPage
    {
        ObservableCollection<Item> Items = new ObservableCollection<Item>();

        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadItems();
        }

        async Task LoadItems()
        {
            var list = await App.Database.GetItemsAsync();
            Items.Clear();
            foreach (var it in list) Items.Add(it);
            ItemsCollection.ItemsSource = Items;

            var low = await App.Database.GetLowStockItemsAsync();
            if (low.Any())
            {
                LowStockLabel.IsVisible = true;
                LowStockLabel.Text = $"Low stock: {string.Join(", ", low.Select(i => i.Name))}";
            }
            else LowStockLabel.IsVisible = false;
        }

        async void OnAddClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.AddItemPage());
        }

        async void OnItemSelected(object sender, SelectionChangedEventArgs e)
        {
            var item = e.CurrentSelection.FirstOrDefault() as Item;
            if (item == null) return;
            await DisplayActionSheet("Item actions", "Cancel", null, "Edit", "Delete");
            ItemsCollection.SelectedItem = null;
        }

        async void OnSellClicked(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.CommandParameter is int itemId)
            {
                await Navigation.PushAsync(new Views.RecordSalePage(itemId));
            }
        }

        async void OnSalesClicked(object sender, EventArgs e)
        {
            var salesPage = new Views.SalesReportPage(); // create below if you want detailed page; otherwise show total
            await Navigation.PushAsync(salesPage);
        }

        async void OnExportClicked(object sender, EventArgs e)
        {
            var items = await App.Database.GetItemsAsync();
            var sales = await App.Database.GetSalesAsync();
            // simple csv export helper
            var csv = Helpers.CsvExporter.ExportInventoryToCsv(items);
            var file = System.IO.Path.Combine(FileSystem.AppDataDirectory, "inventory_export.csv");
            System.IO.File.WriteAllText(file, csv);
            await DisplayAlert("Exported", $"Inventory exported to:\n{file}", "OK");
        }
    }
}
