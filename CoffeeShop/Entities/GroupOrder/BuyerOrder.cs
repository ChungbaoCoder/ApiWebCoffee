using System.Text.Json.Serialization;
using CoffeeShop.Entities.GroupBuyer;

namespace CoffeeShop.Entities.GroupOrder;

public class BuyerOrder
{
    public int OrderId { get; private set; }
    public DateTime CreatedDate { get; private set; } = DateTime.Now;
    public DateTime UpdatedDate { get; private set; } = DateTime.Now;
    public OrderAddress ShipAddress { get; private set; }
    public OrderStatus Status { get; private set; }

    public List<OrderItem> OrderItems = new List<OrderItem>();
    public decimal Total {  get; private set; }

    [JsonIgnore]
    public int BuyerId { get; private set; }
    [JsonIgnore]
    public BuyerUser Buyer { get; private set; }

    private BuyerOrder() { }

    public BuyerOrder(int buyerId, OrderAddress shipAddress, OrderStatus status, List<OrderItem> orderItems)
    {
        BuyerId = buyerId;
        ShipAddress = shipAddress;
        Status = status;
        OrderItems = orderItems;
    }

    public void ConfigStatus(string status)
    {
        Status.SetStatus(status);
        UpdatedDate = DateTime.Now;
    }

    public void SetTotal()
    {
        Total = OrderItems.Sum(oi => oi.Price * oi.Quantity);
    }
}
