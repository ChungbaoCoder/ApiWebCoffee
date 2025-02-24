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

    private BuyerBasket() { Items = new List<BasketItem>(); }

    public BuyerBasket(int buyerId)
    {
        BuyerId = buyerId;
    }

    public int TotalItems => Items.Sum(i => i.Quantity);
    public decimal TotalPrice => Items.Sum(i => i.Price) * TotalItems;

    public void AddItem(int itemVariantId, decimal price, int quantity)
    {
        if (!Items.Any(i => i.ItemVariantId == itemVariantId))
        {
            Items.Add(new BasketItem(itemVariantId, price, quantity));
        }
        else
        {
            var existItem = Items.First(i => i.ItemVariantId == itemVariantId);
            existItem.AddQuantity(quantity);
        }
    }

    public void ClearBasket()
    {
        if (!Items.Any())
            return;

        Items.RemoveAll(i => i.Quantity == 0);
    }

    public int RemoveItem(int itemVariantId)
    {
        if (!Items.Any())
            return 0;

        int quantityRestore = Items.Where(bi => bi.ItemVariantId == itemVariantId).Sum(bi => bi.Quantity);
        Items.RemoveAll(i => i.ItemVariantId == itemVariantId);
        return quantityRestore;
    }

    public Dictionary<int, int> RemoveAllItems()
    {
        var quantityRestore = new Dictionary<int, int>();

        if (!Items.Any())
            return quantityRestore;

        foreach (var item in Items)
        {
            if (item.Quantity != 0)
            {
                quantityRestore.Add(item.ItemVariantId, item.Quantity);
            }
        }

        Items.Clear();
        return quantityRestore;
    }

    public void TransferBuyer(int buyerId)
    {
        BuyerId = buyerId;
    }
}
