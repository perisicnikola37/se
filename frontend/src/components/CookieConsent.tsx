import { useEffect, useState } from "react";

import { motion, useAnimation } from "framer-motion";

import CloseIcon from "@mui/icons-material/Close";

import config from "../config/config.json";
import { Config } from "../types/TranslationTypes";
import { useModal } from "../contexts/GlobalContext";

const CookieConsent = () => {
  const [showCookieConsent, setCookieConsent] = useState(true);
  const controls = useAnimation();
  const { language } = useModal();

  useEffect(() => {
    const cookieConsent = localStorage.getItem("cookieConsent");

    if (!cookieConsent) {
      controls.start({ y: 0 });
      setCookieConsent(true);
    } else {
      setCookieConsent(false);
    }
  }, [controls]);

  const handleAcceptCookies = () => {
    localStorage.setItem("cookieConsent", "true");
    controls.start({ y: "100%" }).then(() => {
      setCookieConsent(false);
    });
  };

  const handleCloseCookies = () => {
    controls.start({ y: "100%" }).then(() => {
      setCookieConsent(false);
    });
  };

  const pagesData = (config as unknown as Config)[language];

  return (
    <motion.div
      id="cookieConsent"
      className={`fixed bottom-0 left-0 right-0 p-4 bg-gray-800 text-white text-center ${showCookieConsent ? "" : "hidden"}`}
      animate={controls}
      initial={{ y: "100%" }}
      transition={{ duration: 0.5 }}
    >
      <div className="container mx-auto relative">
        <button
          id="closeCookies"
          className=" text-gray-500 px-2 py-1 cursor-pointer _close-icon"
          onClick={handleCloseCookies}
        >
          <CloseIcon className="text-red-500 transition-transform transform hover:scale-110" />
        </button>
        <p className="text-sm">
          {pagesData.policyText}
          <a href="/privacy-policy" className="text-blue-500">
            {pagesData.privacyPolicy}
          </a>
          .
        </p>
        <button
          id="acceptCookies"
          className="bg-blue-500 hover:bg-blue-600 duration-300 text-white font-bold py-2 px-4 text-sm mt-2 rounded-full focus:outline-none"
          onClick={handleAcceptCookies}
        >
          {pagesData.acceptButton}
        </button>
      </div>
    </motion.div>
  );
};

export default CookieConsent;
