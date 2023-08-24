using Core.Domain;
using Core.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Core.Application;
public class ProductService : IProductService
{
    IProductRepository _repo;

    public ProductService(IProductRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<ProductDto>> ListAsync(int pageNumber, int pagesize)
    {
        pagesize = pagesize < 10 ? 10 : pagesize;

        return await _repo.PageAsync(pageNumber, pagesize)
            .Select(x => new ProductDto(x.Title)
            {
                Id = x.Id,
                Price = x.Price,
                Discount = x.Discount,
                InventoryCount = x.InventoryCount,
            })
            .ToListAsync();
    }


    public async Task<long> CreateNew(ProductCreateUpdateDto productDto)
    {
        var product = new Product(productDto.Title)
        {
            InventoryCount = productDto.InventoryCount,
            Discount = productDto.Discount,
            Price = productDto.Price,
        };
        await _repo.CreateNew(product);
        await _repo.SaveChangesAsync();
        return product.Id;
    }


    public async Task<bool> Update(long productId, ProductCreateUpdateDto productDto)
    {
        var product = await _repo.GetByIdAsync(productId);
        product.InventoryCount = productDto.InventoryCount;
        product.Discount = productDto.Discount;
        product.Price = productDto.Price;

        var res = await _repo.SaveChangesAsync();
        return res > 0;
    }

    public async Task<int> IncreseInventoryCount(long productId, int inventoryCount)
    {
        var producy = await _repo.GetByIdAsync(productId);
        producy.IncreseInventoryCount(inventoryCount);
        await _repo.SaveChangesAsync();
        return producy.InventoryCount;
    }

}
