using MarketplaceDemo.Data;
using MarketplaceDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace MarketplaceDemo.Core.Patterns
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllAsync();
        Task AddAsync(Product product);
    }

    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _db;
        public ProductRepository(AppDbContext db) => _db = db;

        public async Task<Product?> GetByIdAsync(int id) => await _db.Products.FindAsync(id);
        public async Task<IEnumerable<Product>> GetAllAsync() => await _db.Products.ToListAsync();

        public async Task AddAsync(Product product)
        {
            await _db.Products.AddAsync(product);
            await _db.SaveChangesAsync();
        }
    }
}
