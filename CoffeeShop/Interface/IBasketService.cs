using CoffeeShop.Entities.GroupBasket;

namespace CoffeeShop.Interface;

public interface IBasketService
{
    Task CreateBasketForUser();
    Task<BuyerBasket> AddItemToBasket(int buyer, int catalogItemId, decimal price, int quantity = 1);
    Task DeleteBasketAsync(int basketId);
}
