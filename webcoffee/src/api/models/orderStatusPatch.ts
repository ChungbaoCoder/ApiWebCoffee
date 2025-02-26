import { OrderItemStatus, OrderStatus, PaymentStatus } from "./globalEnum";

export interface OrderStatusPatch {
    orderStatus: OrderStatus,
    paymentStatus: PaymentStatus
};