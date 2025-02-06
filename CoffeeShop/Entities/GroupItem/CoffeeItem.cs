namespace CoffeeShop.Entities.GroupItem;

public class CoffeeItem
{
    public int CoffeeItemId { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string Category { get; private set; }
    public decimal Price { get; private set; }
    public string Size { get; private set; }
    public string PictureUri { get; private set; }
    public Customization Customization { get; private set; }
    public Availability Availability { get; private set; }

    private CoffeeItem() { }

    public CoffeeItem(string name, string description, string category, decimal price, string size, string pictureUri, Customization customization, Availability availability)
    {
        Name = name;
        Description = description;
        Category = category;
        Price = price;
        Size = size;
        PictureUri = pictureUri;
        Customization = customization;
        Availability = availability;
    }

    public void UpdatePrice(decimal newPrice)
    {
        if (newPrice < 1)
            throw new ArgumentOutOfRangeException(nameof(newPrice), "Gía phải lớn hơn 0.");

        Price = newPrice;
    }

    public void UpdateQuantity(int quantity)
    {
        Availability.UpdateQuantity(quantity);
    }

    public void UpdateStatus(bool availableStatus, DateTime restockDate)
    {
        Availability.UpdateStatus(availableStatus, restockDate);
    }

    public void AddTopping(string topping)
    {
        Customization.AddTopping(topping);
    }

    public void AddFlavor(string flavor)
    {
        Customization.AddFlavor(flavor);
    }
}
