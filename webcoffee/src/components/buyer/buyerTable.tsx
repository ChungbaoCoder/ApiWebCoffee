import React from 'react';
import { Buyer } from '../../entities/buyer'

interface BuyerTableProps {
    buyers: Buyer[];
    onDeleteBuyer: (buyerId: number) => void; // Callback for delete action
}

const BuyerTable: React.FC<BuyerTableProps> = ({ buyers, onDeleteBuyer }) => {
    return (
        <table>
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Name</th>
                    <th>Email</th>
                    <th>Phone Number</th>
                    <th>Date Joined</th>
                    <th>Actions</th>
                </tr>
            </thead>
            <tbody>
                {buyers.map((buyer) => (
                    <tr key={buyer.buyerId}>
                        <td>{buyer.buyerId}</td>
                        <td>{buyer.name}</td>
                        <td>{buyer.email}</td>
                        <td>{buyer.phoneNum}</td>
                        <td>{new Date(buyer.dateJoined).toLocaleDateString()}</td>
                        <td>
                            <button onClick={() => onDeleteBuyer(buyer.buyerId)}>
                                Delete Buyer
                            </button>
                            {/* Add Edit Buyer button here later if needed */}
                        </td>
                    </tr>
                ))}
            </tbody>
        </table>
    );
};

export default BuyerTable;