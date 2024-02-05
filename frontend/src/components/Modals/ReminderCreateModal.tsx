import { useState } from "react";
import Button from "@mui/material/Button";
import TextField from "@mui/material/TextField";
import Dialog from "@mui/material/Dialog";
import DialogActions from "@mui/material/DialogActions";
import DialogContent from "@mui/material/DialogContent";
import DialogContentText from "@mui/material/DialogContentText";
import DialogTitle from "@mui/material/DialogTitle";
import { Alert, Select, MenuItem, FormControl, InputLabel, Checkbox } from "@mui/material";
import { useModal } from "../../contexts/GlobalContext";
import useCreateReminder from "../../hooks/Reminders/CreateReminderHook";

const ReminderCreateModal = () => {
    const { createReminder, isLoading } = useCreateReminder();
    const [open, setOpen] = useState(false);
    const { setActionChanged } = useModal();
    const [errorMessage, setErrorMessage] = useState("");
    const [selectedReminderDay, setSelectedReminderDay] = useState("");
    const [selectedActive, setSelectedActive] = useState(false);

    const resetFields = () => {
        setErrorMessage("");
        setSelectedReminderDay("");
        setSelectedActive(false);
    };

    const handleClickOpen = () => {
        setOpen(true);
    };

    const handleClose = () => {
        setOpen(false);
        resetFields();
    };

    const handleCreateIncome = async (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        const formData = new FormData(event.currentTarget);

        const nameValue = formData.get("name");

        if (typeof nameValue !== "string" || nameValue.length < 2) {
            setErrorMessage("Name must be at least 2 characters long");
            return;
        }

        const reminderData = {
            ReminderDay: selectedReminderDay,
            Type: nameValue,
            Active: selectedActive,
        };

        await createReminder(reminderData);
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
                New reminder
            </Button>
            <Dialog
                open={open}
                onClose={handleClose}
                PaperProps={{
                    component: "form",
                    onSubmit: handleCreateIncome,
                }}
            >
                <DialogTitle>New reminder</DialogTitle>
                <DialogContent>
                    <DialogContentText>
                        Enter the details of your new reminder below.
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
                    <FormControl fullWidth sx={{ mt: "5%", mb: "5%" }}>
                        <InputLabel id="demo-simple-select-label">Day</InputLabel>
                        <Select
                            labelId="demo-simple-select-label"
                            id="demo-simple-select"
                            value={selectedReminderDay}
                            onChange={(e) => setSelectedReminderDay(e.target.value as string)}
                            label="Day"
                        >
                            <MenuItem value="Monday">Monday</MenuItem>
                            <MenuItem value="Tuesday">Tuesday</MenuItem>
                            <MenuItem value="Wednesday">Wednesday</MenuItem>
                            <MenuItem value="Thursday">Thursday</MenuItem>
                            <MenuItem value="Friday">Friday</MenuItem>
                            <MenuItem value="Saturday">Saturday</MenuItem>
                            <MenuItem value="Sunday">Sunday</MenuItem>
                        </Select>
                    </FormControl>
                    Active
                    <Checkbox
                        checked={selectedActive}
                        onChange={(e) => setSelectedActive(e.target.checked)}
                        inputProps={{ 'aria-label': 'controlled' }}
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

export default ReminderCreateModal;
