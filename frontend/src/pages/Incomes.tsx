import { useEffect } from "react";
import EnhancedTable from "../components/Table";
import useIncomes from "../hooks/Incomes/AllIncomesHook";

const Incomes = () => {
    const { incomes, loadIncomes } = useIncomes();

    useEffect(() => {
        loadIncomes();
    }, []);

    return (
        <div className="w-full max-w-screen-xl min-h-[48rem] mx-auto p-4 md:py-8">
            <EnhancedTable incomes={incomes} />
        </div>
    );
}

export default Incomes;
