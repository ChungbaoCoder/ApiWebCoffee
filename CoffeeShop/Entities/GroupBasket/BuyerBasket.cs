using CoffeeShop.Entities.GroupBuyer;

namespace CoffeeShop.Entities.GroupBasket;

public class BuyerBasket
{
    public int BasketId { get; private set; }

    private readonly List<BasketItem> _items = new List<BasketItem>();
    public IReadOnlyCollection<BasketItem> Items => _items.AsReadOnly();

    public int BuyerId { get; private set; }
    public BuyerUser Buyer { get; private set; }

    private BuyerBasket() { }

    public BuyerBasket(int buyerId)
    {
        BuyerId = buyerId;
    }

    public int TotalItems => _items.Sum(i => i.Quantity);

    public void AddItem(int coffeeItemId, decimal price, int quantity)
    {
        if (Items.Any(i => i.BasketItemId == coffeeItemId))
        {
            var existItem = Items.First(i => i.CoffeeItemId == coffeeItemId);
            existItem.AddQuantity(quantity);
        }
        else
        {
            _items.Add(new BasketItem(BasketId, coffeeItemId, price, quantity));
        }
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
