using CoffeeShop.Entities.GroupItem;

namespace CoffeeShop.Interface;

public interface ICoffeeItemService
{
    Task<CoffeeItem> CreateItem(CoffeeItem item);
    Task<CoffeeItem> UpdateItem(int coffeeItemId, CoffeeItem item);
    Task<bool> DeleteItem(int coffeeItemId);
    Task<CoffeeItem> GetById(int coffeeItemId);
    Task<List<CoffeeItem>> ListItem(int page, int pageSize);
}
