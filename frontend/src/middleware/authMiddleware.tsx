import { useEffect } from 'react';

export const useAuthMiddleware = () => {
    useEffect(() => {
        const isAuthenticated = () => {
            const token = localStorage.getItem('token');
            return !!token;
        };

        const checkAuthentication = () => {
            if (!isAuthenticated()) {
                window.location.replace('/sign-in');
            }
        };

        checkAuthentication();

        return () => {

        };
    }, []);
};
