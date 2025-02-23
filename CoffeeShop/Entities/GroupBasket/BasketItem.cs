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

    public BasketItem(int basketId, int itemVariantId, decimal price, int quantity)
    {
        BasketId = basketId;
        ItemVariantId = itemVariantId;
        Price = price;
        SetQuantity(quantity);
    }

    //public void SetCustom(Temperature temperature = Temperature.NotSet, Iced iced = Iced.NotSet, Milk milk = Milk.NotSet, Sweetness sweetness = Sweetness.NotSet)
    //{
    //    Temperature = temperature;
    //    Iced = iced;
    //    Milk = milk;
    //    Sweetness = sweetness;
    //}

    public void AddQuantity(int quantity)
    {
        Quantity += quantity;
    }

    public void SetQuantity(int quantity)
    {
        Quantity = quantity;
    }
}
