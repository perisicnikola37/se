import React, { useEffect, useRef } from "react";
import CountUp from "countup.js"; // Import the CountUp directly

const CountUpComponent = () => {
    const countupRef = useRef<HTMLHeadingElement>(null);

    useEffect(() => {
        const initCountUp = () => {
            try {
                const countUpAnim = new CountUp(countupRef.current as HTMLHeadingElement, 0, {
                    startVal: 0,
                    duration: 2,
                });

                if (!countUpAnim.error) {
                    countUpAnim.start();
                } else {
                    console.error(countUpAnim.error);
                }
            } catch (error) {
                console.error("Error initializing CountUp:", error);
            }
        };

        initCountUp();
    }, []);

    return (
        <h1 className="text-3xl font-bold text-center" ref={countupRef}>
            0
        </h1>
    );
};

export default CountUpComponent;
