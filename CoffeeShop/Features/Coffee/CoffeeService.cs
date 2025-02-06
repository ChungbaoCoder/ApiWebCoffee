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

    public async Task<CoffeeItem> CreateItem(CoffeeItem item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));

        _context.CoffeeItems.Add(item);
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task<bool> DeleteItem(int coffeeItemId)
    {
        var existItem = await _context.CoffeeItems.FindAsync(coffeeItemId);

        if (existItem == null)
            return false;

        _context.CoffeeItems.Remove(existItem);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<CoffeeItem> GetById(int coffeeItemId)
    {
        var existItem = await _context.CoffeeItems.FindAsync(coffeeItemId);

        if (existItem == null)
            return null;

        return existItem;
    }

    public async Task<List<CoffeeItem>> ListItem(int page, int pageSize)
    {
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 10;

        var items = await _context.CoffeeItems
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();

        return items;
    }

    public async Task<CoffeeItem> UpdateItem(CoffeeItem item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item), "Coffee item cannot be null");

        var existItem = await _context.CoffeeItems.FindAsync(item.CoffeeItemId);
        if (existItem == null)
            return null;

        existItem.UpdateItem(item.Name, item.Description, item.Category, item.Size, item.PictureUri);
        existItem.UpdatePrice(item.Price);
        existItem.Availability.UpdateStatus(item.Availability.AvailableStatus, item.Availability.RestockDate);
        existItem.Availability.UpdateQuantity(item.Availability.StockQuantity);
        existItem.Customization.UpdateCustomization(item.Customization.MilkType, item.Customization.SugarLevel, item.Customization.Temperature);

        if (item.Customization.Topping != null)
        {
            var toppingsToRemove = existItem.Customization.Topping.Except(item.Customization.Topping).ToList();
            foreach (var topping in toppingsToRemove)
            {
                existItem.Customization.RemoveTopping(topping);
            }

            foreach (var topping in item.Customization.Topping)
            {
                if (!existItem.Customization.Topping.Contains(topping))
                {
                    existItem.Customization.AddTopping(topping);
                }
            }
        }

        if (item.Customization.Flavor != null)
        {
            var flavorsToRemove = existItem.Customization.Flavor.Except(item.Customization.Flavor).ToList();
            foreach (var flavor in flavorsToRemove)
            {
                existItem.Customization.RemoveFlavor(flavor);
            }

            foreach (var flavor in item.Customization.Flavor)
            {
                if (!existItem.Customization.Flavor.Contains(flavor))
                {
                    existItem.Customization.AddFlavor(flavor);
                }
            }
        }
        await _context.SaveChangesAsync();
        return existItem;
    }
}
