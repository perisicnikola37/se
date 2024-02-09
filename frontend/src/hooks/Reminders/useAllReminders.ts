import { useState } from "react";
import axiosConfig from "../../config/axiosConfig";
import { ReminderInterface } from "../../interfaces/globalInterfaces";

const useAllReminders = () => {
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [reminders, setReminders] = useState<ReminderInterface[]>([]);
  const [error, setError] = useState<string | null>(null);

  const fetchAllReminders = async () => {
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

  const loadReminders = async () => {
    setIsLoading(true);
    const result = await fetchAllReminders();
    setIsLoading(false);

    if (result.reminders.length > 0) {
      setReminders(result.reminders);
      setError(null);
    } else {
      setReminders([]);
      setError(result.error);
    }
  };

  return { isLoading, reminders, error, loadReminders };
};

export default useAllReminders;
