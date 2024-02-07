import { Chip } from "@mui/material";
import EastSharpIcon from "@mui/icons-material/EastSharp";
import { useDarkMode } from "../contexts/DarkModeContext";

const SubHeader = () => {
  const iconStyle = { fontSize: 16 };
  const { darkMode } = useDarkMode();

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
        We have launched automated pipelines in our GitLab repository!
        <a
          href="https://git.vegaitsourcing.rs/nikola.perisic/vega-internship-project/-/pipelines"
          target="_blank"
          className="hover:underline ml-2"
        >
          Check it out
          <EastSharpIcon style={iconStyle} />
        </a>
      </p>
    </div>
  );
};

export default SubHeader;
