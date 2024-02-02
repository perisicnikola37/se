// ForgotPassword.tsx

import React, { useState } from 'react';
import { TextField, Button, Typography, Container, Grid, Link, Box, CssBaseline } from "@mui/material";
import useForgotPassword from '../hooks/Authentication/ForgotPasswordHook';

const ForgotPassword: React.FC = () => {
    const { forgotPassword } = useForgotPassword();
    const [email, setEmail] = useState<string>('');

    const handleForgotPassword = () => {
        forgotPassword(email);
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
                    Forgot password?
                </Typography>

                <Box
                    component="form"
                    onSubmit={(e) => {
                        e.preventDefault();
                        handleForgotPassword();
                    }}
                    noValidate
                    sx={{ mt: 1 }}
                >
                    <TextField
                        margin="normal"
                        required
                        fullWidth
                        id="email"
                        label="Email address"
                        name="email"
                        autoComplete="email"
                        autoFocus
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                    />
                    <Button
                        type="submit"
                        fullWidth
                        variant="contained"
                        sx={{ mt: 3, mb: 2 }}
                    >
                        Submit
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

export default ForgotPassword;
