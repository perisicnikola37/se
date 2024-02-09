import { useState } from "react";

import { Helmet } from "react-helmet";

import VisibilityIcon from "@mui/icons-material/Visibility";
import VisibilityOffIcon from "@mui/icons-material/VisibilityOff";
import { Alert, Box, Button, Container, CssBaseline, Grid, IconButton, InputAdornment, Link, TextField, Typography } from "@mui/material";

import { validatePassword } from "../utils/utils";
import useResetPassword from "../hooks/Authentication/useResetPassword";

const ResetPassword = () => {
  const { resetPassword, isLoading } = useResetPassword();
  const [newPassword, setNewPassword] = useState<string>("");
  const [confirmNewPassword, setConfirmNewPassword] = useState<string>("");
  const [errors, setErrors] = useState<{ message: string }[]>([]);
  const [showPassword, setShowPassword] = useState(false);

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

  const togglePasswordVisibility = () => {
    setShowPassword((prev) => !prev);
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
      <Helmet>
        <title>Reset password | Expense Tracker</title>
      </Helmet>
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
            name="password"
            label="New password"
            type={showPassword ? "text" : "password"}
            id="password"
            autoComplete="current-password"
            value={newPassword}
            onChange={(e) => setNewPassword(e.target.value)}
            InputProps={{
              endAdornment: (
                <InputAdornment position="end">
                  <IconButton
                    aria-label="toggle password visibility"
                    onClick={togglePasswordVisibility}
                  >
                    {showPassword ? <VisibilityIcon /> : <VisibilityOffIcon />}
                  </IconButton>
                </InputAdornment>
              ),
            }}
          />
          <TextField
            margin="normal"
            required
            fullWidth
            name="confirmNewPassword"
            label="Confirm new password"
            type={showPassword ? "text" : "password"}
            id="confirmNewPassword"
            autoComplete="confirmNewPassword"
            onChange={(e) => setConfirmNewPassword(e.target.value)}
            InputProps={{
              endAdornment: (
                <InputAdornment position="end">
                  <IconButton
                    aria-label="toggle password visibility"
                    onClick={togglePasswordVisibility}
                  >
                    {showPassword ? <VisibilityIcon /> : <VisibilityOffIcon />}
                  </IconButton>
                </InputAdornment>
              ),
            }}
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
