import React, { useState, useEffect } from 'react';
import { getProductList, deleteProduct } from '../../api/productApi';
import { Product, ItemVariant, Status } from '../../entities/product';

interface ProductListProps { }

const ProductList: React.FC<ProductListProps> = () => {
    const [products, setProducts] = useState<Product[]>([]);
    const [loading, setLoading] = useState<boolean>(true);
    const [error, setError] = useState<Error | null>(null);
    const [deleteError, setDeleteError] = useState<string | null>(null);
    const [page, setPage] = useState<number>(1);
    const [pageSize, setPageSize] = useState<number>(5);

    const fetchProducts = async () => {
        setLoading(true);
        setError(null);
        try {
            const response = await getProductList(page, pageSize);
            setProducts(response.data);
            setLoading(false);
        } catch (err: any) {
            setError(err);
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchProducts();
    }, [page, pageSize]);

    const handleDeleteProduct = async (productId: number) => {
        setDeleteError(null);
        try {
            await deleteProduct(productId);
            console.log(`Product with ID ${productId} deleted successfully.`);
            fetchProducts();
        } catch (deleteErr: any) {
            console.error(`Error deleting product with ID ${productId}:`, deleteErr);
            setDeleteError(`Error deleting product: ${deleteErr.message}`);
        }
    };

    const handlePageChange = (newPage: number) => {
        if (newPage >= 1) {
            setPage(newPage);
        }
    };

    return (
        <div>
            <h2>Product List</h2>
            {deleteError && <p style={{ color: 'red' }}>{deleteError}</p>}
            {loading ? (
                <p>Loading products...</p>
            ) : error ? (
                <p>Error fetching products: {error.message}</p>
            ) : (
                <>
                    <table>
                        <thead>
                            <tr>
                                <th>ID</th>
                                <th>Name</th>
                                <th>Category</th>
                                <th>Description</th>
                                <th>Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                            {products.map((product) => (
                                <React.Fragment key={product.productId}> {/* Use React.Fragment to avoid extra div */}
                                    <tr>
                                        <td>{product.productId}</td>
                                        <td>{product.name}</td>
                                        <td>{product.category}</td>
                                        <td>{product.description}</td>
                                        <td>
                                            <button onClick={() => handleDeleteProduct(product.productId)}>
                                                Delete
                                            </button>
                                            {/* Add Edit button here later */}
                                        </td>
                                    </tr>
                                    {/* Nested table for variants */}
                                    <tr>
                                        <td colSpan={5}> {/* Span all columns of the product row */}
                                            <h4>Variants:</h4>
                                            <table>
                                                <thead>
                                                    <tr>
                                                        <th>Variant ID</th>
                                                        <th>Size</th>
                                                        <th>Stock</th>
                                                        <th>Price</th>
                                                        <th>Status</th>
                                                        {/* Add Variant Actions if needed (Edit/Delete Variant) */}
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    {product.itemVariant.map((variant) => (
                                                        <tr key={variant.itemVariantId}>
                                                            <td>{variant.itemVariantId}</td>
                                                            <td>{variant.size}</td>
                                                            <td>{variant.stockQuantity}</td>
                                                            <td>{variant.price}</td>
                                                            <td>{Status[variant.status]}</td> {/* Display Status enum as string */}
                                                            {/* Add Variant Action buttons here if needed */}
                                                        </tr>
                                                    ))}
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                </React.Fragment>
                            ))}
                        </tbody>
                    </table>
                    <div className="pagination">
                        <button onClick={() => handlePageChange(page - 1)} disabled={page <= 1}>
                            Previous Page
                        </button>
                        <span>Page {page}</span>
                        <button onClick={() => handlePageChange(page + 1)}>
                            Next Page
                        </button>
                    </div>
                </>
            )}
        </div>
    );
};

export default ProductList;