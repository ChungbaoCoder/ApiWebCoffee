import React, { useState } from "react";
import { loginCustomer } from "../../api/authApi";
import { TokenResponse } from "../../api/models/authPost";
import { LoginPost } from "../../api/models/authPost";

const CustomerLoginForm: React.FC = () => {
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
            const response = await loginCustomer(loginRequest); // Use the 'loginCustomer' API
            const tokenResponse: TokenResponse = response.data;
            setSuccessMessage('Customer Login successful!'); // Updated message
            console.log('Customer Login successful!', tokenResponse);
            localStorage.setItem('authToken', tokenResponse.token); // Store token in localStorage (insecure for production)
            // In a real app, you'd likely redirect to a protected page or update user context
            setEmail('');
            setPassword('');
        } catch (error: any) {
            setErrorMessage(`Customer Login failed: ${error.message}`); // Updated message
        }
    };

    return (
        <div>
            <h3>Login as Customer</h3> {/* Updated heading */}
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
                <button type="submit">Login Customer</button> {/* Updated button text */}

                {successMessage && <p style={{ color: 'green' }}>{successMessage}</p>}
                {errorMessage && <p style={{ color: 'red' }}>{errorMessage}</p>}
            </form>
        </div>
    );
};

export default CustomerLoginForm;