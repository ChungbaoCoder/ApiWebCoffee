import React, { useState, useEffect } from 'react';
import { deleteOrder } from '../../api/orderApi';
import { getOrderByBuyerId } from '../../api/orderApi';
import { OrderStatus } from '../../api/models/globalEnum';
import { PaymentStatus } from '../../api/models/globalEnum';
import OrderDeleteButton from './orderDeleteButton';
import OrderStatusUpdateForm from './orderStatusUpdateForm';

interface OrderListByBuyerProps { }

const OrderListByBuyer: React.FC<OrderListByBuyerProps> = () => {
    const [buyerId, setBuyerId] = useState<number | undefined>(undefined);
    const [orders, setOrders] = useState<any[]>([]); // Adjust 'any[]' to your Order model if you have one
    const [loading, setLoading] = useState<boolean>(false);
    const [error, setError] = useState<Error | null>(null);
    const [fetchError, setFetchError] = useState<string | null>(null);
    const [deleteError, setDeleteError] = useState<string | null>(null);
    const [selectedOrderIdForStatusUpdate, setSelectedOrderIdForStatusUpdate] = useState<number | null>(null); // Track order for status update form

    const fetchOrders = async () => {
        setLoading(true);
        setFetchError(null);
        setError(null);
        if (buyerId === undefined) {
            setFetchError("Please enter a Buyer ID to fetch orders.");
            setLoading(false);
            return;
        }
        try {
            const response = await getOrderByBuyerId(buyerId);
            setOrders(response.data); // Access response.data.data to get the array of orders
            setLoading(false);
        } catch (fetchErr: any) {
            setError(fetchErr);
            setFetchError(`Error fetching orders: ${fetchErr.message}`);
            setLoading(false);
        }
    };

    useEffect(() => {
        // Fetch orders if buyerId is already set (e.g., after component re-render)
        if (buyerId !== undefined) {
            fetchOrders();
        }
    }, [buyerId]); // Re-fetch orders when buyerId changes

    const handleDeleteOrder = async (orderId: number) => {
        setDeleteError(null);
        try {
            await deleteOrder(orderId);
            console.log(`Order with ID ${orderId} deleted successfully.`);
            fetchOrders(); // Re-fetch orders to update the list
        } catch (deleteErr: any) {
            console.error(`Error deleting order with ID ${orderId}:`, deleteErr);
            setDeleteError(`Error deleting order: ${deleteErr.message}`);
        }
    };

    const handleShowStatusUpdateForm = (orderId: number) => {
        setSelectedOrderIdForStatusUpdate(orderId);
    };

    const handleCancelStatusUpdateForm = () => {
        setSelectedOrderIdForStatusUpdate(null);
    };

    const handleStatusUpdated = () => {
        setSelectedOrderIdForStatusUpdate(null); // Hide form after update
        fetchOrders(); // Re-fetch orders to refresh data
    };


    return (
        <div>
            <h3>Order List by Buyer ID</h3>
            <div>
                <label htmlFor="buyerId">Buyer ID:</label>
                <input
                    type="number"
                    id="buyerId"
                    value={buyerId === undefined ? '' : buyerId}
                    onChange={(e) => setBuyerId(e.target.value === '' ? undefined : parseInt(e.target.value))}
                />
                <button onClick={fetchOrders} disabled={loading}>Fetch Orders</button>
                {fetchError && <p style={{ color: 'red' }}>{fetchError}</p>}
            </div>

            {deleteError && <p style={{ color: 'red' }}>{deleteError}</p>}
            {loading ? (
                <p>Loading orders...</p>
            ) : error ? (
                <p>Error: {error.message}</p>
            ) : (
                orders.length > 0 ? (
                    <table>
                        <thead>
                            <tr>
                                <th>Order ID</th>
                                <th>Total Price</th>
                                <th>Total Items</th>
                                <th>Order Status</th>
                                <th>Payment Status</th>
                                <th>Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                            {orders.map((order: any) => ( // Adjust 'any' to your Order model
                                <tr key={order.orderId}>
                                    <td>{order.orderId}</td>
                                    <td>{order.total}</td> {/* Use order.total */}
                                    <td>{order.orderItems.length}</td> {/* Calculate totalItems */}
                                    <td>{OrderStatus[order.orderStatus]}</td>
                                    <td>{PaymentStatus[order.paymentStatus]}</td>
                                    <td>
                                        <button onClick={() => handleShowStatusUpdateForm(order.orderId)}>Update Status</button>
                                        <OrderDeleteButton orderId={order.orderId} onDeleteSuccess={fetchOrders} onError={setDeleteError} />
                                    </td>
                                </tr>
                            ))}
                        </tbody>
                    </table>
                ) : (
                    buyerId !== undefined && !loading && !error && <p>No orders found for buyer ID: {buyerId}</p>
                )
            )}

            {selectedOrderIdForStatusUpdate !== null && (
                <OrderStatusUpdateForm
                    orderId={selectedOrderIdForStatusUpdate}
                    onStatusUpdated={handleStatusUpdated}
                    onCancel={handleCancelStatusUpdateForm}
                />
            )}
        </div>
    );
};

export default OrderListByBuyer;