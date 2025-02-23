using System.Text.Json.Serialization;
using CoffeeShop.Entities.GroupBuyer;

namespace CoffeeShop.Entities.GroupBasket;

public class BuyerBasket
{
    public int BasketId { get; private set; }
    public int? BuyerId { get; private set; }

    [JsonIgnore]
    public BuyerUser Buyer { get; private set; }

    public List<BasketItem> Items { get; private set; }

    private BuyerBasket() { }

    public BuyerBasket(int buyerId)
    {
        BuyerId = buyerId;
        Items = new List<BasketItem>();
    }

    public int TotalItems => Items.Sum(i => i.Quantity);
    public decimal TotalPrice => Items.Sum(i => i.Price);

    public void AddItem(int itemVariantId, decimal price, int quantity)
    {
        if (!Items.Any(i => i.BasketItemId == itemVariantId))
        {
            Items.Add(new BasketItem(BasketId, itemVariantId, price, quantity));
        }
        var existItem = Items.First(i => i.ItemVariantId == itemVariantId);
        existItem.AddQuantity(quantity);
    }

    public void ClearBasket()
    {
        Items.RemoveAll(i => i.Quantity == 0);
    }

    public void RemoveItem(int itemVariantId)
    {
        Items.RemoveAll(i => i.ItemVariantId == itemVariantId);
    }

    public void RemoveAllItems()
    {
        Items.Clear();
    }

    public void TransferBuyer(int buyerId)
    {
        BuyerId = buyerId;
    }
}
