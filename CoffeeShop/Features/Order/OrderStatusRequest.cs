using CoffeeShop.Entities.GroupOrder;

namespace CoffeeShop.Features.Order;

public class OrderStatusRequest
{
    public OrderStatus OrderStatus { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
}
