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

    public async Task<BuyerBasket> AddItemToBasket(int buyerId, int coffeeItemId, decimal price, int quantity = 1)
    {
        var basket = await _context.Baskets.FirstOrDefaultAsync(b => b.BuyerId == buyerId);

        if (basket == null)
            return null;

        basket.AddItem(coffeeItemId, price, quantity);
        await _context.SaveChangesAsync();
        return basket;
    }

    public async Task<BuyerBasket> RemoveItemFromBasket(int basketId, int basketItemId)
    {
        var basket = await _context.Baskets.FirstOrDefaultAsync(b => b.BasketId == basketId);

        if (basket == null)
            return null;

        basket.RemoveItem(basketItemId);
        await _context.SaveChangesAsync();
        return basket;
    }

    public async Task<BuyerBasket> CreateBasketForUser(int buyerId)
    {
        var existBasket = await _context.Baskets.FindAsync(buyerId);

        if (existBasket != null)
            return existBasket;

        var basket = new BuyerBasket(buyerId);
        await _context.Baskets.AddAsync(basket);
        await _context.SaveChangesAsync();
        return basket;
    }

    public async Task<bool> DeleteBasket(int basketId)
    {
        var existBasket = await _context.Baskets.FindAsync(basketId);
        if (existBasket == null)
            return false;

        _context.Baskets.Remove(existBasket);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<BuyerBasket> ClearBasket(int basketId)
    {
        var basket = await _context.Baskets.FirstOrDefaultAsync(b => b.BasketId == basketId);

        if (basket == null)
            return null;

        basket.ClearBasket();
        await _context.SaveChangesAsync();
        return basket;
    }
}
