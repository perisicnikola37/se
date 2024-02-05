import React from "react";

const defaultBlog = {
    id: 0,
    title: "Default Blog Title",
    author: "John Doe",
    createdAt: "2024-02-10T09:00:00.000Z",
    content:
        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
    imageUrl:
        "https://assets-global.website-files.com/63e56114746188c54e2936e0/6488d32675e24027363f61f0_Spend%20Management-4.png",
};

const BlogDetail = () => {
    const blog = defaultBlog;

    return (
        <div className="w-full max-w-screen-xl min-h-[48rem] mx-auto p-4 md:py-8">
            <div className="w-full max-w-screen-xl mx-auto p-4 md:py-8">
                <h1 className="text-3xl font-semibold mb-4">{blog.title}</h1>
                <img
                    src={blog.imageUrl}
                    alt={blog.title}
                    className="w-full h-64 object-cover mb-4 rounded-md"
                />
                <p className="text-gray-600 mb-2">Author: {blog.author}</p>
                <p className="text-gray-600">
                    Created At: {new Date(blog.createdAt).toLocaleString()}
                </p>
                <p className="text-gray-800 mt-4">{blog.content}</p>
            </div>
        </div>
    );
};

export default BlogDetail;
