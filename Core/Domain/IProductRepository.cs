namespace Core.Domain
{
    public interface IProductRepository
    {
        Task CreateNew(Product product);
        Task<Product> GetByIdAsync(long id);
        IQueryable<Product> PageAsync(int pageNumber, int pagesize);
        Task<int> SaveChangesAsync();
    }
}