import { useEffect, useRef } from "react";

const CountUp = () => {
    const countupRef = useRef(null);
    let countUpAnim;

    useEffect(() => {
        initCountUp();
    }, []);

    async function initCountUp() {
        const countUpModule = await import("countup.js");
        countUpAnim = new countUpModule.CountUp(countupRef.current, 6);
        if (!countUpAnim.error) {
            countUpAnim.start();
        } else {
            console.error(countUpAnim.error);
        }
    }

    return (
        <>
            <h1
                className="text-3xl font-bold text-center"
                ref={countupRef}
            ></h1>
        </>
    )
}

export default CountUp;