using SQLite;
using System;

namespace DukaInventory.Models
{
    public class Item
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Unique]
        public string SKU { get; set; } // optional unique code

        public string Name { get; set; }

        public string Unit { get; set; } = "pcs";

        public decimal CostPrice { get; set; }

        public decimal SellingPrice { get; set; }

        public int Quantity { get; set; }

        public int LowStockThreshold { get; set; } = 5;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
