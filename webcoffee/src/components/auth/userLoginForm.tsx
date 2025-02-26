import React, { useState } from 'react';
import { login } from '../../api/authApi';
import { LoginPost, TokenResponse } from '../../api/models/authPost'

const UserLoginForm: React.FC = () => {
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
            const response = await login(loginRequest); // Use the 'login' API for users
            const tokenResponse: TokenResponse = response.data;
            setSuccessMessage('User Login successful!'); // Updated message
            console.log('User Login successful!', tokenResponse);
            localStorage.setItem('authToken', tokenResponse.token); // Store token in localStorage (insecure for production)
            // In a real app, you'd likely redirect to a protected page or update user context
            setEmail('');
            setPassword('');
        } catch (error: any) {
            setErrorMessage(`User Login failed: ${error.message}`); // Updated message
        }
    };

    return (
        <div>
            <h3>Login as User</h3> {/* Updated heading */}
            <form onSubmit={handleSubmit}>
                {/* Form fields (same as before) */}
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
                <button type="submit">Login User</button> {/* Updated button text */}

                {successMessage && <p style={{ color: 'green' }}>{successMessage}</p>}
                {errorMessage && <p style={{ color: 'red' }}>{errorMessage}</p>}
            </form>
        </div>
    );
};

export default UserLoginForm;