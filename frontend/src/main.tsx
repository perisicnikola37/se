import React from "react";
import ReactDOM from "react-dom/client";
import App from "./App.tsx";
import "./index.css";
import { RouterProvider, createBrowserRouter } from "react-router-dom";
import SignUp from "./pages/SignUp.tsx";
import SignIn from "./pages/SignIn.tsx";
import Dashboard from "./pages/Dashboard.tsx";
import { LoadingProvider } from "./contexts/LoadingContext.tsx";
import { LanguageProvider } from "./contexts/GetLanguageKeyContext.tsx";
import Expenses from "./pages/Expenses.tsx";
import Incomes from "./pages/Incomes.tsx";
import NotFoundPage from "./pages/NotFoundPage.tsx";
import { UserProvider } from "./contexts/UserContext.tsx";
import { ModalProvider } from "./contexts/GlobalContext.tsx";
import IncomeGroups from "./pages/IncomeGroups.tsx";
import ExpenseGroups from "./pages/ExpenseGroups.tsx";
import Reminders from "./pages/Reminders.tsx";
import Blogs from "./pages/Blogs.tsx";
import IncomeGroupDetail from "./pages/IncomeGroupDetail.tsx";
import ExpenseGroupDetail from "./pages/ExpenseGroupDetail.tsx";
import ForgotPassword from "./pages/ForgotPassword.tsx";
import { DarkModeProvider } from "./contexts/DarkModeContext.tsx";
import BlogDetail from "./pages/BlogDetail.tsx";

const router = createBrowserRouter([
    {
        path: "/",
        element: (
            <DarkModeProvider>
                <LoadingProvider >
                    <ModalProvider>
                        <UserProvider>
                            <LanguageProvider>
                                <App />
                            </LanguageProvider>
                        </UserProvider>
                    </ModalProvider>
                </ LoadingProvider>
            </DarkModeProvider>
        ),
        children: [
            {
                index: true, element: <Dashboard />
            },
            { path: "incomes", element: <Incomes /> },
            { path: "incomes/groups", element: <IncomeGroups /> },
            { path: "incomes/groups/:id", element: <IncomeGroupDetail /> },
            { path: "expenses", element: <Expenses /> },
            { path: "expenses/groups/:id", element: <ExpenseGroupDetail /> },
            { path: "expenses/groups", element: <ExpenseGroups /> },
            { path: "blogs", element: <Blogs /> },
            { path: "blogs/:id", element: <BlogDetail /> },
            { path: "reminders", element: <Reminders /> },
            {
                path: "*",
                element: <NotFoundPage />,
            },
        ],
    },
    {
        path: "/sign-up",
        element: (
            <UserProvider>
                <SignUp />
            </UserProvider>
        ),
    },
    {
        path: "/sign-in",
        element: <SignIn />,
    },
    {
        path: "/forgot/password",
        element: <ForgotPassword />,
    },
]);

ReactDOM.createRoot(document.getElementById("root") as HTMLElement).render(
    <React.StrictMode>
        <RouterProvider router={router} />
    </React.StrictMode>
);


