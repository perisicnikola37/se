export interface StatisticsResponse {
    expenses: { [date: string]: number };
    incomes: { [date: string]: number };
}

export interface DataPoint {
    name: string;
    Incomes: number;
    Expenses: number;
}