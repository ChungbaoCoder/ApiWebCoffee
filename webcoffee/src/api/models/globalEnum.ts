export enum Status{
    NotSet,
    Active,
    Inactive,
    LowStock,
    OutOfStock
};

export enum OrderStatus
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
};

export enum OrderItemStatus
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
};

export enum PaymentStatus
{
    Pending,
    Processing,
    Paid,
    Failed,
    Refunded,
    Cancelled
};