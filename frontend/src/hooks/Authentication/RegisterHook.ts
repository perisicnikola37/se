import axiosConfig from '../../config/axiosConfig';
import { useState } from 'react';
import { AxiosResponse } from 'axios';
import { useNavigate } from 'react-router-dom';

interface RegistrationData {
    username: string;
    email: string;
    accountType: string;
    password: string;
}

interface RegistrationResponse {
    success: boolean;
    message: string;
    token: string; // Add token property to RegistrationResponse
}

const useRegistration = () => {
    const navigate = useNavigate();
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [response, setResponse] = useState<RegistrationResponse | null>(null);
    const [errorMessage, setErrorMessage] = useState<string | null>(null);
    const [fieldErrorMessages, setFieldErrorMessages] = useState<string[]>([]);

    const register = async (registrationData: RegistrationData) => {
        setIsLoading(true);
        try {
            const response: AxiosResponse<RegistrationResponse> = await axiosConfig.post(
                '/api/auth/register',
                registrationData
            );

            if (response.status === 201 || response.status === 200) {
                setFieldErrorMessages([]);
                const { token } = response.data;
                localStorage.setItem('token', token); // Store the token in local storage
                navigate('/'); // Redirect to the home page
            }

            setResponse(response);
        } catch (err) {
            if (err.response && err.response.status === 409) {
                setFieldErrorMessages(['Email is already registered']);
            } else if (err.response && err.response.data) {
                const fieldErrors = err.response.data.map((error: any) => error.errorMessage);
                setFieldErrorMessages(fieldErrors);
                setErrorMessage('An error occurred during registration.');
                console.log(err);
            } else if (err instanceof Error) {
                setErrorMessage('An error occurred during registration.');
                console.log(err);
            } else {
                setErrorMessage('An error occurred during registration.');
                console.log(err);
            }
        } finally {
            setIsLoading(false);
        }
    };

    return { register, isLoading, response, errorMessage, fieldErrorMessages };
};

export default useRegistration;
