import React from "react";
import ReactDOM from "react-dom/client";
import App from "./App.tsx";
import "./index.css";
import { RouterProvider, createBrowserRouter } from "react-router-dom";
import SignUp from "./pages/SignUp.tsx";
import SignIn from "./pages/SignIn.tsx";
import Dashboard from "./pages/Dashboard.tsx";
import Blog from "./pages/Blog.tsx";
// import { ThemeProvider } from "@mui/material/styles";
// import CssBaseline from "@mui/material/CssBaseline";
// import Cookies from "js-cookie";
import { LoadingProvider } from "./contexts/LoadingContext.tsx";
import { LanguageProvider } from "./contexts/GetLanguageKeyContext.tsx";
import Expenses from "./pages/Expenses.tsx";
import Incomes from "./pages/Incomes.tsx";

// const darkTheme = createTheme({
//     palette: {
//         mode: "dark",
//     },
// });

// const lightTheme = createTheme({
//     palette: {
//         mode: "light",
//     },
// });

// const themeCookie = Cookies.get("theme");
// const selectedTheme = themeCookie === "dark" ? darkTheme : lightTheme;
// theme={selectedTheme}
const router = createBrowserRouter([
    {
        path: "/",
        element: (
            // <ThemeProvider theme={"dark"}>
            // <CssBaseline />
            <LoadingProvider>
                <LanguageProvider>
                    <App />
                </LanguageProvider>
            </LoadingProvider>
            // </ThemeProvider>
        ),
        children: [
            { index: true, element: <Dashboard /> },
            { path: "incomes", element: <Incomes /> },
            { path: "expenses", element: <Expenses /> },
            { path: "blogs", element: <Blog /> },
        ],
    },
    {
        path: "/sign-up",
        element: <SignUp />,
    },
    {
        path: "/sign-in",
        element: <SignIn />,
    },
]);

ReactDOM.createRoot(document.getElementById("root") as HTMLElement).render(
    <React.StrictMode>
        <RouterProvider router={router} />
    </React.StrictMode>
);
