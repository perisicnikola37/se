import logo from "../assets/logo.png";
import CookieConsent from "./CookieConsent";
import config from "../config/config.json";
import { Config } from "../types/TranslationTypes";
import { useModal } from "../contexts/GlobalContext";

const Footer = () => {
  const currentYear = new Date().getFullYear();
  const { language } = useModal();

  const languageConfig = (config as unknown as Config)[language];

  return (
    <footer className="bg-[#1976D2] shadow  dark:bg-gray-900 w-[100%]">
      <div className="w-full max-w-screen-xl mx-auto p-4 md:py-8">
        <div className="sm:flex sm:items-center sm:justify-between">
          <a
            href="#"
            className="flex items-center mb-4 sm:mb-0 space-x-3 rtl:space-x-reverse"
          >
            <img src={logo} className="h-8" alt="Flowbite Logo" />
            <span className="self-center text-2xl font-semibold whitespace-nowrap dark:text-white">
              {languageConfig.info.applicationName}
            </span>
          </a>
          <ul className="flex flex-wrap items-center mb-6 text-sm font-medium text-gray-500 sm:mb-0 dark:text-gray-400">
            {languageConfig.footerLinks.map((link, index) => (
              <li key={index}>
                <a href={link.url} className="hover:underline me-4 md:me-6">
                  {link.name}
                </a>
              </li>
            ))}
          </ul>
        </div>
        <hr className="my-6 border-gray-200 sm:mx-auto dark:border-gray-700 lg:my-8" />
        <span className="block text-sm text-gray-500 sm:text-center dark:text-gray-400">
          &copy; {currentYear}{" "}
          <a
            href="https://git.vegaitsourcing.rs/nikola.perisic/vega-internship-project"
            className="hover:underline"
          >
            {languageConfig.info.applicationName}™
          </a>
          . {languageConfig.info.copyright}
        </span>
      </div>

      <CookieConsent />
    </footer>
  );
};

export default Footer;
