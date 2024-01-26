import { Box, Button, InputLabel, MenuItem, Select, TextField } from "@mui/material";

export default function SignIn() {
  return (
    <div className="flex items-center justify-center h-screen bg-[#222629] text-white">
      <div className="sm:w-full sm:max-w-sm text-center">
        <h2 className="mt-10 text-2xl font-bold leading-9 tracking-tight">
          Sign in
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

          <InputLabel id="demo-simple-select-label">Age</InputLabel>
          <Select
            labelId="demo-simple-select-label"
            id="demo-simple-select"
            label="Age"
          >
            <MenuItem value={0}>Regular</MenuItem>
            <MenuItem value={1}>Administrator</MenuItem>
          </Select>

        </Box>

        <Button variant="contained" className="w-[40.5ch]">Sign Up</Button>

        <div className="mt-10">
          <form className="space-y-6" action="#" method="POST"></form>
        </div>
      </div>
    </div>
  );
}
