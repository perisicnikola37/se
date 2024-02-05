import DeleteModal from "../components/Modals/DeleteModal";
import ReminderCreateModal from "../components/Modals/ReminderCreateModal";

const Reminders = () => {
    const reminders = [
        {
            id: 1,
            reminderDayEnum: 0,
            reminderDay: "Monday",
            type: "Meeting",
            createdAt: "2024-01-24T09:25:37.756608",
            active: true,
        },
        {
            id: 2,
            reminderDayEnum: 1,
            reminderDay: "Tuesday",
            type: "Deadline",
            createdAt: "2024-01-25T10:30:45.123456",
            active: false,
        },
        {
            id: 3,
            reminderDayEnum: 2,
            reminderDay: "Wednesday",
            type: "Event",
            createdAt: "2024-01-26T12:15:20.987654",
            active: true,
        },
    ];

    return (
        <div className="w-full max-w-screen-xl min-h-[48rem] mx-auto p-4 md:py-8">
            <div className="flex justify-between items-center mb-4">
                <h1 className="text-3xl font-semibold">Reminders</h1>
                <ReminderCreateModal />
            </div>
            {reminders.length === 0 ? (
                <p>No reminders set.</p>
            ) : (
                <ul className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4">
                    {reminders.map((reminder) => (
                        <li
                            key={reminder.id}
                            className={`${!reminder.active ? "bg-[#FEE9EC]" : "bg-[#d1fff7]"
                                } p-4 rounded-md shadow-md flex flex-col justify-between relative`}
                        >
                            <div className="mb-3">
                                <h2 className="text-xl font-semibold mb-2">{reminder.type}</h2>
                                <p className="text-gray-600 mb-2">
                                    Day: {reminder.reminderDay}
                                </p>
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
    );
};

export default Reminders;
