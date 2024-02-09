import { useState } from "react";

import { useNavigate } from "react-router-dom";

import axiosConfig from "../../config/axiosConfig";
import { RegistrationData, RegistrationResponse } from "../../interfaces/globalInterfaces";

interface ErrorResponse {
  response?: {
    status?: number;
    data?: {
      errorMessage: string;
    }[];
  };
}

const useRegistration = () => {
  const navigate = useNavigate();
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [response, setResponse] = useState<RegistrationResponse | null>(null);
  const [errorMessage, setErrorMessage] = useState<string | null>(null);
  const [fieldErrorMessages, setFieldErrorMessages] = useState<string[]>([]);

  const register = async (registrationData: RegistrationData) => {
    setIsLoading(true);
    try {
      const { data, status } = await axiosConfig.post(
        "/api/auth/register",
        registrationData,
      );

      if (status === 201 || status === 200) {
        setFieldErrorMessages([]);

        localStorage.setItem("token", data.token);
        localStorage.setItem("id", data.id.toString());
        localStorage.setItem("username", data.username);
        localStorage.setItem("email", data.email);
        localStorage.setItem("accountType", data.accountType);
        localStorage.setItem("formattedCreatedAt", data.formattedCreatedAt);
        navigate("/");
      }

      setResponse(data);
    } catch (err) {
      const errorResponse = err as ErrorResponse;

      if (errorResponse.response && errorResponse.response.status === 409) {
        setFieldErrorMessages(["Email is already registered"]);
      } else if (errorResponse.response && errorResponse.response.data) {
        const fieldErrors = errorResponse.response.data.map(
          (error) => error.errorMessage,
        );
        setFieldErrorMessages(fieldErrors);
        setErrorMessage("An error occurred during registration.");
      } else if (err instanceof Error) {
        setErrorMessage("An error occurred during registration.");
      } else {
        setErrorMessage("An error occurred during registration.");
      }
    } finally {
      setIsLoading(false);
    }
  };

  return { register, isLoading, response, errorMessage, fieldErrorMessages };
};

export default useRegistration;
