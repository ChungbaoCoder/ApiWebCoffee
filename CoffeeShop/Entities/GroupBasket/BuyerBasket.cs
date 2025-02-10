using System.Text.Json.Serialization;
using CoffeeShop.Entities.GroupBuyer;

namespace CoffeeShop.Entities.GroupBasket;

public class BuyerBasket
{
    public int BasketId { get; private set; }

    public List<BasketItem> Items = new List<BasketItem>();

    [JsonIgnore]
    public int BuyerId { get; private set; }
    [JsonIgnore]
    public BuyerUser Buyer { get; private set; }

    private BuyerBasket() { }

    public BuyerBasket(int buyerId)
    {
        BuyerId = buyerId;
    }

    public int TotalItems => Items.Sum(i => i.Quantity);

    public void AddItem(int coffeeItemId, decimal price, int quantity)
    {
        if (Items.Any(i => i.BasketItemId == coffeeItemId))
        {
            var existItem = Items.First(i => i.CoffeeItemId == coffeeItemId);
            existItem.AddQuantity(quantity);
        }
        else
        {
            Items.Add(new BasketItem(BasketId, coffeeItemId, price, quantity));
        }
    }

    public void RemoveItem(int coffeeItemId)
    {
        Items.RemoveAll(i => i.CoffeeItemId == coffeeItemId);
    }

    public void ClearBasket()
    {
        Items.RemoveAll(i => i.Quantity == 0);
    }

    public void SetNewBuyerId(int buyerId)
    {
        BuyerId = buyerId;
    }
}
