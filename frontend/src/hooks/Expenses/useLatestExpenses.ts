import { useState } from "react";
import { ExpenseSimplified } from "../../interfaces/globalInterfaces";
import fetchLatestExpenses from "../../services/latestExpensesService";

const useLatestExpenses = () => {
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [expenses, setExpenses] = useState<ExpenseSimplified[]>([]);
  const [error, setError] = useState<string | null>(null);
  const [highestExpense, setHighestExpense] = useState<number>(0);

  const loadLatestExpenses = async () => {
    setIsLoading(true);
    const result = await fetchLatestExpenses();
    setIsLoading(false);

    if (result.expenses.length > 0) {
      setExpenses(result.expenses);
      setHighestExpense(result.highestExpense);
      setError(null);
    } else {
      setError(result.error);
    }
  };

  return { isLoading, expenses, error, highestExpense, loadLatestExpenses };
};

export default useLatestExpenses;
