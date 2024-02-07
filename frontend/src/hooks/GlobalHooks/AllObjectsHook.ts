import { useState } from "react";
import axiosConfig from "../../config/axiosConfig";
import { useModal } from "../../contexts/GlobalContext";
import { FetchObjectsParams } from "../../interfaces/globalInterfaces";
import { ObjectInterface } from "../../types/globalTypes";

const useObjects = <T extends ObjectInterface>() => {
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [objects, setObjects] = useState<T[]>([]);
  const [error, setError] = useState<string | null>(null);
  const { setTotalRecords } = useModal();
  const [rowsPerPage, setRowsPerPage] = useState<number>(5);
  const [totalPages, setTotalPages] = useState<number>(0);
  const [currentPage, setCurrentPage] = useState<number>(0);

  const fetchObjects = async (
    params: FetchObjectsParams,
    objectType: string,
  ): Promise<{ isLoading: boolean; objects: T[]; error: string | null }> => {
    const result = {
      isLoading: true,
      objects: [] as T[],
      error: null as string | null,
    };

    try {
      const response = await axiosConfig.get(`/api/${objectType}s`, { params });
      result.objects = response.data.data;

      setTotalRecords(response.data.totalRecords);
      setRowsPerPage(response.data.pageSize);
      setTotalPages(response.data.totalPages);
      setCurrentPage(response.data.pageNumber);
    } catch (err) {
      result.error = `Error fetching objects`;
    } finally {
      result.isLoading = false;
    }

    return result;
  };

  const loadObjects = async (
    params: FetchObjectsParams,
    objectType: string,
  ): Promise<void> => {
    if (!isLoading) {
      setIsLoading(true);
      const result = await fetchObjects(params, objectType);
      setIsLoading(false);

      if (result.objects.length > 0) {
        setObjects(result.objects);
        setError(null);
      } else {
        setObjects([]);
        setError(result.error);
      }
    }
  };

  return {
    isLoading,
    objects,
    error,
    loadObjects,
    rowsPerPage,
    totalPages,
    currentPage,
    setRowsPerPage,
  };
};

export default useObjects;
