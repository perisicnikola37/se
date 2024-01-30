import axiosConfig from '../../config/axiosConfig';
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';

interface LoginData {
    email: string;
    password: string;
}

interface LoginResponse {
    success: boolean;
    message: string;
    user: {
        id: number;
        username: string;
        email: string;
        accountType: string;
        formattedCreatedAt: string;
        createdAt: string;
        token: string;
    };
}

const useLogin = () => {
    const navigate = useNavigate();
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [response, setResponse] = useState<LoginResponse | null>(null);
    const [errorMessage, setErrorMessage] = useState<string | null>(null);
    const [fieldErrorMessages, setFieldErrorMessages] = useState<string[]>([]);

    const login = async (loginData: LoginData) => {
        setIsLoading(true);
        try {
            const { data, status } = await axiosConfig.post('/api/auth/login', loginData);

            if (status === 200) {
                setFieldErrorMessages([]);

                localStorage.setItem('token', data.user.token);
                localStorage.setItem('id', data.user.id.toString());
                localStorage.setItem('username', data.user.username);
                localStorage.setItem('email', data.user.email);
                localStorage.setItem('accountType', data.user.accountType);
                localStorage.setItem('formattedCreatedAt', data.user.formattedCreatedAt);
                navigate('/');
            }

            setResponse({ data, status });
        } catch (err) {
            if (err.response && err.response.status === 401) {
                setFieldErrorMessages(['Invalid email or password']);
            } else if (err.response && err.response.data) {
                const fieldErrors = err.response.data.map((error: any) => error.errorMessage);
                setFieldErrorMessages(fieldErrors);
                setErrorMessage('An error occurred during login.');
            } else if (err instanceof Error) {
                setErrorMessage('An error occurred during login.');
            } else {
                setErrorMessage('An error occurred during login.');
            }
        } finally {
            setIsLoading(false);
        }
    };

    return { login, isLoading, response, errorMessage, fieldErrorMessages };
};

export default useLogin;
