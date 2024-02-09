import axiosConfig from "../config/axiosConfig";
import { ExpenseSimplified } from "../interfaces/globalInterfaces";

const fetchLatestExpenses = async () => {
  const result = {
    isLoading: true,
    expenses: [] as ExpenseSimplified[],
    error: null as string | null,
    highestExpense: 0,
  };

  try {
    const response = await axiosConfig.get("/api/expenses/latest/5");
    result.expenses = response.data.expenses;
    result.highestExpense = response.data.highestExpense;
  } catch (err) {
    result.error = "Error fetching latest expenses";
  } finally {
    result.isLoading = false;
  }

  return result;
};

export default fetchLatestExpenses;
