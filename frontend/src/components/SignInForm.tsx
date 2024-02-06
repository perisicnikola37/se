import {
    Button,
    CssBaseline,
    TextField,
    FormControlLabel,
    Checkbox,
    Link,
    Grid,
    Box,
    Typography,
    Container,
} from "@mui/material";
import { createTheme, ThemeProvider } from "@mui/material/styles";
import { Alert } from "@mui/material";
import useLogin from "../hooks/Authentication/LogInHook";
import { validateLoginForm } from "../utils/utils";
import { useUser } from "../contexts/UserContext";
import { useState } from "react";

const defaultTheme = createTheme();

export default function SignInForm() {
    const { setUser } = useUser()
    const { login, fieldErrorMessages, isLoading, response } = useLogin();
    const [loginData, setLoginData] = useState({
        email: "",
        password: "",
    });

    const [formError, setFormError] = useState<string | null>(null);

    const handleChange = (event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        setLoginData({
            ...loginData,
            [event.target.name]: event.target.value,
        });
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
                        onSubmit={async (event) => {
                            event.preventDefault();
                            await validateLoginForm(loginData, setFormError, login, setUser, response);
                        }}
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
                                <Link href="forgot/password" variant="body2">
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