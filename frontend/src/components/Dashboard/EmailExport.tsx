import { useModal } from "../../contexts/GlobalContext";
import { Config } from "../../types/TranslationTypes";
import config from "../../config/config.json";
import { EmailExportProps } from "../../interfaces/globalInterfaces";
import { useDarkMode } from "../../contexts/DarkModeContext";

const EmailExport = ({ imageUrl }: EmailExportProps) => {
  const { language } = useModal();
  const { darkMode } = useDarkMode();

  const languageConfig = (config as unknown as Config)[language];

  return (
    <div className="m-8 md:m-20 flex flex-col md:flex-row w-[100%] lg:w-[80%] xl:w-[40%] 2xl:w-[60%]">
      <div className="w-full md:w-3/4 p-4 md:p-20  md:pl-8  md:ml-8 mt-4 md:mt-0">
        <h1 className="text-2xl md:text-4xl tracking-tighter leading-none font-bold mb-2 md:mb-5">
          <span className="text-[#2563EB]">
            {languageConfig.emailExportHeading1}
          </span>{" "}
          {languageConfig.emailExportHeading2}
        </h1>
        <p
          className={
            darkMode ? "text-white mt-5 lg:mt-0" : "text-gray-500 mt-5 lg:mt-0"
          }
        >
          {languageConfig.emailExportText}
        </p>
      </div>
      <div className="w-full md:w-1/2 flex justify-center items-center">
        <img
          src={imageUrl}
          alt="Mailchimp Logo"
          className="max-w-full h-auto md:max-h-full object-cover hidden md:block"
          style={{ width: "300px", height: "auto" }}
        />
      </div>
    </div>
  );
};

export default EmailExport;
