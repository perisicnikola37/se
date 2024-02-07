import { ReactNode } from "react";
import {
  ExpenseInterface,
  IncomeInterface,
} from "../interfaces/globalInterfaces";

export type StatisticsResponseType = {
  expenses: { [date: string]: number };
  incomes: { [date: string]: number };
};

export type Expense = {
  date: string;
  amount: number;
};

export interface Income {
  id: number;
  description: string;
  amount: number;
  incomeGroup: {
    name: string;
  };
}

export type DarkModeProviderProps = {
  children: ReactNode;
};

export type ObjectInterface = IncomeInterface | ExpenseInterface;

export type Order = "asc" | "desc";
