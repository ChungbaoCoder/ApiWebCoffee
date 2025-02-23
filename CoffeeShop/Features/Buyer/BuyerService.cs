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

    public async Task<BuyerUser> AddAddress(int buyerId, Address address)
    {
        var buyer = await _context.Buyer.Include(b => b.Address).FirstOrDefaultAsync(b => b.BuyerId == buyerId);

        if (buyer == null)
            return null;

        buyer.AddAddress(address);
        await _context.SaveChangesAsync();
        return buyer;
    }

    public async Task<BuyerUser> CreateBuyer(string name, string email, string phoneNum)
    {
        var buyer = await _context.Buyer.FirstOrDefaultAsync(b => b.Name == name && b.Email == email);

        if (buyer != null)
            throw new InvalidOperationException("Người dùng với email này đã có rồi.");

        var newBuyer = new BuyerUser(name, email, phoneNum);
        await _context.Buyer.AddAsync(newBuyer);
        await _context.SaveChangesAsync();
        return newBuyer;
    }

    public async Task<bool> DeleteBuyer(int buyerId)
    {
        var buyer = await _context.Buyer.FirstOrDefaultAsync(b => b.BuyerId == buyerId && b.DeletedAt == null);

        if (buyer == null)
            return false;

        buyer.MarkDeletion();
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteManyBuyers(List<int> buyerId)
    {
        var buyers = await _context.Buyer.Where(b => buyerId.Contains(b.BuyerId) && b.DeletedAt == null).ToListAsync();

        if (buyers == null || buyers.Count == 0)
            return false;

        foreach (var buyer in buyers)
        {
            buyer.MarkDeletion();
        }

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<BuyerUser> GetBuyerById(int buyerId)
    {
        var buyer = await _context.Buyer.AsNoTracking().FirstOrDefaultAsync(b => b.BuyerId == buyerId);

        if (buyer == null)
            return null;

        return buyer;
    }

    public async Task<List<BuyerUser>> ListBuyer()
    {
        var buyers = await _context.Buyer.Include(b => b.Address).AsNoTracking().ToListAsync();
        return buyers;
    }

    public async Task<bool> RemoveAddress(int buyerId, int addressId)
    {
        var address = await _context.Address.FirstOrDefaultAsync(b => b.BuyerId == buyerId);

        if (address == null)
            return false;

        _context.Remove(address);
        return true;
    }

    public async Task<Address> UpdateAddress(int buyerId, Address address)
    {
        var buyerAddress = await _context.Address.FirstOrDefaultAsync(b => b.BuyerId == buyerId);

        if (buyerAddress == null)
            return null;

        buyerAddress.UpdateAddress(address.Street, address.City, address.State, address.Country);
        await _context.SaveChangesAsync();
        return buyerAddress;
    }

    public async Task<BuyerUser> UpdateBuyer(int buyerId, string name, string email, string phoneNum)
    {
        var buyer = await _context.Buyer.FindAsync(buyerId);

        if (buyer == null)
            return null;

        buyer.UpdateInfo(name, email, phoneNum);
        await _context.SaveChangesAsync();
        return buyer;
    }
}
