import { useState } from "react";
import { GetObjectInterface } from "../../interfaces/globalInterfaces";
import { fetchObjectById } from "../../services/getObjectByIdService";

const useGetObjectById = (objectId: number, objectType: string) => {
  const [object, setObject] = useState<GetObjectInterface | null>(null);
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  const getObjectById = () => {
    if (objectId) {
      fetchObjectById(objectId, objectType, setObject, setIsLoading, setError);
    }
  };

  return { object, isLoading, error, getObjectById };
};

export default useGetObjectById;
