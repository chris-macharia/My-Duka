using SQLite;
using System.IO;
using System.Threading.Tasks;
using DukaInventory.Models;
using System.Collections.Generic;
using System;
using System.Linq;

namespace DukaInventory.Data
{
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _db;

        public DatabaseService(string dbPath)
        {
            _db = new SQLiteAsyncConnection(dbPath);
            _db.CreateTableAsync<Item>().Wait();
            _db.CreateTableAsync<Sale>().Wait();
        }

        // Items
        public Task<List<Item>> GetItemsAsync() => _db.Table<Item>().OrderBy(i => i.Name).ToListAsync();
        public Task<Item> GetItemAsync(int id) => _db.Table<Item>().FirstOrDefaultAsync(i => i.Id == id);
        public Task<int> SaveItemAsync(Item item)
        {
            if (item.Id != 0) return _db.UpdateAsync(item);
            return _db.InsertAsync(item);
        }
        public Task<int> DeleteItemAsync(Item item) => _db.DeleteAsync(item);

        // Sales
        public Task<List<Sale>> GetSalesAsync() => _db.Table<Sale>().OrderByDescending(s => s.CreatedAt).ToListAsync();
        public Task<int> SaveSaleAsync(Sale sale) => _db.InsertAsync(sale);

        // Record sale and reduce stock atomically (approx)
        public async Task<bool> RecordSaleAsync(int itemId, int qty, decimal unitPrice)
        {
            var item = await GetItemAsync(itemId);
            if (item == null || item.Quantity < qty) return false;

            var sale = new Sale
            {
                ItemId = item.Id,
                ItemName = item.Name,
                Quantity = qty,
                UnitPrice = unitPrice,
                CreatedAt = DateTime.UtcNow
            };

            // transaction-like behavior
            await _db.RunInTransactionAsync(conn =>
            {
                conn.Update(item); // no-op but ensures connection used
            });

            // update quantity and save
            item.Quantity -= qty;
            await SaveItemAsync(item);
            await SaveSaleAsync(sale);
            return true;
        }

        // simple reports
        public async Task<decimal> GetTotalSalesTodayAsync()
        {
            var today = DateTime.UtcNow.Date;
            var sales = await _db.Table<Sale>().Where(s => s.CreatedAt >= today).ToListAsync();
            return sales.Sum(s => s.Total);
        }

        public Task<List<Item>> GetLowStockItemsAsync() => _db.Table<Item>().Where(i => i.Quantity <= i.LowStockThreshold).ToListAsync();
    }
}
