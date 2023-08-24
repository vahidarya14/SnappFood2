using Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }

    public AppDbContext(DbContextOptions contextOptions) : base(contextOptions)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product >().Property(x=>x.Title).HasMaxLength(40);

        modelBuilder.Entity<User >().Property(x=>x.Name).HasMaxLength(40);
        modelBuilder.Entity<User>().HasData(new List<User>
        {
            new User(){Id=1,Name="arash"},
            new User(){Id=2,Name="darush"},
            new User(){Id=3,Name="arya"},
            new User(){Id=4,Name="tahmine"}
        });

        base.OnModelCreating(modelBuilder);
    }
}
