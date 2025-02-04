namespace CoffeeShop.Entities.GroupOrder;

public class Order
{
    public string OrderId { get; private set; }
    public string BuyerId { get; private set; }
    public DateTime CreatedDate { get; private set; }
    public Address ShipAddress { get; private set; }
    public OrderStatus Status { get; private set; }


    private readonly List<OrderItem> _orderItems = new List<OrderItem>();
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

    private Order() { }

    public Order(string buyerId, Address shipAddress, OrderStatus status, List<OrderItem> orderItems)
    {
        BuyerId = buyerId;
        ShipAddress = shipAddress;
        Status = status;
        _orderItems = orderItems;
    }

    public decimal Total => _orderItems.Sum(oi => oi.Price * oi.Quantity);
}
