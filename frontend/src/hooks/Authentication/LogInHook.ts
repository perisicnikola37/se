import axiosConfig from '../../config/axiosConfig';
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';

interface LoginData {
    email: string;
    password: string;
}

interface User {
    id: number;
    username: string;
    email: string;
    accountType: string;
    formattedCreatedAt: string;
    createdAt: string;
    token: string;
}

interface LoginResponse {
    success: boolean;
    message: string;
    user: User;
}

interface ErrorResponse {
    response?: {
        status?: number;
        data?: {
            errorMessage: string;
        }[];
    };
}

interface LoginResult {
    user: User | null;
    success: boolean;
    message: string;
}

const useLogin = () => {
    const navigate = useNavigate();
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [response, setResponse] = useState<LoginResponse | null>(null);
    const [errorMessage, setErrorMessage] = useState<string | null>(null);
    const [fieldErrorMessages, setFieldErrorMessages] = useState<string[]>([]);

    const login = async (loginData: LoginData): Promise<LoginResult> => {
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

            setResponse({ success: data.success, message: data.message, user: data.user });
            return { user: data.user, success: data.success, message: data.message };
        } catch (err) {
            const errorResponse = err as ErrorResponse;

            if (errorResponse.response && errorResponse.response.status === 401) {
                setFieldErrorMessages(['Invalid email or password']);
            } else if (errorResponse.response && errorResponse.response.data) {
                const fieldErrors = errorResponse.response.data.map((error) => error.errorMessage);
                setFieldErrorMessages(fieldErrors);
                setErrorMessage('An error occurred during login.');
            } else if (err instanceof Error) {
                setErrorMessage('An error occurred during login.');
            } else {
                setErrorMessage('An error occurred during login.');
            }

            return { user: null, success: false, message: errorMessage || 'An error occurred during login.' };
        } finally {
            setIsLoading(false);
        }
    };

    return { login, isLoading, response, errorMessage, fieldErrorMessages };
};

export default useLogin;
