using CoffeeShop.Entities.GroupBasket;

namespace CoffeeShop.Interface;

public interface IBasketService
{
    Task<BuyerBasket> CreateBasketForUser(int buyerId);
    Task<BuyerBasket> AddItemToBasket(int buyerId, int basketId, int coffeeItemId, int quantity = 1);
    Task<BuyerBasket> ClearBasket(int basketId);
    Task<BuyerBasket> RemoveItemFromBasket(int basketId, int basketItemId);
    Task<bool> DeleteBasket(int  basketId);
}
