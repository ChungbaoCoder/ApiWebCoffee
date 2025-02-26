import React from 'react';
import './App.css';
import { BrowserRouter, Routes, Route, Link } from 'react-router-dom';
import BasketTable from './components/basket/basketTable';
import AddItemForm from './components/basket/addItemForm';
import LoginForm from './components/auth/loginForm';
import RegisterForm from './components/auth/registerForm';
import ProductList from './components/product/productList';
import BuyerCreateForm from './components/buyer/buyerCreateForm';
import ProductCreateForm from './components/product/productCreateForm';
import BuyerList from './components/buyer/buyerList';
import OrderCreateForm from './components/order/orderCreateForm';
import OrderListByBuyer from './components/order/orderListByBuyer';

function App() {
  const myBasketId = 1;

  return (
    <div className="App">
      <BrowserRouter> {/* Wrap your app with BrowserRouter */}
            <div className="app-container">
                <nav>
                    <ul>
                        <li>
                            <Link to="/">Home</Link> {/* Link to the Home page (Product List) */}
                        </li>
                        <li>
                            <Link to="/basket">Basket</Link> {/* Link to the Basket page */}
                        </li>
                        <li>
                            <Link to="/products">Products</Link> {/* Link to the Product Management page */}
                        </li>
                        <li>
                            <Link to="/buyers">Buyers</Link> {/* Link to the Buyer Management page */}
                        </li>
                        <li>
                            <Link to="/orders">Orders</Link> {/* New Orders link */}
                        </li>
                        <li>
                            <Link to="/register">Register</Link> {/* Link to the Register page */}
                        </li>
                        <li>
                            <Link to="/login">Login</Link> {/* Link to the Login page */}
                        </li>
                    </ul>
                </nav>

                <Routes> {/* Define your routes within Routes */}
                    <Route path="/" element={<HomePage />} /> {/* Route for the home page (Product List) */}
                    <Route path="/basket" element={<BasketPage basketId={myBasketId} />} /> {/* Route for the basket page */}
                    <Route path="/products" element={<ProductPage />} /> {/* Route for the product management page */}
                    <Route path="/buyers" element={<BuyerPage />} /> {/* Route for the buyer management page */}
                    <Route path="/orders" element={<OrderPage />} /> {/* Route for Orders Page */}
                    <Route path="/register" element={<RegisterPage />} /> {/* Route for the register page */}
                    <Route path="/login" element={<LoginPage />} /> {/* Route for the login page */}
                </Routes>
            </div>
        </BrowserRouter>
    </div>
  );
};

function HomePage() {
  return (
      <div>
          <h2>Welcome to the Home Page! (Product List)</h2>
          <ProductList />
      </div>
  );
}

function OrderPage() {
    const handleOrderCreated = () => {
        console.log("Order Created! Order list should refresh on Order Page.");
        // You might want to trigger a refresh in OrderListByBuyer if needed.
    };

    return (
        <section>
            <h2>Order Management</h2>
            <OrderCreateForm onOrderCreated={handleOrderCreated} />
            <OrderListByBuyer />
        </section>
    );
}

interface BasketPageProps {
  basketId: number;
}

function BasketPage({ basketId }: BasketPageProps) {
  return (
      <div>
          <h2>Your Basket</h2>
          <BasketTable basketId={basketId} />
          <AddItemForm basketId={basketId} />
      </div>
  );
}

function ProductPage() {
  const handleProductCreated = () => {
      console.log("Product Created! Product list should refresh on Product Page.");
      // You might want to trigger a refresh in ProductList if needed.
  };

  return (
      <div>
          <h2>Product Management</h2>
          <ProductCreateForm onProductCreated={handleProductCreated} />
          <ProductList />
      </div>
  );
}

function BuyerPage() {
  const handleBuyerCreated = () => {
      console.log("Buyer Created! Buyer list should refresh on Buyer Page.");
      // You might want to trigger a refresh in BuyerList if needed.
  };

  return (
      <div>
          <h2>Buyer Management</h2>
          <BuyerCreateForm onBuyerCreated={handleBuyerCreated} />
          <BuyerList />
      </div>
  );
}

function RegisterPage() {
  return (
      <div>
          <h2>Register</h2>
          <RegisterForm />
      </div>
  );
}

function LoginPage() {
  return (
      <div>
          <h2>Login</h2>
          <LoginForm />
      </div>
  );
}

export default App;
