export type StatisticsResponseType = {
    expenses: { [date: string]: number };
    incomes: { [date: string]: number };
}

export type Expense = {
    date: string;
    amount: number;
}

export type Income = {
    date: string;
    amount: number;
}