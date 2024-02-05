import { useState } from 'react';
import axiosConfig from '../../config/axiosConfig';
import { ObjectData } from '../../interfaces/globalInterfaces';

interface ErrorResponse {
    response?: {
        status?: number;
        data?: {
            errorMessage: string;
        }[];
    };
}

const useCreateObjectGroup = (objectType: string) => {
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [error, setError] = useState<string | null>(null);

    const createObjectGroup = async (objectData: ObjectData) => {
        setIsLoading(true);
        setError(null);

        try {
            const endpoint = `/api/${objectType}s/groups`;

            await axiosConfig.post(endpoint, objectData);
        } catch (err) {
            const errorResponse = err as ErrorResponse;

            alert('catched');
            if (errorResponse.response && errorResponse.response.status === 400) {
                // Handle validation errors
                const validationErrors = (errorResponse.response.data ?? []).map(
                    (error) => error.errorMessage
                );
                setError(validationErrors.join(', '));
            } else if (errorResponse.response && errorResponse.response.data) {
                // Handle other types of errors
                setError('An error occurred while creating the object.');
            } else if (err instanceof Error) {
                // Handle generic errors
                setError('An error occurred while creating the object.');
            } else {
                // Handle other cases
                setError('An unexpected error occurred.');
            }
        } finally {
            setIsLoading(false);
        }
    };

    return { isLoading, error, createObjectGroup };
};

export default useCreateObjectGroup;
