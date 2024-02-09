import { useState } from "react";

import { Alert } from "@mui/material";
import Button from "@mui/material/Button";
import Dialog from "@mui/material/Dialog";
import TextField from "@mui/material/TextField";
import DialogTitle from "@mui/material/DialogTitle";
import DialogActions from "@mui/material/DialogActions";
import DialogContent from "@mui/material/DialogContent";
import DialogContentText from "@mui/material/DialogContentText";

import { useModal } from "../../contexts/GlobalContext";
import useCreateObjectGroup from "../../hooks/GlobalHooks/useCreateObjectGroup";

const ExpenseGroupCreateModal = () => {
  const [open, setOpen] = useState(false);
  const { createObjectGroup, isLoading } = useCreateObjectGroup("expense");
  const { setActionChanged } = useModal();
  const [errorMessage, setErrorMessage] = useState("");

  const handleClickOpen = () => {
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
  };

  const handleCreateExpenseGroup = async (
    event: React.FormEvent<HTMLFormElement>,
  ) => {
    event.preventDefault();
    const formData = new FormData(event.currentTarget);

    const nameValue = formData.get("name");
    const descriptionValue = formData.get("description");

    if (typeof nameValue !== "string" || nameValue.length < 2) {
      setErrorMessage("Name must be at least 2 characters long");
      return;
    }

    if (typeof descriptionValue !== "string" || descriptionValue.length < 8) {
      setErrorMessage("Description must be at least 8 characters long");
      return;
    }

    const expenseGroupData = {
      name: nameValue,
      description: descriptionValue,
    };

    await createObjectGroup(expenseGroupData);
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
        New expense group
      </Button>
      <Dialog
        open={open}
        onClose={handleClose}
        PaperProps={{
          component: "form",
          onSubmit: handleCreateExpenseGroup,
        }}
      >
        <DialogTitle>New expense group</DialogTitle>
        <DialogContent>
          <DialogContentText>
            Enter the details of your new income group below, and we'll help you
            keep track of your financial success. Your prosperity is our
            priority!
          </DialogContentText>
          {errorMessage && (
            <>
              <Alert
                sx={{ background: "#ffcfcf", color: "#000" }}
                variant="filled"
                severity="error"
                style={{ margin: "10px 0" }}
              >
                {errorMessage}
              </Alert>
            </>
          )}
          <TextField
            autoFocus
            required
            margin="dense"
            id="name"
            name="name"
            label="Name"
            type="text"
            fullWidth
            variant="standard"
          />
          <TextField
            autoFocus
            required
            margin="dense"
            id="description"
            name="description"
            label="Description"
            type="text"
            fullWidth
            variant="standard"
          />
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

export default ExpenseGroupCreateModal;
