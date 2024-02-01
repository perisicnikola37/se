import React, { createContext, useContext, useState, ReactNode } from 'react';

interface FetchIncomesParams {
    pageSize: number;
    description?: string | null;
    minAmount?: number | null;
    maxAmount?: number | null;
    incomeGroupId?: string | null;
}

interface ModalContextProps {
    modalState: boolean;
    actionChange: { counter: number; value: boolean };
    appliedFilters: FetchIncomesParams;
    openModal: () => void;
    closeModal: () => void;
    setActionChanged: () => void;
    resetActionChange: () => void;
    setAppliedFilters: (filters: FetchIncomesParams) => void;
    getAppliedFilters: () => FetchIncomesParams;
}

const ModalContext = createContext<ModalContextProps | undefined>(undefined);

interface ModalProviderProps {
    children: ReactNode;
}

export const ModalProvider: React.FC<ModalProviderProps> = ({ children }) => {
    const [modalState, setModalState] = useState(false);
    const [actionChange, setActionChange] = useState({ counter: 0, value: false });
    const [appliedFilters, internalSetAppliedFilters] = useState<FetchIncomesParams>({ pageSize: 50 });

    const openModal = () => {
        setModalState(true);
    };

    const closeModal = () => {
        setModalState(false);
    };

    const setActionChanged = () => {
        setActionChange((prevState) => ({ counter: prevState.counter + 1, value: true }));
    };

    const resetActionChange = () => {
        setActionChange((prevState) => ({ ...prevState, value: false }));
    };

    const setAppliedFilters = (filters: FetchIncomesParams) => {
        internalSetAppliedFilters(filters);
    };

    const getAppliedFilters = () => {
        return appliedFilters;
    };

    const contextValue: ModalContextProps = {
        modalState,
        actionChange,
        appliedFilters,
        openModal,
        closeModal,
        setActionChanged,
        resetActionChange,
        setAppliedFilters,
        getAppliedFilters,
    };

    return (
        <ModalContext.Provider value={contextValue}>
            {children}
        </ModalContext.Provider>
    );
};
export const useModal = (): ModalContextProps => {
    const context = useContext(ModalContext);

    if (!context) {
        throw new Error('useModal must be used within a ModalProvider');
    }

    return context;
};
