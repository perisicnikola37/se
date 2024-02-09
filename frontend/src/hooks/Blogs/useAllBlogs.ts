import { useState } from "react";
import { BlogInterface } from "../../interfaces/globalInterfaces";
import { fetchAllBlogs } from "../../services/allBlogsService";

const useAllBlogs = () => {
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [blogs, setBlogs] = useState<BlogInterface[]>([]);
  const [error, setError] = useState<string | null>(null);

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
