import { motion, useAnimation } from "framer-motion";
import { useEffect, useState } from "react";
import config from "../config/config.json";

const CookieConsent = () => {
    const [showCookieConsent, setCookieConsent] = useState(true);
    const controls = useAnimation();

    useEffect(() => {
        if (!localStorage.getItem("cookieConsent")) {
            setCookieConsent(true);
            controls.start({ y: 0 });
        }
    }, [controls]);

    const handleAcceptCookies = () => {
        localStorage.setItem("cookieConsent", "true");
        controls.start({ y: "100%" }).then(() => {
            setCookieConsent(false);
        });
    };

    const languageKey = "ME";
    const pagesData = config[languageKey];

    return (
        <motion.div
            id="cookieConsent"
            className={`fixed bottom-0 left-0 right-0 p-4 bg-gray-800 text-white text-center ${showCookieConsent ? "" : "hidden"
                }`}
            animate={controls}
            initial={{ y: "100%" }}
            transition={{ duration: 0.5 }}
        >
            <div className="container mx-auto">
                <p className="text-sm">
                    {pagesData.policyText}
                    <a
                        href="/privacy-policy"
                        className="text-blue-500"
                    >
                        {pagesData.privacyPolicy}
                    </a>
                    .
                </p>
                <button
                    id="acceptCookies"
                    className="bg-blue-500 hover:bg-blue-600 text-white font-bold py-2 px-4 text-sm mt-2 rounded-full focus:outline-none"
                    onClick={handleAcceptCookies}
                >
                    {pagesData.acceptButton}
                </button>
            </div>
        </motion.div>
    );
};

export default CookieConsent;
