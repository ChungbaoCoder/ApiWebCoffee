import React, { useState, useEffect } from 'react';
import { createProduct, createVariants } from '../../api/productApi';
import { ProductPost, ItemVariantPost } from '../../api/models/productPost';
import { ProductPut, ItemVariantPut } from '../../api/models/productPut';
import { Status } from '../../api/models/globalEnum';

interface ProductCreateFormProps {
    onProductCreated?: () => void;
}

const ProductCreateForm: React.FC<ProductCreateFormProps> = ({ onProductCreated }) => {
    const [name, setName] = useState<string>('');
    const [description, setDescription] = useState<string>('');
    const [category, setCategory] = useState<string>('');
    const [pictureUri, setPictureUri] = useState<string>('');
    const [variants, setVariants] = useState<ItemVariantPost[]>([{ size: '', stockQuantity: undefined, price: undefined, status: Status.Active }]); // Array of variants
    const [successMessage, setSuccessMessage] = useState<string | null>(null);
    const [errorMessage, setErrorMessage] = useState<string | null>(null);

    const handleAddVariant = () => {
        setVariants([...variants, { size: '', stockQuantity: undefined, price: undefined, status: Status.Active }]);
    };

    const handleRemoveVariant = (index: number) => {
        const updatedVariants = variants.filter((_, i) => i !== index);
        setVariants(updatedVariants);
    };

    const handleVariantChange = (index: number, field: keyof ItemVariantPost, value: any) => {
        const updatedVariants = variants.map((variant, i) =>
            i === index ? { ...variant, [field]: value } : variant
        );
        setVariants(updatedVariants);
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setSuccessMessage(null);
        setErrorMessage(null);

        // Basic client-side validation for variants
        for (const variant of variants) {
            if (variant.size === '' || variant.stockQuantity === undefined || variant.price === undefined || variant.stockQuantity < 0 || variant.price < 0) {
                setErrorMessage("Please ensure all variants have valid Size, Stock (>=0), and Price (>=0).");
                return;
            }
        }

        const productPost: ProductPost = {
            name,
            description,
            category,
            pictureUri,
            itemVariant: variants // Use the variants array from state
        };

        console.log("Product Post Data:", productPost);

        try {
            const createdProductResponse = await createProduct(productPost);
            console.log("Create Product Response:", createdProductResponse);

            setSuccessMessage('Product created successfully!');
            if (onProductCreated) {
                onProductCreated();
            }
            // Reset form
            setName('');
            setDescription('');
            setCategory('');
            setPictureUri('');
            setVariants([{ size: '', stockQuantity: undefined, price: undefined, status: Status.Active }]); // Reset to one empty variant
        } catch (error: any) {
            setErrorMessage(`Error creating product: ${error.message}`);
            console.error("Error creating product:", error);
        }
    };

    return (
        <div>
            <h3>Create New Product</h3>
            <form onSubmit={handleSubmit}>
                <div>
                    <label htmlFor="name">Name:</label>
                    <input type="text" id="name" value={name} onChange={(e) => setName(e.target.value)} required />
                </div>
                <div>
                    <label htmlFor="description">Description:</label>
                    <textarea id="description" value={description} onChange={(e) => setDescription(e.target.value)} />
                </div>
                <div>
                    <label htmlFor="category">Category:</label>
                    <input type="text" id="category" value={category} onChange={(e) => setCategory(e.target.value)} required />
                </div>
                <div>
                    <label htmlFor="pictureUri">Image URL (Optional):</label>
                    <input type="text" id="pictureUri" value={pictureUri} onChange={(e) => setPictureUri(e.target.value)} />
                </div>
                <h4>Variants</h4>
                {variants.map((variant, index) => (
                    <div key={index} style={{ border: '1px solid #eee', padding: '10px', margin: '10px 0' }}>
                        <h5>Variant #{index + 1}</h5>
                        <div>
                            <label htmlFor={`size-${index}`}>Size:</label>
                            <input
                                type="text"
                                id={`size-${index}`}
                                value={variant.size}
                                onChange={(e) => handleVariantChange(index, 'size', e.target.value)}
                                required
                            />
                        </div>
                        <div>
                            <label htmlFor={`stockQuantity-${index}`}>Stock:</label>
                            <input
                                type="number"
                                id={`stockQuantity-${index}`}
                                value={variant.stockQuantity === undefined ? '' : variant.stockQuantity}
                                onChange={(e) => handleVariantChange(index, 'stockQuantity', e.target.value === '' ? undefined : parseInt(e.target.value))}
                                required
                                min="0"
                            />
                        </div>
                        <div>
                            <label htmlFor={`price-${index}`}>Price:</label>
                            <input
                                type="number"
                                id={`price-${index}`}
                                value={variant.price === undefined ? '' : variant.price}
                                onChange={(e) => handleVariantChange(index, 'price', e.target.value === '' ? undefined : parseFloat(e.target.value))}
                                required
                                min="0"
                            />
                        </div>
                        <div>
                            <label htmlFor={`status-${index}`}>Status:</label>
                            <select
                                id={`status-${index}`}
                                value={variant.status}
                                onChange={(e) => handleVariantChange(index, 'status', parseInt(e.target.value) as Status)}
                            >
                                {Object.keys(Status).filter(v => !isNaN(Number(v))).map((key) => (
                                    <option key={key} value={Status[key as keyof typeof Status]}>{key}</option>
                                ))}
                            </select>
                        </div>
                        <button type="button" onClick={() => handleRemoveVariant(index)}>Remove Variant</button>
                    </div>
                ))}
                <button type="button" onClick={handleAddVariant}>Add Variant</button>

                <button type="submit">Create Product</button>

                {successMessage && <p style={{ color: 'green' }}>{successMessage}</p>}
                {errorMessage && <p style={{ color: 'red' }}>{errorMessage}</p>}
            </form>
        </div>
    );
};

export default ProductCreateForm;