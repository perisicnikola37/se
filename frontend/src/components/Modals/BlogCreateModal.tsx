import { useState } from "react";
import { motion } from "framer-motion";
import Button from "@mui/material/Button";
import TextField from "@mui/material/TextField";
import Dialog from "@mui/material/Dialog";
import DialogActions from "@mui/material/DialogActions";
import DialogContent from "@mui/material/DialogContent";
import DialogContentText from "@mui/material/DialogContentText";
import DialogTitle from "@mui/material/DialogTitle";
import { Alert } from "@mui/material";
import { useModal } from "../../contexts/GlobalContext";
import useCreateBlog from "../../hooks/Blogs/CreateBlogHook";

const BlogCreateModal = () => {
  const { createBlog, isLoading } = useCreateBlog();
  const [open, setOpen] = useState(false);
  const { setActionChanged } = useModal();
  const [errorMessage, setErrorMessage] = useState("");
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");

  const resetFields = () => {
    setTitle("");
    setDescription("");
  };

  const handleClickOpen = () => {
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
    resetFields();
  };

  const handleCreateBlog = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    const formData = new FormData(event.currentTarget);

    const nameValue = formData.get("name");

    if (typeof nameValue !== "string" || nameValue.length < 2) {
      setErrorMessage("Name must be at least 2 characters long");
      return;
    }

    const blogData = {
      Text: title,
      Description: description,
    };

    await createBlog(blogData);
    setActionChanged();
    handleClose();
  };

  return (
    <>
      <Button
        sx={{ marginRight: "10px" }}
        variant="outlined"
        onClick={handleClickOpen}
      >
        New blog
      </Button>
      <Dialog
        open={open}
        onClose={handleClose}
        PaperProps={{
          component: "form",
          onSubmit: handleCreateBlog,
        }}
      >
        <DialogTitle>New blog</DialogTitle>
        <DialogContent>
          <DialogContentText>
            Enter the details of your new blog below.
          </DialogContentText>
          {errorMessage && (
            <motion.div
              initial={{ opacity: 0 }}
              animate={{ opacity: 1 }}
              transition={{ duration: 0.5 }}
            >
              <Alert
                sx={{ background: "#ffcfcf", color: "#000" }}
                variant="filled"
                severity="error"
                style={{ margin: "10px 0" }}
              >
                {errorMessage}
              </Alert>
            </motion.div>
          )}
          <TextField
            autoFocus
            required
            margin="dense"
            id="name"
            name="name"
            label="Title"
            type="text"
            fullWidth
            variant="standard"
            onChange={(e) => setTitle(e.target.value)}
          />

          <label className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">
            Your message
          </label>
          <textarea
            rows={5}
            onChange={(e) => setDescription(e.target.value)}
            id="message"
            className="block p-1.5 w-full text-sm  bg-gray-50 rounded-lg border border-gray-300 outline-none"
            placeholder="Write your thoughts here..."
          ></textarea>
        </DialogContent>
        <DialogActions className="m-2">
          <Button onClick={handleClose}>Cancel</Button>
          <Button type="submit" disabled={isLoading}>
            Create
          </Button>
        </DialogActions>
      </Dialog>
    </>
  );
};

export default BlogCreateModal;
