using Core.Application;
using Core.Domain;
using Core.Infrastructure;
using Core.Persistance;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Moq.EntityFrameworkCore;

namespace test
{
    public class ProductServiceTest
    {
        [Test]
        public async Task increase_inventoryCount_of_product()
        {
            var memoryCache = new Mock<IMemoryCache>();
            var examDb = new Mock<IAppDbContext>();
            examDb.Setup(x => x.Products).ReturnsDbSet(new List<Product> {
            new Product("p1"){Id=1, InventoryCount = 3},
            });

            var productService = new ProductService(new ProductRepository(examDb.Object), memoryCache.Object);

            var newInventoryCount = await productService.IncreseInventoryCount(1, 4);

            Assert.AreEqual(7, newInventoryCount);
        }
    }
}