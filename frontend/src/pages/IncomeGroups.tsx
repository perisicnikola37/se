import { useEffect } from "react";

const IncomeGroups = () => {

    useEffect(() => {
        document.title = 'Income groups | Expense Tracker';
    }, []);

    return (
        <div className="w-full max-w-screen-xl min-h-[48rem] mx-auto p-4 md:py-8">
            income groups
        </div>
    )
}

export default IncomeGroups