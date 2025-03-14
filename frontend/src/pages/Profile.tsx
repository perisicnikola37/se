import { useEffect } from "react";

import CountUp from "react-countup";
import { Helmet } from "react-helmet";
import { motion } from "framer-motion";

import { Button } from "@mui/material";

import BorderLinearProgress from "../components/Progress";
import { useDarkMode } from "../contexts/DarkModeContext";
import userPicture from "../../src/assets/profile_image.jpg";
import useCurrentUser from "../hooks/Authentication/useCurrentUser";
import useDeleteAccount from "../hooks/Authentication/useDeleteAccount";

const Profile = () => {
  const { loadCurrentUser, user } = useCurrentUser();
  const { deleteAccount } = useDeleteAccount();
  const { darkMode } = useDarkMode();

  useEffect(() => {
    loadCurrentUser();
  }, []);

  const handleDeleteAccount = () => {
    deleteAccount();
  };

  const totalIncome =
    user?.incomes.reduce((total, income) => total + income.amount, 0) || 0;
  const totalIncomes = user?.incomes.length || 0;
  const totalExpenses = user?.expenses.length || 0;
  const totalProgress = totalIncomes + totalExpenses;

  return (
    <motion.div
      initial={{ opacity: 0, y: -20 }}
      animate={{ opacity: 1, y: 0 }}
      exit={{ opacity: 0, y: 20 }}
      transition={{ duration: 0.5 }}
      className="w-full max-w-screen-xl min-h-[48rem] mx-auto p-4 md:py-8"
    >
      <Helmet>
        <title>User profile | Expense Tracker</title>
      </Helmet>
      <motion.div
        initial={{ opacity: 0, y: -20 }}
        animate={{ opacity: 1, y: 0 }}
        transition={{ duration: 0.5 }}
        className="p-16"
      >
        <motion.div
          initial={{ opacity: 0 }}
          animate={{ opacity: 1 }}
          transition={{ duration: 0.5 }}
          className={`p-8 ${darkMode ? "bg-[#262D3B] text-white" : "bg-white text-black"} shadow mt-24`}
        >
          <div className="grid grid-cols-1 md:grid-cols-3">
            <div className="grid grid-cols-3 text-center order-last md:order-first mt-20 md:mt-0">
              <div>
                <p className="font-bold text-red-500 text-xl">
                  <CountUp end={user?.expenses ? user.expenses.length : 0} />
                </p>
                <p className={darkMode ? "text-white" : "text-gray-400"}>
                  Expenses
                </p>
              </div>
              <div>
                <p className="font-bold text-green-500 text-xl">
                  <CountUp end={user?.incomes ? user.incomes.length : 0} />
                </p>
                <p className={darkMode ? "text-white" : "text-gray-400"}>
                  Incomes
                </p>
              </div>
              <div>
                <p className="font-bold text-[#2563EB] text-xl">
                  $<CountUp end={totalIncome} />
                </p>
                <p className={darkMode ? "text-white" : "text-gray-400"}>
                  Total revenue
                </p>
              </div>
            </div>

            <div className="relative">
              <div className="w-48 h-48 mx-auto absolute inset-x-0 top-0 -mt-24 overflow-hidden rounded-full shadow-2xl flex items-center justify-center">
                <img
                  src={userPicture}
                  alt="User profile picture"
                  className="w-full h-full object-cover rounded-full"
                />
              </div>
            </div>

            <div className="space-x-8 flex justify-between mt-32 md:mt-0 md:justify-center">
              <motion.button
                onClick={handleDeleteAccount}
                initial={{ opacity: 0, y: -20 }}
                animate={{ opacity: 1, y: 0 }}
                transition={{ duration: 0.5 }}
                color="error"
              >
                <Button
                  onClick={handleDeleteAccount}
                  variant="outlined"
                  color="error"
                >
                  Delete account
                </Button>
              </motion.button>
            </div>
          </div>

          <div className="mt-20 text-center border-b pb-12">
            <h1
              className={
                darkMode
                  ? "text-white text-4xl font-medium mb-3"
                  : "text-gray-700 text-4xl font-medium mb-3"
              }
            >
              {user?.username}
            </h1>
            <a
              className="hover:text-[#3363EB] duration-300"
              href={`mailto:${user?.email}`}
            >
              {user?.email}
            </a>
            <p
              className={
                darkMode
                  ? "text-white font-light mt-3"
                  : "text-gray-600 font-light mt-3"
              }
            >
              Podgorica, Montenegro
            </p>
            <div className="mt-10 text-center">
              <h1
                className={
                  darkMode
                    ? "text-white text-2xl font-medium mb-3"
                    : "text-gray-700 text-4xl font-medium mb-3"
                }
              >
                Basic Plan
              </h1>
              <p
                className={
                  darkMode
                    ? "text-white font-light mb-2"
                    : "text-gray-600 font-light mb-2"
                }
              >
                You can add up to {100 - totalProgress} more expenses/incomes
              </p>
              <BorderLinearProgress
                variant="determinate"
                value={totalProgress}
              />
              <p
                className={
                  darkMode
                    ? "text-white mt-2 text-lg font-medium"
                    : "text-gray-700 mt-2 text-lg font-medium"
                }
              >
                {totalProgress}% Complete
              </p>
            </div>
          </div>
        </motion.div>
      </motion.div>
    </motion.div>
  );
};

export default Profile;
