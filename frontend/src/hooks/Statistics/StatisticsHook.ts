import { useState } from 'react';
import axiosConfig from '../../config/axiosConfig';
import { StatisticsResponse } from '../../interfaces/globalInterfaces';

const useStatistics = () => {
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [expenses, setExpenses] = useState<{ date: string; amount: number }[]>([]);
    const [incomes, setIncomes] = useState<{ date: string; amount: number }[]>([]);
    const [error, setError] = useState<string | null>(null);

    const fetchStatistics = async () => {
        const result = {
            isLoading: true,
            expenses: [] as { date: string; amount: number }[],
            incomes: [] as { date: string; amount: number }[],
            error: null as string | null,
        };

        try {
            const response = await axiosConfig.get('/api/summary/last-week');
            const data: StatisticsResponse = response.data;

            result.expenses = Object.entries(data.expenses).map(([date, amount]) => ({ date, amount }));
            result.incomes = Object.entries(data.incomes).map(([date, amount]) => ({ date, amount }));
        } catch (err) {
            result.error = 'Error fetching latest expenses';
        } finally {
            result.isLoading = false;
        }

        return result;
    };

    const loadStatistics = async () => {
        setIsLoading(true);
        const result = await fetchStatistics();
        setIsLoading(false);

        if (result.expenses.length > 0) {
            setExpenses(result.expenses);
            setIncomes(result.incomes);
            setError(null);
        } else {
            setError(result.error);
        }
    };

    return { isLoading, expenses, incomes, error, loadStatistics };
};

export default useStatistics;
