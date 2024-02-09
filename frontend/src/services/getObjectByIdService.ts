import axiosConfig from "../config/axiosConfig";
import { GetObjectInterface } from "../interfaces/globalInterfaces";

export const fetchObjectById = async (
  objectId: number,
  objectType: string,
  setObject: (data: GetObjectInterface | null) => void,
  setIsLoading: (loading: boolean) => void,
  setError: (error: string | null) => void,
) => {
  setIsLoading(true);
  setError(null);

  try {
    const endpoint = `/api/${objectType}s/${objectId}`;
    const response = await axiosConfig.get(endpoint);

    const objectData: GetObjectInterface = response.data;

    setObject(objectData);
  } catch (err) {
    setError(`Error fetching ${objectType} by ID`);
  } finally {
    setIsLoading(false);
  }
};
