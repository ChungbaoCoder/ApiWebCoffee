using CoffeeShop.Database.Configurations;
using CoffeeShop.Entities.GroupBasket;
using CoffeeShop.Entities.GroupItem;
using CoffeeShop.Entities.GroupOrder;
using CoffeeShop.Entities.GroupUser;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Database;

public class CoffeeDbContext : DbContext
{
    public DbSet<Item> Products { get; set; }
    public DbSet<Basket> Baskets { get; set; }
    public DbSet<BasketItem> Items { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Buyer> Buyer { get; set; }

    public CoffeeDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ItemConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
