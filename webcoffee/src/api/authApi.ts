import axios from 'axios';
import { LoginPost, RegisterPost } from './models/authPost';
import { jwtDecode } from 'jwt-decode';

const BASE_URL = 'https://localhost:7055/api/auth';

export const getUserRoleFromToken = (): string | null => {
    const authToken = localStorage.getItem('authToken');
    if (authToken) {
        try {
            const decodedToken: any = jwtDecode(authToken); // Decode token
            return decodedToken.role || null; // Assuming your role claim is named 'role' in the JWT payload
        } catch (error) {
            console.error("Error decoding JWT token:", error);
            return null;
        }
    }
    return null;
};

export const register = async (registerRequest: RegisterPost) => {
    try {
        const response = await axios.post(`${BASE_URL}/user/register`, registerRequest);
        return response.data;
    } 
    catch (error) {
        console.error("Error registing user:", error);
        throw error;
    }
};

export const login = async (loginRequest: LoginPost) => {
    try {
        const response = await axios.post(`${BASE_URL}/user/login`, loginRequest);
        return response.data;
    } 
    catch (error) {
        console.error("Error logging in user:", error);
        throw error;
    }
};

export const registerCustomer = async (registerRequest: RegisterPost) => {
    try {
        const response = await axios.post(`${BASE_URL}/customer/register`, registerRequest);
        return response.data;
    } 
    catch (error) {
        console.error("Error registing user:", error);
        throw error;
    }
};

export const loginCustomer = async (loginRequest: LoginPost) => {
    try {
        const response = await axios.post(`${BASE_URL}/customer/login`, loginRequest);
        return response.data;
    } 
    catch (error) {
        console.error("Error logging in user:", error);
        throw error;
    }
};