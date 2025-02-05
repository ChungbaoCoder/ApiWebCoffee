namespace CoffeeShop.Interface;

public interface IBuyerService
{
    Task Register(string name, string email);
}
