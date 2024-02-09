import { ReactNode } from "react";

import LoadingSpinner from "./LoadingSpinner";

interface LoadingContainerProps {
  loading: boolean;
  children: ReactNode;
}

const LoadingContainer = ({ loading, children }: LoadingContainerProps) => {
  return loading ? <LoadingSpinner /> : <>{children}</>;
};

export default LoadingContainer;
