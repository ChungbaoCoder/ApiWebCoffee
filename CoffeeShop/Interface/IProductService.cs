using CoffeeShop.Entities.GroupItem;

namespace CoffeeShop.Interface;

public interface IProductService
{
    Task<ProductItem> CreateItem(ProductItem item, List<ItemVariant> variants);
    Task<ProductItem> UpdateItem(int productItemId, ProductItem item);
    Task<ProductItem> AddItemVariant(int productItemId, List<ItemVariant> variants);
    Task<ProductItem> UpdateItemVariants(int productItemId, int itemVariantId, ItemVariant itemVariant);
    Task<bool> DeleteItem(int productItemId);
    Task<bool> DeleteManyItems(List<int> productItemId);
    Task<ProductItem> GetById(int productItemId);
    Task<IEnumerable<ProductItem>> ListItem(int page, int pageSize);
}
