import React from "react";

import { createBrowserRouter, RouterProvider } from "react-router-dom";

import ReactDOM from "react-dom/client";

import App from "./App.tsx";
import Blogs from "./pages/Blogs.tsx";
import SignUp from "./pages/SignUp.tsx";
import SignIn from "./pages/SignIn.tsx";
import Incomes from "./pages/Incomes.tsx";
import Profile from "./pages/Profile.tsx";
import Expenses from "./pages/Expenses.tsx";
import Dashboard from "./pages/Dashboard.tsx";
import Reminders from "./pages/Reminders.tsx";
import BlogDetail from "./pages/BlogDetail.tsx";
import NotFoundPage from "./pages/NotFoundPage.tsx";
import IncomeGroups from "./pages/IncomeGroups.tsx";
import ExpenseGroups from "./pages/ExpenseGroups.tsx";
import ResetPassword from "./pages/ResetPassword.tsx";
import PrivacyPolicy from "./pages/PrivacyPolicy.tsx";
import ForgotPassword from "./pages/ForgotPassword.tsx";
import { UserProvider } from "./contexts/UserContext.tsx";
import { ModalProvider } from "./contexts/GlobalContext.tsx";
import IncomeGroupDetail from "./pages/IncomeGroupDetail.tsx";
import { LoadingProvider } from "./contexts/LoadingContext.tsx";
import ExpenseGroupDetail from "./pages/ExpenseGroupDetail.tsx";
import { DarkModeProvider } from "./contexts/DarkModeContext.tsx";

import "./index.css";

const router = createBrowserRouter([
  {
    path: "/",
    element: (
      <DarkModeProvider>
        <LoadingProvider>
          <ModalProvider>
            <UserProvider>
              <App />
            </UserProvider>
          </ModalProvider>
        </LoadingProvider>
      </DarkModeProvider>
    ),
    children: [
      {
        index: true,
        element: <Dashboard />,
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
      { path: "/privacy-policy", element: <PrivacyPolicy /> },
      {
        path: "profile",
        element: (
          <UserProvider>
            <Profile />
          </UserProvider>
        ),
      },
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
    element: (
      <UserProvider>
        <SignIn />
      </UserProvider>
    ),
  },
  {
    path: "/forgot/password",
    element: <ForgotPassword />,
  },
  {
    path: "/reset-password",
    element: <ResetPassword />,
  },
]);

ReactDOM.createRoot(document.getElementById("root") as HTMLElement).render(
  <React.StrictMode>
    <RouterProvider router={router} />
  </React.StrictMode>,
);
