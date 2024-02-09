import { useState } from "react";
import { useDarkMode } from "../contexts/DarkModeContext";
import config from "../config/config.json";
import { Config } from "../types/TranslationTypes";
import { useModal } from "../contexts/GlobalContext";
import { FaqItem } from "../types/globalTypes";

const FAQ = () => {
  const { language } = useModal();
  const { darkMode } = useDarkMode();

  const languageConfig = (config as unknown as Config)[language];
  const faqData: FaqItem[] = languageConfig?.faqData || [];

  const [activeIndex, setActiveIndex] = useState<number | null>(null);

  const toggleCollapse = (index: number) => {
    setActiveIndex((prevIndex) => (prevIndex === index ? null : index));
  };

  return (
    <div className="container mx-auto md:px-6 xl:px-24">
      <section className="mb-10 mt-10">
        <h2 className="mb-6 pl-6 text-3xl font-bold">
          Frequently asked questions
        </h2>

        <div id="accordionFlushExample">
          {faqData.map((item, index) => (
            <div
              key={index}
              className={`rounded-none border ${index === faqData.length - 1 ? "border-b-0" : ""} border-l-0 border-r-0 border-t-0 border-neutral-200 overflow-hidden`}
            >
              <h2 className="mb-0" id={`flush-heading${index}`}>
                <button
                  className={`group relative flex w-full items-center rounded-none border-0 py-4 px-5 text-left text-base font-bold transition hover:z-[2] focus:z-[3] focus:outline-none [&:not([data-te-collapse-collapsed])]:text-primary [&:not([data-te-collapse-collapsed])]:[box-shadow:inset_0_-1px_0_rgba(229,231,235)] dark:[&:not([data-te-collapse-collapsed])]:text-primary-400`}
                  type="button"
                  data-te-collapse-init
                  data-te-target={`#flush-collapse${index}`}
                  aria-expanded={activeIndex === index}
                  aria-controls={`flush-collapse${index}`}
                  onClick={() => toggleCollapse(index)}
                >
                  {item.question}
                  <span
                    className={`ml-auto h-5 w-5 shrink-0 rotate-${activeIndex === index ? "-180deg" : "0"} fill-[#336dec] transition-transform duration-200 ease-in-out group-${activeIndex === index ? "0" : "1"}:fill-[#212529] motion-reduce:transition-none dark:fill-[#8FAEE0] dark:group-${activeIndex === index ? "0" : "1"}:fill-[#eee]`}
                  >
                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16">
                      <path
                        fillRule="evenodd"
                        d="M1.646 4.646a.5.5 0 0 1 .708 0L8 10.293l5.646-5.647a.5.5 0 0 1 .708.708l-6 6a.5.5 0 0 1-.708 0l-6-6a.5.5 0 0 1 0-.708z"
                      />
                    </svg>
                  </span>
                </button>
              </h2>
              <div
                id={`flush-collapse${index}`}
                className={`transition-all ${activeIndex === index ? "max-h-[500px]" : "max-h-0"}`}
                data-te-collapse-item
                aria-labelledby={`flush-heading${index}`}
                data-te-parent="#accordionFlushExample"
              >
                <div
                  className={`py-4 px-5 ${darkMode ? "text-neutral-200" : "text-neutral-500"}`}
                >
                  {item.answer}
                </div>
              </div>
            </div>
          ))}
        </div>
      </section>
    </div>
  );
};

export default FAQ;
