import { DataPoint } from "../interfaces/globalInterfaces";

export const generateData = (
  expenses: number[],
  incomes: number[],
): DataPoint[] => {
  const currentDate = new Date();
  const data: DataPoint[] = [];

  for (let i = 6; i >= 0; i--) {
    const date = new Date(currentDate);
    date.setDate(currentDate.getDate() - i);

    const dateString = `${date.getDate()}.${date.getMonth() + 1}.`;

    const newDataPoint: DataPoint = {
      name: dateString,
      incomes: incomes[i] || 0,
      expenses: expenses[i] || 0,
    };

    data.push(newDataPoint);
  }

  return data;
};

export const generateLoremData = (): DataPoint[] => {
  const currentDate = new Date();
  const data: DataPoint[] = [];

  for (let i = 6; i >= 0; i--) {
    const date = new Date(currentDate);
    date.setDate(currentDate.getDate() - i);

    const dateString = `${date.getDate()}.${date.getMonth() + 1}.`;

    const newDataPoint: DataPoint = {
      name: dateString,
      incomes: Math.floor(Math.random() * 1000),
      expenses: Math.floor(Math.random() * 500),
    };

    data.push(newDataPoint);
  }

  return data;
};
