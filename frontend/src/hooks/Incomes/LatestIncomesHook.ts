import { useState } from 'react';
import axiosConfig from '../../config/axiosConfig';

interface Income {
    description: string;
    amount: number;
    createdAt: string;
}

const useLatestIncomes = () => {
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [incomes, setIncomes] = useState<Income[]>([]);
    const [error, setError] = useState<string | null>(null);
    const [highestIncome, setHighestIncome] = useState<number>(0);

    const fetchLatestIncomes = async () => {
        const result = { isLoading: true, incomes: [] as Income[], error: null as string | null };

        try {
            const response = await axiosConfig.get('/api/incomes/latest/5');
            result.incomes = response.data.incomes;
            setHighestIncome(response.data.highestIncome);
        } catch (err) {
            result.error = 'Error fetching latest incomes';
        } finally {
            result.isLoading = false;
        }

        return result;
    };

    const loadLatestIncomes = async () => {
        if (!isLoading) {
            setIsLoading(true);
            const result = await fetchLatestIncomes();
            setIsLoading(false);

            if (result.incomes.length > 0) {
                setIncomes(result.incomes);
                setError(null);
            } else {
                setError(result.error);
            }
        }
    };

    return { isLoading, incomes, error, highestIncome, loadLatestIncomes };
};

export default useLatestIncomes;
