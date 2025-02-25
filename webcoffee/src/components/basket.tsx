import React, {
    createContext,
    useState,
    useEffect,
    useCallback,
    Dispatch,
    SetStateAction,
} from 'react';
import { getBasketById, createBasketForUser, addItemToBasket, removeItemFromBasket, deleteAllItems, mergeItemWhenLogin } from '../api/basketApi'; // Assuming basketApi.ts is in ../api
import { Basket, BasketItems } from '../entities/basket'; // Assuming Basket interfaces are defined
import jwt_decode, { jwtDecode } from 'jwt-decode';
import { BasketItemPost } from '../api/models/basketItemPost';

interface BasketContextProps {
    basket: Basket | null; // Could be null if no basket yet or loading
    localStorageBasket: BasketItemsLocalStorage[]; // For logged-out users
    isLoggedIn: boolean;
    loadingBasket: boolean;
    addToCart: (variantId: number, quantity: number) => Promise<void>;
    removeFromCart: (variantId: number) => Promise<void>;
    clearCart: () => Promise<void>;
    updateCartItemQuantity: (variantId: number, quantity: number) => Promise<void>;
    setLocalStorageBasketState: Dispatch<SetStateAction<BasketItemsLocalStorage[]>>; // For direct localStorage state updates if needed
}

// Define the shape of items in localStorage (simplified compared to database BasketItems if needed)
export interface BasketItemsLocalStorage {
    variantId: number;
    quantity: number;
}

const BasketContext = createContext<BasketContextProps | undefined>(undefined);

const localStorageBasketKey = 'shoppingCart'; // Key for localStorage

const getBuyerIdFromToken = (): number | null => {
    const token = localStorage.getItem('authToken');
    if (token) {
        try {
            const decodedToken: any = jwtDecode(token); // Type 'any' for simplicity, you can create a proper interface
            // Assuming your JWT payload has 'buyerId' or a similar claim
            return decodedToken.buyerId || decodedToken.sub; // 'sub' is common for user ID in JWT
        } catch (error) {
            console.error('Error decoding JWT token:', error);
            return null;
        }
    }
    return null;
};

const getLocalStorageBasket = (): BasketItemsLocalStorage[] => {
    const cartData = localStorage.getItem(localStorageBasketKey);
    return cartData ? JSON.parse(cartData) : [];
};

const setLocalStorageBasket = (items: BasketItemsLocalStorage[]) => {
    localStorage.setItem(localStorageBasketKey, JSON.stringify(items));
};

