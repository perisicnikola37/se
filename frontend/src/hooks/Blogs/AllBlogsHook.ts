import { useState } from "react";
import axiosConfig from "../../config/axiosConfig";
import { BlogInterface } from "../../interfaces/globalInterfaces";

const useAllBlogs = () => {
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [blogs, setBlogs] = useState<BlogInterface[]>([]);
  const [error, setError] = useState<string | null>(null);

  const fetchAllBlogs = async () => {
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

  const loadBlogs = async () => {
    setIsLoading(true);
    const result = await fetchAllBlogs();
    setIsLoading(false);

    if (result.blogs.length > 0) {
      setBlogs(result.blogs);
      setError(null);
    } else {
      setBlogs([]);
      setError(result.error);
    }
  };

  return { isLoading, blogs, error, loadBlogs };
};

export default useAllBlogs;
