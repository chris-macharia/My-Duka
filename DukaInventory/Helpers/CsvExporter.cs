using System.Collections.Generic;
using System.Text;
using DukaInventory.Models;

namespace DukaInventory.Helpers
{
    public static class CsvExporter
    {
        public static string ExportInventoryToCsv(IEnumerable<Item> items)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Id,SKU,Name,Qty,UnitPrice,CostPrice,LowStockThreshold");
            foreach (var it in items)
            {
                sb.AppendLine($"{it.Id},{Safe(it.SKU)},{Safe(it.Name)},{it.Quantity},{it.SellingPrice},{it.CostPrice},{it.LowStockThreshold}");
            }
            return sb.ToString();
        }

        static string Safe(string s) => string.IsNullOrEmpty(s) ? "" : s.Replace(",", " ");
    }
}
