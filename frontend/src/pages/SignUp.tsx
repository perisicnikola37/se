import { useEffect } from "react";

import { Helmet } from "react-helmet";

import SignUpForm from "../components/SignUpForm";
import { useUser } from "../contexts/UserContext";

export default function SignUp() {
  const { isLoggedIn } = useUser();

  useEffect(() => {
    if (isLoggedIn()) {
      window.history.back();
    }
  }, [isLoggedIn]);

  return (
    <>
      <Helmet>
        <title>Sign Up | Expense Tracker</title>
      </Helmet>
      <SignUpForm />
    </>
  );
}
