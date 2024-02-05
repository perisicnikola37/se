import React, { createContext, useContext, useState, ReactNode } from 'react';
import { FetchObjectParams, ModalContextProps } from '../interfaces/contextsInterfaces';

const ModalContext = createContext<ModalContextProps | undefined>(undefined);

interface ModalProviderProps {
    children: ReactNode;
}

export const ModalProvider: React.FC<ModalProviderProps> = ({ children }) => {
    const [modalState, setModalState] = useState(false);
    const [totalRecords, setTotalRecordsState] = useState(0);
    const [pageNumber, setPageNumberState] = useState(0);
    const [actionChange, setActionChange] = useState({ counter: 0, value: false });
    const [appliedFilters, internalSetAppliedFilters] = useState<FetchObjectParams>({ pageSize: 5, pageNumber: 1 });

    const openModal = () => {
        setModalState(true);
    };

    const setTotalRecords = (number: number) => {
        setTotalRecordsState(number);
    };

    const setPageNumber = (number: number) => {
        setPageNumberState(number);
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

    const setAppliedFilters = (filters: FetchObjectParams) => {
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
        totalRecords: totalRecords,
        setTotalRecords,
        pageNumber: pageNumber,
        setPageNumber
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
