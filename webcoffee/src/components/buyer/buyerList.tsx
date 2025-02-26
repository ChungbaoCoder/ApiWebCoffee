import React, { useState, useEffect } from 'react';
import { Buyer } from '../../entities/buyer'
import { getBuyerList, deleteBuyer, addAddress } from '../../api/buyerApi'
import BuyerTable from './buyerTable';
import AddressForm from './addressForm';
import { AddressPost } from '../../api/models/buyerPost';

interface BuyerListProps { }

const BuyerList: React.FC<BuyerListProps> = () => {
    const [buyers, setBuyers] = useState<Buyer[]>([]);
    const [loading, setLoading] = useState<boolean>(true);
    const [error, setError] = useState<Error | null>(null);
    const [deleteError, setDeleteError] = useState<string | null>(null);
    const [addAddressError, setAddAddressError] = useState<string | null>(null);
    const [selectedBuyerIdForAddress, setSelectedBuyerIdForAddress] = useState<number | null>(null); // Track buyer for address form

    const fetchBuyers = async () => {
        setLoading(true);
        setError(null);
        try {
            const response = await getBuyerList();
            setBuyers(response.data); // Assuming API returns items in response.data.items
            setLoading(false);
        } catch (err: any) {
            setError(err);
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchBuyers();
    }, []); // Fetch buyers on component mount

    const handleDeleteBuyer = async (buyerId: number) => {
        setDeleteError(null);
        try {
            await deleteBuyer(buyerId);
            console.log(`Buyer with ID ${buyerId} deleted successfully.`);
            fetchBuyers(); // Re-fetch buyers to update the list
        } catch (deleteErr: any) {
            console.error(`Error deleting buyer with ID ${buyerId}:`, deleteErr);
            setDeleteError(`Error deleting buyer: ${deleteErr.message}`);
        }
    };

    const handleAddAddressToBuyer = async (buyerId: number, addressPost: AddressPost) => {
        setAddAddressError(null);
        try {
            await addAddress(buyerId, addressPost);
            console.log(`Address added to buyer with ID ${buyerId} successfully.`);
            setSelectedBuyerIdForAddress(null); // Close the address form after successful add
            // Optionally re-fetch buyer details if you want to display addresses in the buyer list (more complex)
            alert('Address added successfully!'); // Simple feedback for now
        } catch (addressErr: any) {
            console.error(`Error adding address to buyer with ID ${buyerId}:`, addressErr);
            setAddAddressError(`Error adding address: ${addressErr.message}`);
        }
    };

    const handleShowAddressForm = (buyerId: number) => {
        setSelectedBuyerIdForAddress(buyerId); // Set the selected buyer ID to show address form
    };

    const handleCancelAddressForm = () => {
        setSelectedBuyerIdForAddress(null); // Hide address form
    };


    return (
        <div>
            <h2>Buyer List</h2>
            {deleteError && <p style={{ color: 'red' }}>{deleteError}</p>}
            {addAddressError && <p style={{ color: 'red' }}>{addAddressError}</p>}
            {loading ? (
                <p>Loading buyers...</p>
            ) : error ? (
                <p>Error fetching buyers: {error.message}</p>
            ) : (
                <>
                    <BuyerTable buyers={buyers} onDeleteBuyer={handleDeleteBuyer} />

                    {buyers.map(buyer => (
                        <div key={buyer.buyerId} style={{ marginTop: '20px', borderTop: '1px solid #ccc', paddingTop: '10px' }}>
                            <button onClick={() => handleShowAddressForm(buyer.buyerId)} disabled={selectedBuyerIdForAddress === buyer.buyerId}>
                                Add Address for {buyer.name}
                            </button>

                            {selectedBuyerIdForAddress === buyer.buyerId && (
                                <AddressForm
                                    buyerId={buyer.buyerId}
                                    onAddressSubmit={(addressData) => handleAddAddressToBuyer(buyer.buyerId, addressData)}
                                    onCancel={handleCancelAddressForm}
                                />
                            )}
                        </div>
                    ))}
                </>
            )}
        </div>
    );
};

export default BuyerList;