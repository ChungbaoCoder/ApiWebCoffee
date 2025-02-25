export interface Buyer {
    buyerId: number,
    name: string
    email: string,
    phoneNum: string,
    dateJoined: Date
};

export interface Address {
    addressId: number,
    street: string,
    city: string,
    state: string,
    country: string
};