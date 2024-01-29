// Dashboard.tsx
import { AnimatePresence, MotionStyle, motion } from "framer-motion";
import ChartReacharts from "../components/Chart";
import { Table } from "@mui/material";
import ViewInArIcon from '@mui/icons-material/ViewInAr';
import FeaturedInSection from "../components/FeaturedIn";
import LatestIncomes from "../components/LatestIncomes";
import 'slick-carousel/slick/slick.css';
import 'slick-carousel/slick/slick-theme.css';
import LatestExpenses from "../components/LatestExpenses";

const Dashboard = () => {
    const h1MotionStyle: MotionStyle = {
        fontFamily: 'Inter, ui-sans-serif, system-ui, -apple-system, "Segoe UI", Roboto, "Helvetica Neue", Arial, "Noto Sans", sans-serif, "Apple Color Emoji", "Segoe UI Emoji", "Segoe UI Symbol", "Noto Color Emoji"',
        fontStyle: 'normal',
        fontVariantCaps: 'normal',
        fontVariantEastAsian: 'normal',
        fontVariantLigatures: 'normal',
        fontVariantNumeric: 'normal',
        fontWeight: '800',
    };

    return (
        <div className="flex-grow flex flex-col  items-center justify-center">
            <div className="_main-text">
                <motion.h4
                    style={h1MotionStyle}
                    className="font-extrabold text-4xl mt-20 tracking-tighter leading-none"
                    initial={{ opacity: 0, scale: 0.8 }}
                    animate={{ opacity: 1, scale: 1 }}
                    transition={{ duration: 0.8 }}
                >
                    Track expenses even faster with <br /> our brand new Expense Tracker application
                </motion.h4>
                <motion.p
                    className="_main-text_paragraph mt-3 text-gray-500"
                    initial={{ opacity: 0, scale: 0.8 }}
                    animate={{ opacity: 1, scale: 1 }}
                    transition={{ duration: 0.8 }}
                >
                    Take advantage of our open-source solution, designed to streamline and enhance your expense tracking experience. Dive into a wealth of features and functionalities that empower you to manage your finances effortlessly.
                </motion.p>
            </div>
            <div className="mt-5">
                <button type="button" className="text-white bg-blue-700 hover:bg-blue-800  font-medium rounded-md text-sm px-3 py-1.5 me-2 mb-2 dark:bg-blue-600 dark:hover:bg-blue-700 focus:outline-none dark:focus:ring-blue-800">
                    <a href="#">Get started</a>
                </button>

                <button type="button" className="py-1.5 px-3 me-2 mb-2 text-sm font-medium text-gray-900  bg-white rounded-lg border border-gray-200 hover:bg-gray-100 hover:text-blue-700 focus:z-10 ">
                    <ViewInArIcon style={{ fontSize: '14px' }} className="mr-2" />
                    <a href="https://git.vegaitsourcing.rs/nikola.perisic/vega-internship-project" target="_blank">
                        Explore repository
                    </a>
                </button>
            </div>

            <AnimatePresence>
                <ChartReacharts />
                <motion.div
                    key="table1"
                    initial={{ opacity: 0 }}
                    animate={{ opacity: 1 }}
                    exit={{ opacity: 0 }}
                    transition={{ duration: 1.5 }}
                    layout
                >
                    <Table />
                </motion.div>
                <motion.div
                    key="table2"
                    initial={{ opacity: 0 }}
                    animate={{ opacity: 1 }}
                    exit={{ opacity: 0 }}
                    transition={{ duration: 1.5 }}
                    layout
                ></motion.div>
            </AnimatePresence>

            <LatestIncomes />
            <LatestExpenses />

            <FeaturedInSection />
        </div >
    );
};

export default Dashboard;
