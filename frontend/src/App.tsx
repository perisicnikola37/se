import { motion, AnimatePresence } from "framer-motion";
import { useEffect, useRef } from "react";
import { Button, Grid, Table } from "@mui/material";
import { ThreeDots } from "react-loader-spinner";
import Swal from "sweetalert2";
import { Delete } from "@mui/icons-material";
import FormDialog from "./components/Form";
import ChartReacharts from "./components/Chart";
import BlogCard from "./components/BlogCard";
import NavBar from "./components/NavBar";
import { useLoading } from "./contexts/LoadingContext";
import Footer from "./components/Footer";

function App() {

    const { loading, setLoadingState } = useLoading();

    useEffect(() => {
        const timeout = setTimeout(() => {
            setLoadingState(false);
        }, 600);

        return () => clearTimeout(timeout);
    }, []);

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

                    <Footer />
                </>
            )}
        </>
    );
}
export default App;