import { useState } from "react";
import axiosConfig from "../../config/axiosConfig";
import Swal from "sweetalert2";
import { ForgotPasswordHookInterface } from "../../interfaces/globalInterfaces";

const useForgotPassword = (): ForgotPasswordHookInterface => {
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [success, setSuccess] = useState<boolean>(false);
    const [error, setError] = useState<string | null>(null);

    const forgotPassword = async (email: string) => {
        setIsLoading(true);

        try {
            const response = await axiosConfig.post("/api/auth/forgot/password", {
                userEmail: email,
            });
            if (response.data.message) {
                setSuccess(true);
                setError(null);
                Swal.fire({
                    position: "top-right",
                    icon: "success",
                    title: "Reset password link sent to your email",
                    showConfirmButton: false,
                    timer: 1500,
                });
            } else {
                setError("Failed to send request. Please try again.");
                setSuccess(false);
            }
        } catch (err) {
            setError("Error occurred. Please try again.");
            setSuccess(false);
        } finally {
            setIsLoading(false);
        }
    };

    return { isLoading, success, error, forgotPassword };
};

export default useForgotPassword;
