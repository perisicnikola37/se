import { motion, AnimatePresence, useAnimation } from "framer-motion";
import { useEffect, useRef, useState } from "react";
import { Button, Grid, Table } from "@mui/material";
import logo from "./assets/logo.png";
import { ThreeDots } from "react-loader-spinner";
import Swal from "sweetalert2";
import { Delete } from "@mui/icons-material";
import FormDialog from "./components/Form";
import ChartReacharts from "./components/Chart";
import BlogCard from "./components/BlogCard";
import NavBar from "./components/NavBar";

function App() {
    const currentYear = new Date().getFullYear();

    const [loading1, setLoading] = useState(true);

    useEffect(() => {
        const timeout = setTimeout(() => {
            setLoading(false);
        }, 600);

        return () => clearTimeout(timeout);
    }, []);

    const [showBanner, setShowBanner] = useState(false);
    const controls = useAnimation();

    useEffect(() => {
        if (!localStorage.getItem("cookieConsent")) {
            setShowBanner(true);
            controls.start({ y: 0 });
        }
    }, [controls]);

    const handleAcceptCookies = () => {
        localStorage.setItem("cookieConsent", "true");
        controls.start({ y: "100%" }).then(() => {
            setShowBanner(false);
        });
    };

    const blogData = [
        { title: "Blog Post 1", content: "Lorem ipsum dolor sit amet..." },
        { title: "Blog Post 2", content: "Lorem ipsum dolor sit amet..." },
        { title: "Blog Post 3", content: "Lorem ipsum dolor sit amet..." },
        { title: "Blog Post 3", content: "Lorem ipsum dolor sit amet..." },
        { title: "Blog Post 3", content: "Lorem ipsum dolor sit amet..." },
    ];

    const openSwal = () => {
        Swal.fire({
            title: "Are you sure?",
            text: "You won't be able to revert this!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Yes, delete it!",
        }).then((result) => {
            if (result.isConfirmed) {
                Swal.fire({
                    title: "Deleted!",
                    text: "Your file has been deleted.",
                    icon: "success",
                });
            }
        });
    };

    const countupRef = useRef(null);
    let countUpAnim;

    useEffect(() => {
        initCountUp();
    }, []);

    async function initCountUp() {
        const countUpModule = await import("countup.js");
        countUpAnim = new countUpModule.CountUp(countupRef.current, 6);
        if (!countUpAnim.error) {
            countUpAnim.start();
        } else {
            console.error(countUpAnim.error);
        }
    }

    return (
        <>
            {loading1 ? (
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
                <>
                    <NavBar />

                    <FormDialog />
                    <Button
                        variant="outlined"
                        onClick={openSwal}
                        startIcon={<Delete />}
                    >
                        Delete
                    </Button>

                    <AnimatePresence>
                        <center>
                            <h1
                                className="text-3xl font-bold text-center"
                                ref={countupRef}
                            ></h1>
                        </center>
                        {/* <ChartsOverviewDemo /> */}
                        <center>
                            <ChartReacharts />
                        </center>
                        <motion.div
                            key="table1"
                            initial={{ opacity: 0 }}
                            animate={{ opacity: 1 }}
                            exit={{ opacity: 0 }}
                            transition={{ duration: 1.5 }}
                            layout
                        >
                            <Table />
                        </motion.div>
                        <motion.div
                            key="table2"
                            initial={{ opacity: 0 }}
                            animate={{ opacity: 1 }}
                            exit={{ opacity: 0 }}
                            transition={{ duration: 1.5 }}
                            layout
                        ></motion.div>
                    </AnimatePresence>
                    <center>
                        <Grid container spacing={2}>
                            {blogData.map((blog, index) => (
                                <Grid item xs={12} sm={6} md={3} key={index}>
                                    <BlogCard
                                        title={blog.title}
                                        content={blog.content}
                                    />
                                </Grid>
                            ))}
                        </Grid>
                    </center>

                    <footer className="bg-[#1976D2] shadow dark:bg-gray-900">
                        <div className="w-full max-w-screen-xl mx-auto p-4 md:py-8">
                            <div className="sm:flex sm:items-center sm:justify-between">
                                <a
                                    href="#"
                                    className="flex items-center mb-4 sm:mb-0 space-x-3 rtl:space-x-reverse"
                                >
                                    <img
                                        src={logo}
                                        className="h-8"
                                        alt="Flowbite Logo"
                                    />
                                    <span className="self-center text-2xl font-semibold whitespace-nowrap dark:text-white">
                                        Expense Tracker
                                    </span>
                                </a>
                                <ul className="flex flex-wrap items-center mb-6 text-sm font-medium text-gray-500 sm:mb-0 dark:text-gray-400">
                                    <li>
                                        <a
                                            href="#"
                                            className="hover:underline me-4 md:me-6"
                                        >
                                            About
                                        </a>
                                    </li>
                                    <li>
                                        <a
                                            href="#"
                                            className="hover:underline me-4 md:me-6"
                                        >
                                            Privacy Policy
                                        </a>
                                    </li>
                                    <li>
                                        <a
                                            href="#"
                                            className="hover:underline me-4 md:me-6"
                                        >
                                            Licensing
                                        </a>
                                    </li>
                                    <li>
                                        <a href="#" className="hover:underline">
                                            Contact
                                        </a>
                                    </li>
                                </ul>
                            </div>
                            <hr className="my-6 border-gray-200 sm:mx-auto dark:border-gray-700 lg:my-8" />
                            <span className="block text-sm text-gray-500 sm:text-center dark:text-gray-400">
                                &copy; {currentYear}{" "}
                                <a href="https://git.vegaitsourcing.rs/nikola.perisic/vega-internship-project" className="hover:underline">
                                    Expense Tracker™
                                </a>
                                . All Rights Reserved.
                            </span>

                        </div>

                        <motion.div
                            id="cookieConsent"
                            className={`fixed bottom-0 left-0 right-0 p-4 bg-gray-800 text-white text-center ${showBanner ? "" : "hidden"
                                }`}
                            animate={controls}
                            initial={{ y: "100%" }}
                            transition={{ duration: 0.5 }}
                        >
                            <div className="container mx-auto">
                                <p className="text-sm">
                                    We use cookies to enhance your experience.
                                    By using our website, you agree to our{" "}
                                    <a
                                        href="/privacy-policy"
                                        className="text-blue-500"
                                    >
                                        Privacy Policy
                                    </a>
                                    .
                                </p>
                                <button
                                    id="acceptCookies"
                                    className="bg-blue-500 hover:bg-blue-600 text-white font-bold py-2 px-4 text-sm mt-2 rounded-full focus:outline-none"
                                    onClick={handleAcceptCookies}
                                >
                                    Accept
                                </button>
                            </div>
                        </motion.div>
                    </footer>
                </>
            )}
        </>
    );
}
export default App;