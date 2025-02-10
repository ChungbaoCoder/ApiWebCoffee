using System.Text.Json.Serialization;

namespace CoffeeShop.Entities.GroupOrder;

public class OrderItem
{
    public int OrderItemId { get; private set; } 
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }
    public ItemOrdered Item { get; private set; }

    [JsonIgnore]
    public int OrderId { get; private set; }
    [JsonIgnore]
    public BuyerOrder Order { get; private set; }

    private OrderItem() { }

    public OrderItem(decimal price, int quantity, ItemOrdered coffeeItem)
    {
        Price = price;
        Quantity = quantity;
        Item = coffeeItem;
    }
}
