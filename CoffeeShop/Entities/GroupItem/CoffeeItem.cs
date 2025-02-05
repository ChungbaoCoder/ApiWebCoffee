namespace CoffeeShop.Entities.GroupItem;

public class CoffeeItem
{
    public int CoffeeItemId { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public string Size { get; private set; }
    public string Category { get; private set; }
    public string PictureUri { get; private set; }
    public Customization Customization { get; private set; }
    public Availability Availability { get; private set; }

    private CoffeeItem() { }

    public CoffeeItem(string name, string description, decimal price, string size, string category, string pictureUri, Customization customization, Availability availability)
    {
        Name = name;
        Description = description;
        Price = price;
        Size = size;
        Category = category;
        PictureUri = pictureUri;
        Customization = customization;
        Availability = availability;
    }
}
