import { createContext, useContext, useState } from "react";
import {
  LoadingContextProps,
  LoadingProviderProps,
} from "../interfaces/contextsInterfaces";

const LoadingContext = createContext<LoadingContextProps | undefined>(
  undefined,
);

export const LoadingProvider = ({ children }: LoadingProviderProps) => {
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
    throw new Error("useLoading must be used within a LoadingProvider");
  }

  return context;
};
