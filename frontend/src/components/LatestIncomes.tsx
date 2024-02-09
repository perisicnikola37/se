import { useEffect, useRef } from "react";
import Slider from "react-slick";
import EastSharpIcon from "@mui/icons-material/EastSharp";
import {
  LineChart,
  Line,
  XAxis,
  YAxis,
  CartesianGrid,
  Tooltip,
  Legend,
} from "recharts";
import AttachMoneyIcon from "@mui/icons-material/AttachMoney";
import useLatestIncomes from "../hooks/Incomes/useLatestIncomes";
import { formatDate } from "../utils/utils";
import { motion } from "framer-motion";

const LatestIncomes = () => {
  const slider = useRef<Slider | null>(null);
  const { incomes, loadLatestIncomes, highestIncome } = useLatestIncomes();

  useEffect(() => {
    const fetchData = async () => {
      await loadLatestIncomes();
    };

    setTimeout(() => {
      fetchData();
    }, 1000);
  }, []);

  const settings = {
    dots: true,
    infinite: true,
    speed: 500,
    slidesToShow: 1,
    slidesToScroll: 1,
  };

  const incomeData = incomes.map((income) => [
    {
      name: "Initial state",
      $: 0,
    },
    {
      name: "Highest income",
      $: highestIncome,
    },
    {
      name: "This income",
      $: income.amount,
    },
  ]);

  return (
    <>
      {incomes.length > 1 && (
        <>
          <Slider
            ref={slider}
            {...settings}
            className="w-[90%] lg:w-1/2 xs:w-1/2 rounded-lg m-10 mt-20"
          >
            {incomes.map((income, index) => (
              <motion.div
                key={index}
                initial={{ opacity: 0 }}
                animate={{ opacity: 1 }}
                exit={{ opacity: 0 }}
                transition={{ duration: 0.5 }}
                className="bg-green-100 p-4 mx-4 flex justify-between items-center w-full"
              >
                <div>
                  <div className="text-xl font-bold mb-2">
                    {income.description}
                  </div>
                  <div className="text-2xl">${income.amount.toFixed(2)}</div>
                  <p>{formatDate(income.createdAt)}</p>
                </div>
                <div className="hidden-sm float-right">
                  <LineChart
                    key={index}
                    width={500}
                    height={200}
                    data={incomeData[index]}
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
                    <Line type="monotone" dataKey="$" stroke="#2563EB" />
                  </LineChart>
                </div>
                <div className="sm:hidden float-right">
                  <AttachMoneyIcon className="text-green-500 mr-2" />
                </div>
              </motion.div>
            ))}
          </Slider>
          <motion.button
            className="mb-10"
            onClick={() => slider?.current?.slickNext()}
            initial={{ opacity: 0 }}
            animate={{ opacity: 1 }}
            exit={{ opacity: 0 }}
            transition={{ duration: 0.5 }}
          >
            Next <EastSharpIcon onClick={() => slider?.current?.slickNext()} />
          </motion.button>
        </>
      )}
    </>
  );
};

export default LatestIncomes;
