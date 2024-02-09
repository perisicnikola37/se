import { useState } from "react";

import axiosConfig from "../../config/axiosConfig";
import { CreateReminderData } from "../../interfaces/globalInterfaces";

const useCreateReminder = () => {
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  const createReminder = async (reminderData: CreateReminderData) => {
    setIsLoading(true);

    try {
      await axiosConfig.post("/api/reminders", reminderData);
    } catch (err) {
      setError("Error creating a new reminder.");
    } finally {
      setIsLoading(false);
    }
  };

  return { isLoading, error, createReminder };
};

export default useCreateReminder;
