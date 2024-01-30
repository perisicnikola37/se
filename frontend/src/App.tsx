// App.tsx
import { useEffect } from "react";
import { ThreeDots } from "react-loader-spinner";
import NavBar from "./components/NavBar";
import { useLoading } from "./contexts/LoadingContext";
import Footer from "./components/Footer";
import { Outlet, useLocation, useNavigate } from "react-router-dom";

function App() {
    const { loading, setLoadingState } = useLoading();
    const navigate = useNavigate();
    const location = useLocation();

    useEffect(() => {
        const token = localStorage.getItem('token');

        // Exclude "/" path from redirection
        if (!token && location.pathname !== "/") {
            navigate("/sign-in");
        }
    }, [navigate, location]);

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
                        <NavBar />
                        <Outlet />
                        <Footer />
                    </div>
                )}
            </div>
        </div>
    );
}

export default App;
