using CoffeeShop.Entities.GroupBasket;

namespace CoffeeShop.Interface;

public interface IBasketService
{
    Task<BuyerBasket> ViewBasket(int basketId);
    Task<BuyerBasket> CreateBasketForUser(int buyerId);
    Task<BuyerBasket> AddItemToBasket(int basketId, int itemVariantId, int quantity = 1);
    Task<BuyerBasket> MergeWhenLogin(int basketId, List<BasketItem> items);
    Task<bool> ClearBasket(int basketId);
    Task<bool> RemoveItemFromBasket(int basketId, int itemVariantId);
    Task<bool> DeleteAllItems(int basketId);
}
