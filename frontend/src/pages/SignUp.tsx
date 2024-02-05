import { useEffect } from "react";
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
        <SignUpForm />
    );
}
