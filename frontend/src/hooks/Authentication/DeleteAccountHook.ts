import { useState } from "react";
import Swal from "sweetalert2";
import { useNavigate } from "react-router-dom";
import axiosConfig from "../../config/axiosConfig";
import { User } from "../../interfaces/globalInterfaces";
import { handleLogout } from "../../utils/utils";

const useDeleteAccount = () => {
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [user, setUser] = useState<User | null>(null);
  const [error, setError] = useState<string | null>(null);
  const navigate = useNavigate();

  const deleteAccountOperation = async () => {
    const result = {
      isLoading: true,
      user: null as User | null,
      error: null as string | null,
    };

    try {
      const response = await axiosConfig.delete("/api/users/delete/account");
      result.user = response.data;
    } catch (err) {
      result.error = "Error deleting account.";
    } finally {
      result.isLoading = false;
    }

    return result;
  };

  const deleteAccount = async () => {
    const shouldDelete = await showConfirmationModal();

    if (shouldDelete) {
      setIsLoading(true);
      const result = await deleteAccountOperation();
      setIsLoading(false);

      if (result) {
        setUser(result.user);
        setError(null);

        Swal.fire({
          icon: "success",
          title: "Account Deleted",
          text: "Your account has been successfully deleted.",
        }).then(() => {
          handleLogout();
          navigate("/sign-in");
        });
      } else {
        setError(result);
      }
    }
  };

  const showConfirmationModal = async () => {
    const confirmationResult = await Swal.fire({
      icon: "warning",
      title: "Are you sure?",
      text: "You won't be able to revert this!",
      showCancelButton: true,
      confirmButtonColor: "#3085d6",
      cancelButtonColor: "#d33",
      confirmButtonText: "Yes, delete it!",
    });

    return confirmationResult.isConfirmed;
  };

  return { isLoading, user, error, deleteAccount };
};

export default useDeleteAccount;
