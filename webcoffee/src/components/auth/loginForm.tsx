import React, { useState } from 'react';
import { login } from '../../api//authApi';
import { LoginPost, TokenResponse } from '../../api/models/authPost'

const LoginForm: React.FC = () => {
    const [email, setEmail] = useState<string>('');
    const [password, setPassword] = useState<string>('');
    const [successMessage, setSuccessMessage] = useState<string | null>(null);
    const [errorMessage, setErrorMessage] = useState<string | null>(null);

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setSuccessMessage(null);
        setErrorMessage(null);

        const loginRequest: LoginPost = {
            email,
            password,
        };

        try {
            const response = await login(loginRequest);
            const tokenResponse: TokenResponse = response.data; // Assuming API returns TokenResponse in data
            setSuccessMessage('Login successful!');
            console.log('Login successful!', tokenResponse); // Log token for now
            localStorage.setItem('authToken', tokenResponse.token); // Store token in localStorage (insecure for production)
            // In a real app, you'd likely redirect to a protected page or update user context
            setEmail('');
            setPassword('');
        } catch (error: any) {
            setErrorMessage(`Login failed: ${error.message}`);
        }
    };

    return (
        <div>
            <h3>Login</h3>
            <form onSubmit={handleSubmit}>
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
                <button type="submit">Login</button>

                {successMessage && <p style={{ color: 'green' }}>{successMessage}</p>}
                {errorMessage && <p style={{ color: 'red' }}>{errorMessage}</p>}
            </form>
        </div>
    );
};

export default LoginForm;