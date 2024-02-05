import { useEffect } from "react";
import { Link } from "react-router-dom";
import useAllBlogs from "../hooks/Blogs/AllBlogsHook";
import { truncateString } from "../utils/utils";

const Blogs = () => {
    const { loadBlogs, blogs } = useAllBlogs()

    useEffect(() => {
        document.title = "Blogs | Expense Tracker";

        loadBlogs()
    }, []);

    return (
        <div className="w-full max-w-screen-xl min-h-[48rem] mx-auto p-4 md:py-8">
            <h1 className="text-3xl font-semibold mb-4">Blogs</h1>
            {blogs.length === 0 ? (
                <p>No blogs available.</p>
            ) : (
                <ul className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4">
                    {blogs.map((blog) => (
                        <li key={blog.id} className="bg-white p-6 rounded-md shadow-md">
                            <img
                                alt={blog.text}
                                src="https://assets-global.website-files.com/63e56114746188c54e2936e0/6488d32675e24027363f61f0_Spend%20Management-4.png"
                                className="w-full h-32 object-cover mb-4 rounded-md"
                            />
                            <Link to={`/blogs/${blog.id}`}>
                                <h2 className="text-xl font-semibold mb-2 hover:text-[#2563EB] duration-300">
                                    {blog.text}
                                </h2>
                            </Link>
                            <p className="text-gray-600 mb-2">Author: {blog.author}</p>
                            <p className="text-gray-600">
                                Created At: {new Date(blog.createdAt).toLocaleString()}
                            </p>
                            <p className="text-gray-800 mt-4">{truncateString(blog.description, 90)}</p>
                        </li>
                    ))}
                </ul>
            )}
        </div>
    );
};

export default Blogs;
