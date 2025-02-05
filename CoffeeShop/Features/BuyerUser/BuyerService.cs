using CoffeeShop.Database;
using CoffeeShop.Entities.GroupBuyer;
using CoffeeShop.Interface;

namespace CoffeeShop.Features.BuyerUser;

public class BuyerService : IBuyerService
{
    private readonly CoffeeDbContext _context;

    public BuyerService(CoffeeDbContext context)
    {
        _context = context;
    }

    public async Task Register(string name, string email)
    {
        var result = await _context.Buyer.AddAsync(new Buyer(name, email));
        _context.SaveChanges();
    }
}
