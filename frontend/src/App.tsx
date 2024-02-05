// App.tsx
import { useEffect } from "react";
import { ThreeDots } from "react-loader-spinner";
import NavBar from "./components/NavBar";
import { useLoading } from "./contexts/LoadingContext";
import Footer from "./components/Footer";
import { Outlet, useLocation, useNavigate } from "react-router-dom";
import { useUser } from "./contexts/UserContext";
import { useDarkMode } from "./contexts/DarkModeContext";
import { CssBaseline, ThemeProvider, createTheme } from "@mui/material";

function App() {
    const { loading, setLoadingState } = useLoading();
    const navigate = useNavigate();
    const location = useLocation();
    const { isLoggedIn } = useUser();
    const { darkMode } = useDarkMode();

    const theme = createTheme({
        palette: {
            mode: darkMode ? "dark" : "light",
        },
    });

    useEffect(() => {
        // Check if the URL contains "reset-password"
        const isResetPasswordRoute =
            window.location.pathname.includes("reset-password");

        // Exclude "/" path and "reset-password" route from redirection
        if (!isLoggedIn() && location.pathname !== "/" && !isResetPasswordRoute) {
            navigate("/sign-in");
        }
    }, [navigate, location, isLoggedIn]);

    useEffect(() => {
        const timeout = setTimeout(() => {
            setLoadingState(false);
        }, 600);

        return () => clearTimeout(timeout);
    }, [setLoadingState]);

    return (
        <div className="flex flex-col min-h-screen">
            <div className="flex-grow">
                {loading ? (
                    <div className="flex justify-center items-center h-screen">
                        <ThreeDots
                            visible={true}
                            height="80"
                            width="80"
                            color="#193269"
                            ariaLabel="revolving-dot-loading"
                            wrapperStyle={{}}
                            wrapperClass=""
                        />
                    </div>
                ) : (
                    <div className="flex flex-col min-h-full">
                        <ThemeProvider theme={theme}>
                            <CssBaseline />
                            <NavBar />
                            <Outlet />
                            <Footer />
                        </ThemeProvider>
                    </div>
                )}
            </div>
        </div>
    );
}

export default App;
