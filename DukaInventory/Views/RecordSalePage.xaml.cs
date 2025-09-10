using Microsoft.Maui.Controls;
using DukaInventory.Models;
using System;

namespace DukaInventory.Views
{
    public partial class RecordSalePage : ContentPage
    {
        private int _itemId;
        private Item _item = null!;

        public RecordSalePage(int itemId)
        {
            InitializeComponent();
            _itemId = itemId;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            _item = await App.Database.GetItemAsync(_itemId);
            if (_item == null)
            {
                await DisplayAlert("Error", "Item not found", "OK");
                await Navigation.PopAsync();
                return;
            }

            ItemLabel.Text = $"{_item.Name} - Ksh {_item.SellingPrice:F2}";
            StockLabel.Text = $"Stock: {_item.Quantity}";
            PriceEntry.Text = _item.SellingPrice.ToString("F2");
        }

        async void OnRecordClicked(object sender, EventArgs e)
        {
            if (!int.TryParse(QtyEntry.Text, out int qty) || qty <= 0)
            {
                await DisplayAlert("Error", "Enter valid quantity", "OK");
                return;
            }

            if (!decimal.TryParse(PriceEntry.Text, out decimal price))
            {
                await DisplayAlert("Error", "Enter valid price", "OK");
                return;
            }

            var ok = await App.Database.RecordSaleAsync(_itemId, qty, price);
            if (!ok)
            {
                await DisplayAlert("Error", "Not enough stock or item missing", "OK");
                return;
            }

            await DisplayAlert("Success", "Sale recorded", "OK");
            await Navigation.PopAsync();
        }
    }
}
