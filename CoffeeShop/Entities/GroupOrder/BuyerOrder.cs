using System.Diagnostics.Metrics;
using System.IO;
using System.Text.Json.Serialization;
using CoffeeShop.Entities.GroupBuyer;

namespace CoffeeShop.Entities.GroupOrder;

public class BuyerOrder
{
    public int OrderId { get; private set; }
    public int BuyerId { get; private set; }

    [JsonIgnore]
    public BuyerUser Buyer { get; private set; }

    public DateTime OrderedDate { get; private set; }
    public decimal Total { get; private set; }
    public List<OrderItem> OrderItems { get; private set; }
    public OrderStatus OrderStatus { get; private set; }
    public PaymentStatus PaymentStatus { get; private set; }
    public OrderAddress Address { get; private set; }
    public DateTime UpdatedDate { get; private set; }

    [JsonIgnore]
    public DateTime? DeletedAt { get; private set; }

    private BuyerOrder() { }

    public BuyerOrder(int buyerId, List<OrderItem> orderItems)
    {
        BuyerId = buyerId;
        OrderItems = orderItems;
        OrderStatus = OrderStatus.PendingPayment;
        PaymentStatus = PaymentStatus.Pending;
        UpdatedDate = DateTime.Now;
    }

    public void UpdateShippingAddress(Address address)
    {
        if (Address == null)
            Address = new OrderAddress(address.Street, address.City, address.State, address.Country);
        else
            Address.UpdateShippingAddress(address.Street, address.City, address.State, address.Country);
    }

    public void SetStatus(OrderStatus newOrderStatus)
    {
        OrderStatus = newOrderStatus;
    }

    public void SetPaymentStatus(PaymentStatus newPaymentStatus)
    {
        PaymentStatus = newPaymentStatus;
        UpdateOrderStatusBasedOnPayment();
        UpdatedDate = DateTime.Now;
    }

    private void UpdateOrderStatusBasedOnPayment()
    {
        switch (PaymentStatus)
        {
            case PaymentStatus.Paid:
                if (OrderStatus == OrderStatus.PendingPayment)
                {
                    OrderStatus = OrderStatus.Processing; // Payment received, start processing order
                }
                else if (OrderStatus == OrderStatus.OnHold)
                {
                    OrderStatus = OrderStatus.Processing; // Payment received after being on hold, resume processing
                }
                // You might have other order statuses to transition from upon payment (e.g., from Failed back to Processing if payment retry succeeds)
                break;

            case PaymentStatus.Failed:
                OrderStatus = OrderStatus.Failed; // Payment failed, order is failed
                break;

            case PaymentStatus.Refunded:
                OrderStatus = OrderStatus.Refunded; // Payment refunded, order is refunded
                break;

            case PaymentStatus.Cancelled:
                OrderStatus = OrderStatus.Cancelled; // Payment cancelled, order is cancelled
                break;

            case PaymentStatus.Processing:
                // Payment is processing, OrderStatus might remain PendingPayment or become Processing depending on your logic.
                // For this example, we might not change OrderStatus immediately to Processing just because payment is processing.
                // OrderStatus.Processing is more about order fulfillment processing, not just payment processing.
                break;

            case PaymentStatus.Pending:
                // Payment is pending, OrderStatus is already PendingPayment by default or could remain so.
                break;

            default:
                // Handle unexpected PaymentStatus if needed
                break;
        }
    }

    public void SetTotal()
    {
        Total = OrderItems.Sum(oi => oi.Price * oi.Quantity);
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
