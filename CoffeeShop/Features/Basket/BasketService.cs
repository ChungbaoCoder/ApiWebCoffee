using CoffeeShop.Database;
using CoffeeShop.Entities.GroupBasket;
using CoffeeShop.Interface;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Features.Basket;

public class BasketService : IBasketService
{
    private readonly CoffeeDbContext _context;
    public BasketService(CoffeeDbContext context)
    {
        _context = context;
    }

    public async Task<BuyerBasket> AddItemToBasket(int buyerId, int basketId, int coffeeItemId, int quantity = 1)
    {
        var basket = await _context.Baskets.Where(b => b.BuyerId == buyerId && b.BasketId == basketId).FirstOrDefaultAsync();

        if (basket == null)
            return null;

        var item = await _context.CoffeeItems.FindAsync(coffeeItemId);

        if (item == null)
            return null;

        basket.AddItem(coffeeItemId, item.Price, quantity);
        await _context.SaveChangesAsync();
        return basket;
    }

    public async Task<BuyerBasket> RemoveItemFromBasket(int basketId, int coffeeItemId)
    {
        var basket = await _context.Baskets.Include(b => b.Items).FirstOrDefaultAsync(b => b.BasketId == basketId);

        if (basket == null)
            return null;

        basket.RemoveItem(coffeeItemId);
        await _context.SaveChangesAsync();
        return basket;
    }

    public async Task<BuyerBasket> CreateBasketForUser(int buyerId)
    {
        var buyer = await _context.Buyer.FindAsync(buyerId);

        if (buyer == null)
            return null;

        var newbasket = new BuyerBasket(buyerId);
        await _context.Baskets.AddAsync(newbasket);
        await _context.SaveChangesAsync();
        return newbasket;
    }

    public async Task<bool> DeleteBasket(int basketId)
    {
        var basket = await _context.Baskets.FindAsync(basketId);
        if (basket == null)
            return false;

        _context.Baskets.Remove(basket);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<BuyerBasket> ClearBasket(int basketId)
    {
        var items = await _context.BasketItems.FirstOrDefaultAsync(bi => bi.BasketId == basketId);

        if (items == null)
            return null;

        var basket = await _context.Baskets.Include(b => b.Items).FirstOrDefaultAsync(b => b.BasketId == basketId);

        if (basket == null)
            return null;

        basket.ClearBasket();
        await _context.SaveChangesAsync();
        return basket;
    }
}
