using Microsoft.Maui.Controls;
using DukaInventory.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace DukaInventory.Views
{
    public partial class SalesReportPage : ContentPage
    {
        ObservableCollection<Sale> Sales = new ObservableCollection<Sale>();
        public SalesReportPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var list = await App.Database.GetSalesAsync();
            Sales.Clear();
            foreach (var s in list) Sales.Add(s);
            SalesCollection.ItemsSource = Sales;

            var total = await App.Database.GetTotalSalesTodayAsync();
            TotalLabel.Text = $"Total sales today: Ksh {total:F2}";
        }
    }
}
