using CoffeeShop.Entities.GroupBuyer;

namespace CoffeeShop.Interface;

public interface IBuyerService
{
    Task<BuyerUser> CreateBuyer(string name, string email);
    Task<BuyerUser> UpdateBuyer(int buyerId, string name, string email);
    Task<bool> DeleteBuyer(int buyerId);
    Task<BuyerUser> GetBuyerById(int buyerId);
    Task<List<BuyerUser>> ListBuyer();
}
