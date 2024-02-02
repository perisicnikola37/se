import { useEffect } from "react";

const Blogs = () => {

    useEffect(() => {
        document.title = 'Blogs | Expense Tracker';
    }, []);

    return (
        <div className="w-full max-w-screen-xl min-h-[48rem] mx-auto p-4 md:py-8">
            Blogs
        </div>
    )
}

export default Blogs