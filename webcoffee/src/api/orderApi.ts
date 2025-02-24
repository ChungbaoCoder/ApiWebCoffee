import axios from 'axios';
import { OrderItemPost } from './models/orderItemPost';
import { OrderStatusPatch } from './models/orderStatusPatch';

const BASE_URL = 'https://localhost:7055/api/order';

const header = {
    headers: {
      'Authorization': `Bearer ${localStorage.getItem('authToken')}`
    }
};

export const getOrderByBuyerId = async (buyerId: number) => {
    try {
        const response = await axios.get(`${BASE_URL}/buyer/${buyerId}`);
        return response.data;
    } 
    catch (error) {
        console.error(`Error fetching order by buyer Id ${buyerId}:`, error);
        throw error;
    }
};

export const getOrderById = async (orderId: number) => {
    try {
        const response = await axios.get(`${BASE_URL}/${orderId}`);
        return response.data;
    } 
    catch (error) {
        console.error(`Error fetching order with Id ${orderId}:`, error);
        throw error;
    }
};

export const createOrder = async (buyerId: number, orderItemPost: OrderItemPost[]) => {
    try {
        const response = await axios.post(`${BASE_URL}/buyer/${buyerId}`, orderItemPost);
        return response.data;
    } 
    catch (error) {
        console.error(`Error creating order for buyer with Id ${buyerId}:`, error);
        throw error;
    }
};

export const updateOrderStatus = async (orderId: number, orderStatus: OrderStatusPatch) => {
    try {
        const response = await axios.patch(`${BASE_URL}/${orderId}`, orderStatus);
        return response.data;
    } 
    catch (error) {
        console.error(`Error updating order status for order Id ${orderId}:`, error);
        throw error;
    }
};

export const deleteOrder = async (orderId: number) => {
    try {
        const response = await axios.delete(`${BASE_URL}/${orderId}`);
        return response.data;
    } 
    catch (error) {
        console.error(`Error deleting order with Id ${orderId}:`, error);
        throw error;
    }
};