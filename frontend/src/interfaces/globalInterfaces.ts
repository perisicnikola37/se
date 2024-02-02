export interface StatisticsResponse {
    expenses: { [date: string]: number };
    incomes: { [date: string]: number };
}

export interface DataPoint {
    name: string;
    incomes: number;
    expenses: number;
}

export interface IncomeInterface {
    id: number;
    description: string;
    amount: number;
    createdAt: string;
    incomeGroup: {
        id: number;
        name: string;
        description: string;
        createdAt: string;
        incomes: IncomeInterface[] | null;
    } | null;
    user: {
        username: string;
    } | null;
}

export interface ExpenseInterface {
    id: number;
    description: string;
    amount: number;
    createdAt: string;
    expenseGroup: {
        id: number;
        name: string;
        description: string;
        createdAt: string;
        expenses: ExpenseInterface[] | null;
    } | null;
    user: {
        username: string;
    } | null;
}


export interface SimplifiedObjectInterface {
    id: number;
    description: string;
    amount: number;
    createdAt: string;
    incomeGroupId: number;
    userId: number;
    userUsername: string;
}

export interface ObjectGroupInterface {
    id: number;
    name: string;
    description: string;
    incomes: SimplifiedObjectInterface[] | null;
}