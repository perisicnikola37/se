import config from "../config/config.json";
import { useDarkMode } from "../contexts/DarkModeContext";
import { useModal } from "../contexts/GlobalContext";
import { Config } from "../types/TranslationTypes";

const FeaturedInSection = () => {
  const { language } = useModal();
  const { darkMode } = useDarkMode();

  const languageConfig = (config as unknown as Config)[language];

  return (
    !darkMode && (
      <div className="max-w-4xl mt-10 lg:mt-28 mx-auto text-center select-none">
        <h2
          className={
            darkMode
              ? "text-xl font-semibold -tracking-tight mb-4 text-white uppercase"
              : "text-xl font-semibold -tracking-tight mb-4 text-gray-500 uppercase"
          }
        >
          {languageConfig.featuredIn}
        </h2>

        <div className="flex flex-col items-center space-y-4 sm:flex-row sm:space-y-0 sm:space-x-4">
          <img
            width={100}
            src="https://pngimg.com/uploads/github/github_PNG23.png"
            alt="GitHub"
            title="GitHub"
            className="max-w-xs h-auto filter grayscale hover:filter-none transition-all duration-300"
          />

          <img
            width={130}
            src="https://images.ctfassets.net/xz1dnu24egyd/4QGmokIyrhHxpfmYIKHq17/ef43a9f88f2a9c1da8f5382383756d46/gitlab-logo-150.jpg"
            alt="GitLab"
            title="GitLab"
            className="max-w-xs h-auto filter grayscale hover:filter-none hover:brightness-110 transition-all duration-300"
          />

          <img
            width={80}
            src="https://upload.wikimedia.org/wikipedia/commons/thumb/0/01/LinkedIn_Logo.svg/2560px-LinkedIn_Logo.svg.png"
            alt="LinkedIn"
            title="LinkedIn"
            className="max-w-xs h-auto filter grayscale hover:filter-none hover:brightness-110 transition-all duration-300 mr-4"
          />

          <img
            width={80}
            src="https://1000logos.net/wp-content/uploads/2016/11/Facebook-Logo-2019.png"
            alt="Facebook"
            title="Facebook"
            className="max-w-xs h-auto filter grayscale hover:filter-none hover:brightness-110 transition-all duration-300"
          />
          <img
            width={60}
            src="https://upload.wikimedia.org/wikipedia/commons/thumb/e/e1/Logo_of_YouTube_%282015-2017%29.svg/2560px-Logo_of_YouTube_%282015-2017%29.svg.png"
            alt="YouTube"
            title="YouTube"
            className="max-w-xs h-auto filter grayscale hover:filter-none hover:brightness-110 transition-all duration-300"
          />
        </div>
      </div>
    )
  );
};

export default FeaturedInSection;
