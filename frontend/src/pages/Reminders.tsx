import { useEffect, useState } from "react";
import DeleteModal from "../components/Modals/DeleteModal";
import useAllReminders from "../hooks/Reminders/AllRemindersHook";
import { Skeleton } from "@mui/material"; // Import Skeleton component
import ReminderCreateModal from "../components/Modals/ReminderCreateModal";

const Reminders = () => {
    const { loadReminders, reminders } = useAllReminders();
    const [isLoading, setIsLoading] = useState(true);

    useEffect(() => {
        document.title = "Reminders | Expense Tracker";
        loadReminders();
        setTimeout(() => {
            setIsLoading(false);
        }, 700);
    }, []);

    const skeletonCount = reminders.length > 0 ? reminders.length : 0;

    return (
        <div className="w-full max-w-screen-xl min-h-[48rem] mx-auto p-4 md:py-8">
            <div className="flex justify-between items-center mb-4">
                <h1 className="text-3xl font-semibold">Reminders</h1>
                <ReminderCreateModal />
            </div>
            {isLoading ? (
                <ul className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4">
                    {[...Array(skeletonCount)].map((_, index) => (
                        <li key={index} className="bg-white p-6 rounded-md shadow-md">
                            <Skeleton variant="rectangular" width="100%" height={35} />
                            <Skeleton variant="text" width="80%" height={20} />
                            <Skeleton variant="text" width="60%" height={20} />
                            <Skeleton variant="text" width="90%" height={55} />
                        </li>
                    ))}
                </ul>
            ) : reminders.length === 0 ? (
                <p>No reminders set.</p>
            ) : (
                <div>
                    {reminders.length > 0 && (
                        <ul className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4">
                            {reminders.map((reminder) => (
                                <li
                                    key={reminder.id}
                                    className={`${!reminder.active ? "bg-[#FEE9EC]" : "bg-[#d1fff7]"
                                        } p-4 rounded-md shadow-md flex flex-col justify-between relative`}
                                >
                                    <div className="mb-3">
                                        <h2 className="text-xl font-semibold mb-2">{reminder.type}</h2>
                                        <p className="text-gray-600 mb-2">Day: {reminder.reminderDay}</p>
                                        <p className="text-gray-600">
                                            Created at: {new Date(reminder.createdAt).toLocaleString()}
                                        </p>
                                    </div>
                                    <DeleteModal id={reminder.id} objectType="reminder" />
                                </li>
                            ))}
                        </ul>
                    )}
                </div>
            )}
        </div>
    );
};

export default Reminders;
