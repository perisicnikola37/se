import axiosConfig from "../config/axiosConfig";
import { StatisticsResponse } from "../interfaces/globalInterfaces";

export const fetchStatistics = async () => {
  const result = {
    isLoading: true,
    expenses: [] as { date: string; amount: number }[],
    incomes: [] as { date: string; amount: number }[],
    error: null as string | null,
  };

  try {
    const response = await axiosConfig.get("/api/summary/last-week");
    const data: StatisticsResponse = response.data;

    result.expenses = Object.entries(data.expenses).map(([date, amount]) => ({
      date,
      amount,
    }));
    result.incomes = Object.entries(data.incomes).map(([date, amount]) => ({
      date,
      amount,
    }));
  } catch (err) {
    result.error = "Error fetching latest expenses";
  } finally {
    result.isLoading = false;
  }

  return result;
};
