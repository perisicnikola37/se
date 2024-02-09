import { useState } from "react";

import { IncomeSimplified } from "../../interfaces/globalInterfaces";
import fetchLatestIncomes from "../../services/latestIncomesService";

const useLatestIncomes = () => {
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [incomes, setIncomes] = useState<IncomeSimplified[]>([]);
  const [error, setError] = useState<string | null>(null);
  const [highestIncome, setHighestIncome] = useState<number>(0);

  const loadLatestIncomesData = async () => {
    setIsLoading(true);
    const result = await fetchLatestIncomes();
    setIsLoading(false);

    if (result.incomes.length > 0) {
      setIncomes(result.incomes);
      setHighestIncome(result.highestIncome);
      setError(null);
    } else {
      setError(result.error);
    }
  };

  return {
    isLoading,
    incomes,
    error,
    highestIncome,
    loadLatestIncomes: loadLatestIncomesData,
  };
};

export default useLatestIncomes;
