import { useEffect } from "react";
import { Helmet } from "react-helmet";
import { useModal } from "../contexts/GlobalContext";
import useObjectGroups from "../hooks/GlobalHooks/GetObjectsHook";
import EnhancedTable from "../components/Tables/IncomeGroupsTable";
import IncomeGroupCreateModal from "../components/Modals/IncomeGroupCreateModal";
import { Alert } from "@mui/material";

const IncomeGroups = () => {
  const { objectGroups, fetchObjectGroups } = useObjectGroups("income");
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
        <title>Income groups | Expense Tracker</title>
      </Helmet>
      {objectGroups.length == 0 ? (
        <div className="flex justify-between _create-button-page">
          <Alert sx={{ width: "70%" }} severity="error">
            There are no income groups. Please create one.
          </Alert>
          <IncomeGroupCreateModal />
        </div>
      ) : (
        <EnhancedTable incomeGroups={objectGroups} />
      )}
    </div>
  );
};

export default IncomeGroups;
