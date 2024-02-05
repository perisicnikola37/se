import { useEffect } from "react";
import { Link } from "react-router-dom";

const Blogs = () => {
    useEffect(() => {
        document.title = "Blogs | Expense Tracker";
    }, []);

    const blogs = [
        {
            id: 1,
            title: "Introduction to React Hooks",
            author: "John Doe",
            createdAt: "2024-02-01T14:30:00.000Z",
            content:
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
            imageUrl:
                "https://resources.tallysolutions.com/us/wp-content/uploads/2021/11/cogs-vs-expenses-whats-the-difference.jpg",
        },
        {
            id: 2,
            title: "CSS Grid Layout Tutorial",
            author: "Jane Smith",
            createdAt: "2024-02-02T10:15:00.000Z",
            content:
                "Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.",
            imageUrl:
                "https://happay.com/blog/wp-content/uploads/sites/12/2022/08/non-operating-expenses.png",
        },
        {
            id: 3,
            title: "JavaScript ES6 Features",
            author: "Bob Johnson",
            createdAt: "2024-02-03T16:45:00.000Z",
            content:
                "Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
            imageUrl:
                "https://assets-global.website-files.com/63e56114746188c54e2936e0/6488d32675e24027363f61f0_Spend%20Management-4.png",
        },
        {
            id: 3,
            title: "JavaScript ES6 Features",
            author: "Bob Johnson",
            createdAt: "2024-02-03T16:45:00.000Z",
            content:
                "Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
            imageUrl:
                "https://assets-global.website-files.com/63e56114746188c54e2936e0/6488d32675e24027363f61f0_Spend%20Management-4.png",
        },
        {
            id: 3,
            title: "JavaScript ES6 Features",
            author: "Bob Johnson",
            createdAt: "2024-02-03T16:45:00.000Z",
            content:
                "Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
            imageUrl:
                "https://assets-global.website-files.com/63e56114746188c54e2936e0/6488d32675e24027363f61f0_Spend%20Management-4.png",
        },
        {
            id: 3,
            title: "JavaScript ES6 Features",
            author: "Bob Johnson",
            createdAt: "2024-02-03T16:45:00.000Z",
            content:
                "Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
            imageUrl:
                "https://assets-global.website-files.com/63e56114746188c54e2936e0/6488d32675e24027363f61f0_Spend%20Management-4.png",
        },
    ];

    return (
        <div className="w-full max-w-screen-xl min-h-[48rem] mx-auto p-4 md:py-8">
            <h1 className="text-3xl font-semibold mb-4">Blogs</h1>
            {blogs.length === 0 ? (
                <p>No blogs available.</p>
            ) : (
                <ul className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4">
                    {blogs.map((blog) => (
                        <li key={blog.id} className="bg-white p-6 rounded-md shadow-md">
                            {blog.imageUrl && (
                                <img
                                    src={blog.imageUrl}
                                    alt={blog.title}
                                    className="w-full h-32 object-cover mb-4 rounded-md"
                                />
                            )}
                            <Link to={`/blogs/${blog.id}`}>
                                <h2 className="text-xl font-semibold mb-2 hover:text-[#2563EB] duration-300">
                                    {blog.title}
                                </h2>
                            </Link>
                            <p className="text-gray-600 mb-2">Author: {blog.author}</p>
                            <p className="text-gray-600">
                                Created At: {new Date(blog.createdAt).toLocaleString()}
                            </p>
                            <p className="text-gray-800 mt-4">{blog.content}</p>
                        </li>
                    ))}
                </ul>
            )}
        </div>
    );
};

export default Blogs;
