import { useEffect } from "react";
import { Link, useParams } from "react-router-dom";
import useGetObjectGroupById from "../hooks/GlobalHooks/GetObjectGroupHook";
import { Breadcrumbs, Typography } from "@mui/material";
import { motion } from "framer-motion";

const ExpenseGroupDetail = () => {
    const { id } = useParams();
    const { object, getObjectGroupById } = useGetObjectGroupById(id ? parseInt(id) : 0, "expense");

    useEffect(() => {
        if (id) {
            getObjectGroupById();
        }
    }, [id]);

    return (
        <motion.div
            initial={{ opacity: 0, y: -20 }}
            animate={{ opacity: 1, y: 0 }}
            exit={{ opacity: 0, y: 20 }}
            transition={{ duration: 0.5 }}
            className="w-full max-w-screen-xl min-h-[48rem] mx-auto p-4 md:py-8"
        >
            <Breadcrumbs aria-label="breadcrumb" sx={{ marginBottom: "30px" }}>
                <Link to="/" className="hover:text-[#2563EB] transition-colors duration-300">
                    Dashboard
                </Link>
                <Link to="/expenses/groups" className="hover:text-[#2563EB] transition-colors duration-300">
                    Expense groups
                </Link>
                <Typography color="text.primary">Expense group details</Typography>
            </Breadcrumbs>

            {object && (
                <motion.div
                    initial={{ opacity: 0, y: -20 }}
                    animate={{ opacity: 1, y: 0 }}
                    transition={{ duration: 0.5 }}
                    className="grid grid-cols-1 md:grid-cols-2 gap-4"
                >
                    <motion.div
                        initial={{ opacity: 0, y: -20 }}
                        animate={{ opacity: 1, y: 0 }}
                        transition={{ duration: 0.5 }}
                        className="bg-white rounded-md shadow-md p-6"
                    >
                        <h2 className="text-2xl font-bold mb-4">Expense group details</h2>
                        <p className="text-lg mb-2">Name: {object.name}</p>
                        <p className="text-base">Description: {object.description}</p>
                    </motion.div>

                    <motion.div
                        initial={{ opacity: 0, y: -20 }}
                        animate={{ opacity: 1, y: 0 }}
                        transition={{ duration: 0.5 }}
                        className="bg-[#cbffc0] rounded-md shadow-md p-6"
                    >
                        <h2 className="text-2xl font-bold mb-4">Expenses</h2>
                        {object.expenses && object.expenses.length > 0 ? (
                            object.expenses.map((expense) => (
                                <motion.div
                                    key={expense.id}
                                    initial={{ opacity: 0, y: -20 }}
                                    animate={{ opacity: 1, y: 0 }}
                                    transition={{ duration: 0.5 }}
                                    className="mb-4"
                                >
                                    <p className="text-lg">{expense.description}</p>
                                    <p className="text-base">Amount: ${expense.amount}</p>
                                </motion.div>
                            ))
                        ) : (
                            <motion.p
                                initial={{ opacity: 0, y: -20 }}
                                animate={{ opacity: 1, y: 0 }}
                                transition={{ duration: 0.5 }}
                            >
                                No expenses found for this particular group.
                            </motion.p>
                        )}
                    </motion.div>
                </motion.div>
            )}
        </motion.div>
    );
};

export default ExpenseGroupDetail;
