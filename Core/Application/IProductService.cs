namespace Core.Application
{
    public interface IProductService
    {
        Task<long> CreateNew(ProductCreateUpdateDto productDto);
        Task<int> IncreseInventoryCount(long productId, int inventoryCount);
        Task<List<ProductDto>> ListAsync(int pageNumber, int pagesize);
        Task<bool> Update(long productId, ProductCreateUpdateDto productDto);
    }
}