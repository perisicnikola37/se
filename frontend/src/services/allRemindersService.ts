import axiosConfig from "../config/axiosConfig";
import { ReminderInterface } from "../interfaces/globalInterfaces";

export const fetchAllReminders = async () => {
    const result = {
        isLoading: true,
        reminders: [] as ReminderInterface[],
        error: null as string | null,
    };

    try {
        const response = await axiosConfig.get("/api/reminders");
        result.reminders = response.data;
    } catch (err) {
        result.error = "Error fetching reminders.";
    } finally {
        result.isLoading = false;
    }

    return result;
};