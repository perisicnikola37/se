import { useEffect } from "react";
import SignInForm from "../components/SignInForm";
import { useUser } from "../contexts/UserContext";
import { Helmet } from "react-helmet";

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
