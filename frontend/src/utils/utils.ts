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