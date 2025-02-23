namespace CoffeeShop.Entities.GroupOrder;

public enum OrderStatus
{
    PendingPayment,
    Processing,
    Packing,
    Shipped,
    OutForDelivery,
    Delivered,
    Completed,
    Cancelled,
    ReturnRequested,
    ReturnInProgress,
    Returned,
    Refunded,
    OnHold,
    Failed
}

public enum OrderItemStatus
{
    Pending,
    Processing,
    Packed,
    Shipped,
    Delivered,
    Cancelled,
    Returned,
    Refunded,
    Backordered,
    OutOfStock,
    Replaced
}

public enum PaymentStatus
{
    Pending,
    Processing,
    Paid,
    Failed,
    Refunded,
    Cancelled
}
