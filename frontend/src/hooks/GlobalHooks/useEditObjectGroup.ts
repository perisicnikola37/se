import { useState } from "react";
import axiosConfig from "../../config/axiosConfig";
import { UpdateObjectGroupInterface } from "../../interfaces/globalInterfaces";

const useEditObjectGroup = () => {
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  const editObjectGroup = async (
    objectId: number,
    objectType: string,
    updatedData: UpdateObjectGroupInterface,
  ) => {
    setIsLoading(true);
    setError(null);

    try {
      const endpoint = `/api/${objectType}s/groups/${objectId}`;

      await axiosConfig.put(endpoint, updatedData);
    } catch (err) {
      setError(`Error editing ${objectType}`);
    } finally {
      setIsLoading(false);
    }
  };

  return { isLoading, error, editObjectGroup };
};

export default useEditObjectGroup;
