import { useState } from 'react';
import axiosConfig from '../../config/axiosConfig';
import { ObjectGroupDataInterface } from '../../interfaces/globalInterfaces';

const fetchObjectGroupById = async (objectId: number, objectType: string, setObject: (data: ObjectGroupDataInterface | null) => void, setIsLoading: (loading: boolean) => void, setError: (error: string | null) => void) => {
    setIsLoading(true);
    setError(null);

    try {
        const endpoint = `/api/${objectType}s/groups/${objectId}`;
        const response = await axiosConfig.get(endpoint);

        const objectData: ObjectGroupDataInterface = response.data;

        setObject(objectData);
    } catch (err) {
        setError(`Error fetching ${objectType} group by ID`);
    } finally {
        setIsLoading(false);
    }
};

const useGetObjectGroupById = (objectId: number, objectType: string) => {
    const [object, setObject] = useState<ObjectGroupDataInterface | null>(null);
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [error, setError] = useState<string | null>(null);

    const getObjectGroupById = () => {
        if (objectId) {
            fetchObjectGroupById(objectId, objectType, setObject, setIsLoading, setError);
        }
    };

    return { object, isLoading, error, getObjectGroupById };
};

export default useGetObjectGroupById;
