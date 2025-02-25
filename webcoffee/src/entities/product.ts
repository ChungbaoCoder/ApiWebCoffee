export interface Product {
	productId: number,
    name: string,
    description: string,
    category: string,
    imageUri?: string,
	itemVariant: ItemVariant[]
}

export interface ItemVariant {
	itemVariantId: number,
	size: string,
	stockQuantity: number,
	price: number,
	status: Status
}

export enum Status{
    NotSet,
    Active,
    Inactive,
    LowStock,
    OutOfStock
};