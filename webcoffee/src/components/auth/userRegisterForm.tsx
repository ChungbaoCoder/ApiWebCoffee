import React, { useState } from 'react';
import { register } from '../../api/authApi';
import { RegisterPost } from '../../api/models/authPost'

const UserRegisterForm: React.FC = () => {
    const [name, setName] = useState<string>('');
    const [email, setEmail] = useState<string>('');
    const [phoneNum, setPhoneNum] = useState<string>('');
    const [password, setPassword] = useState<string>('');
    const [successMessage, setSuccessMessage] = useState<string | null>(null);
    const [errorMessage, setErrorMessage] = useState<string | null>(null);

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setSuccessMessage(null);
        setErrorMessage(null);

        const registerRequest: RegisterPost = {
            name,
            email,
            phoneNum,
            password,
        };

        try {
            await register(registerRequest); // Use the 'register' API for users
            setSuccessMessage('User Registration successful! You can now log in as a user.'); // Updated message
            // Optionally clear the form after successful registration
            setName('');
            setEmail('');
            setPhoneNum('');
            setPassword('');
        } catch (error: any) {
            setErrorMessage(`User Registration failed: ${error.message}`); // Updated message
        }
    };

    return (
        <div>
            <h3>Register as User</h3> {/* Updated heading */}
            <form onSubmit={handleSubmit}>
                {/* Form fields (same as before) */}
                <div>
                    <label htmlFor="name">Name:</label>
                    <input
                        type="text"
                        id="name"
                        value={name}
                        onChange={(e) => setName(e.target.value)}
                        required
                    />
                </div>
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
                    <label htmlFor="phoneNum">Phone Number:</label>
                    <input
                        type="tel"
                        id="phoneNum"
                        value={phoneNum}
                        onChange={(e) => setPhoneNum(e.target.value)}
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
                <button type="submit">Register User</button> {/* Updated button text */}

                {successMessage && <p style={{ color: 'green' }}>{successMessage}</p>}
                {errorMessage && <p style={{ color: 'red' }}>{errorMessage}</p>}
            </form>
        </div>
    );
};

export default UserRegisterForm;