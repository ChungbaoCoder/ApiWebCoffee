import axios from 'axios';
import { ItemVariantPost, ProductPost } from './models/productPost';
import { ItemVariantPut, ProductPut } from './models/productPut';

const BASE_URL = 'https://localhost:7055/api/product';

const header = {
    headers: {
      'Authorization': `Bearer ${localStorage.getItem('authToken')}`
    }
};

export const getProductList = async (page: number = 1, pageSize: number = 10) => {
    try {
        const response = await axios.get(`${BASE_URL}?page=${page}&pageSize=${pageSize}`);
        return response.data;
    } 
    catch (error) {
        console.error("Error fetching products list:", error);
        throw error;
    }
};

export const getProductById = async (productId: number) => {
    try {
        const response = await axios.get(`${BASE_URL}/${productId}`);
        return response.data;
    } 
    catch (error) {
        console.error(`Error fetching product with Id ${productId}:`, error);
        throw error;
    }
};


export const createProduct = async (productPost: ProductPost) => {
    try {
        const response = await axios.post(BASE_URL, productPost);
        return response.data;
    } 
    catch (error) {
        console.error("Error creating product:", error);
        throw error;
    }
};

export const createVariants = async (productId: number, variantsPost: ItemVariantPost) => {
    try {
        const response = await axios.post(`${BASE_URL}/${productId}/variant`, variantsPost);
        return response.data;
    }
    catch (error) {
        console.error(`Error creating variants for product Id ${productId}:`, error);
        throw error;
    }
}

export const updateProduct = async (productId: number, productPut: ProductPut) => {
    try {
        const response = await axios.put(`${BASE_URL}/${productId}`, productPut);
        return response.data;
    } 
    catch (error) {
        console.error(`Error updating product with Id ${productId}:`, error);
        throw error;
    }
};

export const updateVariants = async (productId: number, variantId: number, variantsPut: ItemVariantPut) => {
    try {
        const response = await axios.put(`${BASE_URL}/${productId}/variant/${variantId}`, variantsPut);
        return response.data;
    }
    catch (error) {
        console.error(`Error updating variants for product Id ${productId}:`, error);
        throw error;
    }
}

export const deleteProduct = async (productId: number) => {
    try {
      const response = await axios.delete(`${BASE_URL}/${productId}`);
      return response.data;
    } catch (error) {
      console.error(`Error deleting product with Id ${productId}:`, error);
      throw error;
    }
};