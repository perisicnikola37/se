import React, { useState } from "react";
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
  createTheme,
  ThemeProvider,
  Alert,
  FormControl,
  InputLabel,
  MenuItem,
  Select,
  SelectChangeEvent,
  InputAdornment,
  IconButton,
} from "@mui/material";
import useRegistration from "../hooks/Authentication/useRegistration";
import VisibilityIcon from "@mui/icons-material/Visibility";
import VisibilityOffIcon from "@mui/icons-material/VisibilityOff";

const defaultTheme = createTheme();

export default function SignUpForm() {
  const [registrationData, setRegistrationData] = useState({
    username: "",
    email: "",
    accountType: "Regular",
    password: "",
    passwordConfirmation: "",
  });

  const [passwordMatchError, setPasswordMatchError] = useState<string | null>(
    null,
  );
  const [showPassword, setShowPassword] = useState(false);

  const { register, fieldErrorMessages, isLoading } = useRegistration();

  const handleChange = (
    event:
      | React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
      | SelectChangeEvent<string>,
  ) => {
    if ("target" in event) {
      const { name, value } = event.target as
        | HTMLInputElement
        | HTMLTextAreaElement;
      setRegistrationData((prevData) => ({
        ...prevData,
        [name]: value,
      }));
    } else {
      const { name, value } = event;
      setRegistrationData((prevData) => ({
        ...prevData,
        [name]: value,
      }));
    }
  };

  const togglePasswordVisibility = () => {
    setShowPassword((prev) => !prev);
  };

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    if (registrationData.password !== registrationData.passwordConfirmation) {
      setPasswordMatchError("Passwords do not match.");
      return;
    } else {
      setPasswordMatchError(null);
    }
    await register(registrationData);
  };

  return (
    <ThemeProvider theme={defaultTheme}>
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
            Sign Up
          </Typography>

          <Box
            component="form"
            onSubmit={handleSubmit}
            noValidate
            sx={{ mt: 1 }}
          >
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
              id="username"
              label="Username"
              name="username"
              autoComplete="username"
              onChange={handleChange}
            />
            <TextField
              margin="normal"
              required
              fullWidth
              id="email"
              label="Email Address"
              name="email"
              autoComplete="email"
              onChange={handleChange}
            />
            <TextField
              margin="normal"
              required
              fullWidth
              name="password"
              label="Password"
              type={showPassword ? "text" : "password"}
              id="password"
              autoComplete="current-password"
              onChange={handleChange}
              InputProps={{
                endAdornment: (
                  <InputAdornment position="end">
                    <IconButton
                      aria-label="toggle password visibility"
                      onClick={togglePasswordVisibility}
                    >
                      {showPassword ? (
                        <VisibilityIcon />
                      ) : (
                        <VisibilityOffIcon />
                      )}
                    </IconButton>
                  </InputAdornment>
                ),
              }}
            />
            <TextField
              margin="normal"
              required
              fullWidth
              name="passwordConfirmation"
              label="Confirm password"
              type="password"
              id="passwordConfirmation"
              autoComplete="current-password"
              sx={{ mb: 2 }}
              onChange={handleChange}
            />
            {passwordMatchError && (
              <Alert sx={{ mb: 2 }} severity="error">
                {passwordMatchError}
              </Alert>
            )}

            <FormControl fullWidth>
              <InputLabel id="demo-simple-select-label">Type</InputLabel>
              <Select
                labelId="demo-simple-select-label"
                id="demo-simple-select"
                name="accountType"
                value={registrationData.accountType}
                onChange={(e) => handleChange(e)}
              >
                <MenuItem value="Regular">Regular</MenuItem>
                <MenuItem value="Administrator">Administrator</MenuItem>
              </Select>
            </FormControl>
            <FormControlLabel
              control={<Checkbox value="remember" color="primary" />}
              label="Remember me"
            />
            <Button
              type="submit"
              fullWidth
              variant="contained"
              sx={{ mt: 3, mb: 2 }}
              disabled={isLoading}
            >
              {isLoading ? "Loading..." : "Sign In"}
            </Button>

            <Grid container>
              <Grid item xs>
                <Link href="forgot/password" variant="body2">
                  Forgot password?
                </Link>
              </Grid>
              <Grid item>
                <Link href="/sign-in" variant="body2">
                  {"Already have an account? Sign In"}
                </Link>
              </Grid>
            </Grid>
          </Box>
        </Box>
      </Container>
    </ThemeProvider>
  );
}
