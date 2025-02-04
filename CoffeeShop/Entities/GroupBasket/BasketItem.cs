namespace CoffeeShop.Entities.GroupBasket;

public class BasketItem
{
    public int BasketItemId { get; private set; }
    public int BasketId { get; private set; }
    public int ItemId { get; private set; }
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }

    private BasketItem() { }

    public BasketItem(int itemId, decimal price, int quantity)
    {
        ItemId = itemId;
        Price = price;
        SetQuantity(quantity);
    }

    public void AddQuantity(int quantity)
    {
        Quantity += quantity;
    }

    public void SetQuantity(int quantity)
    {
        Quantity = quantity;
    }
}
