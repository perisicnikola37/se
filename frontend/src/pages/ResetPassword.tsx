import { useState } from "react";
import {
    TextField,
    Button,
    Typography,
    Container,
    Grid,
    Link,
    Box,
    CssBaseline,
    Alert,
} from "@mui/material";
import useResetPassword from "../hooks/Authentication/ResetPasswordHook";
import { validatePassword } from "../utils/utils";

const ResetPassword = () => {
    const { resetPassword, isLoading } = useResetPassword();
    const [newPassword, setNewPassword] = useState<string>("");
    const [confirmNewPassword, setConfirmNewPassword] = useState<string>("");
    const [errors, setErrors] = useState<{ message: string }[]>([]);

    const handleResetPassword = async () => {
        const newErrors = validatePassword(newPassword, confirmNewPassword);
        setErrors(newErrors);
        if (newErrors.length > 0) {
            return;
        }

        try {
            await resetPassword(newPassword);
        } catch (error) {
            console.error(error);
        }

        setErrors([]);
    };

    return (
        <Container
            component="main"
            maxWidth="xs"
            sx={{
                height: "100vh",
                display: "flex",
                flexDirection: "column",
                justifyContent: "center",
                alignItems: "center",
            }}
        >
            <CssBaseline />
            <Box
                sx={{
                    marginTop: 8,
                    display: "flex",
                    flexDirection: "column",
                    alignItems: "center",
                }}
            >
                <Typography component="h1" variant="h5">
                    Reset your password
                </Typography>

                <Box
                    component="form"
                    onSubmit={(e) => {
                        e.preventDefault();
                        handleResetPassword();
                    }}
                    noValidate
                    sx={{ mt: 1 }}
                >
                    {errors.length > 0 && (
                        <Box sx={{ mb: 2, maxHeight: "130px", overflowY: "auto" }}>
                            {errors.map((error, index) => (
                                <Alert key={index} severity="error">
                                    {error.message}
                                </Alert>
                            ))}
                        </Box>
                    )}
                    <TextField
                        margin="normal"
                        required
                        fullWidth
                        id="newPassword"
                        label="New password"
                        name="newPassword"
                        autoComplete="newPassword"
                        autoFocus
                        value={newPassword}
                        onChange={(e) => setNewPassword(e.target.value)}
                    />
                    <TextField
                        margin="normal"
                        required
                        fullWidth
                        id="confirmNewPassword"
                        label="Confirm new password"
                        name="confirmNewPassword"
                        autoComplete="confirmNewPassword"
                        autoFocus
                        value={confirmNewPassword}
                        onChange={(e) => setConfirmNewPassword(e.target.value)}
                    />
                    <Button
                        type="submit"
                        fullWidth
                        variant="contained"
                        sx={{ mt: 3, mb: 2 }}
                    >
                        {isLoading ? "Loading..." : "Reset password"}
                    </Button>

                    <Grid container justifyContent="center">
                        <Grid item>
                            <Link href="/sign-in" variant="body2">
                                {"Already have an account? Sign In"}
                            </Link>
                        </Grid>
                    </Grid>
                </Box>
            </Box>
        </Container>
    );
};

export default ResetPassword;
