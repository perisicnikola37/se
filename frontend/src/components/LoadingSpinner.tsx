import { ThreeDots } from "react-loader-spinner";

const LoadingSpinner = () => (
    <div className="flex justify-center items-center h-screen">
        <ThreeDots
            visible={true}
            height="80"
            width="80"
            color="#193269"
            ariaLabel="revolving-dot-loading"
            wrapperStyle={{}}
            wrapperClass=""
        />
    </div>
);

export default LoadingSpinner;