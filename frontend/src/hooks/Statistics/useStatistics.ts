import { useState } from "react";

import { fetchStatistics } from "../../services/statisticsService";

const useStatistics = () => {
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [expenses, setExpenses] = useState<{ date: string; amount: number }[]>(
    [],
  );
  const [incomes, setIncomes] = useState<{ date: string; amount: number }[]>(
    [],
  );
  const [error, setError] = useState<string | null>(null);

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
