import Swal from "sweetalert2";
import { Delete } from "@mui/icons-material";
import { Button } from "@mui/material";

const DeleteModal = () => {
    const openSwal = () => {
        Swal.fire({
            title: "Are you sure?",
            text: "You won't be able to revert this!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Yes, delete it!",
        }).then((result) => {
            if (result.isConfirmed) {
                Swal.fire({
                    title: "Deleted!",
                    text: "Your file has been deleted.",
                    icon: "success",
                });
            }
        });
    };
    return (
        <Button
            variant="outlined"
            onClick={openSwal}
            startIcon={<Delete />}
        >
            Delete
        </Button>
    )
}
export default DeleteModal;