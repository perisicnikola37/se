import { useState } from "react";

import axiosConfig from "../../config/axiosConfig";

const useDeleteObjectGroup = () => {
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  const deleteObjectGroup = async (objectId: number, objectType: string) => {
    setIsLoading(true);
    setError(null);

    try {
      const endpoint = `/api/${objectType}s/groups/${objectId}`;

      await axiosConfig.delete(endpoint);
    } catch (err) {
      setError(`Error deleting ${objectType} group.`);
    } finally {
      setIsLoading(false);
    }
  };

  return { isLoading, error, deleteObjectGroup };
};

export default useDeleteObjectGroup;