export const BasketProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
    const [basket, setBasket] = useState<Basket | null>(null);
    const [localStorageBasket, setLocalStorageBasketState] = useState<BasketItemsLocalStorage[]>(getLocalStorageBasket());
    const [loadingBasket, setLoadingBasket] = useState<boolean>(false);
    const isLoggedIn = !!localStorage.getItem('authToken'); // Simple check for token presence

    const buyerId = getBuyerIdFromToken();

    // Function to initialize the cart based on login status
    const initializeCart = useCallback(async () => {
        setLoadingBasket(true);
        try {
            if (isLoggedIn && buyerId) {
                // Logged-in user: Fetch or create basket from database
                try {
                    const fetchedBasket = await getBasketById(buyerId); // Assuming your getBasketById uses buyerId as basketId for logged in users
                    setBasket(fetchedBasket);
                } catch (error: any) {
                    if (error.response && error.response.status === 404) { // Basket not found, create one
                        const newBasket = await createBasketForUser(buyerId);
                        setBasket(newBasket);
                    } else {
                        console.error("Error fetching or creating basket:", error);
                        // Handle error appropriately (e.g., display error message)
                    }
                }
            } else {
                // Logged-out user: Load cart from localStorage (already done in useState initialization)
                setBasket(null); // No database basket for logged-out users
            }
        } finally {
            setLoadingBasket(false);
        }
    }, [isLoggedIn, buyerId]); // Depend on isLoggedIn and buyerId to re-initialize on login/logout


    useEffect(() => {
        initializeCart();
    }, [initializeCart]);

    // Function to handle cart merging on login
    const mergeCartsOnLogin = useCallback(async () => {
        if (isLoggedIn && buyerId && localStorageBasket.length > 0 && basket?.basketId) {
            try {
                const basketItemsPost: BasketItemPost[] = localStorageBasket.map(item => ({
                    variantId: item.variantId,
                    quantity: item.quantity
                }));
                await mergeItemWhenLogin(basket.basketId, basketItemsPost);

                // After merging, fetch the updated basket from the database
                const updatedBasket = await getBasketById(basket.basketId);
                setBasket(updatedBasket);

                // Clear localStorage cart
                setLocalStorageBasketState([]);
                localStorage.removeItem(localStorageBasketKey);

            } catch (error) {
                console.error("Error merging carts on login:", error);
                // Handle error (e.g., show error message to user)
            }
        }
    }, [isLoggedIn, buyerId, localStorageBasket, basket?.basketId]);


    useEffect(() => {
        if (isLoggedIn) {
            mergeCartsOnLogin(); // Attempt to merge carts when user logs in
        }
    }, [isLoggedIn, mergeCartsOnLogin]);


    const addToCart = useCallback(async (variantId: number, quantity: number) => {
        if (isLoggedIn && basket?.basketId) {
            // Logged-in: API call to add to database basket
            try {
                const basketItemPost: BasketItemPost = {
                    variantId: variantId,
                    quantity: quantity
                };
                const updatedBasket = await addItemToBasket(basket.basketId, basketItemPost);
                setBasket(updatedBasket);
            } catch (error) {
                console.error("Error adding item to basket:", error);
                // Handle error (e.g., show error message)
            }
        } else {
            // Logged-out: Update localStorage basket
            const currentLocalStorageCart = getLocalStorageBasket();
            const existingItemIndex = currentLocalStorageCart.findIndex(item => item.variantId === variantId);

            if (existingItemIndex !== -1) {
                // Item already in cart, update quantity
                const updatedCart = [...currentLocalStorageCart];
                updatedCart[existingItemIndex].quantity += quantity;
                setLocalStorageBasketState(updatedCart);
                setLocalStorageBasket(updatedCart);
            } else {
                // Item not in cart, add new item
                const updatedCart = [...currentLocalStorageCart, { variantId, quantity }];
                setLocalStorageBasketState(updatedCart);
                setLocalStorageBasket(updatedCart);
            }
        }
    }, [isLoggedIn, basket?.basketId]);

    const removeFromCart = useCallback(async (variantId: number) => {
        if (isLoggedIn && basket?.basketId) {
            // Logged-in: API call to remove from database basket
            try {
                const updatedBasket = await removeItemFromBasket(basket.basketId, variantId);
                setBasket(updatedBasket);
            } catch (error) {
                console.error("Error removing item from basket:", error);
                // Handle error
            }
        } else {
            // Logged-out: Update localStorage basket
            const currentLocalStorageCart = getLocalStorageBasket();
            const updatedCart = currentLocalStorageCart.filter(item => item.variantId !== variantId);
            setLocalStorageBasketState(updatedCart);
            setLocalStorageBasket(updatedCart);
        }
    }, [isLoggedIn, basket?.basketId]);

    const clearCart = useCallback(async () => {
        if (isLoggedIn && basket?.basketId) {
            // Logged-in: API call to clear database basket
            try {
                await deleteAllItems(basket.basketId);
                setBasket(null); // Or fetch empty basket again if API returns it
            } catch (error) {
                console.error("Error clearing basket:", error);
                // Handle error
            }
        } else {
            // Logged-out: Clear localStorage basket
            setLocalStorageBasketState([]);
            localStorage.removeItem(localStorageBasketKey);
        }
    }, [isLoggedIn, basket?.basketId]);

    const updateCartItemQuantity = useCallback(async (variantId: number, quantity: number) => {
        if (isLoggedIn && basket?.basketId) {
            // Logged-in: API call to update quantity (you might need a specific API endpoint for this)
            try {
                const basketItemPost: BasketItemPost = {
                    variantId: variantId,
                    quantity: quantity
                };
                const updatedBasket = await addItemToBasket(basket.basketId, basketItemPost); // Reusing addItem - adjust API if needed for quantity update
                setBasket(updatedBasket);
            } catch (error) {
                console.error("Error updating item quantity in basket:", error);
                // Handle error
            }
        } else {
            // Logged-out: Update localStorage
            const currentLocalStorageCart = getLocalStorageBasket();
            const existingItemIndex = currentLocalStorageCart.findIndex(item => item.variantId === variantId);
            if (existingItemIndex !== -1) {
                const updatedCart = [...currentLocalStorageCart];
                updatedCart[existingItemIndex].quantity = quantity;
                setLocalStorageBasketState(updatedCart);
                setLocalStorageBasket(updatedCart);
            }
        }
    }, [isLoggedIn, basket?.basketId]);


    const value: BasketContextProps = {
        basket,
        localStorageBasket,
        isLoggedIn,
        loadingBasket,
        addToCart,
        removeFromCart,
        clearCart,
        updateCartItemQuantity,
        setLocalStorageBasketState
    };

    return (
        <BasketContext.Provider value={value}>
            {children}
        </BasketContext.Provider>
    );
};

export const useBasket = () => {
    const context = React.useContext(BasketContext);
    if (context === undefined) {
        throw new Error('useBasket must be used within a BasketProvider');
    }
    return context;
};