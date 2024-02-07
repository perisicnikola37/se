import { AnimatePresence, MotionStyle, motion } from "framer-motion";
import { Table } from "@mui/material";
import ViewInArIcon from "@mui/icons-material/ViewInAr";
import FeaturedInSection from "../components/FeaturedIn";
import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";
import LatestSection from "../components/Dashboard/LatestSection";
import { useUser } from "../contexts/UserContext";
import Chart from "../components/Chart";
import Newsletter from "../components/Newsletter";
import MailChimp from "../components/Dashboard/Mailchimp";
import EmailExport from "../components/Dashboard/EmailExport";
import FAQ from "../components/FAQ";
import Pricing from "../components/Pricing";
import Testimonials from "../components/Testimonials";
import { Helmet } from "react-helmet";
import { useModal } from "../contexts/GlobalContext";
import config from "../config/config.json";
import { Config } from "../types/TranslationTypes";
// import ContactUs from "../components/ContactUs";

const Dashboard = () => {
  const { isLoggedIn } = useUser();
  const { language } = useModal();

  const languageConfig = (config as unknown as Config)[language];

  const h1MotionStyle: MotionStyle = {
    fontFamily:
      'Inter, ui-sans-serif, system-ui, -apple-system, "Segoe UI", Roboto, "Helvetica Neue", Arial, "Noto Sans", sans-serif, "Apple Color Emoji", "Segoe UI Emoji", "Segoe UI Symbol", "Noto Color Emoji"',
    fontStyle: "normal",
    fontVariantCaps: "normal",
    fontVariantEastAsian: "normal",
    fontVariantLigatures: "normal",
    fontVariantNumeric: "normal",
    fontWeight: "800",
  };

  return (
    <div className="flex-grow flex flex-col items-center justify-center min-h-screen">
      <Helmet>
        <title>Dashboard | Expense Tracker</title>
      </Helmet>
      {!isLoggedIn() ? (
        <>
          <div className="_main-text">
            <motion.h4
              style={h1MotionStyle}
              className="font-extrabold text-5xl lg:mt-20 mt-7 tracking-tighter leading-none"
              initial={{ opacity: 0, scale: 0.8 }}
              animate={{ opacity: 1, scale: 1 }}
              transition={{ duration: 0.8 }}
            >
              {languageConfig.mainTextOnDashboard1}
              <br />
              {languageConfig.mainTextOnDashboard2}
            </motion.h4>
            <motion.p
              className="_main-text_paragraph mt-3 text-gray-500"
              initial={{ opacity: 0, scale: 0.8 }}
              animate={{ opacity: 1, scale: 1 }}
              transition={{ duration: 0.8 }}
            >
              {languageConfig.textOnDashboard}
            </motion.p>
          </div>
          <div className="mt-5 flex flex-col sm:flex-row w-[95%] sm:w-auto">
            <button
              type="button"
              className="text-white  bg-blue-700 hover:bg-blue-800  font-medium rounded-md text-sm px-3 py-1.5 me-2 mb-2 dark:bg-blue-600 dark:hover:bg-blue-700 focus:outline-none dark:focus:ring-blue-800"
            >
              <a href="/sign-in">{languageConfig.getStarted}</a>
            </button>

            <button
              type="button"
              className="py-1.5 px-3 me-2 mb-2 text-sm font-medium text-gray-900  bg-white rounded-lg border border-gray-200 hover:bg-gray-100 hover:text-blue-700 focus:z-10 "
            >
              <ViewInArIcon style={{ fontSize: "14px" }} className="mr-2" />
              <a
                href="https://git.vegaitsourcing.rs/nikola.perisic/vega-internship-project"
                target="_blank"
              >
                {languageConfig.exploreRepository}
              </a>
            </button>
          </div>
        </>
      ) : (
        <>
          <div className="_main-text mx-auto text-center">
            <motion.h4
              style={h1MotionStyle}
              className="font-extrabold text-5xl mt-20 tracking-tighter leading-none"
              initial={{ opacity: 0, scale: 0.8 }}
              animate={{ opacity: 1, scale: 1 }}
              transition={{ duration: 0.8 }}
            >
              Your last <span className="text-[#2563EB]">week</span> summary
            </motion.h4>
            <motion.p
              className="_main-text_paragraph mt-3 text-gray-500"
              initial={{ opacity: 0, scale: 0.8 }}
              animate={{ opacity: 1, scale: 1 }}
              transition={{ duration: 0.8 }}
            >
              <u className="font-bold">Chart</u> you can see below provides
              users with a concise overview of their financial activities over
              the past seven days. This dynamic and interactive dashboard
              leverages charts to visually represent the user's expenses and
              incomes, empowering them with valuable insights into their
              financial trends.
            </motion.p>
          </div>
        </>
      )}

      <AnimatePresence>
        <Chart />

        <motion.div
          key="table1"
          initial={{ opacity: 0 }}
          animate={{ opacity: 1 }}
          exit={{ opacity: 0 }}
          transition={{ duration: 1.5 }}
          layout
        >
          <Table />
        </motion.div>
        <motion.div
          key="table2"
          initial={{ opacity: 0 }}
          animate={{ opacity: 1 }}
          exit={{ opacity: 0 }}
          transition={{ duration: 1.5 }}
          layout
        ></motion.div>
      </AnimatePresence>

      {isLoggedIn() && <LatestSection />}

      <FeaturedInSection />

      <MailChimp imageUrl="https://1000logos.net/wp-content/uploads/2023/04/Mailchimp-logo.png" />
      <EmailExport imageUrl="https://i.postimg.cc/d098fSsM/removed.png" />
      <Pricing />

      <Testimonials />

      <FAQ />

      <Newsletter />

      {/* <ContactUs /> */}
    </div>
  );
};

export default Dashboard;
