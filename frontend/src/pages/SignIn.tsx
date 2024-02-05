import { useEffect } from "react";
import SignInForm from "../components/SignInForm";
import { useUser } from "../contexts/UserContext";

export default function SignIn() {
    const { isLoggedIn } = useUser();

    useEffect(() => {
        if (isLoggedIn()) {
            window.history.back();
        }
    }, [isLoggedIn]);

    return <SignInForm />;
}
