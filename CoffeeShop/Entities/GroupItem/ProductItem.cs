using System.Text.Json.Serialization;

namespace CoffeeShop.Entities.GroupItem;

public class ProductItem
{
    public int ProductId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string Category { get; private set; } = string.Empty;
    public string ImageUri { get; private set; } = string.Empty;
    public DateTime CreatedDate { get; private set; } = DateTime.Now;
    public DateTime? UpdatedDate { get; private set; }
    public List<ItemVariant> ItemVariant { get; private set; }

    [JsonIgnore]
    public DateTime? DeletedAt { get; private set; }

    private ProductItem() { ItemVariant = new List<ItemVariant>(); }

    public ProductItem(string name, string description, string category, string imageUri)
    {
        UpdateProductItem(name, description, category, imageUri);
        ItemVariant = new List<ItemVariant>();
    }

    public void UpdateProductItem(string name, string description, string category, string imageUri)
    {
        Name = name;
        Description = description;
        Category = category;
        ImageUri = imageUri;
        UpdatedDate = DateTime.Now;
    }

    public void AddVariant(List<ItemVariant> itemVariants)
    {
        ItemVariant.AddRange(itemVariants);
    }

    public void UpdateVariant(int itemVariantId, ItemVariant itemVariant)
    {
        if (ItemVariant.Any())
        {
            var variant = ItemVariant.First(iv => iv.ItemVariantId == itemVariantId);

            variant.UpdateVariant(itemVariant.Size, itemVariant.Price);
            variant.SetQuantity(itemVariant.StockQuantity);
            variant.SetStatus(itemVariant.Status);
        }
    }

    public void MarkDeletion()
    {
        DeletedAt = DateTime.Now;

        if (DeletedAt != null)
        {
            foreach (var item in ItemVariant)
            {
                item.MarkDeletion();
            }
        }
    }

    public void UnMarkDeletion()
    {
        DeletedAt = null;
    }
}
