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
import useForgotPassword from "../hooks/Authentication/ForgotPasswordHook";

const ForgotPassword = () => {
  const { forgotPassword, isLoading } = useForgotPassword();
  const [email, setEmail] = useState<string>("");
  const [errorMessage, setErrorMessage] = useState<string>("");

  const handleForgotPassword = async () => {
    if (email === "") {
      setErrorMessage("Email field is required");
      return;
    } else if (email.length < 8) {
      setErrorMessage("Email must be at least 8 characters long");
      return;
    } else {
      setErrorMessage("");
    }

    try {
      await forgotPassword(email);
    } catch (error) {
      console.error(error);
      setErrorMessage("An unexpected error occurred. Please try again.");
    }
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
          {errorMessage && (
            <Alert sx={{ mt: 1, mb: 1 }} severity="error">
              {errorMessage}
            </Alert>
          )}
          <TextField
            required
            margin="normal"
            fullWidth
            placeholder="john@gmail.com"
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
            {isLoading ? "Loading..." : "Send reset link"}
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
