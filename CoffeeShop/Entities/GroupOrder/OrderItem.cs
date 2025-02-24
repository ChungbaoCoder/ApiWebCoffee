using System.Text.Json.Serialization;
using CoffeeShop.Entities.GroupItem;

namespace CoffeeShop.Entities.GroupOrder;

public class OrderItem
{
    [JsonIgnore]
    public int OrderItemId { get; private set; }
    [JsonIgnore]
    public int OrderId { get; private set; }
    [JsonIgnore]
    public BuyerOrder Order { get; private set; }

    public int ItemVariantId { get; private set; }

    [JsonIgnore]
    public ItemVariant ItemVariant { get; private set; }

    public decimal Price { get; private set; }
    public int Quantity { get; private set; }
    public OrderItemStatus OrderItemStatus { get; private set; } 

    private OrderItem() { }

    public OrderItem(int itemVariantId, decimal price, int quantity)
    {
        ItemVariantId = itemVariantId;
        Price = price;
        Quantity = quantity;
        UpdateItemStatus();
    }

    public void SetPrice(decimal price)
    {
        Price = price;
    }

    public void UpdateItemStatus(OrderItemStatus status = OrderItemStatus.Pending)
    {
        OrderItemStatus = status;
    }
}
