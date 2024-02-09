import { useState } from "react";
import { ReminderInterface } from "../../interfaces/globalInterfaces";
import { fetchAllReminders } from "../../services/allRemindersService";

const useAllReminders = () => {
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [reminders, setReminders] = useState<ReminderInterface[]>([]);
  const [error, setError] = useState<string | null>(null);

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
