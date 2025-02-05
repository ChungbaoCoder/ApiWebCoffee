using CoffeeShop.Database;
using CoffeeShop.Interface;

namespace CoffeeShop.Features.BuyerUser;

public class BuyerService : IBuyerService
{
    private readonly CoffeeDbContext _context;

    public BuyerService(CoffeeDbContext context)
    {
        _context = context;
    }


}
