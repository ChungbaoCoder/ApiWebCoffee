import { Status } from "./globalEnum";

export interface ProductPost{
    name: string,
    description: string,
    category: string,
    pictureUri?: string,
    itemVariant: ItemVariantPost[]
};

export interface ItemVariantPost{
    size?: string,
	stockQuantity?: number,
	price?: number,
	status: Status
};