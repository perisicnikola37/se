import { Suspense, lazy } from "react";

const LatestSection = () => {
    const LatestIncomes = lazy(() => import("../../components/LatestIncomes"));
    const LatestExpenses = lazy(() => import("../../components/LatestExpenses"));

    return (
        <>
            <Suspense>
                <LatestIncomes />
                <LatestExpenses />
            </Suspense>
        </>
    );
};

export default LatestSection;
