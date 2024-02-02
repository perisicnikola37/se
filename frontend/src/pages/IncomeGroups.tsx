import { useEffect } from "react";
import { useModal } from "../contexts/GlobalContext";
import useObjectGroups from "../hooks/GlobalHooks/GetObjectsHook";
import EnhancedTable from "../components/Tables/IncomeGroupsTable";

const IncomeGroups = () => {
    const { objectGroups, fetchObjectGroups } = useObjectGroups("income");
    const { actionChange, appliedFilters } = useModal();

    useEffect(() => {
        document.title = 'Income groups | Expense Tracker';
    }, []);

    useEffect(() => {
        fetchObjectGroups();
    }, [actionChange, appliedFilters]);

    return (
        <div className="w-full max-w-screen-xl min-h-[48rem] mx-auto p-4 md:py-8">
            <EnhancedTable incomeGroups={objectGroups} />
        </div>
    );
}

export default IncomeGroups;
