using CoffeeShop.Database;
using CoffeeShop.Entities.GroupBuyer;
using CoffeeShop.Interface;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Features.Buyer;

public class BuyerService : IBuyerService
{
    private readonly CoffeeDbContext _context;

    public BuyerService(CoffeeDbContext context)
    {
        _context = context;
    }

    public async Task<BuyerUser> CreateBuyer(string name, string email)
    {
        var buyer = await _context.Buyer.FirstOrDefaultAsync(b => b.Name == name && b.Email == email);

        if (buyer != null)
            throw new InvalidOperationException("Người dùng với email này đã có rồi.");

        var newBuyer = new BuyerUser(name, email);
        await _context.Buyer.AddAsync(newBuyer);
        await _context.SaveChangesAsync();
        return newBuyer;
    }

    public async Task<bool> DeleteBuyer(int buyerId)
    {
        var buyer = await _context.Buyer.FindAsync(buyerId);

        if (buyer == null)
            return false;

        _context.Buyer.Remove(buyer);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<BuyerUser> GetBuyerById(int buyerId)
    {
        var buyer = await _context.Buyer.FindAsync(buyerId);

        if (buyer == null)
            return null;

        return buyer;
    }

    public async Task<List<BuyerUser>> ListBuyer()
    {
        var buyers = await _context.Buyer.ToListAsync();
        return buyers;
    }

    public async Task<BuyerUser> UpdateBuyer(int buyerId, string name, string email)
    {
        var buyer = await _context.Buyer.FindAsync(buyerId);

        if (buyer == null)
            return null;

        buyer.UpdateInfo(name, email);
        await _context.SaveChangesAsync();
        return buyer;
    }
}
