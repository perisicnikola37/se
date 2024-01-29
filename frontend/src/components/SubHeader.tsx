import { Chip } from "@mui/material";
import EastSharpIcon from '@mui/icons-material/EastSharp';

const SubHeader = () => {
    const iconStyle = { fontSize: 16 };

    return (
        <div style={{ fontSize: "12px", WebkitFontSmoothing: "antialiased", MozOsxFontSmoothing: "grayscale", backgroundColor: "#F9FAFB" }} className="sticky w-full flex font-bold items-center justify-center border-b border-gray-200 h-10">
            <Chip label="New" variant="outlined" size="small" className="text-xs mr-2 " />
            <p className="text-gray-700">
                We have launched automated pipelines in our GitLab repository!
                <a href="https://git.vegaitsourcing.rs/nikola.perisic/vega-internship-project/-/pipelines" target="_blank" className="text-blue-500 hover:underline ml-2">
                    Check it out
                    <EastSharpIcon style={iconStyle} />
                </a>
            </p>
        </div>
    );
}

export default SubHeader;
