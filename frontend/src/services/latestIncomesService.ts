import axiosConfig from "../config/axiosConfig";
import { IncomeSimplified } from "../interfaces/globalInterfaces";

const fetchLatestIncomes = async () => {
  const result = {
    isLoading: true,
    incomes: [] as IncomeSimplified[],
    error: null as string | null,
    highestIncome: 0,
  };

  try {
    const response = await axiosConfig.get("/api/incomes/latest/5");
    result.incomes = response.data.incomes;
    result.highestIncome = response.data.highestIncome;
  } catch (err) {
    result.error = "Error fetching latest incomes";
  } finally {
    result.isLoading = false;
  }

  return result;
};

export default fetchLatestIncomes;
