import { Link, useParams } from "react-router-dom";
import { useEffect } from "react";
import { motion } from "framer-motion";
import useGetBlogById from "../hooks/Blogs/GetBlogHook";
import { Breadcrumbs, Typography } from "@mui/material";
import { Helmet } from "react-helmet";

const BlogDetail = () => {
  const { id } = useParams();
  const { blog, getBlogById } = useGetBlogById(id ? parseInt(id) : 0);

  useEffect(() => {
    getBlogById();
  }, [id]);

  return (
    <>
      <Helmet>
        <title>Blog details | Expense Tracker</title>
      </Helmet>
      <motion.div
        initial={{ opacity: 0, y: -20 }}
        animate={{ opacity: 1, y: 0 }}
        exit={{ opacity: 0, y: 20 }}
        transition={{ duration: 0.5 }}
        className="w-full max-w-screen-xl min-h-[48rem] mx-auto p-4 md:py-8"
      >
        <Breadcrumbs aria-label="breadcrumb">
          <Link
            to="/"
            className="hover:text-[#2563EB] transition-colors duration-300"
          >
            Dashboard
          </Link>
          <Link
            to="/blogs"
            className="hover:text-[#2563EB] transition-colors duration-300"
          >
            Blogs
          </Link>
          <Typography color="text.primary">Blog details</Typography>
        </Breadcrumbs>

        <motion.div
          initial={{ opacity: 0, y: -20 }}
          animate={{ opacity: 1, y: 0 }}
          transition={{ duration: 0.5 }}
          className="w-full max-w-screen-xl mx-auto p-4 md:py-8"
        >
          <h1 className="text-3xl font-semibold mb-4">{blog?.text}</h1>
          <motion.img
            initial={{ opacity: 0, y: -20 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ duration: 0.5 }}
            src="https://assets-global.website-files.com/63e56114746188c54e2936e0/6488d32675e24027363f61f0_Spend%20Management-4.png"
            alt={blog?.text}
            className="w-full h-64 object-cover mb-4 rounded-md"
          />
          <motion.p
            initial={{ opacity: 0, y: -20 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ duration: 0.5 }}
            className="text-gray-600 mb-2"
          >
            Author: {blog?.author}
          </motion.p>
          <motion.p
            initial={{ opacity: 0, y: -20 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ duration: 0.5 }}
            className="text-gray-600"
          >
            {blog?.createdAt &&
              `Created at: ${new Date(blog.createdAt).toLocaleString()}`}
          </motion.p>
          <motion.p
            initial={{ opacity: 0, y: -20 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ duration: 0.5 }}
            className="text-gray-600 mt-5"
          >
            {blog?.description}
          </motion.p>
        </motion.div>
      </motion.div>
    </>
  );
};

export default BlogDetail;
