namespace CoffeeShop.Entities.GroupOrder;

public class ItemOrdered
{
    public int CoffeeItemId { get; private set; }
    public string ItemName { get; private set; }
    public string PictureUri { get; private set; }

    private ItemOrdered() { }

    public ItemOrdered(int coffeeItemId, string itemName, string pictureUri)
    {
        CoffeeItemId = coffeeItemId;
        ItemName = itemName;
        PictureUri = pictureUri;
    }
}
