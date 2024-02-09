import { useEffect } from "react";

import { motion } from "framer-motion";
import { Link, useParams } from "react-router-dom";

import { Breadcrumbs, Typography } from "@mui/material";

import useGetObjectGroupById from "../hooks/GlobalHooks/useGetObjectGroupById";

const IncomeGroupDetail = () => {
  const { id } = useParams();
  const { object, getObjectGroupById } = useGetObjectGroupById(
    id ? parseInt(id) : 0,
    "income",
  );

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
        <Link
          to="/"
          className="hover:text-[#2563EB] transition-colors duration-300"
        >
          Dashboard
        </Link>
        <Link
          to="/incomes/groups"
          className="hover:text-[#2563EB] transition-colors duration-300"
        >
          Income groups
        </Link>
        <Typography color="text.primary">Income group details</Typography>
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
            <h2 className="text-2xl font-bold mb-4">Income group details</h2>
            <p className="text-lg mb-2">Name: {object.name}</p>
            <p className="text-base">Description: {object.description}</p>
          </motion.div>

          <motion.div
            initial={{ opacity: 0, y: -20 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ duration: 0.5 }}
            className="bg-[#cbffc0] rounded-md shadow-md p-6"
          >
            <h2 className="text-2xl font-bold mb-4">Incomes</h2>
            {object.incomes && object.incomes.length > 0 ? (
              object.incomes.map((income) => (
                <motion.div
                  key={income.id}
                  initial={{ opacity: 0, y: -20 }}
                  animate={{ opacity: 1, y: 0 }}
                  transition={{ duration: 0.5 }}
                  className="mb-4"
                >
                  <p className="text-lg">Name: {income.description}</p>
                  <p className="text-base">Amount: ${income.amount}</p>
                </motion.div>
              ))
            ) : (
              <motion.p
                initial={{ opacity: 0, y: -20 }}
                animate={{ opacity: 1, y: 0 }}
                transition={{ duration: 0.5 }}
              >
                No incomes found for this particular group.
              </motion.p>
            )}
          </motion.div>
        </motion.div>
      )}
    </motion.div>
  );
};

export default IncomeGroupDetail;
