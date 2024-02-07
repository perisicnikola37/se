import { useState } from "react";
import axiosConfig from "../../config/axiosConfig";
import { UpdateObjectInterface } from "../../interfaces/globalInterfaces";

const useEditObject = () => {
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  const editObject = async (
    objectId: number,
    objectType: string,
    updatedData: UpdateObjectInterface,
  ) => {
    setIsLoading(true);
    setError(null);

    try {
      const endpoint = `/api/${objectType}s/${objectId}`;

      await axiosConfig.put(endpoint, updatedData);
    } catch (err) {
      setError(`Error editing ${objectType}`);
    } finally {
      setIsLoading(false);
    }
  };

  return { isLoading, error, editObject };
};

export default useEditObject;
