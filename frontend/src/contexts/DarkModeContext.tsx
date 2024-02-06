import { createContext, useContext, useState, ReactNode } from "react";
import { DarkModeContextProps } from "../interfaces/contextsInterfaces";

const DarkModeContext = createContext<DarkModeContextProps | undefined>(
    undefined
);

type DarkModeProviderProps = {
    children: ReactNode;
};

const DarkModeProvider = ({ children }: DarkModeProviderProps) => {
    const [darkMode, setDarkMode] = useState<boolean>(false);

    const toggleDarkMode = () => {
        setDarkMode((prevDarkMode) => !prevDarkMode);
    };

    return (
        <DarkModeContext.Provider value={{ darkMode, toggleDarkMode }}>
            {children}
        </DarkModeContext.Provider>
    );
};

const useDarkMode = (): DarkModeContextProps => {
    const context = useContext(DarkModeContext);
    if (!context) {
        throw new Error("useDarkMode must be used within a DarkModeProvider");
    }
    return context;
};

export { DarkModeProvider, useDarkMode };
