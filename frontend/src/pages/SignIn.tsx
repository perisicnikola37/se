import { useEffect } from "react";

import { Helmet } from "react-helmet";

import SignInForm from "../components/SignInForm";
import { useUser } from "../contexts/UserContext";

export default function SignIn() {
  const { isLoggedIn } = useUser();

  useEffect(() => {
    if (isLoggedIn()) {
      window.history.back();
    }
  }, [isLoggedIn]);

  return (
    <>
      <Helmet>
        <title>Sign In | Expense Tracker</title>
      </Helmet>
      <SignInForm />
    </>
  );
}
