import KeyboardArrowUpIcon from "@mui/icons-material/KeyboardArrowUp";
import { ScrollToTopButtonProps } from "../interfaces/globalInterfaces";

const ScrollToTopButton = ({ onClick }: ScrollToTopButtonProps) => (
    <div
        className="scroll-to-top rounded-lg z-10"
        onClick={onClick}
        style={{
            position: "fixed",
            bottom: "20px",
            right: "20px",
        }}
    >
        <KeyboardArrowUpIcon
            style={{
                fontSize: "40px",
                cursor: "pointer",
                background: "#fff",
                borderRadius: "40px",
            }}
        />
    </div>
);

export default ScrollToTopButton;
