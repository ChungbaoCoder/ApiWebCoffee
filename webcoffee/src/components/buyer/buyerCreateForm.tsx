import React, { useState } from 'react';
import { createBuyer } from '../../api/buyerApi';
import { BuyerPost } from '../../api/models/buyerPost';

interface BuyerCreateFormProps {
    onBuyerCreated?: () => void; // Optional callback when buyer is created
}

const BuyerCreateForm: React.FC<BuyerCreateFormProps> = ({ onBuyerCreated }) => {
    const [name, setName] = useState<string>('');
    const [email, setEmail] = useState<string>('');
    const [phoneNum, setPhoneNum] = useState<string>('');
    const [successMessage, setSuccessMessage] = useState<string | null>(null);
    const [errorMessage, setErrorMessage] = useState<string | null>(null);

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setSuccessMessage(null);
        setErrorMessage(null);

        const buyerPost: BuyerPost = {
            name,
            email,
            phoneNum,
        };

        try {
            await createBuyer(buyerPost);
            setSuccessMessage('Buyer created successfully!');
            if (onBuyerCreated) {
                onBuyerCreated(); // Callback to refresh buyer list
            }
            // Reset form fields
            setName('');
            setEmail('');
            setPhoneNum('');
        } catch (error: any) {
            setErrorMessage(`Error creating buyer: ${error.message}`);
        }
    };

    return (
        <div>
            <h3>Create New Buyer</h3>
            <form onSubmit={handleSubmit}>
                <div>
                    <label htmlFor="name">Name:</label>
                    <input type="text" id="name" value={name} onChange={(e) => setName(e.target.value)} required />
                </div>
                <div>
                    <label htmlFor="email">Email:</label>
                    <input type="email" id="email" value={email} onChange={(e) => setEmail(e.target.value)} required />
                </div>
                <div>
                    <label htmlFor="phoneNum">Phone Number:</label>
                    <input type="tel" id="phoneNum" value={phoneNum} onChange={(e) => setPhoneNum(e.target.value)} required />
                </div>

                <button type="submit">Create Buyer</button>

                {successMessage && <p style={{ color: 'green' }}>{successMessage}</p>}
                {errorMessage && <p style={{ color: 'red' }}>{errorMessage}</p>}
            </form>
        </div>
    );
};

export default BuyerCreateForm;