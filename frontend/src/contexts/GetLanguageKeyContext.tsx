import { createContext, useContext, useState } from "react";
import {
  LanguageContextType,
  LanguageProviderProps,
} from "../interfaces/contextsInterfaces";

const LanguageContext = createContext<LanguageContextType | undefined>(
  undefined,
);

export const LanguageProvider = ({ children }: LanguageProviderProps) => {
  const [languageKey, setLanguageKey] = useState("EN");

  const changeLanguage = (newLanguageKey: string) => {
    setLanguageKey(newLanguageKey);
  };

  return (
    <LanguageContext.Provider value={{ languageKey, changeLanguage }}>
      {children}
    </LanguageContext.Provider>
  );
};

export const useLanguage = (): LanguageContextType => {
  const context = useContext(LanguageContext);
  if (!context) {
    throw new Error("useLanguage must be used within a LanguageProvider");
  }
  return context;
};
