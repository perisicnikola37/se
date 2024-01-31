import { useState } from 'react';
import axiosConfig from '../../config/axiosConfig';

type ObjectType = {
    description: string;
    amount: number;
    incomeGroupId: number;
};

const fetchObjectById = async (objectId: number, objectType: string, setObject: (data: ObjectType | null) => void, setIsLoading: (loading: boolean) => void, setError: (error: string | null) => void) => {
    setIsLoading(true);
    setError(null);

    try {
        const endpoint = `/api/${objectType}s/${objectId}`;
        const response = await axiosConfig.get(endpoint);

        const objectData: ObjectType = response.data;

        setObject(objectData);
    } catch (err) {
        setError(`Error fetching ${objectType} by ID`);
    } finally {
        setIsLoading(false);
    }
};

const useGetObjectById = (objectId: number, objectType: string) => {
    const [object, setObject] = useState<ObjectType | null>(null);
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [error, setError] = useState<string | null>(null);

    const getObjectById = () => {
        if (objectId) {
            fetchObjectById(objectId, objectType, setObject, setIsLoading, setError);
        }
    };

    return { object, isLoading, error, getObjectById };
};

export default useGetObjectById;
