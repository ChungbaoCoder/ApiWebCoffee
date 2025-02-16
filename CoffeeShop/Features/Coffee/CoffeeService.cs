﻿using CoffeeShop.Database;
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
        _context.CoffeeItems.Add(item);
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task<bool> DeleteItem(int coffeeItemId)
    {
        var item = await _context.CoffeeItems.FindAsync(coffeeItemId);

        if (item == null)
            return false;

        _context.CoffeeItems.Remove(item);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<CoffeeItem> GetById(int coffeeItemId)
    {
        var item = await _context.CoffeeItems.FindAsync(coffeeItemId);

        if (item == null)
            return null;

        var availability = await _context.Availability.FirstOrDefaultAsync(a => a.CoffeeItemId == coffeeItemId);
        var customization = await _context.Customizations.FirstOrDefaultAsync(c => c.CoffeeItemId == coffeeItemId);

        item.Customization = customization;
        item.Availability = availability;
        return item;
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

    public async Task<CoffeeItem> UpdateItem(int coffeeItemId, CoffeeItem item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item), "Sản phẩm không có dữ liệu");

        var existItem = await _context.CoffeeItems.FindAsync(coffeeItemId);

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

            existItem.Customization.AddTopping(item.Customization.Topping);
        }

        if (item.Customization.Flavor != null)
        {
            var flavorsToRemove = existItem.Customization.Flavor.Except(item.Customization.Flavor).ToList();
            foreach (var flavor in flavorsToRemove)
            {
                existItem.Customization.RemoveFlavor(flavor);
            }

            existItem.Customization.AddFlavor(item.Customization.Flavor);
        }
        await _context.SaveChangesAsync();
        return existItem;
    }
}
