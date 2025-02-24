using CoffeeShop.Database;
using CoffeeShop.Entities.GroupOrder;
using CoffeeShop.Features.CustomExceptions;
using CoffeeShop.Interface;
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
        var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId && o.DeletedAt == null);

        if (order == null)
            return false;

        order.MarkDeletion();
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteManyOrders(List<int> orderId)
    {
        var orders = await _context.Orders.Where(o => orderId.Contains(o.OrderId) && o.DeletedAt == null).ToListAsync();

        if (orders == null || orders.Count == 0)
            return false;

        foreach (var order in orders)
        {
            order.MarkDeletion();
        }
        
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<BuyerOrder> CreateOrder(int buyerId, List<OrderItem> orderItems)
    {
        var buyer = await _context.Buyer.FirstOrDefaultAsync(b => b.BuyerId == buyerId && b.DeletedAt == null);

        if (buyer == null)
            throw new NotFoundException("Không tìm thầy người mua");

        var address = await _context.Address.FirstOrDefaultAsync(a => a.BuyerId == buyerId);

        if (address == null)
            throw new NotFoundException("Không tìm thấy địa chỉ giao hàng của khách");

        var itemIdsFromOrder = orderItems.Select(oi => oi.ItemVariantId).ToList();
        var items = await _context.ItemVariants.Where(iv => itemIdsFromOrder.Contains(iv.ItemVariantId)).ToListAsync();
        var lookUp = items.ToDictionary(i => i.ItemVariantId, i => i);

        var vaildOrderItem = new List<OrderItem>();

        foreach (var orderItem in orderItems)
        {
            if (lookUp.TryGetValue(orderItem.ItemVariantId, out var item))
            {
                if (orderItem.Price != item.Price)
                    orderItem.SetPrice(item.Price);
            }
            vaildOrderItem.Add(orderItem);
        }

        var order = new BuyerOrder(buyerId, vaildOrderItem);
        order.UpdateShippingAddress(address);
        order.SetTotal();

        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
        return order;
    }

    public async Task<IEnumerable<BuyerOrder>> GetOrderByBuyerId(int buyerId)
    {
        var orders = await _context.Orders.Include(o => o.OrderItems).Where(o => o.BuyerId == buyerId && o.DeletedAt == null).AsNoTracking().ToListAsync();

        if (orders == null)
            return [];

        return orders;
    }

    public async Task<BuyerOrder> GetOrderById(int orderId)
    {
        var order = await _context.Orders.Include(o => o.OrderItems).AsNoTracking().FirstOrDefaultAsync(o => o.OrderId == orderId && o.DeletedAt == null);

        if (order == null)
            return null;

        return order;
    }

    public async Task<BuyerOrder> UpdateOrderStatus(int orderId, OrderStatus orderStatus, PaymentStatus paymentStatus)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId && o.DeletedAt == null);

        if (order == null)
            return null;

        order.SetStatus(orderStatus);
        order.SetPaymentStatus(paymentStatus);
        await _context.SaveChangesAsync();
        return order;
    }
}
