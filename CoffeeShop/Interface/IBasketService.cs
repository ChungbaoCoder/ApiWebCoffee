using CoffeeShop.Entities.GroupBasket;

namespace CoffeeShop.Interface;

public interface IBasketService
{
    Task<Basket> AddItemToBasket(string username, int catalogItemId, decimal price, int quantity = 1);
    Task<Basket> SetQuantities(int basketId, Dictionary<string, int> quantities);
    Task DeleteBasketAsync(int basketId);
}
