import { useEffect, useState } from "react";

import Swal from "sweetalert2";

import { Alert } from "@mui/material";
import Button from "@mui/material/Button";
import Dialog from "@mui/material/Dialog";
import TextField from "@mui/material/TextField";
import DialogTitle from "@mui/material/DialogTitle";
import DialogActions from "@mui/material/DialogActions";
import DialogContent from "@mui/material/DialogContent";
import DialogContentText from "@mui/material/DialogContentText";

import { useModal } from "../../contexts/GlobalContext";
import useEditObjectGroup from "../../hooks/GlobalHooks/useEditObjectGroup";
import useGetObjectGroupById from "../../hooks/GlobalHooks/useGetObjectGroupById";

const IncomeGroupEditModal = ({ id }: { id: number; objectType: string }) => {
  const [open, setOpen] = useState(false);
  const { setActionChanged, actionChange } = useModal();

  const { isLoading, editObjectGroup } = useEditObjectGroup();

  const [errorMessage, setErrorMessage] = useState("");

  const { getObjectGroupById, object } = useGetObjectGroupById(id, "income");

  useEffect(() => {
    getObjectGroupById();
  }, [actionChange]);

  const handleClickOpen = () => {
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
  };

  const handleCreateIncome = async (
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

    const incomeGroupData = {
      id: id,
      description: descriptionValue,
      name: nameValue,
    };

    // Call the correct function
    await editObjectGroup(id, "income", incomeGroupData);
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
        Edit income group
      </Button>
      <Dialog
        open={open}
        onClose={handleClose}
        PaperProps={{
          component: "form",
          onSubmit: handleCreateIncome,
        }}
      >
        <DialogTitle>Edit income group</DialogTitle>
        <DialogContent>
          <DialogContentText>
            Enter the details of your updated income group below, and we'll help
            you keep track of your financial success. Your prosperity is our
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
            label="Income group name"
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
            label="Income description"
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

export default IncomeGroupEditModal;
