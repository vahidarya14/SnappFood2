using Core.Application;
using Core.Domain;
using Core.Infrastructure;
using Core.Persistance;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Moq.EntityFrameworkCore;

namespace test
{
    public class OrderTest
    {
        [Test]
        public async Task increase_inventoryCount_of_product()
        {
            var p = new Product("p1") { Id = 4, InventoryCount = 10 };
            var memoryCache = new Mock<IMemoryCache>();
            var examDb = new Mock<IAppDbContext>();
            examDb.Setup(x => x.Products).ReturnsDbSet(new List<Product> {
            p
            });
            examDb.Setup(x => x.Users).ReturnsDbSet(new List<User> {
            new User(){Id=1},
            });
            examDb.Setup(x => x.Orders).ReturnsDbSet(new List<Order> {
            });
            examDb.Setup(x => x.BeginTransactionAsync()).Returns(Task.FromResult( new Mock<IDbContextTransaction>().Object ));

            var orderService = new OrderService(new ProductRepository(examDb.Object),new OrderRepository(examDb.Object),new UserRepository(examDb.Object), memoryCache.Object);

            var newInventoryCount = await orderService.Buy(1, 4);

            Assert.AreEqual(9, p.InventoryCount);
        }
    }
}
