namespace CoffeeShop.Entities.GroupOrder;

public class ItemOrdered
{
    public int ItemId { get; private set; }
    public string ItemName { get; private set; }
    public string PictureUri { get; private set; }

    private ItemOrdered() { }

    public ItemOrdered(int itemId, string itemName, string pictureUri)
    {
        ItemId = itemId;
        ItemName = itemName;
        PictureUri = pictureUri;
    }
}
