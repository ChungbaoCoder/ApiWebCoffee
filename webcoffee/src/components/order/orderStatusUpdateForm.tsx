import React, { useState } from 'react';
import { OrderStatus } from '../../api/models/globalEnum';
import { PaymentStatus } from '../../api/models/globalEnum';
import { OrderStatusPatch } from '../../api/models/orderStatusPatch';
import { updateOrderStatus } from '../../api/orderApi';

interface OrderStatusUpdateFormProps {
    orderId: number;
    onStatusUpdated: () => void; // Callback when status is updated
    onCancel: () => void; // Callback to cancel/hide the form
}

const OrderStatusUpdateForm: React.FC<OrderStatusUpdateFormProps> = ({ orderId, onStatusUpdated, onCancel }) => {
    const [orderStatus, setOrderStatus] = useState<OrderStatus>(OrderStatus.PendingPayment); // Default status
    const [paymentStatus, setPaymentStatus] = useState<PaymentStatus>(PaymentStatus.Pending); // Default payment status
    const [successMessage, setSuccessMessage] = useState<string | null>(null);
    const [errorMessage, setErrorMessage] = useState<string | null>(null);

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setSuccessMessage(null);
        setErrorMessage(null);

        const statusPatch: OrderStatusPatch = {
            orderStatus,
            paymentStatus,
        };

        try {
            await updateOrderStatus(orderId, statusPatch);
            setSuccessMessage('Order status updated successfully!');
            onStatusUpdated(); // Call callback to notify parent component
        } catch (error: any) {
            setErrorMessage(`Error updating order status: ${error.message}`);
        }
    };

    return (
        <div>
            <h4>Update Order Status (Order ID: {orderId})</h4>
            <form onSubmit={handleSubmit}>
                <div>
                    <label htmlFor="orderStatus">Order Status:</label>
                    <select
                        id="orderStatus"
                        value={orderStatus}
                        onChange={(e) => setOrderStatus(parseInt(e.target.value) as OrderStatus)}
                    >
                        {Object.keys(OrderStatus).filter(v => isNaN(Number(v))).map((key) => (
                            <option key={key} value={OrderStatus[key as keyof typeof OrderStatus]}>{key}</option>
                        ))}
                    </select>
                </div>
                <div>
                    <label htmlFor="paymentStatus">Payment Status:</label>
                    <select
                        id="paymentStatus"
                        value={paymentStatus}
                        onChange={(e) => setPaymentStatus(parseInt(e.target.value) as PaymentStatus)}
                    >
                        {Object.keys(PaymentStatus).filter(v => isNaN(Number(v))).map((key) => (
                            <option key={key} value={PaymentStatus[key as keyof typeof PaymentStatus]}>{key}</option>
                        ))}
                    </select>
                </div>

                <button type="submit">Update Status</button>
                <button type="button" onClick={onCancel}>Cancel</button>

                {successMessage && <p style={{ color: 'green' }}>{successMessage}</p>}
                {errorMessage && <p style={{ color: 'red' }}>{errorMessage}</p>}
            </form>
        </div>
    );
};

export default OrderStatusUpdateForm;