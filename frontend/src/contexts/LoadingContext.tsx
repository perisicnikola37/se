import React, { createContext, useContext, useState, ReactNode } from 'react';
import { LoadingContextProps } from '../interfaces/contextsInterfaces';

const LoadingContext = createContext<LoadingContextProps | undefined>(undefined);

interface LoadingProviderProps {
    children: ReactNode;
}

export const LoadingProvider: React.FC<LoadingProviderProps> = ({ children }) => {
    const [loading, setLoading] = useState(true);

    const setLoadingState = (state: boolean) => {
        setLoading(state);
    };

    return (
        <LoadingContext.Provider value={{ loading, setLoadingState }}>
            {children}
        </LoadingContext.Provider>
    );
};

export const useLoading = (): LoadingContextProps => {
    const context = useContext(LoadingContext);

    if (!context) {
        throw new Error('useLoading must be used within a LoadingProvider');
    }

    return context;
};
