import { useEffect } from "react";
import EnhancedTable from "../components/Table";
import { useModal } from "../contexts/GlobalContext";
import useObjects from "../hooks/GlobalHooks/AllObjectsHook";

const Incomes = () => {
    const { objects, loadObjects } = useObjects();
    const { actionChange, appliedFilters } = useModal();

    useEffect(() => {
        document.title = 'Incomes | Expense Tracker';
    }, []);

    useEffect(() => {
        loadObjects(appliedFilters, "income");
    }, [actionChange, appliedFilters]);

    return (
        <div className="w-full max-w-screen-xl min-h-[48rem] mx-auto p-4 md:py-8">
            <EnhancedTable incomes={objects} rowsPerPage={5} />
        </div>
    );
}

export default Incomes;
