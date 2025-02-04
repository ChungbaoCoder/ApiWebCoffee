namespace CoffeeShop.Entities.GroupItem;

public class Item
{
    public int ItemId { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public string Category { get; private set; }
    public string PictureUri { get; private set; }

    public Item(string name, string description, decimal price, string category, string pictureUri)
    {
        Name = name;
        Description = description;
        Price = price;
        Category = category;
        PictureUri = pictureUri;
    }
}
