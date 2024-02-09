import { useState } from "react";

import { User } from "../../interfaces/globalInterfaces";
import fetchCurrentUser from "../../services/currentUserService";

const useCurrentUser = () => {
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [user, setUser] = useState<User | null>(null);
  const [error, setError] = useState<string | null>(null);

  const loadCurrentUser = async () => {
    setIsLoading(true);
    const result = await fetchCurrentUser();
    setIsLoading(false);

    if (result.user) {
      setUser(result.user);
      setError(null);
    } else {
      setError(result.error);
    }
  };

  return { isLoading, user, error, loadCurrentUser };
};

export default useCurrentUser;
