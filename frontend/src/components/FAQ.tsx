import { useState } from "react";

const FAQ = () => {
  const [isCollapseOneVisible, setIsCollapseOneVisible] = useState(true);
  const [isCollapseTwoVisible, setIsCollapseTwoVisible] = useState(false);
  const [isCollapseThreeVisible, setIsCollapseThreeVisible] = useState(false);

  const toggleCollapseOne = () => {
    setIsCollapseOneVisible(!isCollapseOneVisible);
    setIsCollapseTwoVisible(false);
    setIsCollapseThreeVisible(false);
  };

  const toggleCollapseTwo = () => {
    setIsCollapseTwoVisible(!isCollapseTwoVisible);
    setIsCollapseOneVisible(false);
    setIsCollapseThreeVisible(false);
  };

  const toggleCollapseThree = () => {
    setIsCollapseThreeVisible(!isCollapseThreeVisible);
    setIsCollapseOneVisible(false);
    setIsCollapseTwoVisible(false);
  };

  return (
    <div className="container mx-auto md:px-6 xl:px-24">
      <section className="mb-10 mt-10">
        <h2 className="mb-6 pl-6 text-3xl font-bold">
          Frequently asked questions
        </h2>

        <div id="accordionFlushExample">
          <div className="rounded-none border border-t-0 border-l-0 border-r-0 border-neutral-200 overflow-hidden">
            <h2 className="mb-0" id="flush-headingOne">
              <button
                className="group relative flex w-full items-center rounded-none border-0 py-4 px-5 text-left text-base font-bold transition hover:z-[2] focus:z-[3] focus:outline-none [&:not([data-te-collapse-collapsed])]:text-primary [&:not([data-te-collapse-collapsed])]:[box-shadow:inset_0_-1px_0_rgba(229,231,235)] dark:[&:not([data-te-collapse-collapsed])]:text-primary-400"
                type="button"
                data-te-collapse-init
                data-te-target="#flush-collapseOne"
                aria-expanded={isCollapseOneVisible}
                aria-controls="flush-collapseOne"
                onClick={toggleCollapseOne}
              >
                How do I add a new expense in the Expense Tracker application?
                <span
                  className={`ml-auto h-5 w-5 shrink-0 rotate-${
                    isCollapseOneVisible ? "-180deg" : "0"
                  } fill-[#336dec] transition-transform duration-200 ease-in-out group-${
                    isCollapseOneVisible ? "0" : "1"
                  }:fill-[#212529] motion-reduce:transition-none dark:fill-[#8FAEE0] dark:group-${
                    isCollapseOneVisible ? "0" : "1"
                  }:fill-[#eee]`}
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
              id="flush-collapseOne"
              className={`transition-all ${
                isCollapseOneVisible ? "max-h-[500px]" : "max-h-0"
              }`}
              data-te-collapse-item
              aria-labelledby="flush-headingOne"
              data-te-parent="#accordionFlushExample"
            >
              <div className="py-4 px-5 text-neutral-500">
                To add a new expense in the Expense Tracker application, follow
                these steps:
                <br />
                Navigate to the "Add Expense" section within the application.
                <br />
                Input the necessary details, such as the expense description,
                amount and category.
                <br />
                Click <span className="font-bold">"Add Expense"</span> button to
                record the new expense.
              </div>
            </div>
          </div>
          <div className="rounded-none border border-l-0 border-r-0 border-t-0 border-neutral-200 overflow-hidden">
            <h2 className="mb-0" id="flush-headingTwo">
              <button
                className="group relative flex w-full items-center rounded-none border-0 py-4 px-5 text-left text-base font-bold transition hover:z-[2] focus:z-[3] focus:outline-none [&:not([data-te-collapse-collapsed])]:text-primary [&:not([data-te-collapse-collapsed])]:[box-shadow:inset_0_-1px_0_rgba(229,231,235)] dark:[&:not([data-te-collapse-collapsed])]:text-primary-400"
                type="button"
                data-te-collapse-init
                data-te-collapse-collapsed
                data-te-target="#flush-collapseTwo"
                aria-expanded={isCollapseTwoVisible}
                aria-controls="flush-collapseTwo"
                onClick={toggleCollapseTwo}
              >
                Can I categorize and track my expenses by different spending
                categories?
                <span
                  className={`ml-auto h-5 w-5 shrink-0 rotate-${
                    isCollapseTwoVisible ? "-180deg" : "0"
                  } fill-[#336dec] transition-transform duration-200 ease-in-out group-${
                    isCollapseTwoVisible ? "0" : "1"
                  }:fill-[#212529] motion-reduce:transition-none dark:fill-[#8FAEE0] dark:group-${
                    isCollapseTwoVisible ? "0" : "1"
                  }:fill-[#eee]`}
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
              id="flush-collapseTwo"
              className={`transition-all ${
                isCollapseTwoVisible ? "max-h-[500px]" : "max-h-0"
              }`}
              data-te-collapse-item
              aria-labelledby="flush-headingTwo"
              data-te-parent="#accordionFlushExample"
            >
              <div className="pb-2 px-5 text-neutral-500">
                Yes, you can categorize and track your expenses by different
                spending categories. When adding a new expense, choose the
                appropriate category from the available options. This feature
                helps you organize and analyze your{" "}
                <span className="font-bold">spending patterns</span>, providing
                valuable insights into your financial{" "}
                <span className="font-bold">habits</span>.
              </div>
            </div>
          </div>
          <div className="rounded-none border border-l-0 border-r-0 border-b-0 border-t-0 border-neutral-200 overflow-hidden">
            <h2 className="mb-0" id="flush-headingThree">
              <button
                className="group relative flex w-full items-center rounded-none border-0 py-4 px-5 text-left text-base font-bold transition hover:z-[2] focus:z-[3] focus:outline-none [&:not([data-te-collapse-collapsed])]:text-primary [&:not([data-te-collapse-collapsed])]:[box-shadow:inset_0_-1px_0_rgba(229,231,235)] dark:[&:not([data-te-collapse-collapsed])]:text-primary-400"
                type="button"
                data-te-collapse-init
                data-te-collapse-collapsed
                data-te-target="#flush-collapseThree"
                aria-expanded={isCollapseThreeVisible}
                aria-controls="flush-collapseThree"
                onClick={toggleCollapseThree}
              >
                How can I generate reports to view my spending patterns over
                time?
                <span
                  className={`ml-auto h-5 w-5 shrink-0 rotate-${
                    isCollapseThreeVisible ? "-180deg" : "0"
                  } fill-[#336dec] transition-transform duration-200 ease-in-out group-${
                    isCollapseThreeVisible ? "0" : "1"
                  }:fill-[#212529] motion-reduce:transition-none dark:fill-[#8FAEE0] dark:group-${
                    isCollapseThreeVisible ? "0" : "1"
                  }:fill-[#eee]`}
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
              id="flush-collapseThree"
              className={`transition-all ${
                isCollapseThreeVisible ? "max-h-[500px]" : "max-h-0"
              }`}
              data-te-collapse-item
              aria-labelledby="flush-headingThree"
              data-te-parent="#accordionFlushExample"
            >
              <div className="px-5 text-neutral-500">
                To generate reports, follow these steps:
                <br />
                Navigate to the <span className="font-bold">"Expenses"</span> or
                <span className="font-bold">"Incomes"</span> page.
                <br />
                Click on <span className="font-bold">
                  "Export to email"
                </span>{" "}
                button.
                <br />
                Explore <span className="font-bold">
                  visual representation
                </span>{" "}
                of your expenses and incomes. Gain insights into where your
                money is going and make informed financial decisions based on
                the generated report.
              </div>
            </div>
          </div>
        </div>
      </section>
    </div>
  );
};

export default FAQ;
