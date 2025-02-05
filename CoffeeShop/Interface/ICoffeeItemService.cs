using CoffeeShop.Entities.GroupItem;

namespace CoffeeShop.Interface;

public interface ICoffeeItemService
{
    Task<CoffeeItem> Create(CoffeeItem item);
    Task<CoffeeItem> Update(CoffeeItem item);
    Task<CoffeeItem> Delete(int coffeeItemId);
    Task<CoffeeItem> GetById(int coffeeItemId);
    Task<List<CoffeeItem>> List();
}
