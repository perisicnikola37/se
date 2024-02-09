import { Chip } from "@mui/material";
import EastSharpIcon from "@mui/icons-material/EastSharp";

import config from "../config/config.json";
import { Config } from "../types/TranslationTypes";
import { useModal } from "../contexts/GlobalContext";
import { useDarkMode } from "../contexts/DarkModeContext";

const SubHeader = () => {
  const iconStyle = { fontSize: 16 };
  const { darkMode } = useDarkMode();
  const { language } = useModal();

  const languageConfig = (config as unknown as Config)[language];

  return (
    <div
      style={{
        fontSize: "12px",
        WebkitFontSmoothing: "antialiased",
        MozOsxFontSmoothing: "grayscale",
        backgroundColor: darkMode ? "#374151" : "#F9FAFB",
        color: darkMode ? "#fff" : "#374151",
        borderBottom: !darkMode ? undefined : "none",
      }}
      className="_sub-header sticky w-full flex font-bold items-center justify-center border-b border-gray-200 h-10"
    >
      <Chip
        label="New"
        variant="outlined"
        size="small"
        className="text-xs mr-2"
      />
      <p>
        {languageConfig.subHeaderMessage}
        <a
          href="https://git.vegaitsourcing.rs/nikola.perisic/vega-internship-project/-/pipelines"
          target="_blank"
          className="hover:underline ml-2"
        >
          {languageConfig.checkItOut}
          <EastSharpIcon style={iconStyle} />
        </a>
      </p>
    </div>
  );
};

export default SubHeader;
