import { useState } from 'react';
import axiosConfig from '../../config/axiosConfig';
import { BlogType } from '../../interfaces/globalInterfaces';

const fetchBlogById = async (objectId: number, setBlog: (data: BlogType | null) => void, setIsLoading: (loading: boolean) => void, setError: (error: string | null) => void) => {
    setIsLoading(true);
    setError(null);

    try {
        const endpoint = `/api/blogs/${objectId}`;
        const response = await axiosConfig.get(endpoint);

        const objectData: BlogType = response.data;

        setBlog(objectData);
    } catch (err) {
        setError(`Error fetching blog by ID`);
    } finally {
        setIsLoading(false);
    }
};

const useGetBlogById = (objectId: number) => {
    const [blog, setBlog] = useState<BlogType | null>(null);
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [error, setError] = useState<string | null>(null);

    const getBlogById = () => {
        if (objectId) {
            fetchBlogById(objectId, setBlog, setIsLoading, setError);
        }
    };

    return { blog, isLoading, error, getBlogById };
};

export default useGetBlogById;
