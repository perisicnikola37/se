import { useLocation, useNavigate } from "react-router-dom";

import { useUser } from "../contexts/UserContext";

export const useAuthenticationMiddleware = () => {
  const navigate = useNavigate();
  const location = useLocation();
  const { isLoggedIn } = useUser();

  return () => {
    // Check if the URL contains "reset-password"
    const isResetPasswordRoute =
      window.location.pathname.includes("reset-password");

    // Exclude "/" path and "reset-password" route from redirection
    if (
      !isLoggedIn() &&
      location.pathname !== "/" &&
      location.pathname !== "/privacy-policy" &&
      !isResetPasswordRoute
    ) {
      navigate("/sign-in");
    }
  };
};
