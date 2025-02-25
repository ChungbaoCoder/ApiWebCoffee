import React, { useState, useEffect } from 'react';
import { BasketItemPost } from '../../api/models/basketItemPost';
import { addItemToBasket } from '../../api/basketApi';

interface AddItemFormProps {
    basketId: number;
    onItemAdded?: () => void;
}

const AddItemForm: React.FC<AddItemFormProps> = ({ basketId, onItemAdded }) => {
    const [variantId, setVariantId] = useState<number | undefined>(undefined);
    const [quantity, setQuantity] = useState<number>(1);
    const [successMessage, setSuccessMessage] = useState<string | null>(null);
    const [errorMessage, setErrorMessage] = useState<string | null>(null);

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();

        setSuccessMessage(null);
        setErrorMessage(null);

        if (variantId === undefined) {
            setErrorMessage("Please enter a Variant ID.");
            return;
        }

        const basketItemPost: BasketItemPost = {
            itemVariantId: variantId,
            quantity: quantity,
        };

        try {
            await addItemToBasket(basketId, basketItemPost);
            setSuccessMessage('Item added to basket successfully!');
            if (onItemAdded) {
                onItemAdded();
            }

            setVariantId(undefined);
            setQuantity(1);
        } catch (error: any) {
            setErrorMessage(`Error adding item to basket: ${error.message}`);
        }
    };

    return (
        <div>
            <form onSubmit={handleSubmit}>
                <div>
                    <label htmlFor="variantId">Variant ID:</label>
                    <input
                        type="number"
                        id="variantId"
                        value={variantId === undefined ? '' : variantId}
                        onChange={(e) => setVariantId(e.target.value === '' ? undefined : parseInt(e.target.value))}
                        required
                    />
                </div>
                <div>
                    <label htmlFor="quantity">Quantity:</label>
                    <input
                        type="number"
                        id="quantity"
                        value={quantity}
                        onChange={(e) => setQuantity(parseInt(e.target.value))}
                        min="1"
                        required
                    />
                </div>
                <button type="submit">Add Item</button>

                {successMessage && <p style={{ color: 'green' }}>{successMessage}</p>}
                {errorMessage && <p style={{ color: 'red' }}>{errorMessage}</p>}
            </form>
        </div>
    );
};

export default AddItemForm;