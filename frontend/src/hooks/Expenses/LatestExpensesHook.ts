import { useState, useEffect } from 'react';
import axiosConfig from '../../config/axiosConfig';

interface Expense {
    title: string;
    amount: number;
}

const fetchLatestExpenses = async () => {
    const result = { isLoading: false, expenses: [], error: null as string | null };

    try {
        const response = await axiosConfig.get('/api/expenses/latest/5');
        result.expenses = response.data;
        console.log(response.data);
    } catch (err) {
        result.error = 'Error fetching latest expenses';
    } finally {
        result.isLoading = false;
    }

    return result;
};

const useLatestExpenses = () => {
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [expenses, setExpenses] = useState<Expense[]>([]);
    const [error, setError] = useState<string | null>(null);

    const loadLatestExpenses = async () => {
        setIsLoading(true);
        const result = await fetchLatestExpenses();
        setIsLoading(false);

        if (result.expenses) {
            setExpenses(result.expenses);
            setError(null);
        } else {
            setError(result.error);
        }
    };

    useEffect(() => {
        loadLatestExpenses();
    }, []);

    return { isLoading, expenses, error, loadLatestExpenses };
};

export default useLatestExpenses;
