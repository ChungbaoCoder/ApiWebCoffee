import { Status } from "./globalEnum";

export interface ProductPut{
    name: string,
    description: string,
    category: string,
    pictureUri?: string
};

export interface ItemVariantPut{
    size: string,
    stockQuantity: number,
    price: number,
    status: Status
};