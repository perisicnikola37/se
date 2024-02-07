import { useEffect } from "react";
import { useModal } from "../contexts/GlobalContext";
import useObjectGroups from "../hooks/GlobalHooks/GetObjectsHook";
import EnhancedTable from "../components/Tables/ExpenseGroupsTable";
import { Helmet } from "react-helmet";
import { Alert } from "@mui/material";
import ExpenseGroupCreateModal from "../components/Modals/ExpenseGroupCreateModal";

const ExpenseGroups = () => {
  const { objectGroups, fetchObjectGroups } = useObjectGroups("expense");
  const { actionChange } = useModal();

  useEffect(() => {
    fetchObjectGroups();
    window.scrollTo({
      top: 0,
    });
  }, [actionChange]);

  return (
    <div className="w-full max-w-screen-xl min-h-[48rem] mx-auto p-4 md:py-8">
      <Helmet>
        <title>Expense groups | Expense Tracker</title>
      </Helmet>
      {objectGroups.length == 0 ? (
        <div className="flex justify-between _create-button-page">
          <Alert sx={{ width: "70%" }} severity="error">
            There are no expense groups. Please create one.
          </Alert>
          <ExpenseGroupCreateModal />
        </div>
      ) : (
        <EnhancedTable expenseGroups={objectGroups} />
      )}
    </div>
  );
};

export default ExpenseGroups;
