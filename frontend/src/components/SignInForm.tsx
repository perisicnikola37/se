import React, { useState } from "react";
import Button from "@mui/material/Button";
import CssBaseline from "@mui/material/CssBaseline";
import TextField from "@mui/material/TextField";
import FormControlLabel from "@mui/material/FormControlLabel";
import Checkbox from "@mui/material/Checkbox";
import Link from "@mui/material/Link";
import Grid from "@mui/material/Grid";
import Box from "@mui/material/Box";
import Typography from "@mui/material/Typography";
import Container from "@mui/material/Container";
import { createTheme, ThemeProvider } from "@mui/material/styles";
import { Alert } from "@mui/material";
import useLogin from "../hooks/Authentication/LogInHook";
import { isEmailValid } from "../utils/utils";

const defaultTheme = createTheme();

export default function SignInForm() {
    const [loginData, setLoginData] = useState({
        email: "",
        password: "",
    });

    const [formError, setFormError] = useState<string | null>(null);

    const { login, fieldErrorMessages, isLoading } = useLogin();

    const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();

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
    };

    const handleChange = (event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        const { name, value } = event.target;
        setLoginData((prevData) => ({
            ...prevData,
            [name]: value,
        }));
    };

    return (
        <ThemeProvider theme={defaultTheme}>
            <Container component="main" maxWidth="xs" sx={{ height: "100vh", display: "flex", flexDirection: "column", justifyContent: "center", alignItems: "center" }}>
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
                        Sign In
                    </Typography>
                    <Box
                        component="form"
                        onSubmit={handleSubmit}
                        noValidate
                        sx={{ mt: 1 }}
                    >
                        {formError && (
                            <Alert sx={{ mb: 2 }} severity="error">
                                {formError}
                            </Alert>
                        )}
                        {fieldErrorMessages && (
                            <Box sx={{ mb: 2, maxHeight: "130px", overflowY: "auto" }}>
                                {fieldErrorMessages.map((errorMessage, index) => (
                                    <Alert key={index} severity="error">
                                        {errorMessage}
                                    </Alert>
                                ))}
                            </Box>
                        )}
                        <TextField
                            margin="normal"
                            required
                            fullWidth
                            id="email"
                            label="Email Address"
                            name="email"
                            autoComplete="email"
                            autoFocus
                            onChange={handleChange}
                        />
                        <TextField
                            margin="normal"
                            required
                            fullWidth
                            name="password"
                            label="Password"
                            type="password"
                            id="password"
                            autoComplete="current-password"
                            onChange={handleChange}
                        />
                        <FormControlLabel
                            control={
                                <Checkbox value="remember" color="primary" />
                            }
                            label="Remember me"
                        />
                        <Button
                            type="submit"
                            fullWidth
                            variant="contained"
                            sx={{ mt: 3, mb: 2 }}
                            disabled={isLoading}
                        >
                            {isLoading ? "LOADING..." : "Sign In"}
                        </Button>
                        <Grid container>
                            <Grid item xs>
                                <Link href="#" variant="body2">
                                    Forgot password?
                                </Link>
                            </Grid>
                            <Grid item>
                                <Link href="/sign-up" variant="body2">
                                    {"Don't have an account? Sign Up"}
                                </Link>
                            </Grid>
                        </Grid>
                    </Box>
                </Box>
            </Container>
        </ThemeProvider>
    );
}