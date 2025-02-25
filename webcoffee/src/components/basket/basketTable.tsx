import React, { useState, useEffect } from 'react';
import { Basket, BasketItems } from '../../entities/basket';
import { getBasketById, removeItemFromBasket } from '../../api/basketApi';

interface BasketTableProps {
    basketId: number;
}

const BasketTable: React.FC<BasketTableProps> = ({ basketId }) => {
    const [basket, setBasket] = useState<Basket | null>(null);
    const [loading, setLoading] = useState<boolean>(true);
    const [error, setError] = useState<Error | null>(null);
    const [removeItemError, setRemoveItemError] = useState<string | null>(null);

    const fetchBasket = async () => {
        setLoading(true);
        setError(null);
        try {
            const response = await getBasketById(basketId);
            setBasket(response.data);
            setLoading(false);
            setRemoveItemError(null);
        } catch (err: any) {
            setError(err);
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchBasket();
    }, [basketId]);

    const handleRemoveItem = async (variantId: number) => {
        setRemoveItemError(null);
        try {
            await removeItemFromBasket(basketId, variantId);
            console.log(`Item with variantId ${variantId} removed successfully.`);
            fetchBasket();
        } catch (removeError: any) {
            console.error(`Error removing item with variantId ${variantId}:`, removeError);
            setRemoveItemError(`Error removing item: ${removeError.message}`);
        }
    };

    if (loading) {
        return <p>Loading basket data...</p>;
    }

    if (error) {
        return <p>Error fetching basket: {error.message}</p>;
    }

    if (!basket) {
        return <p>Basket not found or data is unavailable.</p>;
    }

    if (!basket.items || basket.items.length === 0) {
        return <p>Your basket is empty.</p>;
    }

    return (
        <div>
            <table>
                <thead>
                    <tr>
                        <th>Item Variant ID</th>
                        <th>Price</th>
                        <th>Quantity</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {basket.items.map((item: BasketItems) => (
                        <tr key={item.itemVariantId}>
                            <td>{item.itemVariantId}</td>
                            <td>{item.price}</td>
                            <td>{item.quantity}</td>
                            <td>
                                <button onClick={() => handleRemoveItem(item.itemVariantId)}>
                                    Remove
                                </button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
            {removeItemError && <p style={{ color: 'red' }}>{removeItemError}</p>}
        </div>
    );
};

export default BasketTable;