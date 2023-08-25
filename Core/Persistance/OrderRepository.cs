using Core.Domain;
using Core.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Core.Persistance;

public class OrderRepository : IOrderRepository
{
    IAppDbContext _db;

    public OrderRepository(IAppDbContext db)
    {
        _db = db;
    }


    public async Task CreateNew(Order order)
    {
        await _db.Orders.AddAsync(order);
    }


    public IQueryable<Order> PageAsync(int pageNumber, int pagesize)
    {
        return _db.Orders.Skip(pageNumber * pagesize).Take(pagesize);
    }


    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await _db.BeginTransactionAsync();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _db.SaveChangesAsync();
    }
}