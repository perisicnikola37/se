import axiosConfig from "../config/axiosConfig";
import { ObjectGroupDataInterface } from "../interfaces/globalInterfaces";

export const fetchObjectGroupById = async (
  objectId: number,
  objectType: string,
  setObject: (data: ObjectGroupDataInterface | null) => void,
  setIsLoading: (loading: boolean) => void,
  setError: (error: string | null) => void,
) => {
  setIsLoading(true);
  setError(null);

  try {
    const endpoint = `/api/${objectType}s/groups/${objectId}`;
    const response = await axiosConfig.get(endpoint);

    const objectData: ObjectGroupDataInterface = response.data;

    setObject(objectData);
  } catch (err) {
    setError(`Error fetching ${objectType} group by ID`);
  } finally {
    setIsLoading(false);
  }
};
