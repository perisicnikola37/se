import axiosConfig from '../../config/axiosConfig';
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useUser } from '../../contexts/UserContext';

interface RegistrationData {
    username: string;
    email: string;
    accountType: string;
    password: string;
}

interface RegistrationResponse {
    success: boolean;
    message: string;
    username: string;
    token: string;
    id: number;
    email: string;
    accountType: string;
    formattedCreatedAt: string;
}

const useRegistration = () => {
    const navigate = useNavigate();
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [response, setResponse] = useState<RegistrationResponse | null>(null);
    const [errorMessage, setErrorMessage] = useState<string | null>(null);
    const [fieldErrorMessages, setFieldErrorMessages] = useState<string[]>([]);
    const { setUser } = useUser();

    const register = async (registrationData: RegistrationData) => {
        setIsLoading(true);
        try {
            const { data, status } = await axiosConfig.post(
                '/api/auth/register',
                registrationData
            );

            if (status === 201 || status === 200) {
                setFieldErrorMessages([]);

                localStorage.setItem('token', data.token);
                localStorage.setItem('id', data.id);
                localStorage.setItem('username', data.username);
                localStorage.setItem('email', data.email);
                localStorage.setItem('accountType', data.accountType);
                localStorage.setItem('formattedCreatedAt', data.formattedCreatedAt);
                navigate('/');
            }

            setResponse({ data, status });
        } catch (err) {
            // ... (error handling code)
        } finally {
            setIsLoading(false);
        }
    };

    return { register, isLoading, response, errorMessage, fieldErrorMessages };
};

export default useRegistration;
