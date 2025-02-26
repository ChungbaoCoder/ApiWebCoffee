import React, { useState, useEffect } from 'react';
import { AddressPost } from '../../api/models/buyerPost';

interface AddressFormProps {
    buyerId: number;
    onAddressSubmit: (addressData: AddressPost) => void; // Callback to handle address submission
    onCancel: () => void; // Callback to cancel/hide the form
}

const AddressForm: React.FC<AddressFormProps> = ({ buyerId, onAddressSubmit, onCancel }) => {
    const [street, setStreet] = useState<string>('');
    const [city, setCity] = useState<string>('');
    const [state, setState] = useState<string>('');
    const [country, setCountry] = useState<string>('');
    const [errorMessage, setErrorMessage] = useState<string | null>(null);

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setErrorMessage(null);

        const addressPost: AddressPost = {
            street,
            city,
            state,
            country,
        };

        try {
            onAddressSubmit(addressPost); // Call the callback to handle API call in parent component
            // Form clearing is handled in parent component after successful API call if needed
        } catch (error: any) {
            setErrorMessage(`Error adding address: ${error.message}`);
        }
    };

    return (
        <div>
            <h4>Add Address for Buyer ID: {buyerId}</h4>
            <form onSubmit={handleSubmit}>
                <div>
                    <label htmlFor="street">Street:</label>
                    <input type="text" id="street" value={street} onChange={(e) => setStreet(e.target.value)} required />
                </div>
                <div>
                    <label htmlFor="city">City:</label>
                    <input type="text" id="city" value={city} onChange={(e) => setCity(e.target.value)} required />
                </div>
                <div>
                    <label htmlFor="state">State/Province:</label>
                    <input type="text" id="state" value={state} onChange={(e) => setState(e.target.value)} required />
                </div>
                <div>
                    <label htmlFor="country">Country:</label>
                    <input type="text" id="country" value={country} onChange={(e) => setCountry(e.target.value)} required />
                </div>

                <button type="submit">Add Address</button>
                <button type="button" onClick={onCancel}>Cancel</button> {/* Cancel button */}

                {errorMessage && <p style={{ color: 'red' }}>{errorMessage}</p>}
            </form>
        </div>
    );
};

export default AddressForm;