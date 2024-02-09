import KeyboardArrowUpIcon from "@mui/icons-material/KeyboardArrowUp";
import { ScrollToTopButtonProps } from "../interfaces/globalInterfaces";
import { useDarkMode } from "../contexts/DarkModeContext";

const ScrollToTopButton = ({ onClick }: ScrollToTopButtonProps) => {
  const { darkMode } = useDarkMode();

  const buttonStyle = {
    fontSize: "40px",
    cursor: "pointer",
    borderRadius: "40px",
    background: darkMode ? "#2563EB" : "#fff",
  };

  return (
    <div
      className="scroll-to-top rounded-lg z-10"
      onClick={onClick}
      style={{
        position: "fixed",
        bottom: "20px",
        right: "20px",
      }}
    >
      <KeyboardArrowUpIcon style={buttonStyle} />
    </div>
  );
};

export default ScrollToTopButton;
