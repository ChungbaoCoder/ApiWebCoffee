using CoffeeShop.Database;
using CoffeeShop.Entities.GroupItem;
using CoffeeShop.Interface;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Features.Product;

public class ProductService : IProductService
{
    private readonly CoffeeDbContext _context;
    public ProductService(CoffeeDbContext context)
    {
        _context = context;
    }

    public async Task<ProductItem> AddItemVariant(int productItemId, List<ItemVariant> variants)
    {
        var item = await _context.Products.Include(p => p.ItemVariant).FirstOrDefaultAsync(p => p.ProductId == productItemId && p.DeletedAt == null);

        if (item == null)
            return null;

        item.AddVariant(variants);
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task<ProductItem> CreateItem(ProductItem item, List<ItemVariant> variants)
    {
        item.AddVariant(variants);
        _context.Products.Add(item);
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task<bool> DeleteItem(int productItemId)
    {
        var item = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == productItemId && p.DeletedAt == null);

        if (item == null)
            return false;

        item.MarkDeletion();
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteManyItems(List<int> productItemId)
    {
        var items = await _context.Products.Where(p => productItemId.Contains(p.ProductId) && p.DeletedAt == null).ToListAsync();

        if (items == null || items.Count == 0)
            return false;

        foreach (var item in items)
        {
            item.MarkDeletion();
        }
        
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<ProductItem> GetById(int productItemId)
    {
        var item = await _context.Products.AsNoTracking().Include(p => p.ItemVariant).FirstOrDefaultAsync(p => p.ProductId == productItemId && p.DeletedAt == null);

        if (item == null)
            return null;

        return item;
    }

    public async Task<IEnumerable<ProductItem>> ListItem(int page, int pageSize)
    {
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 10;

        var items = await _context.Products
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .Include(p => p.ItemVariant)
            .Where(p => p.DeletedAt == null)
            .ToListAsync();

        if (items == null)
            return [];

        return items;
    }

    public async Task<ProductItem> UpdateItem(int productItemId, ProductItem item)
    {
        var existItem = await _context.Products.FindAsync(productItemId);

        if (existItem == null)
            return null;

        existItem.UpdateProductItem(item.Name, item.Description, item.Category, item.ImageUri);
        await _context.SaveChangesAsync();
        return existItem;
    }

    public async Task<ProductItem> UpdateItemVariants(int productItemId, int itemVariantId, ItemVariant itemVariant)
    {
        var item = await _context.Products.Include(p => p.ItemVariant).FirstOrDefaultAsync(p => p.ProductId == productItemId && p.DeletedAt == null);

        if (item == null)
            return null;

        item.UpdateVariant(itemVariantId, itemVariant);
        await _context.SaveChangesAsync();
        return item;
    }
}
