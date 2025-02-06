namespace CoffeeShop.Entities.GroupBasket;

public class BasketItem
{
    public int BasketItemId { get; private set; }
    public int CoffeeItemId { get; private set; }
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }

    public int BasketId { get; private set; }
    public BuyerBasket Basket { get; private set; }

    private BasketItem() { }

    public BasketItem(int basketId, int coffeeItemId, decimal price, int quantity)
    {
        BasketId = basketId;
        CoffeeItemId = coffeeItemId;
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
