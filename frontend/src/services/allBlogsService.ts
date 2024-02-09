import axiosConfig from "../config/axiosConfig";
import { BlogInterface } from "../interfaces/globalInterfaces";

export const fetchAllBlogs = async () => {
  const result = {
    isLoading: true,
    blogs: [] as BlogInterface[],
    error: null as string | null,
  };

  try {
    const response = await axiosConfig.get("/api/blogs");
    result.blogs = response.data;
  } catch (err) {
    result.error = "Error fetching blogs.";
  } finally {
    result.isLoading = false;
  }

  return result;
};
