import { useState } from 'react';
import axiosConfig from '../../config/axiosConfig';
import { CreateBlogData } from '../../interfaces/globalInterfaces';

const useCreateBlog = () => {
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [error, setError] = useState<string | null>(null);

    const createBlog = async (blogData: CreateBlogData) => {
        setIsLoading(true);

        try {
            await axiosConfig.post('/api/blogs', blogData);
        } catch (err) {
            setError('Error creating a new reminder.');
        } finally {
            setIsLoading(false);
        }
    };

    return { isLoading, error, createBlog };
};

export default useCreateBlog;
