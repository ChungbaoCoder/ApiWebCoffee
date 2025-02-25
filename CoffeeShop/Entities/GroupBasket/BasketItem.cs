using System.Text.Json.Serialization;
using CoffeeShop.Entities.GroupItem;

namespace CoffeeShop.Entities.GroupBasket;

public class BasketItem
{
    [JsonIgnore]
    public int BasketItemId { get; private set; }
    [JsonIgnore]
    public int BasketId { get; private set; }
    [JsonIgnore]
    public BuyerBasket Basket { get; private set; }

    public int ItemVariantId { get; private set; }
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }

    private BasketItem() { }

    public BasketItem(int itemVariantId, decimal price, int quantity)
    {
        ItemVariantId = itemVariantId;
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

    public void SetPrice(decimal price)
    {
        Price = price;
    }
}
