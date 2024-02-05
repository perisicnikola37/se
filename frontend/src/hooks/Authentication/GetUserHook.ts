import { useState } from 'react';
import axiosConfig from '../../config/axiosConfig';
import { User } from '../../interfaces/globalInterfaces';

const useCurrentUser = () => {
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [user, setUser] = useState<User | null>(null);
    const [error, setError] = useState<string | null>(null);

    const fetchCurrentUser = async () => {
        const result = { isLoading: true, user: null as User | null, error: null as string | null };

        try {
            const response = await axiosConfig.get('/api/auth/user');
            result.user = response.data;
        } catch (err) {
            result.error = 'Error fetching current user.';
        } finally {
            result.isLoading = false;
        }

        return result;
    };

    const loadCurrentUser = async () => {
        setIsLoading(true);
        const result = await fetchCurrentUser();
        setIsLoading(false);

        if (result.user) {
            setUser(result.user);
            setError(null);
        } else {
            setError(result.error);
        }
    };

    return { isLoading, user, error, loadCurrentUser };
};

export default useCurrentUser;
