import { useState } from "react";
import axiosConfig from "../../config/axiosConfig";
import Swal from "sweetalert2";
import { useLocation } from "react-router-dom";
import { ResetPasswordHookInterface } from "../../interfaces/globalInterfaces";

const useResetPassword = (): ResetPasswordHookInterface => {
  const location = useLocation();
  const queryParams = new URLSearchParams(location.search);

  const email = queryParams.get("email") || "";
  const resetToken = queryParams.get("token") || "";

  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [success, setSuccess] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  const showSuccessMessage = () => {
    Swal.fire({
      position: "top-right",
      icon: "success",
      title: "Password reset successful",
      showConfirmButton: false,
      timer: 1500,
    });
  };

  const showErrorMessage = (message: string) => {
    setError(message);
    Swal.fire({
      position: "center",
      icon: "error",
      title: message,
      showConfirmButton: false,
      timer: 1500,
    });
  };

  const resetPassword = async (newPassword: string) => {
    setIsLoading(true);

    try {
      const response = await axiosConfig.post("/api/auth/reset/password", {
        userEmail: email,
        ResetToken: resetToken,
        newPassword,
      });

      if (response.data.message) {
        setSuccess(true);
        showSuccessMessage();
      } else {
        showErrorMessage("Failed to reset password. Please try again.");
      }
    } catch (err) {
      showErrorMessage("Invalid token or token expired.");
      setSuccess(false);
    } finally {
      setIsLoading(false);
    }
  };

  return { isLoading, success, error, resetPassword };
};

export default useResetPassword;
