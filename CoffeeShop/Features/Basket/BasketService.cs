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

    public async Task<BuyerBasket> AddItemToBasket(int basketId, int itemVariantId, int quantity = 1)
    {
        var basket = await _context.Baskets.FindAsync(basketId);

        if (basket == null)
            return null;

        var item = await _context.ItemVariants.FindAsync(itemVariantId);

        if (item == null)
            return null;

        basket.AddItem(itemVariantId, item.Price, quantity);
        await _context.SaveChangesAsync();
        return basket;
    }

    public async Task<bool> RemoveItemFromBasket(int basketId, int itemVariantId)
    {
        var basket = await _context.Baskets.Include(b => b.Items).FirstOrDefaultAsync(b => b.BasketId == basketId);

        if (basket == null)
            return false;

        basket.RemoveItem(itemVariantId);
        await _context.SaveChangesAsync();
        return true;
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

    public async Task<bool> DeleteAllItems(int basketId)
    {
        var basket = await _context.Baskets.FindAsync(basketId);
        if (basket == null)
            return false;

        basket.RemoveAllItems();
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ClearBasket(int basketId)
    {
        var items = await _context.BasketItems.FindAsync(basketId);

        if (items == null)
            return false;

        var basket = await _context.Baskets.Include(b => b.Items).FirstOrDefaultAsync(b => b.BasketId == basketId);

        if (basket == null)
            return false;

        basket.ClearBasket();
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<BuyerBasket> ViewBasket(int basketId)
    {
        var basket = await _context.Baskets.Include(b => b.Items).AsNoTracking().FirstOrDefaultAsync(b => b.BasketId == basketId);
        if (basket == null)
            return null;

        return basket;
    }
}
