using CoffeeShop.Entities.GroupOrder;

namespace CoffeeShop.Interface;

public interface IOrderService
{
    Task<BuyerOrder> CreateOrder(int buyerId, List<OrderItem> items);
    Task<BuyerOrder> UpdateOrderStatus(int orderId, OrderStatus orderStatus, PaymentStatus paymentStatus);
    Task<bool> DeleteOrder(int orderId);
    Task<bool> DeleteManyOrders(List<int> orderId);
    Task<BuyerOrder> GetOrderById(int orderId);
    Task<List<BuyerOrder>> GetOrderByBuyerId(int buyerId);
}
