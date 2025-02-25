export interface Basket{
    basketId: number,
    buyerId: number,
    items: BasketItems[]
};

export interface BasketItems{
    itemVariantId: number,
    price: number,
    quantity: number
};