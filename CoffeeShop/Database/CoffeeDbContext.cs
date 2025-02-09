using System.Reflection;
using CoffeeShop.Entities.GroupBasket;
using CoffeeShop.Entities.GroupItem;
using CoffeeShop.Entities.GroupOrder;
using CoffeeShop.Entities.GroupBuyer;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CoffeeShop.Infrastructure.Auth;

namespace CoffeeShop.Database;

public class CoffeeDbContext : IdentityDbContext<ApplicationUser>
{
    public CoffeeDbContext(DbContextOptions<CoffeeDbContext> options) : base(options)
    {
    }

    public DbSet<CoffeeItem> CoffeeItems { get; set; }
    public DbSet<BuyerBasket> Baskets { get; set; }
    public DbSet<BasketItem> BasketItems { get; set; }
    public DbSet<BuyerOrder> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<BuyerUser> Buyer { get; set; }
    public DbSet<Address> Address { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
