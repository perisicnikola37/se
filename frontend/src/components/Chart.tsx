import { useEffect } from "react";
import {
    LineChart,
    Line,
    XAxis,
    YAxis,
    CartesianGrid,
    Tooltip,
    Legend,
} from "recharts";
import useStatistics from "../hooks/Statistics/StatisticsHook";
import { DataPoint } from "../interfaces/globalInterfaces";

const generateData = (expenses: number[], incomes: number[]): DataPoint[] => {
    const currentDate = new Date();
    const data: DataPoint[] = [];

    for (let i = 6; i >= 0; i--) {
        const date = new Date(currentDate);
        date.setDate(currentDate.getDate() - i);

        const dateString = `${date.getDate()}.${date.getMonth() + 1}.`;

        const newDataPoint: DataPoint = {
            name: dateString,
            Incomes: incomes[i] || 0,
            Expenses: expenses[i] || 0,
        };

        data.push(newDataPoint);
    }

    return data;
};

const Chart = () => {
    const { expenses, incomes, loadStatistics } = useStatistics();
    const data = generateData(expenses.map((e) => e.amount), incomes.map((i) => i.amount));

    useEffect(() => {
        loadStatistics();
    }, []);

    return (
        <>
            <LineChart
                className="_chart m-auto mt-10"
                width={600}
                height={300}
                data={data}
                margin={{
                    top: 5,
                    right: 30,
                    left: 20,
                    bottom: 5,
                }}
            >
                <CartesianGrid strokeDasharray="3 3" />
                <XAxis dataKey="name" />
                <YAxis />
                <Tooltip />
                <Legend />

                <Line
                    type="monotone"
                    dataKey="Incomes"
                    stroke="#8884d8"
                    activeDot={{ r: 8 }}
                />

                <Line
                    type="monotone"
                    dataKey="Expenses"
                    stroke="#ff6767"
                    activeDot={{ r: 8 }}
                />
            </LineChart>
        </>
    );
};

export default Chart;
