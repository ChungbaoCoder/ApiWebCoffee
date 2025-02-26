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
    const [pictureUri, setImgUri] = useState<string>('');
    const [size, setSize] = useState<string>('');
    const [stockQuantity, setStock] = useState<number | undefined>(undefined);
    const [price, setPrice] = useState<number | undefined>(undefined);
    const [successMessage, setSuccessMessage] = useState<string | null>(null);
    const [errorMessage, setErrorMessage] = useState<string | null>(null);

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setSuccessMessage(null);
        setErrorMessage(null);

        if (stockQuantity === undefined || price === undefined) {
            setErrorMessage("Stock and Price for variant are required.");
            return;
        }

        const productPost: ProductPost = {
            name,
            description,
            category,
            pictureUri,
            itemVariant: [{
                size,
                stockQuantity,
                price,
                status: Status.Active
            }]
        };

        try {
            const createdProductResponse = await createProduct(productPost);
            const productId = createdProductResponse.data.productId;

            const variantPost: ItemVariantPost = {
                size,
                stockQuantity,
                price,
                status: Status.Active
            };
            await createVariants(productId, variantPost);

            setSuccessMessage('Product created successfully!');
            if (onProductCreated) {
                onProductCreated();
            }

            setName('');
            setDescription('');
            setCategory('');
            setImgUri('');
            setSize('');
            setStock(undefined);
            setPrice(undefined);

        } catch (error: any) {
            setErrorMessage(`Error creating product: ${error.message}`);
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
                    <label htmlFor="imgUri">Image URL (Optional):</label>
                    <input type="text" id="imgUri" value={pictureUri} onChange={(e) => setImgUri(e.target.value)} />
                </div>
                <h4>Variant Information (At least one variant required)</h4>
                <div>
                    <label htmlFor="size">Size:</label>
                    <input type="text" id="size" value={size} onChange={(e) => setSize(e.target.value)} required />
                </div>
                <div>
                    <label htmlFor="stock">Stock:</label>
                    <input type="number" id="stock" value={stockQuantity === undefined ? '' : stockQuantity} onChange={(e) => setStock(parseInt(e.target.value))} required />
                </div>
                <div>
                    <label htmlFor="price">Price:</label>
                    <input type="number" id="price" value={price === undefined ? '' : price} onChange={(e) => setPrice(parseFloat(e.target.value))} required />
                </div>


                <button type="submit">Create Product</button>

                {successMessage && <p style={{ color: 'green' }}>{successMessage}</p>}
                {errorMessage && <p style={{ color: 'red' }}>{errorMessage}</p>}
            </form>
        </div>
    );
};

export default ProductCreateForm;