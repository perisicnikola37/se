import { useEffect } from "react";

const Incomes = () => {
    useEffect(() => {
        console.log("Incomes page loaded");
    }
        , []);
    return (
        <><h1>Incomes</h1></>
    )
}

export default Incomes;