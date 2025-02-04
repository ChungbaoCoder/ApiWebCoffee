namespace CoffeeShop.Entities.GroupBasket;

public class Basket
{
    public int BasketId { get; private set; }
    public string BuyerId { get; private set; }

    private readonly List<BasketItem> _items = new List<BasketItem>();
    public IReadOnlyCollection<BasketItem> Items => _items.AsReadOnly();
    public int TotalItems => _items.Sum(i => i.Quantity);

    private Basket() { }

    public Basket(string buyerId)
    {
        BuyerId = buyerId;
    }

    public void AddItem(int basketItemId, decimal price, int quantity)
    {
        if (!Items.Any(i => i.BasketItemId == basketItemId))
        {
            _items.Add(new BasketItem(basketItemId, price, quantity));
        }

        var existItem = Items.First(i => i.BasketItemId == basketItemId);
        existItem.AddQuantity(quantity);
    }

    public void RemoveEmptyItem()
    {
        _items.RemoveAll(i => i.Quantity == 0);
    }

    public void SetNewBuyerId(string buyerId)
    {
        BuyerId = buyerId;
    }
}
