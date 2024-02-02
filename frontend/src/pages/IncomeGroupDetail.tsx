import { useEffect } from "react";
import { Link, useParams } from "react-router-dom";
import useGetObjectGroupById from "../hooks/GlobalHooks/GetObjectGroupHook";
import { Breadcrumbs, Typography } from "@mui/material";

const IncomeGroupDetail = () => {
    const { id } = useParams();
    const { object, getObjectGroupById } = useGetObjectGroupById(parseInt(id), "income");

    useEffect(() => {
        getObjectGroupById();
    }, []);

    return (
        <div className="w-full max-w-screen-xl min-h-[48rem] mx-auto p-4 md:py-8">
            <Breadcrumbs aria-label="breadcrumb" sx={{ marginBottom: "30px" }}>
                <Link to="/" className="hover:text-[#2563EB] transition-colors duration-300">
                    Dashboard
                </Link>
                <Link to="/incomes/groups" className="hover:text-[#2563EB] transition-colors duration-300">
                    Income groups
                </Link>
                <Typography color="text.primary">Income group details</Typography>
            </Breadcrumbs>

            {object && (
                <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                    {/* Displaying Income Group Information */}
                    <div className="bg-white rounded-md shadow-md p-6">
                        <h2 className="text-2xl font-bold mb-4">Income group details</h2>
                        <p className="text-lg mb-2">Name: {object.name}</p>
                        <p className="text-base">Description: {object.description}</p>
                    </div>

                    {/* Displaying Incomes */}
                    <div className="bg-[#cbffc0] rounded-md shadow-md p-6">
                        <h2 className="text-2xl font-bold mb-4">Incomes</h2>
                        {object.incomes.map((income) => (
                            <div key={income.id} className="mb-4">
                                <p className="text-lg">{income.name}</p>
                                <p className="text-base">Amount: ${income.amount}</p>
                            </div>
                        ))}
                    </div>
                </div>
            )}
        </div>
    );
};

export default IncomeGroupDetail;
