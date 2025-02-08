using CoffeeShop.Database;
using CoffeeShop.Entities.GroupOrder;
using CoffeeShop.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Features.Order;

public class OrderService : IOrderService
{
    private readonly CoffeeDbContext _context;
    public OrderService(CoffeeDbContext context)
    {
        _context = context;
    }

    public async Task<bool> DeleteOrder(int orderId)
    {
        var order = await _context.Orders.FindAsync(orderId);

        if (order == null)
            return false;

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<BuyerOrder> CreateOrder(int buyerId, int basketId, OrderAddress shipAddress, OrderStatus orderStatus)
    {
        var basket = await _context.Baskets.FirstOrDefaultAsync(b => b.BuyerId == buyerId && b.BasketId == basketId);

        if (basket == null)
            return null;

        List<OrderItem> orderItems = new List<OrderItem>();

        foreach (var item in basket.Items)
        {
            var itemOrdered = await _context.CoffeeItems.Where(ci => ci.CoffeeItemId == item.CoffeeItemId)
                .Select(c => new
                {
                    c.CoffeeItemId,
                    c.Name,
                    c.PictureUri
                }).FirstAsync();

            OrderItem orderItem = new OrderItem(item.Price, item.Quantity, new ItemOrdered(itemOrdered.CoffeeItemId, itemOrdered.Name, itemOrdered.PictureUri));
            orderItems.Add(orderItem);
        }

        var order = new BuyerOrder(buyerId, shipAddress, orderStatus, orderItems);
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
        return order;
    }

    public async Task<List<BuyerOrder>> GetOrderByBuyerId(int buyerId)
    {
        var orders = await _context.Orders.Where(o => o.BuyerId == buyerId).ToListAsync();
        return orders;
    }

    public async Task<BuyerOrder> GetOrderById(int orderId)
    {
        var order = await _context.Orders.FindAsync(orderId);

        if (order == null)
            return null;

        return order;
    }

    public async Task<BuyerOrder> UpdateOrderStatus(int orderId, string orderStatus)
    {
        var order = await _context.Orders.FindAsync(orderId);

        if (order == null)
            return null;

        order.ConfigStatus(orderStatus);
        await _context.SaveChangesAsync();
        return order;
    }
}
