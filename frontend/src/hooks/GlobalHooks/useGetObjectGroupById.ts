import { useState } from "react";
import { ObjectGroupDataInterface } from "../../interfaces/globalInterfaces";
import { fetchObjectGroupById } from "../../services/getObjectGroupByIdService";

const useGetObjectGroupById = (objectId: number, objectType: string) => {
  const [object, setObject] = useState<ObjectGroupDataInterface | null>(null);
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  const getObjectGroupById = () => {
    if (objectId) {
      fetchObjectGroupById(
        objectId,
        objectType,
        setObject,
        setIsLoading,
        setError,
      );
    }
  };

  return { object, isLoading, error, getObjectGroupById };
};

export default useGetObjectGroupById;
