import { useState } from "react";
import Button from "@mui/material/Button";
import TextField from "@mui/material/TextField";
import Dialog from "@mui/material/Dialog";
import DialogActions from "@mui/material/DialogActions";
import DialogContent from "@mui/material/DialogContent";
import DialogContentText from "@mui/material/DialogContentText";
import DialogTitle from "@mui/material/DialogTitle";
import { Alert, Autocomplete } from "@mui/material";
import useCreateObject from "../../hooks/GlobalHooks/useCreateObject";
import { useModal } from "../../contexts/GlobalContext";
import useObjectGroups from "../../hooks/GlobalHooks/useObjectGroups";

const IncomeCreateModal = () => {
  const { fetchObjectGroups, objectGroups } = useObjectGroups("income");
  const [open, setOpen] = useState(false);
  const { isLoading, createObject } = useCreateObject("income");
  const { setActionChanged } = useModal();
  const [errorMessage, setErrorMessage] = useState("");
  const [selectedIncomeGroup, setSelectedIncomeGroup] = useState("");

  const handleClickOpen = () => {
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
  };

  const handleAutocompleteOpen = () => {
    fetchObjectGroups();
  };

  const handleCreateIncome = async (
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

    const incomeData = {
      description: descriptionValue,
      amount,
      incomeGroupId: Number(selectedIncomeGroup),
    };

    await createObject(incomeData);
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
        New income
      </Button>
      <Dialog
        open={open}
        onClose={handleClose}
        PaperProps={{
          component: "form",
          onSubmit: handleCreateIncome,
        }}
      >
        <DialogTitle>New income</DialogTitle>
        <DialogContent>
          <DialogContentText>
            Enter the details of your new income below, and we'll help you keep
            track of your financial success. Your prosperity is our priority!
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
            label="Income description"
            type="text"
            fullWidth
            variant="standard"
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
          />

          <Autocomplete
            onOpen={handleAutocompleteOpen}
            id="combo-box-demo"
            options={objectGroups}
            getOptionLabel={(option) => option.name}
            sx={{ width: "100%", marginTop: "20px" }}
            renderInput={(params) => (
              <TextField {...params} label="Income group" required />
            )}
            value={objectGroups.find(
              (group) => group.id === Number(selectedIncomeGroup),
            )}
            onChange={(_, newValue) =>
              setSelectedIncomeGroup((newValue?.id || "") as string)
            }
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

export default IncomeCreateModal;
