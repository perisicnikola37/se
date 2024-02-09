import Swal from "sweetalert2";

import { Button } from "@mui/material";
import { Delete } from "@mui/icons-material";

import { useModal } from "../../contexts/GlobalContext";
import useDeleteObject from "../../hooks/GlobalHooks/useDeleteObject";

const DeleteModal = ({
  id,
  objectType,
}: {
  id: number;
  objectType: string;
}) => {
  const { deleteObject } = useDeleteObject();
  const { setActionChanged, resetActionChange, openModal } = useModal();

  const openSwal = async () => {
    const result = await Swal.fire({
      title: "Are you sure?",
      text: "You won't be able to revert this!",
      icon: "warning",
      showCancelButton: true,
      confirmButtonColor: "#3085d6",
      cancelButtonColor: "#d33",
      confirmButtonText: "Yes, delete it!",
    });

    if (result.isConfirmed) {
      try {
        await deleteObject(id, objectType);
        Swal.fire({
          title: "Deleted!",
          text: `${objectType.charAt(0).toUpperCase() + objectType.slice(1)} has been deleted.`,
          icon: "success",
        });

        setActionChanged();
      } catch (error) {
        console.error("Error deleting object:", error);
      }
    } else if (result.dismiss === Swal.DismissReason.cancel) {
      resetActionChange();
    }
  };

  return (
    <Button
      variant="outlined"
      onClick={() => {
        openSwal();
        openModal();
      }}
      startIcon={<Delete />}
    >
      Delete
    </Button>
  );
};

export default DeleteModal;
