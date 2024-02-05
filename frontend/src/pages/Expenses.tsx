import { useEffect } from "react";
import { useModal } from "../contexts/GlobalContext";
import useObjects from "../hooks/GlobalHooks/AllObjectsHook";
import EnhancedTable from "../components/Tables/ExpensesTable";
import { ExpenseInterface } from "../interfaces/globalInterfaces";

const Expenses = () => {
    const { objects, loadObjects } = useObjects<ExpenseInterface>();
    const { actionChange, appliedFilters } = useModal();

    useEffect(() => {
        document.title = 'Expenses | Expense Tracker';
    }, []);

    useEffect(() => {
        loadObjects(appliedFilters, "expense");
    }, [actionChange, appliedFilters]);

    return (
        <div className="w-full max-w-screen-xl min-h-[48rem] mx-auto p-4 md:py-8">
            <EnhancedTable expenses={objects} rowsPerPage={5} />
        </div>
    );
}

export default Expenses;
