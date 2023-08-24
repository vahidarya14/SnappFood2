using Core.Domain;
using Core.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Core.Persistance;

public class ProductRepository : IProductRepository
{
    AppDbContext _db;

    public ProductRepository(AppDbContext db)
    {
        _db = db;
    }
    public IQueryable<Product> PageAsync(int pageNumber, int pagesize)
    {
        return _db.Products.Skip(pageNumber * pagesize).Take(pagesize);
    }

    public async Task<Product> GetByIdAsync(long id)
    {
        return await _db.Products.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task CreateNew(Product product)
    {
        var allreadyWithThisName = await _db.Products.AnyAsync(x => x.Title == product.Title);
        if (allreadyWithThisName)
            throw new Exception("product with the same name allready exsists");

        await _db.AddAsync(product);
    }


    public async Task<int> SaveChangesAsync()
    {
        return await _db.SaveChangesAsync();
    }
}
