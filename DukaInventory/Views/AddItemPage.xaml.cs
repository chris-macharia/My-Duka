using Microsoft.Maui.Controls;
using DukaInventory.Models;
using System;

namespace DukaInventory.Views
{
    public partial class AddItemPage : ContentPage
    {
        public AddItemPage()
        {
            InitializeComponent();
        }

        async void OnSaveClicked(object sender, EventArgs e)
        {
            var name = NameEntry.Text?.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                await DisplayAlert("Validation", "Please enter item name", "OK");
                return;
            }

            if (!int.TryParse(QtyEntry.Text, out int qty)) qty = 0;
            decimal.TryParse(CostEntry.Text, out decimal cost);
            decimal.TryParse(SellEntry.Text, out decimal sell);
            int.TryParse(LowStockEntry.Text, out int low);

            var item = new Item
            {
                Name = name,
                SKU = SkuEntry.Text,
                Quantity = qty,
                CostPrice = cost,
                SellingPrice = sell,
                LowStockThreshold = low > 0 ? low : 5
            };

            await App.Database.SaveItemAsync(item);
            await Navigation.PopAsync();
        }
    }
}
