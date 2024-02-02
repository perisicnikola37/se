import { useEffect } from "react";
import { Link, useParams } from "react-router-dom";
import useGetObjectGroupById from "../hooks/GlobalHooks/GetObjectGroupHook";
import { Breadcrumbs, Typography } from "@mui/material";

const ExpenseGroupDetail = () => {
    const { id } = useParams();
    const { object, getObjectGroupById } = useGetObjectGroupById(parseInt(id), "expense");

    useEffect(() => {
        getObjectGroupById();
    }, []);

    return (
        <div className="w-full max-w-screen-xl min-h-[48rem] mx-auto p-4 md:py-8">
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
                <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                    <div className="bg-white rounded-md shadow-md p-6">
                        <h2 className="text-2xl font-bold mb-4">Expense group details</h2>
                        <p className="text-lg mb-2">Name: {object.name}</p>
                        <p className="text-base">Description: {object.description}</p>
                    </div>

                    <div className="bg-[#cbffc0] rounded-md shadow-md p-6">
                        <h2 className="text-2xl font-bold mb-4">Expenses</h2>
                        {object.expenses.map((expense) => (
                            <div key={expense.id} className="mb-4">
                                <p className="text-lg">{expense.name}</p>
                                <p className="text-base">Amount: ${expense.amount}</p>
                            </div>
                        ))}
                    </div>
                </div>
            )}
        </div>
    );
};

export default ExpenseGroupDetail;
