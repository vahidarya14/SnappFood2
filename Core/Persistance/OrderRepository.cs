using Core.Domain;
using Core.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Core.Persistance;

public class OrderRepository : IOrderRepository
{
    AppDbContext _db;

    public OrderRepository(AppDbContext db)
    {
        _db = db;
    }


    public async Task CreateNew(Order order)
    {
        await _db.AddAsync(order);
    }


    public IQueryable<Order> PageAsync(int pageNumber, int pagesize)
    {
        return _db.Orders.Skip(pageNumber * pagesize).Take(pagesize);
    }


    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await _db.Database.BeginTransactionAsync();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _db.SaveChangesAsync();
    }
}