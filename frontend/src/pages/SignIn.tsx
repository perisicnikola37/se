import { Box, Button, TextField } from "@mui/material";
import logo from "../assets/logo.png";

export default function SignIn() {
  return (
    <div className="flex items-center justify-center h-screen bg-[#222629] text-white">
      <div className="sm:w-full sm:max-w-sm text-center">
        <img className="mx-auto w-auto" src={logo} alt="Your Company" />
        <h2 className="mt-10 text-2xl font-bold leading-9 tracking-tight">
          Sign in to your account
        </h2>

        <Box
          component="form"
          sx={{
            "& > :not(style)": { m: 1, width: "35ch" },
          }}
          noValidate
          autoComplete="off"
        >
          <TextField id="outlined-basic" label="Email" variant="outlined" />
          <TextField id="outlined-basic" label="Password" variant="outlined" />
          <TextField
            id="outlined-basic"
            label="Confirm password"
            variant="outlined"
          />
          <TextField id="outlined-basic" label="Account Type" variant="outlined" />
        </Box>

        <Button variant="contained">Sign Up</Button>

        <div className="mt-10">
          <form className="space-y-6" action="#" method="POST"></form>
        </div>
      </div>
    </div>
  );
}
