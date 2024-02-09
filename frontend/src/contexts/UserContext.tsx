import { createContext, useContext, useState } from "react";

import { User, UserContextProps, UserProviderProps } from "../interfaces/contextsInterfaces";

const UserContext = createContext<UserContextProps | undefined>(undefined);

export const UserProvider = ({ children }: UserProviderProps) => {
  const [user, setUser] = useState<User>({
    token: localStorage.getItem("token") || null,
    id: localStorage.getItem("id")
      ? parseInt(localStorage.getItem("id")!)
      : null,
    username: localStorage.getItem("username") || null,
    email: localStorage.getItem("email") || null,
    accountType: localStorage.getItem("accountType") || null,
    formattedCreatedAt: localStorage.getItem("formattedCreatedAt") || null,
  });

  const isLoggedIn = () => {
    return user.token !== null;
  };

  return (
    <UserContext.Provider value={{ user, setUser, isLoggedIn }}>
      {children}
    </UserContext.Provider>
  );
};

export const useUser = () => {
  const context = useContext(UserContext);

  if (!context) {
    throw new Error("useUser must be used within a UserProvider");
  }

  return context;
};
