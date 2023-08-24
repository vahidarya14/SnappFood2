using Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Core.Application;
public class ProductService : IProductService
{
    IProductRepository _repo;
    IMemoryCache _memoryCache;

    public ProductService(IProductRepository repo, IMemoryCache memoryCache)
    {
        _repo = repo;
        _memoryCache = memoryCache;
    }

    public async Task<ProductDto> GetByIdAsync(long productId)
    {
        if (_memoryCache.TryGetValue($"prod{productId}", out ProductDto? productDto))
            return productDto;
        
        var product = await _repo.GetByIdAsync(productId);
        if (product != null)
            productDto = new ProductDto(product.Title)
            {
                Id = product.Id,
                Discount = product.Discount,
                InventoryCount = product.InventoryCount,
                Price = product.Price,
            };

        UpdateCache(productDto);
        return productDto;
    }

    void UpdateCache(ProductDto productDto)
    {
        _memoryCache.Set($"prod{productDto.Id}", productDto);
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
        //product.InventoryCount = productDto.InventoryCount;
        product.Discount = productDto.Discount;
        product.Price = productDto.Price;

        var res = await _repo.SaveChangesAsync();

        _memoryCache.Remove($"prod{productId}");
        return res > 0;
    }

    public async Task<int> IncreseInventoryCount(long productId, int inventoryCount)
    {
        var product = await _repo.GetByIdAsync(productId);
        product.IncreseInventoryCount(inventoryCount);
        await _repo.SaveChangesAsync();

        _memoryCache.Remove($"prod{productId}");

        return product.InventoryCount;
    }

}
