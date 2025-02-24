import { OrderItemStatus, OrderStatus } from "./globalEnum";

export interface OrderStatusPatch {
    orderStatus: OrderStatus,
    orderItemStatus: OrderItemStatus
};