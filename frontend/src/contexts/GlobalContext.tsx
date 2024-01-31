// ModalContext.tsx
import React, { createContext, useContext, useState, ReactNode } from 'react';

interface ModalContextProps {
    modalState: boolean;
    actionChange: { counter: number; value: boolean }; // Update the type here
    openModal: () => void;
    closeModal: () => void;
    setActionChanged: () => void;
    resetActionChange: () => void;
}

const ModalContext = createContext<ModalContextProps | undefined>(undefined);

interface ModalProviderProps {
    children: ReactNode;
}

export const ModalProvider: React.FC<ModalProviderProps> = ({ children }) => {
    const [modalState, setModalState] = useState(false);
    const [actionChange, setActionChange] = useState({ counter: 0, value: false });

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

    const contextValue: ModalContextProps = {
        modalState,
        actionChange,
        openModal,
        closeModal,
        setActionChanged,
        resetActionChange,
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
