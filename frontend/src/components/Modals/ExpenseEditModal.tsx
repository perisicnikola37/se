import { useEffect, useState } from "react";
import Button from "@mui/material/Button";
import TextField from "@mui/material/TextField";
import Dialog from "@mui/material/Dialog";
import DialogActions from "@mui/material/DialogActions";
import DialogContent from "@mui/material/DialogContent";
import DialogContentText from "@mui/material/DialogContentText";
import DialogTitle from "@mui/material/DialogTitle";
import { Alert, Autocomplete } from "@mui/material";
import { useModal } from "../../contexts/GlobalContext";
import useObjectGroups from "../../hooks/GlobalHooks/GetObjectsHook";
import useGetObjectById from "../../hooks/GlobalHooks/GetObjectHook";
import useEditObject from "../../hooks/GlobalHooks/EditObjectHook";
import Swal from "sweetalert2";

const ExpenseEditModal = ({ id }: { id: number; objectType: string }) => {
  const { fetchObjectGroups, objectGroups } = useObjectGroups("expense");
  const [open, setOpen] = useState(false);
  const { isLoading, editObject } = useEditObject();
  const { setActionChanged, actionChange } = useModal();
  const [errorMessage, setErrorMessage] = useState("");
  const [selectedExpenseGroup, setSelectedExpenseGroup] = useState("");

  const { getObjectById, object } = useGetObjectById(id, "expense");

  type ObjectGroup = {
    id: number;
    name: string;
  };

  useEffect(() => {
    getObjectById();
  }, [actionChange]);

  useEffect(() => {
    if (object) {
      setSelectedExpenseGroup(String(object.expenseGroupId));
    }
  }, [object]);

  const handleClickOpen = () => {
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
  };

  const handleAutocompleteOpen = () => {
    fetchObjectGroups();
  };

  const handleCreateExpense = async (
    event: React.FormEvent<HTMLFormElement>,
  ) => {
    event.preventDefault();
    const formData = new FormData(event.currentTarget);

    const descriptionValue = formData.get("description");
    let amountValue = formData.get("amount");

    if (typeof descriptionValue !== "string" || descriptionValue.length < 8) {
      setErrorMessage("Description must be at least 8 characters long");
      return;
    }

    // Check if amountValue is null or undefined
    if (amountValue === null || amountValue === undefined) {
      setErrorMessage("Amount is required");
      return;
    }

    // Convert amountValue to string
    amountValue = amountValue.toString();

    // Check if amountValue is a valid number and greater than 0
    if (isNaN(parseFloat(amountValue)) || parseFloat(amountValue) <= 0) {
      setErrorMessage("Amount must be a valid number greater than 0");
      return;
    }

    const amount = parseFloat(amountValue);

    const expenseData = {
      id: id,
      description: descriptionValue,
      amount,
      expenseGroupId: Number(selectedExpenseGroup),
    };

    await editObject(id, "expense", expenseData);
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

  const handleAutocompleteChange = (
    _event: React.SyntheticEvent<Element, Event>,
    newValue: ObjectGroup | null,
  ) => {
    setSelectedExpenseGroup(newValue?.id.toString() || "");
  };

  return (
    <>
      <Button
        className="_add_object"
        sx={{ marginRight: "10px" }}
        variant="outlined"
        onClick={handleClickOpen}
      >
        Edit expense
      </Button>
      <Dialog
        open={open}
        onClose={handleClose}
        PaperProps={{
          component: "form",
          onSubmit: handleCreateExpense,
        }}
      >
        <DialogTitle>Edit expense</DialogTitle>
        <DialogContent>
          <DialogContentText>
            Enter the details of your updated expense below, and we'll help you
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
            id="description"
            name="description"
            label="Expense description"
            type="text"
            fullWidth
            variant="standard"
            defaultValue={object?.description || ""}
          />
          <TextField
            autoFocus
            required
            margin="dense"
            id="amount"
            name="amount"
            label="Amount"
            type="text"
            fullWidth
            variant="standard"
            inputProps={{
              pattern: "^[0-9]+(.[0-9]+)?$",
              title: "Please enter a valid number",
            }}
            defaultValue={object?.amount || ""}
          />

          <Autocomplete
            onOpen={handleAutocompleteOpen}
            id="combo-box-demo"
            options={objectGroups}
            getOptionLabel={(option) => option.name}
            sx={{ width: "100%", marginTop: "20px" }}
            renderInput={(params) => (
              <TextField {...params} label="Expense group" required />
            )}
            value={objectGroups.find(
              (group) => group.id === Number(selectedExpenseGroup),
            )}
            onChange={handleAutocompleteChange}
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

export default ExpenseEditModal;
