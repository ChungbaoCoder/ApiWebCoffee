namespace CoffeeShop.Entities.GroupOrder;

public class OrderItem
{
    public int OrderId { get; private set; }
    public ItemOrdered? Item { get; private set; }
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }

    private OrderItem() { }

    public OrderItem(ItemOrdered item, decimal price, int quantity)
    {
        Item = item;
        Price = price;
        Quantity = quantity;
    }
}
