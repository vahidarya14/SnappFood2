using Microsoft.EntityFrameworkCore.Storage;

namespace Core.Domain
{
    public interface IOrderRepository
    {
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CreateNew(Order order);
        IQueryable<Order> PageAsync(int pageNumber, int pagesize);
        Task<int> SaveChangesAsync();
    }
}