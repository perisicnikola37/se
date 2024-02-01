import { useState } from 'react';
import axiosConfig from '../../config/axiosConfig';
import { useModal } from '../../contexts/GlobalContext';
import { IncomeInterface } from '../../interfaces/globalInterfaces';

interface FetchObjectsParams {
    pageSize: number;
    description?: string | null;
    minAmount?: number | null;
    maxAmount?: number | null;
    incomeGroupId?: string | null;
}

const useObjects = () => {
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [objects, setObjects] = useState<IncomeInterface[]>([]);
    const [error, setError] = useState<string | null>(null);
    const { setTotalRecords } = useModal();
    const [rowsPerPage, setRowsPerPage] = useState<number>(5);
    const [totalPages, setTotalPages] = useState<number>(0);
    const [currentPage, setCurrentPage] = useState<number>(0);

    const fetchObjects = async (params: FetchObjectsParams, objectType: string) => {
        const result = { isLoading: true, objects: [] as IncomeInterface[], error: null as string | null };

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

    const loadObjects = async (params: FetchObjectsParams, objectType: string) => {
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

    return { isLoading, objects, error, loadObjects, rowsPerPage, totalPages, currentPage, setRowsPerPage };
};

export default useObjects;
