import { useEffect, useState } from "react";

const useScrollToTop = () => {
  const [showScrollToTop, setShowScrollToTop] = useState(false);

  useEffect(() => {
    const handleScroll = () => {
      const shouldShow = window.scrollY > 800;
      setShowScrollToTop(shouldShow);
    };

    window.addEventListener("scroll", handleScroll);

    return () => {
      window.removeEventListener("scroll", handleScroll);
    };
  }, []);

  const scrollToTop = () => {
    window.scrollTo({
      top: 0,
      behavior: "smooth",
    });
  };

  return { showScrollToTop, scrollToTop };
};

export default useScrollToTop;
