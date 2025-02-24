export interface Order{
    orderId: number,
    buyerId: number,
    orderDate: Date,
    total: number,
    orderItems: OrderItems[],
    status: string,
    paymentStatus: string,
    address: Address,
    updatedDate: Date
};

export interface OrderItems{
    itemId: number,
    price: number,
    quantity: number,
    status: string
};

export interface Address{
    street: string,
    city: string,
    state: string,
    country: string
};