import { Dispatch, SetStateAction } from "react";
import { User } from "../interfaces/contextsInterfaces";
import { LoginResponse, LoginResult } from "../interfaces/globalInterfaces";

export const isEmailValid = (email: string) => {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailRegex.test(email);
};

export const formatDate = (isoDate: string) => {
    const date = new Date(isoDate);
    const day = date.getDate().toString().padStart(2, '0');
    const month = (date.getMonth() + 1).toString().padStart(2, '0');
    const year = date.getFullYear();
    return `${day}-${month}-${year}`;
};

export const truncateString = (str: string, maxLength: number) => {
    if (str.length > maxLength) {
        return str.substring(0, maxLength - 3) + '...';
    }
    return str;
}

export const validatePassword = (newPassword: string, confirmNewPassword: string) => {
    const errors: { message: string }[] = [];

    if (newPassword.length < 8) {
        errors.push({
            message: "Your password must be at least 8 characters long.",
        });
    }

    if (newPassword.length > 36) {
        errors.push({
            message: "Your password must not exceed 36 characters.",
        });
    }

    if (!/[A-Z]+/.test(newPassword)) {
        errors.push({
            message: "Your password must contain at least one uppercase letter.",
        });
    }

    if (!/[a-z]+/.test(newPassword)) {
        errors.push({
            message: "Your password must contain at least one lowercase letter.",
        });
    }

    if (!/[0-9]+/.test(newPassword)) {
        errors.push({
            message: "Your password must contain at least one number.",
        });
    }

    if (!/[\\!\\?\\*\\.]+/.test(newPassword)) {
        errors.push({
            message:
                "Your password must contain at least one of the following characters: !?*.",
        });
    }

    if (newPassword !== confirmNewPassword) {
        errors.push({ message: "Passwords do not match" });
    }

    return errors;
};

export const validateEmail = (email: string) => {
    const regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return regex.test(email);
};

export const validateLoginForm = async (
    loginData: { email: string; password: string },
    setFormError: Dispatch<SetStateAction<string | null>>,
    login: (data: { email: string; password: string }) => Promise<LoginResult>,
    setUser: Dispatch<SetStateAction<User>>,
    response: LoginResponse | null
): Promise<void> => {
    if (!loginData.email || !loginData.password) {
        setFormError("Please fill in both email and password.");
        return;
    }

    if (!isEmailValid(loginData.email)) {
        setFormError("Please enter a valid email address.");
        return;
    }

    if (loginData.password.length < 8) {
        setFormError("Password must have a minimum of 8 characters.");
        return;
    }

    setFormError(null);

    await login(loginData);

    setUser((prev) => {
        if (response && response.user) {
            return {
                ...prev,
                id: response.user.id ?? prev.id,
                accountType: response.user.accountType ?? prev.accountType,
                email: response.user.email ?? prev.email,
                formattedCreatedAt: response.user.formattedCreatedAt ?? prev.formattedCreatedAt,
                token: response.user.token ?? prev.token,
                username: response.user.username ?? prev.username,
            };
        } else {
            return prev;
        }
    });
};