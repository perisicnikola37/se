import { useState } from "react";

import { BlogType } from "../../interfaces/globalInterfaces";
import { fetchBlogById } from "../../services/getBlogByIdService";

const useGetBlogById = (objectId: number) => {
  const [blog, setBlog] = useState<BlogType | null>(null);
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  const getBlogById = () => {
    if (objectId) {
      fetchBlogById(objectId, setBlog, setIsLoading, setError);
    }
  };

  return { blog, isLoading, error, getBlogById };
};

export default useGetBlogById;
