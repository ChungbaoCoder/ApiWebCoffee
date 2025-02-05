using CoffeeShop.Entities.GroupUser;

namespace CoffeeShop.Interface;

public interface IUserService
{
    Task RegisterUser(Buyer buyer);
}
