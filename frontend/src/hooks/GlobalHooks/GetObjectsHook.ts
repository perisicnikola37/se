import { useState } from 'react';
import axiosConfig from '../../config/axiosConfig';
import { ObjectGroupInterface } from '../../interfaces/globalInterfaces';

const useObjectGroups = (objectType: string) => {
    const [objectGroups, setObjectGroups] = useState<ObjectGroupInterface[]>([]);
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [error, setError] = useState<string | null>(null);

    const fetchObjectGroups = async () => {
        setIsLoading(true);
        setError(null);

        try {
            const endpoint = `/api/${objectType}s/groups`;
            const response = await axiosConfig.get<ObjectGroupInterface[]>(endpoint);
            setObjectGroups(response.data);
        } catch (err) {
            setError(`An error occurred while fetching ${objectType} groups.`);
        } finally {
            setIsLoading(false);
        }
    };

    return { objectGroups, isLoading, error, fetchObjectGroups };
};

export default useObjectGroups;
