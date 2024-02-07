import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import useAllBlogs from "../hooks/Blogs/AllBlogsHook";
import { truncateString } from "../utils/utils";
import { Alert, Breadcrumbs, Skeleton, Typography } from "@mui/material";
import { Helmet } from "react-helmet";
import { motion } from "framer-motion";
import BlogCreateModal from "../components/Modals/BlogCreateModal";
import { useModal } from "../contexts/GlobalContext";
import DeleteModal from "../components/Modals/DeleteModal";

const Blogs = () => {
  const { loadBlogs, blogs } = useAllBlogs();
  const [isLoading, setIsLoading] = useState(true);
  const { actionChange } = useModal();

  useEffect(() => {
    loadBlogs();
    setTimeout(() => {
      setIsLoading(false);
    }, 700);
  }, [actionChange]);

  const skeletonCount = blogs.length > 0 ? blogs.length : 0;

  return (
    <div className="w-full max-w-screen-xl min-h-[48rem] mx-auto p-4 md:py-8">
      <Helmet>
        <title>Blogs | Expense Tracker</title>
      </Helmet>
      <div className="flex justify-between items-center mb-10">
        <h1 className="text-3xl font-semibold">Blogs</h1>
        <BlogCreateModal />
        <div className="hidden md:block">
          <Breadcrumbs aria-label="breadcrumb">
            <Link
              to="/"
              className="hover:text-[#2563EB] transition-colors duration-300"
            >
              Dashboard
            </Link>
            <Typography color="text.primary">Blogs</Typography>
          </Breadcrumbs>
        </div>
      </div>
      {isLoading ? (
        <ul className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4">
          {[...Array(skeletonCount)].map((_, index) => (
            <li key={index} className="bg-white p-6 rounded-md shadow-md">
              <Skeleton variant="rectangular" width="100%" height={200} />
              <Skeleton variant="text" width="80%" height={20} />
              <Skeleton variant="text" width="60%" height={16} />
              <Skeleton variant="text" width="70%" height={16} />
              <Skeleton variant="text" width="90%" height={60} />
            </li>
          ))}
        </ul>
      ) : (
        <>
          {blogs.length === 0 ? (
            <motion.div
              initial={{ opacity: 0 }}
              animate={{ opacity: 1 }}
              transition={{ duration: 0.15 }}
            >
              <Alert severity="info">No blogs found.</Alert>
            </motion.div>
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
                  <p className="text-gray-800 mt-4 mb-3">
                    {truncateString(blog.description, 90)}
                  </p>
                  <DeleteModal id={blog.id} objectType="blog" />
                </li>
              ))}
            </ul>
          )}
        </>
      )}
    </div>
  );
};

export default Blogs;
