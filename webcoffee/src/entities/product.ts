export interface Product {
	id: number,
    name: string,
    description: string,
    category: string,
    imageUrl?: string,
	detail: ItemVariant[]
}

export interface ItemVariant {
	id: number,
	size: string,
	stock: number,
	price: number,
	status: string
}