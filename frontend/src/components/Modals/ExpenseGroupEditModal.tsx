import { useEffect, useState } from "react";
import Button from "@mui/material/Button";
import TextField from "@mui/material/TextField";
import Dialog from "@mui/material/Dialog";
import DialogActions from "@mui/material/DialogActions";
import DialogContent from "@mui/material/DialogContent";
import DialogContentText from "@mui/material/DialogContentText";
import DialogTitle from "@mui/material/DialogTitle";
import { Alert } from "@mui/material";
import { useModal } from "../../contexts/GlobalContext";
import Swal from "sweetalert2";
import useEditObjectGroup from "../../hooks/GlobalHooks/EditObjectGroupHook";
import useGetObjectGroupById from "../../hooks/GlobalHooks/GetObjectGroupHook";

const ExpenseGroupEditModal = ({ id }: { id: number; objectType: string }) => {
  const [open, setOpen] = useState(false);
  const { setActionChanged, actionChange } = useModal();

  const { isLoading, editObjectGroup } = useEditObjectGroup();

  const [errorMessage, setErrorMessage] = useState("");

  const { getObjectGroupById, object } = useGetObjectGroupById(id, "expense");

  useEffect(() => {
    getObjectGroupById();
  }, [actionChange]);

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
      id: id,
      description: descriptionValue,
      name: nameValue,
    };

    // Call the correct function
    await editObjectGroup(id, "expense", expenseGroupData);
    setActionChanged();
    handleClose();

    await Swal.fire({
      position: "center",
      icon: "success",
      title: "Your work has been saved",
      showConfirmButton: false,
      timer: 1500,
    });
    setActionChanged();
  };

  return (
    <>
      <Button
        className="_add_object"
        sx={{ marginRight: "10px" }}
        variant="outlined"
        onClick={handleClickOpen}
      >
        Edit expense group
      </Button>
      <Dialog
        open={open}
        onClose={handleClose}
        PaperProps={{
          component: "form",
          onSubmit: handleCreateExpenseGroup,
        }}
      >
        <DialogTitle>Edit expense group</DialogTitle>
        <DialogContent>
          <DialogContentText>
            Enter the details of your updated expense group below, and we'll
            help you keep track of your financial success. Your prosperity is
            our priority!
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
            label="Expense group name"
            type="text"
            fullWidth
            variant="standard"
            defaultValue={object?.name || ""}
          />

          <TextField
            autoFocus
            required
            margin="dense"
            id="description"
            name="description"
            label="Expense description"
            type="text"
            fullWidth
            variant="standard"
            defaultValue={object?.description || ""}
          />
        </DialogContent>
        <DialogActions className="m-2">
          <Button onClick={handleClose}>Cancel</Button>
          <Button type="submit" disabled={isLoading}>
            Update
          </Button>
        </DialogActions>
      </Dialog>
    </>
  );
};

export default ExpenseGroupEditModal;
