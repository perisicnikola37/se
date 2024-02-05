import { useState } from 'react';
import axiosConfig from '../../config/axiosConfig';

interface User {
    username: string;
}

interface Blog {
    id: number;
    description: string;
    author: string;
    text: string;
    createdAt: string;
    userId: number;
    user: User;
}

const useAllBlogs = () => {
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [blogs, setBlogs] = useState<Blog[]>([]);
    const [error, setError] = useState<string | null>(null);

    const fetchAllBlogs = async () => {
        const result = { isLoading: true, blogs: [] as Blog[], error: null as string | null };

        try {
            const response = await axiosConfig.get('/api/blogs');
            result.blogs = response.data;
        } catch (err) {
            result.error = 'Error fetching blogs.';
        } finally {
            result.isLoading = false;
        }

        return result;
    };

    const loadBlogs = async () => {
        setIsLoading(true);
        const result = await fetchAllBlogs();
        setIsLoading(false);

        if (result.blogs.length > 0) {
            setBlogs(result.blogs);
            setError(null);
        } else {
            setError(result.error);
        }
    };

    return { isLoading, blogs, error, loadBlogs };
};

export default useAllBlogs;
