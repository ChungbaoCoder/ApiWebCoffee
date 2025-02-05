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

    public Task<CoffeeItem> Create(CoffeeItem item)
    {
        var result = _context.CoffeeItems.Add(item);

        if (result == null)
        {
            throw new Exception();
        }
        _context.SaveChanges();
        return Task.FromResult(item);
    }

    public async Task<CoffeeItem> Delete(int coffeeItemId)
    {
        return await _context.CoffeeItems.FindAsync(coffeeItemId);
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
