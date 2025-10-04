using MarketplaceDemo.Data;

namespace MarketplaceDemo.Core.Patterns
{
    public interface IUnitOfWork
    {
        IProductRepository Products { get; }
        Task<int> SaveChangesAsync();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;
        public UnitOfWork(AppDbContext db) => _db = db;

        public IProductRepository Products => new ProductRepository(_db);
        public Task<int> SaveChangesAsync() => _db.SaveChangesAsync();
    }
}
