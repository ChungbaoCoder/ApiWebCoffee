using CoffeeShop.Database.Configurations;
using CoffeeShop.Entities.GroupBasket;
using CoffeeShop.Entities.GroupItem;
using CoffeeShop.Entities.GroupOrder;
using CoffeeShop.Entities.GroupUser;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Database;

public class CoffeeDbContext : DbContext
{
    public CoffeeDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<CoffeeItem> CoffeeItems { get; set; }
    public DbSet<Basket> Baskets { get; set; }
    public DbSet<BasketItem> BasketItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Buyer> Buyer { get; set; }
    public DbSet<Address> Address { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BasketItemConfiguration());
        modelBuilder.ApplyConfiguration(new BasketConfiguration());
        modelBuilder.ApplyConfiguration(new AddressConfiguration());
        modelBuilder.ApplyConfiguration(new BuyerConfiguration());
        modelBuilder.ApplyConfiguration(new CoffeeItemConfiguration());
        modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
        modelBuilder.ApplyConfiguration(new OrderConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
