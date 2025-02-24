import { Status } from "./globalEnum";

export interface ProductPost{
    name: string,
    description: string,
    category: string,
    imgUri?: string,
    items: ItemVariantPost[]
};

export interface ItemVariantPost{
    size: string,
	stock: number,
	price: number,
	status: Status
};