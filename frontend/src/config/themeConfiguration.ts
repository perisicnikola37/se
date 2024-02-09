import { createTheme } from "@mui/material";

export const getTheme = (darkMode: boolean) => {
  return createTheme({
    palette: {
      mode: darkMode ? "dark" : "light",
    },
  });
};
