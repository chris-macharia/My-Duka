using SQLite;
using System;

namespace DukaInventory.Models
{
    public class Sale
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int ItemId { get; set; }

        public string ItemName { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Total => UnitPrice * Quantity;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
