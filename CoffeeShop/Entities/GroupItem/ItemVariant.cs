using System.Text.Json.Serialization;
using CoffeeShop.Entities.GroupOrder;

namespace CoffeeShop.Entities.GroupItem;

public class ItemVariant
{
    public int ItemVariantId { get; private set; }

    [JsonIgnore]
    public int ProductId { get; private set; }
    [JsonIgnore]
    public ProductItem ProductItem { get; private set; }

    public string Size { get; private set; } = string.Empty;
    public int StockQuantity {  get; private set; }
    public decimal Price { get; private set; }
    public ItemStatus Status { get; private set; }

    [JsonIgnore]
    public DateTime? DeletedAt { get; private set; }
    [JsonIgnore]
    public List<OrderItem> OrderItems { get; private set; }

    private ItemVariant() { }

    public ItemVariant(string size, int stockQuantity, decimal price, ItemStatus status = ItemStatus.NotSet)
    {
        UpdateVariant(size, price);
        SetQuantity(stockQuantity);
        Status = status;
    }

    public void UpdateVariant(string size, decimal price)
    {
        Size = size;
        Price = price;
    }

    public void AddQuantity(int stockQuantity)
    {
        StockQuantity += stockQuantity;
        SetStatusForQuantity();
    }

    public void SetQuantity(int stockQuantity)
    {
        StockQuantity = stockQuantity;
        SetStatusForQuantity();
    }

    public void SetStatus(ItemStatus status)
    {
        Status = status;
    }

    private void SetStatusForQuantity()
    {
        if (StockQuantity < 1)
            Status = ItemStatus.OutOfStock;
        else if (StockQuantity < 10)
            Status = ItemStatus.LowStock;
    }

    public void MarkDeletion()
    {
        DeletedAt = DateTime.Now;
    }

    public void UnMarkDeletion()
    {
        DeletedAt = null;
    }
}
