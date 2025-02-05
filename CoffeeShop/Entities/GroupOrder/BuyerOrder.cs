namespace CoffeeShop.Entities.GroupOrder;

public class BuyerOrder
{
    public int OrderId { get; private set; }
    public int BuyerId { get; private set; }
    public DateTime CreatedDate { get; private set; } = DateTime.Now;
    public DateTime UpdatedDate { get; private set; } = DateTime.Now;
    public OrderAddress ShipAddress { get; private set; }
    public OrderStatus Status { get; private set; }

    private readonly List<OrderItem> _orderItems = new List<OrderItem>();
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();
    public decimal Total => _orderItems.Sum(oi => oi.Price * oi.Quantity);

    private BuyerOrder() { }

    public BuyerOrder(int buyerId, OrderAddress shipAddress, OrderStatus status, List<OrderItem> orderItems)
    {
        BuyerId = buyerId;
        ShipAddress = shipAddress;
        Status = status;
        _orderItems = orderItems;
    }

    public void ConfigStatus(string status)
    {
        Status.SetStatus(status);
        UpdatedDate = DateTime.Now;
    }
}
