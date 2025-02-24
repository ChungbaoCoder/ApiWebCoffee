export interface Basket{
    basketId: number,
    buyerId: number,
    items: BasketItems[]
};

export interface BasketItems{
    itemId: number,
    price: number,
    quantity: number
};