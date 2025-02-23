using CoffeeShop.Entities.GroupBuyer;

namespace CoffeeShop.Interface;

public interface IBuyerService
{
    Task<BuyerUser> CreateBuyer(string name, string email, string phoneNum);
    Task<BuyerUser> UpdateBuyer(int buyerId, string name, string email, string phoneNum);
    Task<BuyerUser> AddAddress(int buyerId, Address address);
    Task<Address> UpdateAddress(int buyerId, Address address);
    Task<bool> RemoveAddress(int buyerId, int addressId);
    Task<bool> DeleteBuyer(int buyerId);
    Task<bool> DeleteManyBuyers(List<int> buyerId);
    Task<BuyerUser> GetBuyerById(int buyerId);
    Task<List<BuyerUser>> ListBuyer();
}
