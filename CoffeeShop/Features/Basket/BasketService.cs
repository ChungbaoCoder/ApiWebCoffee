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
        var existBasket = await _context.Baskets.FirstOrDefaultAsync(b => b.BuyerId == buyerId);

        if (existBasket == null)
            throw new InvalidOperationException("Không tìm thấy được giỏ hàng cho người dùng.");

        existBasket.AddItem(coffeeItemId, price, quantity);
        await _context.SaveChangesAsync();
        return existBasket;
    }

    public async Task<BuyerBasket> ClearBasket(int basketId)
    {
        var existBasket = await _context.Baskets.FirstOrDefaultAsync(b => b.BasketId == basketId);

        if (existBasket == null)
            throw new InvalidOperationException("Không tìm thấy giỏ hàng.");

        existBasket.RemoveEmptyItem();
        await _context.SaveChangesAsync();
        return existBasket;
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
}
