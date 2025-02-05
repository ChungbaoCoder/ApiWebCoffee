using CoffeeShop.Entities.GroupBuyer;

namespace CoffeeShop.Interface;

public interface IUserService
{
    Task RegisterUser(Buyer buyer);
}
