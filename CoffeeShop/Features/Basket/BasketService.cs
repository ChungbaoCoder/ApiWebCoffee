using CoffeeShop.Database;
using CoffeeShop.Entities.GroupBasket;
using CoffeeShop.Features.CustomExceptions;
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
        var basket = await _context.Baskets.Include(b => b.Items).FirstOrDefaultAsync(b => b.BasketId == basketId);

        if (basket == null)
            throw new NotFoundException("Không tìm thấy giỏ hàng");

        var item = await _context.ItemVariants.FirstOrDefaultAsync(iv => iv.Status != Entities.GroupItem.ItemStatus.Inactive && iv.ItemVariantId == itemVariantId);

        if (item == null)
            throw new NotFoundException("Không tìm thấy sản phẩm để thêm vào");

        if (item.StockQuantity >= quantity && item.StockQuantity > 0)
        {
            item.AddQuantity(-quantity);
            basket.AddItem(itemVariantId, item.Price, quantity);
            await _context.SaveChangesAsync();
        }
        else if (item.StockQuantity <= quantity)
        {
            quantity = item.StockQuantity;
            item.AddQuantity(-quantity);
            basket.AddItem(itemVariantId, item.Price, quantity);
            await _context.SaveChangesAsync();
        }

        return basket;
    }

    public async Task<bool> RemoveItemFromBasket(int basketId, int itemVariantId)
    {
        var basket = await _context.Baskets.Include(b => b.Items).FirstOrDefaultAsync(b => b.BasketId == basketId);

        if (basket == null)
            return false;

        var item = await _context.ItemVariants.FirstOrDefaultAsync(iv => iv.Status != Entities.GroupItem.ItemStatus.Inactive && iv.ItemVariantId == itemVariantId);

        if (item == null)
            return false;

        item.AddQuantity(basket.RemoveItem(itemVariantId));
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<BuyerBasket> CreateBasketForUser(int buyerId)
    {
        var buyer = await _context.Buyer.FindAsync(buyerId);

        if (buyer == null)
            throw new NotFoundException("Không tìm thấy người mua");

        var newbasket = new BuyerBasket(buyerId);
        await _context.Baskets.AddAsync(newbasket);
        await _context.SaveChangesAsync();
        return newbasket;
    }

    public async Task<bool> DeleteAllItems(int basketId)
    {
        var basket = await _context.Baskets.Include(b => b.Items).FirstOrDefaultAsync(b => b.BasketId == basketId);

        if (basket == null)
            return false;

        var restoreItem = basket.RemoveAllItems();
        var itemVariantKey = restoreItem.Keys;

        if (restoreItem.Count != 0)
        {
            var items = await _context.ItemVariants.Where(iv => itemVariantKey.Contains(iv.ItemVariantId)).ToListAsync();

            foreach (var item in restoreItem)
            {
                var itemToUpdate = items.First(i => i.ItemVariantId == item.Key);
                itemToUpdate.AddQuantity(item.Value);
            }
        }

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
            throw new NotFoundException("Không tìm thấy giỏ hàng");

        return basket;
    }
}
