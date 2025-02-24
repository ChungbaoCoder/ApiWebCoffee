import axios from 'axios';
import { LoginPost, RegisterPost } from './models/authPost';

const BASE_URL = 'https://localhost:7055/api/auth/user';

const header = {
    headers: {
      'Authorization': `Bearer ${localStorage.getItem('authToken')}`
    }
};

export const register = async (registerRequest: RegisterPost) => {
    try {
        const response = await axios.post(`${BASE_URL}/register`, registerRequest);
        return response.data;
    } 
    catch (error) {
        console.error("Error registing user:", error);
        throw error;
    }
};

export const login = async (loginRequest: LoginPost) => {
    try {
        const response = await axios.post(`${BASE_URL}/login`, loginRequest);
        return response.data;
    } 
    catch (error) {
        console.error("Error logging in user:", error);
        throw error;
    }
};