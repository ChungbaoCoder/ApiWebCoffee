import React, { useState } from 'react';
import { RegisterPost } from '../api/models/authPost';
import { register } from '../api/authApi';

const Register: React.FC = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [confirmPassword, setConfirmPassword] = useState('');
    const [error, setError] = useState('');
    const [successMessage, setSuccessMessage] = useState('');
    const [isLoading, setIsLoading] = useState(false);

    const handleRegister = async (e: any) => {
        e.preventDefault();
        setError('');
        setSuccessMessage('');
        setIsLoading(true);
    
        if (password !== confirmPassword) {
            setError("Passwords do not match.");
            setIsLoading(false);
            return;
        }
    
        try {
            const registerRequest = { email, password } as RegisterPost;
            const responseData = await register(registerRequest);

            setSuccessMessage('Registration successful! Please login.');

            setEmail('');
            setPassword('');
            setConfirmPassword('');
            
            console.log('Registration successful:', responseData);
    
        } catch (error: any) {
            console.error('Registration failed:', error);
            setError(error.response?.data?.message || 'Registration failed. Please try again.');
        } 
        finally {
            setIsLoading(false);
        }
    };
    
    return (
        <div>
            <h2>Register</h2>
            {error && <p style={{ color: 'red' }}>{error}</p>}
            {successMessage && <p style={{ color: 'green' }}>{successMessage}</p>}
            
            <form onSubmit={handleRegister}>

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
                
                <div>
                    <label htmlFor="confirmPassword">Confirm Password:</label>
                    <input
                    type="password"
                    id="confirmPassword"
                    value={confirmPassword}
                    onChange={(e) => setConfirmPassword(e.target.value)}
                    required
                    />
                </div>

                <button type="submit" disabled={isLoading}>
                {isLoading ? 'Registering...' : 'Register'}
                </button>

            </form>

        </div>
    );
};

export default Register;