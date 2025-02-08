using CoffeeShop.Entities.GroupOrder;

namespace CoffeeShop.Interface;

public interface IOrderService
{
    Task<BuyerOrder> CreateOrder(int buyerId, int basketId, OrderAddress shipAddress, OrderStatus orderStatus);
    Task<BuyerOrder> UpdateOrderStatus(int orderId, string orderStatus);
    Task<bool> DeleteOrder(int orderId);
    Task<BuyerOrder> GetOrderById(int orderId);
    Task<List<BuyerOrder>> GetOrderByBuyerId(int buyerId);
}
