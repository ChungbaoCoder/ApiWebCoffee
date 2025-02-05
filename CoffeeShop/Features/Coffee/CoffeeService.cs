using CoffeeShop.Database;
using CoffeeShop.Entities.GroupItem;
using CoffeeShop.Interface;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Features.Coffee;

public class CoffeeService : ICoffeeItemService
{
    private readonly CoffeeDbContext _context;

    public CoffeeService(CoffeeDbContext context)
    {
        _context = context;
    }

    //static List<CoffeeItem> items = new List<CoffeeItem>
    //    {
    //        new CoffeeItem(
    //            "Caramel Latte", "A delicious blend of espresso, steamed milk, and caramel syrup.", "Latte", 4.99m, "Medium",
    //            "caramel-latte.jpg",
    //            new Customization("Flavor", "Caramel"),
    //            new Availability(true, DateTime.Now)
    //        ),
    //        new CoffeeItem(
    //            "Vanilla Cappuccino", "A creamy cappuccino with a hint of vanilla.", "Cappuccino", 3.99m, "Large",
    //            "vanilla-cappuccino.jpg",
    //            new Customization("Flavor", "Vanilla"),
    //            new Availability(false, DateTime.Now.AddHours(2))
    //        ),
    //        new CoffeeItem(
    //            "Espresso", "A bold and rich espresso shot.", "Espresso", 2.49m, "Small",
    //            "espresso.jpg",
    //            new Customization("Extras", "Extra Shot"),
    //            new Availability(true, DateTime.Now)
    //        ),
    //        new CoffeeItem(
    //            "Iced Americano", "Chilled espresso served over ice.", "Americano", 3.49m, "Medium",
    //            "iced-americano.jpg",
    //            new Customization("Ice Level", "Extra Ice"),
    //            new Availability(true, DateTime.Now)
    //        ),
    //        new CoffeeItem(
    //            "Mocha", "A perfect blend of chocolate and espresso, topped with whipped cream.", "Mocha", 5.29m, "Large",
    //            "mocha.jpg",
    //            new Customization("Toppings", "Whipped Cream"),
    //            new Availability (true, DateTime.Now)
    //        )
    //    };

    public Task<CoffeeItem> Create(CoffeeItem item)
    {
        var result = _context.CoffeeItems.Add(item);

        if (result == null )
        {
            throw new Exception();
        }
        _context.SaveChanges();
        return Task.FromResult(item);
    }

    public Task<CoffeeItem> Delete(int coffeeItemId)
    {
        throw new NotImplementedException();
    }

    public Task<CoffeeItem> GetById(int coffeeItemId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<CoffeeItem>> List()
    {
        return await _context.CoffeeItems.ToListAsync();
    }

    //var result = _context.CoffeeItems.ToListAsync();
    //return result;

    public Task<CoffeeItem> Update(CoffeeItem item)
    {
        throw new NotImplementedException();
    }
}
