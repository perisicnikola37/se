import { useState } from 'react';
import axiosConfig from '../../config/axiosConfig';

interface Expense {
    description: string;
    amount: number;
    createdAt: string;
}

const useLatestExpenses = () => {
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [expenses, setExpenses] = useState<Expense[]>([]);
    const [error, setError] = useState<string | null>(null);
    const [highestExpense, setHighestExpense] = useState<number>(0);

    const fetchLatestExpenses = async () => {
        const result = { isLoading: true, expenses: [] as Expense[], error: null as string | null };

        try {
            const response = await axiosConfig.get('/api/expenses/latest/5');
            result.expenses = response.data.expenses;
            setHighestExpense(response.data.highestExpense);
        } catch (err) {
            result.error = 'Error fetching latest expenses';
        } finally {
            result.isLoading = false;
        }

        return result;
    };

    const loadLatestExpenses = async () => {
        setIsLoading(true);
        const result = await fetchLatestExpenses();
        setIsLoading(false);

        if (result.expenses.length > 0) {
            setExpenses(result.expenses);
            setError(null);
        } else {
            setError(result.error);
        }
    };

    return { isLoading, expenses, error, highestExpense, loadLatestExpenses };
};

export default useLatestExpenses;
