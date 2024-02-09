import { useState } from "react";

import axiosConfig from "../../config/axiosConfig";
import { ExportToEmailHookResult } from "../../interfaces/globalInterfaces";

const useExportToEmail = (): ExportToEmailHookResult => {
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [success, setSuccess] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  const exportToEmail = async (email: string): Promise<void> => {
    setIsLoading(true);

    try {
      const response = await axiosConfig.post("/api/users/email/send", {
        ToEmail: email,
      });
      if (response.data && response.data.success) {
        setSuccess(true);
        setError(null);
      } else {
        setSuccess(false);
        setError("Failed to export to email. Please try again.");
      }
    } catch (err) {
      setSuccess(false);
      setError("Error exporting to email. Please try again.");
    } finally {
      setIsLoading(false);
    }
  };

  return { isLoading, success, error, exportToEmail };
};

export default useExportToEmail;
