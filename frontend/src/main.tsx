import React from "react";
import ReactDOM from "react-dom/client";
import App from "./App.tsx";
import "./index.css";

import { RouterProvider, createBrowserRouter } from "react-router-dom";
import SignUp from "./pages/SignUp.tsx";
import SignIn from "./pages/SignIn.tsx";
import Dashboard from "./pages/Dashboard.tsx";
import Blog from "./pages/Blog.tsx";
import Transactions from "./pages/Transaction.tsx";

import { ThemeProvider, createTheme } from "@mui/material/styles";
import CssBaseline from "@mui/material/CssBaseline";
import Cookies from "js-cookie";

const darkTheme = createTheme({
    palette: {
        mode: "dark",
    },
});

const lightTheme = createTheme({
    palette: {
        mode: "light",
    },
});

const themeCookie = Cookies.get("theme");
const selectedTheme = themeCookie === "dark" ? darkTheme : lightTheme;

// Kreirajte router
const router = createBrowserRouter([
    {
        path: "/",
        element: (
            <ThemeProvider theme={selectedTheme}>
                <CssBaseline />
                <App />
            </ThemeProvider>
        ),
    },
    {
        path: "/sign-up",
        element: <SignUp />,
    },
    {
        path: "/sign-in",
        element: <SignIn />,
    },
    {
        path: "/dashboard",
        element: <Dashboard />,
    },
    {
        path: "/transactions",
        element: <Transactions />,
    },
    {
        path: "/blogs",
        element: <Blog />,
    },
]);

ReactDOM.createRoot(document.getElementById("root") as HTMLElement).render(
    <React.StrictMode>
        <RouterProvider router={router} />
    </React.StrictMode>
);