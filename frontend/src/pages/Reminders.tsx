import { useEffect } from "react";

const Reminders = () => {

    useEffect(() => {
        document.title = 'Reminders | Expense Tracker';
    }, []);

    return (
        <div className="w-full max-w-screen-xl min-h-[48rem] mx-auto p-4 md:py-8">
            Reminders
        </div>
    )
}

export default Reminders