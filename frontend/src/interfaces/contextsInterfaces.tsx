import { Dispatch, ReactNode, SetStateAction } from "react";

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

export interface User {
    token: string | null;
    id: number | null;
    username: string | null;
    email: string | null;
    accountType: string | null;
    formattedCreatedAt: string | null;
}

export interface UserContextProps {
    user: User;
    setUser: Dispatch<SetStateAction<User>>;
    isLoggedIn: () => boolean;
}

export interface FetchObjectParams {
    pageNumber: number;
    pageSize: number;
    description?: string | null;
    minAmount?: number | null;
    maxAmount?: number | null;
    incomeGroupId?: string | null;
    expenseGroupId?: string | null;
}

export interface ModalContextProps {
    modalState: boolean;
    // pagination
    totalRecords: number;
    setTotalRecords: (number: number) => void;
    pageNumber: number;
    setPageNumber: (number: number) => void;
    actionChange: { counter: number; value: boolean };
    appliedFilters: FetchObjectParams;
    openModal: () => void;
    closeModal: () => void;
    setActionChanged: () => void;
    resetActionChange: () => void;
    setAppliedFilters: (filters: FetchObjectParams) => void;
    getAppliedFilters: () => FetchObjectParams;
}

export interface DarkModeContextProps {
    darkMode: boolean;
    toggleDarkMode: (prev: boolean) => void;
}

export interface UserProviderProps {
    children: ReactNode;
}