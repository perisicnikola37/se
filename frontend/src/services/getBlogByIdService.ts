import axiosConfig from "../config/axiosConfig";
import { BlogType } from "../interfaces/globalInterfaces";

export const fetchBlogById = async (
  objectId: number,
  setBlog: (data: BlogType | null) => void,
  setIsLoading: (loading: boolean) => void,
  setError: (error: string | null) => void,
) => {
  setIsLoading(true);
  setError(null);

  try {
    const endpoint = `/api/blogs/${objectId}`;
    const response = await axiosConfig.get(endpoint);

    const objectData: BlogType = response.data;

    setBlog(objectData);
  } catch (err) {
    setError(`Error fetching blog by ID`);
  } finally {
    setIsLoading(false);
  }
};
