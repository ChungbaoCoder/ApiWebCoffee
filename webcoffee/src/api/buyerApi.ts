import axios from 'axios';
import { AddressPost, BuyerPost } from './models/buyerPost';
import { AddressPut, BuyerPut } from './models/buyerPut';

const BASE_URL = 'https://localhost:7055/api/buyer';

const header = {
    headers: {
      'Authorization': `Bearer ${localStorage.getItem('authToken')}`
    }
};

export const getBuyerList = async () => {
    try {
        const response = await axios.get(BASE_URL);
        return response.data;
    } 
    catch (error) {
        console.error("Error fetching buyers list:", error);
        throw error;
    }
};

export const getBuyerById = async (buyerId: number) => {
    try {
        const response = await axios.get(`${BASE_URL}/${buyerId}`);
        return response.data;
    } 
    catch (error) {
        console.error(`Error fetching buyer with Id ${buyerId}:`, error);
        throw error;
    }
};

export const createBuyer = async (buyerPost: BuyerPost) => {
    try {
        const response = await axios.post(`${BASE_URL}`, buyerPost);
        return response.data;
    } 
    catch (error) {
        console.error("Error creating buyer:", error);
        throw error;
    }
};

export const addAddress = async (buyerId: number, addressPost: AddressPost) => {
    try {
        const response = await axios.post(`${BASE_URL}/${buyerId}/address`, addressPost);
        return response.data;
    } 
    catch (error) {
        console.error(`Error add address for buyer with Id ${buyerId}:`, error);
        throw error;
    }
};

export const updateBuyer = async (buyerId: number, buyerPut: BuyerPut) => {
    try {
        const response = await axios.put(`${BASE_URL}/${buyerId}`, buyerPut);
        return response.data;
    } 
    catch (error) {
        console.error(`Error updating buyer with Id ${buyerId}:`, error);
        throw error;
    }
};

export const updateAddress = async (buyerId: number, addressId: number, addressPut: AddressPut) => {
    try {
        const response = await axios.put(`${BASE_URL}/${buyerId}/address/${addressId}`, addressPut);
        return response.data;
    } 
    catch (error) {
        console.error(`Error updating address for buyer Id ${buyerId}:`, error);
        throw error;
    }
};

export const setDefaultAddress = async (buyerId: number, addressId: number) => {
    try {
        const response = await axios.patch(`${BASE_URL}/${buyerId}/address/${addressId}`);
        return response.data;
    } 
    catch (error) {
        console.error(`Error set default address for buyer Id ${buyerId}:`, error);
        throw error;
    }
};

export const deleteBuyer = async (buyerId: number) => {
    try {
        const response = await axios.delete(`${BASE_URL}/${buyerId}`);
        return response.data;
    } 
    catch (error) {
        console.error(`Error deleting buyer with Id ${buyerId}:`, error);
        throw error;
    }
};

export const deleteAddress = async (buyerId: number, addressId: number) => {
    try {
        const response = await axios.delete(`${BASE_URL}/${buyerId}/address/${addressId}`);
        return response.data;
    } 
    catch (error) {
        console.error(`Error deleting address from buyer with Id ${buyerId}:`, error);
        throw error;
    }
};