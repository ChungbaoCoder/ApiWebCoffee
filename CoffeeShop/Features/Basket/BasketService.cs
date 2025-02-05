using CoffeeShop.Entities.GroupBasket;
using CoffeeShop.Interface;

namespace CoffeeShop.Features.Basket;

public class BasketService : IBasketService
{
    public Task<BuyerBasket> AddItemToBasket(int buyer, int catalogItemId, decimal price, int quantity = 1)
    {
        throw new NotImplementedException();
    }

    public Task CreateBasketForUser()
    {
        throw new NotImplementedException();
    }

    public Task DeleteBasketAsync(int basketId)
    {
        throw new NotImplementedException();
    }
}
