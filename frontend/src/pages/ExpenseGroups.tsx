import { useEffect } from "react";
import { useModal } from "../contexts/GlobalContext";
import useObjectGroups from "../hooks/GlobalHooks/GetObjectsHook";
import EnhancedTable from "../components/Tables/ExpenseGroupsTable";
import { Helmet } from "react-helmet";

const ExpenseGroups = () => {
    const { objectGroups, fetchObjectGroups } = useObjectGroups("expense");
    const { actionChange } = useModal();

    useEffect(() => {
        fetchObjectGroups();
    }, [actionChange]);

    return (
        <div className="w-full max-w-screen-xl min-h-[48rem] mx-auto p-4 md:py-8">
            <Helmet>
                <title>Expense groups | Expense Tracker</title>
            </Helmet>
            <EnhancedTable expenseGroups={objectGroups} />
        </div>
    );
}

export default ExpenseGroups;
