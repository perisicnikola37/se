import { useEffect } from "react";
import { useModal } from "../contexts/GlobalContext";
import useObjects from "../hooks/GlobalHooks/useObjects";
import EnhancedTable from "../components/Tables/ExpensesTable";
import { ExpenseInterface } from "../interfaces/globalInterfaces";
import { Helmet } from "react-helmet";

const Expenses = () => {
  const { objects, loadObjects } = useObjects<ExpenseInterface>();
  const { actionChange, appliedFilters } = useModal();

  useEffect(() => {
    loadObjects(appliedFilters, "expense");
    window.scrollTo({
      top: 0,
    });
  }, [actionChange, appliedFilters]);

  return (
    <div className="w-full max-w-screen-xl min-h-[48rem] mx-auto p-4 md:py-8">
      <Helmet>
        <title>Expenses | Expense Tracker</title>
      </Helmet>
      <EnhancedTable expenses={objects} rowsPerPage={5} />
    </div>
  );
};

export default Expenses;
