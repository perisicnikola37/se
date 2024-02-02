import { useEffect } from "react";

const ExpenseGroups = () => {

    useEffect(() => {
        document.title = 'Expense groups | Expense Tracker';
    }, []);

    return (
        <div className="w-full max-w-screen-xl min-h-[48rem] mx-auto p-4 md:py-8">
            expense groups
        </div>
    )
}

export default ExpenseGroups