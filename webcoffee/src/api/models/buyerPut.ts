export interface BuyerPut {
    name: string
    email: string,
    phoneNum: string
}

export interface AddressPut {
    addressId: number,
    street: string,
    city: string,
    state: string,
    country: string
}