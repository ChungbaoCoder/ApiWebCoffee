import React, { useState } from 'react';
import { deleteOrder } from '../../api/orderApi';

interface OrderDeleteButtonProps {
    orderId: number;
    onDeleteSuccess: () => void; // Callback for successful deletion
    onError: (message: string) => void; // Callback for error handling
}

const OrderDeleteButton: React.FC<OrderDeleteButtonProps> = ({ orderId, onDeleteSuccess, onError }) => {
    const handleDelete = async () => {
        try {
            await deleteOrder(orderId);
            console.log(`Order with ID ${orderId} deleted successfully.`);
            onDeleteSuccess(); // Call success callback to refresh list
        } catch (error: any) {
            console.error(`Error deleting order with ID ${orderId}:`, error);
            onError(`Error deleting order: ${error.message}`); // Call error callback
        }
    };

    return (
        <button onClick={handleDelete} style={{ marginLeft: '5px', backgroundColor: 'red', color: 'white', border: 'none', padding: '8px 12px', borderRadius: '4px', cursor: 'pointer' }}>
            Delete Order
        </button>
    );
};

export default OrderDeleteButton;