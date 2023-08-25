using Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Core.Infrastructure;

public interface IAppDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken=default);
    Task<IDbContextTransaction> BeginTransactionAsync();
}
