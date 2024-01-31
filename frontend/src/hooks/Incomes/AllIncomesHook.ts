import { useState } from 'react';
import axiosConfig from '../../config/axiosConfig';
import { IncomeInterface } from '../../interfaces/globalInterfaces';

const useIncomes = () => {
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [incomes, setIncomes] = useState<IncomeInterface[]>([]);
    const [error, setError] = useState<string | null>(null);
    const [highestIncome, setHighestIncome] = useState<number>(0);

    const fetchIncomes = async () => {
        const result = { isLoading: true, incomes: [] as IncomeInterface[], error: null as string | null };

        try {
            const response = await axiosConfig.get('/api/incomes?pageSize=50');
            result.incomes = response.data.data;
            setHighestIncome(response.data.data[0]?.amount || 0);
        } catch (err) {
            result.error = 'Error fetching latest incomes';
        } finally {
            result.isLoading = false;
        }

        return result;
    };

    const loadIncomes = async () => {
        if (!isLoading) {
            setIsLoading(true);
            const result = await fetchIncomes();
            setIsLoading(false);

            if (result.incomes.length > 0) {
                setIncomes(result.incomes);
                setError(null);
            } else {
                setError(result.error);
            }
        }
    };

    return { isLoading, incomes, error, highestIncome, loadIncomes };
};

export default useIncomes;
