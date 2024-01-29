import { ReactNode } from "react";

export interface LoadingContextProps {
    loading: boolean;
    setLoadingState: (state: boolean) => void;
}

export interface LoadingProviderProps {
    children: ReactNode;
}

export interface LanguageContextType {
    languageKey: string;
    changeLanguage: (newLanguageKey: string) => void;
}

export interface LanguageProviderProps {
    children: ReactNode;
}