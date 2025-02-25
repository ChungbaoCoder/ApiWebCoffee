import React from 'react';
import './App.css';
import BasketTable from './components/basket/basketTable';
import AddItemForm from './components/basket/addItemForm';
import LoginForm from './components/auth/loginForm';
import RegisterForm from './components/auth/registerForm';
import ProductList from './components/product/productList';
import productCreateForm from './components/product/productCreateForm';
import ProductCreateForm from './components/product/productCreateForm';

function App() {
  const myBasketId = 1;

  return (
    <div className="App">
      {/* <section>
          <h2>Current Basket</h2>
          <BasketTable basketId={myBasketId} />
      </section>

      <section>
          <h2>Add Item to Basket</h2>
          <AddItemForm basketId={myBasketId} />
      </section> */}
      
      {/* <RegisterForm></RegisterForm>
      <LoginForm></LoginForm> */}

      <ProductList></ProductList>
      <ProductCreateForm></ProductCreateForm>
    </div>
  );
}

export default App;
