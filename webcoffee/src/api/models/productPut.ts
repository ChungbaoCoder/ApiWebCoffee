import { Status } from "./globalEnum";

export interface ProductPut{
    name: string,
    description: string,
    category: string,
    imgUri?: string
};

export interface ItemVariantPut{
    size: string,
    stock: number,
    price: number,
    status: Status
};