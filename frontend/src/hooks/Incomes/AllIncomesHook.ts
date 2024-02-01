import { useState } from 'react';
import axiosConfig from '../../config/axiosConfig';
import { IncomeInterface } from '../../interfaces/globalInterfaces';
import { useModal } from '../../contexts/GlobalContext';

interface FetchIncomesParams {
    pageSize: number;
    description?: string | null;
    minAmount?: number | null;
    maxAmount?: number | null;
    incomeGroupId?: string | null;
}

const useIncomes = () => {
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [incomes, setIncomes] = useState<IncomeInterface[]>([]);
    const [error, setError] = useState<string | null>(null);
    const [highestIncome, setHighestIncome] = useState<number>(0);
    // const [totalRecords, setTotalRecords] = useState<number>(0); // Corrected the type
    const [totalPages, setTotalPages] = useState<number>(0);
    const [rowsPerPage, setRowsPerPage] = useState<number>(5);
    const [currentPage, setCurrentPage] = useState<number>(0);
    const { setTotalRecords } = useModal()

    const fetchIncomes = async (params: FetchIncomesParams) => {
        const result = { isLoading: true, incomes: [] as IncomeInterface[], error: null as string | null };

        try {
            const response = await axiosConfig.get('/api/incomes', { params });
            result.incomes = response.data.data;

            setTotalRecords(response.data.totalRecords);
            setRowsPerPage(response.data.pageSize);
            setTotalPages(response.data.totalPages);
            setCurrentPage(response.data.pageNumber);

            setHighestIncome(response.data.data[0]?.amount || 0);
        } catch (err) {
            result.error = 'Error fetching latest incomes';
        } finally {
            result.isLoading = false;
        }

        return result;
    };

    const loadIncomes = async (params: FetchIncomesParams) => {
        if (!isLoading) {
            setIsLoading(true);
            const result = await fetchIncomes(params);
            setIsLoading(false);

            if (result.incomes.length > 0) {
                setIncomes(result.incomes);
                setError(null);
            } else {
                setIncomes([]);
                setError(result.error);
            }
        }
    };

    return { isLoading, incomes, error, highestIncome, loadIncomes, rowsPerPage, totalPages, currentPage, setRowsPerPage, };
};

export default useIncomes;
