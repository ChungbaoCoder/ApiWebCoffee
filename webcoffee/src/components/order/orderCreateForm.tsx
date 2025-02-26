import React, { useState } from 'react';
import { OrderItemPost } from '../../api/models/orderItemPost';
import { createOrder } from '../../api/orderApi';

interface OrderCreateFormProps {
    onOrderCreated?: () => void; // Optional callback for when an order is created
}

const OrderCreateForm: React.FC<OrderCreateFormProps> = ({ onOrderCreated }) => {
    const [buyerId, setBuyerId] = useState<number | undefined>(undefined);
    const [orderItems, setOrderItems] = useState<OrderItemPost[]>([{ itemVariantId: undefined, price: undefined, quantity: 1 }]);
    const [successMessage, setSuccessMessage] = useState<string | null>(null);
    const [errorMessage, setErrorMessage] = useState<string | null>(null);

    const handleAddItem = () => {
        setOrderItems([...orderItems, { itemVariantId: undefined, price: undefined, quantity: 1 }]);
    };

    const handleRemoveItem = (index: number) => {
        const updatedItems = orderItems.filter((_, i) => i !== index);
        setOrderItems(updatedItems);
    };

    const handleItemChange = (index: number, field: keyof OrderItemPost, value: any) => {
        const updatedItems = orderItems.map((item, i) =>
            i === index ? { ...item, [field]: value } : item
        );
        setOrderItems(updatedItems);
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setSuccessMessage(null);
        setErrorMessage(null);

        if (buyerId === undefined) {
            setErrorMessage("Buyer ID is required.");
            return;
        }

        // Basic client-side validation for itemVariantId, price, quantity (you can enhance this)
        for (const item of orderItems) {
            if (item.itemVariantId === undefined || item.price === undefined || item.quantity === undefined || item.quantity <= 0 || item.price < 0) {
                setErrorMessage("Please ensure all order items have valid Variant ID, Price (>=0), and Quantity (>0).");
                return;
            }
        }

        try {
            await createOrder(buyerId, orderItems);
            setSuccessMessage('Order created successfully!');
            if (onOrderCreated) {
                onOrderCreated();
            }
            // Reset form
            setBuyerId(undefined);
            setOrderItems([{ itemVariantId: undefined, price: undefined, quantity: 1 }]);
        } catch (error: any) {
            setErrorMessage(`Error creating order: ${error.message}`);
        }
    };

    return (
        <div>
            <h3>Create New Order</h3>
            <form onSubmit={handleSubmit}>
                <div>
                    <label htmlFor="buyerId">Buyer ID:</label>
                    <input
                        type="number"
                        id="buyerId"
                        value={buyerId === undefined ? '' : buyerId}
                        onChange={(e) => setBuyerId(e.target.value === '' ? undefined : parseInt(e.target.value))}
                        required
                    />
                </div>

                <h4>Order Items</h4>
                {orderItems.map((item, index) => (
                    <div key={index} style={{ border: '1px solid #eee', padding: '10px', margin: '10px 0' }}>
                        <h5>Item #{index + 1}</h5>
                        <div>
                            <label htmlFor={`variantId-${index}`}>Variant ID:</label>
                            <input
                                type="number"
                                id={`variantId-${index}`}
                                value={item.itemVariantId === undefined ? '' : item.itemVariantId}
                                onChange={(e) => handleItemChange(index, 'itemVariantId', e.target.value === '' ? undefined : parseInt(e.target.value))}
                                required
                            />
                        </div>
                        <div>
                            <label htmlFor={`price-${index}`}>Price:</label>
                            <input
                                type="number"
                                id={`price-${index}`}
                                value={item.price === undefined ? '' : item.price}
                                onChange={(e) => handleItemChange(index, 'price', e.target.value === '' ? undefined : parseFloat(e.target.value))}
                                required
                            />
                        </div>
                        <div>
                            <label htmlFor={`quantity-${index}`}>Quantity:</label>
                            <input
                                type="number"
                                id={`quantity-${index}`}
                                value={item.quantity}
                                onChange={(e) => handleItemChange(index, 'quantity', parseInt(e.target.value))}
                                min="1"
                                required
                            />
                        </div>
                        <button type="button" onClick={() => handleRemoveItem(index)}>Remove Item</button>
                    </div>
                ))}
                <button type="button" onClick={handleAddItem}>Add Item</button>

                <button type="submit">Create Order</button>

                {successMessage && <p style={{ color: 'green' }}>{successMessage}</p>}
                {errorMessage && <p style={{ color: 'red' }}>{errorMessage}</p>}
            </form>
        </div>
    );
};

export default OrderCreateForm;