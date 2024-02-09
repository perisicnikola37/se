import { useEffect } from "react";

import { CartesianGrid, Label, Legend, Line, LineChart, Tooltip, XAxis, YAxis } from "recharts";

import { useUser } from "../contexts/UserContext";
import useStatistics from "../hooks/Statistics/useStatistics";
import { generateData, generateLoremData } from "../utils/generateChartData";

const Chart = () => {
  const { expenses, incomes, loadStatistics } = useStatistics();
  const { isLoggedIn } = useUser();
  const data = isLoggedIn()
    ? generateData(
      expenses.map((e) => e.amount),
      incomes.map((i) => i.amount),
    )
    : generateLoremData();

  useEffect(() => {
    if (isLoggedIn()) loadStatistics();
  }, [isLoggedIn]);

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
        }}
      >
        <CartesianGrid strokeDasharray="3 3" />
        <XAxis dataKey="name">
          <Label
            value="x axis - last 7 days"
            offset={-120}
            position="right"
            dy={20}
          />
        </XAxis>
        <YAxis>
          <Label value="y axis - amount" angle={-90} position="insideLeft" />
        </YAxis>
        <Tooltip />
        <Legend />
        <Line
          type="monotone"
          dataKey="incomes"
          stroke="#8884d8"
          activeDot={{ r: 8 }}
        />
        <Line
          type="monotone"
          dataKey="expenses"
          stroke="#ff6767"
          activeDot={{ r: 8 }}
        />
      </LineChart>
    </>
  );
};

export default Chart;
