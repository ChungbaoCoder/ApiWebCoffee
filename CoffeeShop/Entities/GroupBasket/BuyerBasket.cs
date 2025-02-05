namespace CoffeeShop.Entities.GroupBasket;

public class BuyerBasket
{
    public int BasketId { get; private set; }
    public int BuyerId { get; private set; }

    private readonly List<BasketItem> _items = new List<BasketItem>();
    public IReadOnlyCollection<BasketItem> Items => _items.AsReadOnly();
    public int TotalItems => _items.Sum(i => i.Quantity);

    public BuyerBasket(int buyerId)
    {
        BuyerId = buyerId;
    }

    public void AddItem(int coffeeItemId, decimal price, int quantity)
    {
        if (!Items.Any(i => i.BasketItemId == coffeeItemId))
        {
            _items.Add(new BasketItem(BasketId, coffeeItemId, price, quantity));
            return;
        }
        var existItem = Items.First(i => i.CoffeeItemId == coffeeItemId);
        existItem.AddQuantity(quantity);
    }

    public void RemoveEmptyItem()
    {
        _items.RemoveAll(i => i.Quantity == 0);
    }

    public void SetNewBuyerId(int buyerId)
    {
        BuyerId = buyerId;
    }
}
