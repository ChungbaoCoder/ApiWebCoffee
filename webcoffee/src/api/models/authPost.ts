export interface RegisterPost {
    name: string,
    email: string,
    phoneNum: string,
    password: string
};

export interface LoginPost {
    email: string,
    password: string
}

export interface TokenResponse {
    token: string,
    refreshToken: string,
    dateAdded: Date,
    dateExpired: Date
};