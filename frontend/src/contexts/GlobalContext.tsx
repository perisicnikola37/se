import { createContext, useContext, useState, ReactNode } from "react";
import {
  FetchObjectParams,
  ModalContextProps,
} from "../interfaces/contextsInterfaces";

interface ModalProviderProps {
  children: ReactNode;
}

const ModalContext = createContext<ModalContextProps | undefined>(undefined);

export const ModalProvider = ({ children }: ModalProviderProps) => {
  const [modalState, setModalState] = useState(false);
  const [language, setLanguage] = useState(
    localStorage.getItem("defaultLanguage") || "EN",
  );
  const [totalRecords, setTotalRecordsState] = useState(0);
  const [pageNumber, setPageNumberState] = useState(0);
  const [actionChange, setActionChange] = useState({
    counter: 0,
    value: false,
  });
  const [appliedFilters, internalSetAppliedFilters] =
    useState<FetchObjectParams>({ pageSize: 5, pageNumber: 1 });

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
    setActionChange((prevState) => ({
      counter: prevState.counter + 1,
      value: true,
    }));
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
    totalRecords,
    setTotalRecords,
    pageNumber,
    setPageNumber,
    language,
    setLanguage,
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
    throw new Error("useModal must be used within a ModalProvider");
  }

  return context;
};
