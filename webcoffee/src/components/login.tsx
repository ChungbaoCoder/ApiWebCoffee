import React, { useState } from 'react';
import { LoginPost } from '../api/models/authPost';
import { login } from '../api/authApi';

const Login: React.FC = () =>  {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');
    const [isLoading, setIsLoading] = useState(false);

    const handleLogin = async (e: any) => {
        e.preventDefault();
        setError('');
        setIsLoading(true);
    
        try {
          const loginRequest = { email, password } as LoginPost;
          const responseData = await login(loginRequest);
    
          const authToken = responseData.token;
          localStorage.setItem('authToken', authToken);
    
          console.log('Login successful!', responseData);
          // Example:  You might want to redirect the user to a dashboard page
          // window.location.href = '/dashboard';
    
        } 
        catch (error: any) {
            console.error('Login failed:', error);
            setError(error.response?.data?.message || 'Login failed. Please check your credentials.');
        } 
        finally {
            setIsLoading(false);
        }
    };

    return (
        <div>
            <h2>Login</h2>
            {error && <p style={{ color: 'red' }}>{error}</p>}
            
            <form onSubmit={handleLogin}>

                <div>
                    <label htmlFor="email">Email:</label>
                    <input
                        type="email"
                        id="email"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                        required
                    />
                </div>

                <div>
                    <label htmlFor="password">Password:</label>
                    <input
                        type="password"
                        id="password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        required
                    />
                </div>

                <button type="submit" disabled={isLoading}>
                {isLoading ? 'Logging in...' : 'Login'}
                </button>

            </form>
            
        </div>
    );
};

export default Login;