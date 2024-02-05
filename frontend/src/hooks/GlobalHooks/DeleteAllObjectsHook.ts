import { useState } from 'react';
import axiosConfig from '../../config/axiosConfig';

const useDeleteAllObjects = () => {
    const [isLoading, setIsLoading] = useState<boolean>(false);

    const deleteAllObjects = async (objectType: string) => {
        setIsLoading(true);

        try {
            const endpoint = `/api/${objectType}s`;

            await axiosConfig.delete(endpoint);
        } finally {
            setIsLoading(false);
        }
    };

    return { isLoading, deleteAllObjects };
};

export default useDeleteAllObjects;
