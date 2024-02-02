import { useState } from 'react';
import axiosConfig from '../../config/axiosConfig';

interface ForgotPasswordHookInterface {
    isLoading: boolean;
    success: boolean;
    error: string | null;
    forgotPassword: (email: string) => Promise<void>;
}

const useForgotPassword = (): ForgotPasswordHookInterface => {
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [success, setSuccess] = useState<boolean>(false);
    const [error, setError] = useState<string | null>(null);

    const forgotPassword = async (email: string): Promise<void> => {
        setIsLoading(true);

        try {
            const response = await axiosConfig.post('/api/auth/forgot/password', { userEmail: email });
            if (response.data && response.data.success) {
                setSuccess(true);
                setError(null);
            } else {
                setSuccess(false);
                setError('Failed to send request. Please try again.');
            }
        } catch (err) {
            setSuccess(false);
            setError('Error occured.Please try again.');
        } finally {
            setIsLoading(false);
        }
    };

    return { isLoading, success, error, forgotPassword };
};

export default useForgotPassword;
