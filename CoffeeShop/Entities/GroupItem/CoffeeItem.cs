namespace CoffeeShop.Entities.GroupItem;

public class CoffeeItem
{
    public int CoffeeItemId { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string Category { get; private set; }
    public decimal Price { get; private set; }
    public string Size { get; private set; }
    public string PictureUri { get; private set; } = string.Empty;
    public Customization Customization { get; set; }
    public Availability Availability { get; set; }

    private CoffeeItem() { }

    public CoffeeItem(string name, string description, string category, decimal price, string size, string pictureUri, Availability availability, Customization customization)
    {
        UpdateItem(name, description, category, size, pictureUri);
        UpdatePrice(price);
        Availability = availability;
        Customization = customization;
    }

    public void UpdateItem(string name, string description, string category, string size, string pictureUri)
    {
        Name = name;
        Description = description;
        Category = category;
        Size = size;
        PictureUri = pictureUri;
    }

    public void UpdatePrice(decimal newPrice)
    {
        if (newPrice < 1)
            throw new ArgumentOutOfRangeException(nameof(newPrice), "Giá phải lớn hơn 0.");

        Price = newPrice;
    }
}
