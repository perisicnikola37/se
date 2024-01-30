import { useState } from 'react';
import axiosConfig from '../../config/axiosConfig';

interface Income {
    title: string;
    amount: number;
}

const fetchLatestIncomes = async () => {
    const result = { isLoading: false, incomes: [], error: null as string | null };

    try {
        const response = await axiosConfig.get('/api/incomes/latest/5');
        result.incomes = response.data;
        console.log(response.data)
    } catch (err) {
        result.error = 'Error fetching latest incomes';
    } finally {
        result.isLoading = false;
    }

    return result;
};

const useLatestIncomes = () => {
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [incomes, setIncomes] = useState<Income[]>([]);
    const [error, setError] = useState<string | null>(null);

    const loadLatestIncomes = async () => {
        setIsLoading(true);
        const result = await fetchLatestIncomes();
        setIsLoading(false);

        if (result.incomes) {
            setIncomes(result.incomes);
            setError(null);
        } else {
            setError(result.error);
        }
    };

    return { isLoading, incomes, error, loadLatestIncomes };
};

export default useLatestIncomes;
