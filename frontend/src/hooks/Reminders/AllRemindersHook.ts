import { useState } from 'react';
import axiosConfig from '../../config/axiosConfig';


interface Reminder {
    id: number;
    reminderDay: string;
    type: string;
    createdAt: string;
    active: boolean;
}

const useAllReminders = () => {
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [reminders, setReminders] = useState<Reminder[]>([]);
    const [error, setError] = useState<string | null>(null);

    const fetchAllReminders = async () => {
        const result = { isLoading: true, reminders: [] as Reminder[], error: null as string | null };

        try {
            const response = await axiosConfig.get('/api/reminders');
            result.reminders = response.data;
        } catch (err) {
            result.error = 'Error fetching reminders.';
        } finally {
            result.isLoading = false;
        }

        return result;
    };

    const loadReminders = async () => {
        setIsLoading(true);
        const result = await fetchAllReminders();
        setIsLoading(false);

        if (result.reminders.length > 0) {
            setReminders(result.reminders);
            setError(null);
        } else {
            setError(result.error);
        }
    };

    return { isLoading, reminders, error, loadReminders };
};

export default useAllReminders;
