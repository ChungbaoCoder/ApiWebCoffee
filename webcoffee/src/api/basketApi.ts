import axios from 'axios';
import { BasketItemPost } from './models/basketItemPost';

const BASE_URL = 'https://localhost:7055/api/basket';

const header = {
    headers: {
      'Authorization': `Bearer ${localStorage.getItem('authToken')}`
    }
};

export const getBasketById = async (basketId: number) => {
    try {
        const response = await axios.get(`${BASE_URL}/${basketId}`);
        return response.data;
    } 
    catch (error) {
        console.error(`Error fetching basket with Id ${basketId}:`, error);
        throw error;
    }
};

export const createBasketForUser = async (buyerId: number) => {
    try {
        const response = await axios.post(`${BASE_URL}`, buyerId);
        return response.data;
    } 
    catch (error) {
        console.error(`Error creating basket for buyer with Id ${buyerId}:`, error);
        throw error;
    }
};

export const addItemToBasket = async (basketId: number, basketItemPost: BasketItemPost) => {
    try {
        const response = await axios.post(`${BASE_URL}/${basketId}/item`, basketItemPost);
        return response.data;
    } 
    catch (error) {
        console.error(`Error adding item to basket with Id ${basketId}:`, error);
        throw error;
    }
};

export const mergeItemWhenLogin = async (basketId: number, basketItemsPost: BasketItemPost[]) => {
    try {
        const response = await axios.post(`${BASE_URL}/${basketId}/item/merge`, basketItemsPost);
        return response.data;
    } 
    catch (error) {
        console.error(`Error adding items to basket with Id ${basketId}:`, error);
        throw error;
    }
};

export const removeItemFromBasket = async (basketId: number, variantId: number) => {
    try {
        const response = await axios.delete(`${BASE_URL}/${basketId}/item/${variantId}`);
        return response.data;
    } 
    catch (error) {
        console.error(`Error removing item in basket with Id ${basketId}:`, error);
        throw error;
    }
};

export const deleteAllItems = async (basketId: number) => {
    try {
        const response = await axios.delete(`${BASE_URL}/${basketId}`);
        return response.data;
    } 
    catch (error) {
        console.error(`Error removing all items in basket with Id ${basketId}:`, error);
        throw error;
    }
};