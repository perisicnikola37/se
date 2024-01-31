import { useEffect } from "react";
import EnhancedTable from "../components/Table";
import useIncomes from "../hooks/Incomes/AllIncomesHook";
import { useModal } from "../contexts/GlobalContext";

const Incomes = () => {
    const { incomes, loadIncomes } = useIncomes();
    const { actionChange } = useModal();

    useEffect(() => {
        loadIncomes();
    }, [actionChange]);

    return (
        <div className="w-full max-w-screen-xl min-h-[48rem] mx-auto p-4 md:py-8">
            <EnhancedTable incomes={incomes} />
        </div>
    );
}

export default Incomes;
