import { useState } from "react";

import axiosConfig from "../../config/axiosConfig";

const useDeleteObject = () => {
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  const deleteObject = async (objectId: number, objectType: string) => {
    setIsLoading(true);
    setError(null);

    try {
      const endpoint = `/api/${objectType}s/${objectId}`;

      await axiosConfig.delete(endpoint);
    } catch (err) {
      setError(`Error deleting ${objectType}`);
    } finally {
      setIsLoading(false);
    }
  };

  return { isLoading, error, deleteObject };
};

export default useDeleteObject;
